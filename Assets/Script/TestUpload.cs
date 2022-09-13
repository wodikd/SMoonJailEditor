//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using UnityEngine.Networking;
//using UniRx;

//public class MapUploader : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(UploadFile());
//    }

//    IEnumerator UploadFile()
//    {
//        UnityWebRequest file = UnityWebRequest.Get("C:\wow.kawai");
//        yield return file.SendWebRequest();

//        WWWForm postForm = new();
//        postForm.AddBinaryData("file", file.downloadHandler.data, Path.GetFileName("C:\wow.kawai"));

//        UnityWebRequest req = UnityWebRequest.Post("http://koreaarmy.dothome.co.kr/smj/uploadMap.php", postForm);
//        req.SetRequestHeader("Content-Type", "multipart/form-data");
//        yield return req.SendWebRequest();

//        Debug.Log("file name: " + Path.GetFileName("C:\wow.kawai"));
//        Debug.Log(req.responseCode);
//        Debug.Log(req.downloadHandler.text);
//    }
//}
