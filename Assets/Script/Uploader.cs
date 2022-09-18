using System;
using System.IO;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public class Uploader : MonoBehaviour
{
    private readonly CompositeDisposable _compositeDisposable = new();

    void Start()
    {
        // 업로드
        // UploadMap(filePath: "C:/wow.kawai", apiURL: "http://49.247.34.41/upload.php").Subscribe(request =>
        // {
        //     {
        //         if (request.webRequest.result == UnityWebRequest.Result.Success)
        //         {
        //             Debug.Log("UPLOAD SUCCESS");
        //         }
        //         else
        //         {
        //             Debug.LogError(
        //                 $"UPLOAD Failed: {request.webRequest.result}\n{request.webRequest.method} | {request.webRequest.uri} | {request.webRequest.responseCode}");
        //         }
        //     }
        //     ;
        // }).AddTo(_compositeDisposable);

        // 다운로드
        DownloadMap2(mapId: "map_ihYKyCTneG").Subscribe(
            onNext: b => Debug.Log("DOWNLOAD SUCCESS"), onError: e => Debug.LogError("DOWNLOAD FAILED" + e)
        ).AddTo(_compositeDisposable);

        // 이건 다운로드 풀어쓴거
        // DownloadMap(apiURL: "http://49.247.34.41/.kawai", mapId: "map_20220919-02:19:47").Subscribe(request =>
        //     {
        //         if (request.webRequest.result == UnityWebRequest.Result.Success)
        //         {
        //             Debug.Log("DOWNLOAD SUCCESS");
        //
        //             string destination = Application.persistentDataPath + "/map_20220919-02:19:47.kawai";
        //             if (File.Exists(destination))
        //             {
        //                 File.Delete(destination);
        //             }
        //
        //             File.WriteAllBytes(destination, request.webRequest.downloadHandler.data);
        //         }
        //         else
        //         {
        //             Debug.LogError($"DOWNLOAD FAILED: {request.webRequest.result}");
        //         }
        //     })
        //     .AddTo(_compositeDisposable);
    }

    private IObservable<UnityWebRequestAsyncOperation> UploadMap(string filePath, string apiURL) => UnityWebRequest
        .Get(filePath).SendWebRequest().AsAsyncOperationObservable().Select(
            file =>
            {
                WWWForm postForm = new WWWForm();
                postForm.AddBinaryData("file", file.webRequest.downloadHandler.data, Path.GetFileName(filePath));
                return postForm;
            }
        ).SelectMany(form => UnityWebRequest.Post(apiURL, form).SendWebRequest()
            .AsAsyncOperationObservable());

    private IObservable<UnityWebRequestAsyncOperation> DownloadMap(string apiURL, string mapId) => UnityWebRequest
        .Get(apiURL + "/" + mapId + ".kawai").SendWebRequest()
        .AsAsyncOperationObservable();

    private IObservable<bool> DownloadMap2(string mapId) => Observable.Create<bool>(
        observer =>
        {
            CompositeDisposable compositeDisposable = new();
            
            UnityWebRequest
                .Get($"http://49.247.34.41/{mapId}.kawai").SendWebRequest()
                .AsAsyncOperationObservable()
                .Subscribe(request =>
                {
                    if (request.webRequest.result == UnityWebRequest.Result.Success)
                    {
                        try
                        {
                            string destination = $"{Application.persistentDataPath}/{mapId}.kawai";
                            if (File.Exists(destination))
                            {
                                File.Delete(destination);
                            }
                            
                            File.WriteAllBytes(destination, request.webRequest.downloadHandler.data);
                            
                            observer.OnNext(true);
                        }
                        catch (Exception e)
                        {
                            observer.OnError(new Exception("Failed to save file", e));
                        }
                    }
                    else
                    {
                        observer.OnError(new Exception($"DOWNLOAD FAILED: {request.webRequest.result}"));
                        Debug.LogError($"DOWNLOAD FAILED: {request.webRequest.result}");
                    }
                });
            
            return Disposable.Create(() =>
            {
                compositeDisposable.Dispose();
            });
        });
}