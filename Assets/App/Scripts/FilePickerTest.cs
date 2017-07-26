using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Mirko.HoloToolkitExtensions;
using HoloToolkit.Unity.InputModule;

#if NETFX_CORE

using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Threading;

#endif



public class FilePickerTest : MonoBehaviour
{
    public BaseRayStabilizer Stabilizer;
    public Material selectorMat;
    public Material scaleMat;
    public GameObject menuPrefab;
    public GameObject holoCamera;

#if WINDOWS_UWP
    public void Start()
    {
        Debug.Log("Started (UWP)");
    }

    public void OpenFile()
    {
        SelectFile();
    }

    public void SelectFile() {
        StorageFile aFile = null;
        byte[] bytes = null;
        bool threadDone = false;
        UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
        {
            FileOpenPicker thePicker = new FileOpenPicker();
            thePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            thePicker.ViewMode = PickerViewMode.Thumbnail;
            thePicker.CommitButtonText = "OK";
            thePicker.FileTypeFilter.Add("*");
            aFile = await thePicker.PickSingleFileAsync();
            threadDone = true;
        }, false);
        while(!threadDone)
        {

        }
        threadDone = false;
        if(aFile != null){
            UnityEngine.WSA.Application.InvokeOnUIThread(async () => {
                bytes = await ReadFile(aFile); 
                threadDone = true;
            }, false);
            while(!threadDone)
            {

            }
            if(bytes != null)
            {
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(bytes);
                var mainasset = assetBundle.LoadAsset(assetBundle.GetAllAssetNames()[0]);
                Vector3 spawnPosition = holoCamera.transform.forward * 3 + holoCamera.transform.position;
                GameObject gameObject = Instantiate(mainasset, spawnPosition, Quaternion.identity) as GameObject;
                gameObject.AddComponent<TapToSelect>();
                SpatialMappingCollisionDetector collisionDetector = gameObject.AddComponent<SpatialMappingCollisionDetector>();
                SpatialManipulator spatialManipulator = gameObject.AddComponent<SpatialManipulator>();
                Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
                SelectForBoundingBox selectBounding = gameObject.AddComponent<SelectForBoundingBox>();
                gameObject.transform.name = "object";
                spatialManipulator.Stabilizer = Stabilizer;
                spatialManipulator.CollisonDetector = collisionDetector;
                rigidBody.useGravity = false;
                rigidBody.isKinematic = true;
                selectBounding.sarSelectorMat = selectorMat;
                selectBounding.sarScaleMat = scaleMat;
                selectBounding.menuPrefab = menuPrefab;
                selectBounding.holoCamera = holoCamera;
                assetBundle.Unload(false);
            }
        }
    }

    /// <summary>
    /// Loads the byte data from a StorageFile
    /// </summary>
    /// <param name="file">The file to read</param>
    public async Task<byte[]> ReadFile(StorageFile file)
    {
        byte[] fileBytes = null;
        using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
        {
            fileBytes = new byte[stream.Size];
            using (DataReader reader = new DataReader(stream))
            {
                await reader.LoadAsync((uint)stream.Size);
                reader.ReadBytes(fileBytes);
            }
        }
        Debug.Log(fileBytes);
        return fileBytes;
    }
}

#else
    public void Start()
    {
        Debug.Log("Started (Editor)");
    }

    public void OpenFile()
    {
    }
}

#endif