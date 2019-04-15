using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Camera))]
public class CopyCameraToTargetCamera : MonoBehaviour {

    /******************************************
	 * Copy the transform, camera settings and post process layer from this camera to the core camera at scene start up.
	 * 
	 * If the camera is animated, you can enable continuous update of the core camera with the toggles AlwaysUdate* below.
	 * 
	 * If you change your camera properties at runtime by script, you should call the updateCoreCamera function to update your changes to the core camera
	 * 
	 * ****************************************/


    //Whether the camera properties should be updated at each frame or not
    [Tooltip("Should the transform be copied to the Core camera at each frame ?")]
    public bool updateTransformOnStart = true;

    [Tooltip("Should the camera settings be copied to the Core camera at each frame ?")]
    public bool updateCameraOnStart = true;

    [Tooltip("Should the postprocess be copied to the Core camera at each frame ?")]
    public bool updatePostProcessOnStart = true;

    public string TargetCameraName;

    [Tooltip("Should the transform be copied to the Core camera at each frame ?")]
    public bool disableAfterUpdate = false;

    //Whether the camera properties should be updated at each frame or not
    [Tooltip("Should the transform be copied to the Core camera at each frame ?")]
	public bool alwaysUpdateTransform = false;

	[Tooltip("Should the camera settings be copied to the Core camera at each frame ?")]
	public bool alwaysUpdateCamera = false;

	[Tooltip("Should the postprocess be copied to the Core camera at each frame ?")]
	public bool alwaysUpdatePostProcess = false;

	//Core camera attributes
	private GameObject coreCameraObject;
	private Camera coreCamera;
    private Camera currentCam;

	private PostProcessLayer corePostProcessLayer;

	//This camera attributes
	private PostProcessLayer postProcessLayer;
	private bool hasPostProcessLayer;

    private void OnEnable()
    {
        currentCam = GetComponent<Camera>();
        //Check if this camera has post process
        postProcessLayer = GetComponent<PostProcessLayer>();

		if (postProcessLayer) {
			hasPostProcessLayer = true;
		} else {
			hasPostProcessLayer = false;
		}

		//Get core camera components
		GetCoreCameraComponents();

		//Update core camera
		UpdateCoreCamera(updateTransformOnStart, updateCameraOnStart, updatePostProcessOnStart && hasPostProcessLayer);

	}

	private void Update() {

		UpdateCoreCamera(alwaysUpdateTransform, alwaysUpdateCamera, alwaysUpdatePostProcess && hasPostProcessLayer);

	}

	private void GetCoreCameraComponents() {

		//Look for CoreCamera object
		coreCameraObject = GameObject.Find(TargetCameraName);

		if (!coreCameraObject) {
			Debug.LogWarning("Could not find CoreCamera object to copy settings to.");
			return;
		}

		//Look for Camera Component
		coreCamera = coreCameraObject.GetComponent<Camera>();

		if (!coreCamera) {
			Debug.LogWarning("CoreCamera does not contain a camera component.");
			return;
		}

		if (hasPostProcessLayer) {
			//Look for PostProcessLayer Component
			corePostProcessLayer = coreCameraObject.GetComponent<PostProcessLayer>();

			if (!corePostProcessLayer) {
				Debug.Log("CoreCamera do not have a post process layer, adding one.");
				corePostProcessLayer = coreCameraObject.AddComponent<PostProcessLayer>();
			}
		}

        currentCam.enabled = disableAfterUpdate;
    }

	public void UpdateCoreCamera(bool updateTransform, bool updateCamera, bool updatePostProcess) {

        //Copy layer to CoreCamera (for postprocess mainly)
        coreCameraObject.layer = gameObject.layer;

		if (updateCamera) {
			//Copy camera settings to CoreCamera
			CopyCameraComponent(currentCam, coreCamera);
		}

		if (updateTransform) {
			//Copy transform to CoreCamera
			CopyTransformComponent(transform, coreCameraObject.transform);
		}

		if (updatePostProcess) {
			//If post processings, copy post processing settings to CoreCamera
			CopyPostProcessLayerComponent(postProcessLayer, corePostProcessLayer);
		}
    }

	private void CopyCameraComponent(Camera source, Camera destination) {

		//Camera properties
		destination.clearFlags = source.clearFlags;
		destination.backgroundColor = source.backgroundColor;
		destination.cullingMask = source.cullingMask;
		destination.orthographic = source.orthographic;
		destination.projectionMatrix = source.projectionMatrix;
		destination.orthographicSize = source.orthographicSize;
		destination.fieldOfView = source.fieldOfView;
		destination.farClipPlane = source.farClipPlane;
		destination.nearClipPlane = source.nearClipPlane;
		destination.rect = source.rect;
		destination.depth = source.depth;
		destination.renderingPath = source.renderingPath;
		destination.targetTexture = source.targetTexture;
		destination.targetDisplay = source.targetDisplay;
		destination.allowHDR = source.allowHDR;
		destination.allowMSAA = source.allowMSAA;
		destination.allowDynamicResolution = source.allowDynamicResolution;
		destination.aspect = source.aspect;
		destination.clearStencilAfterLightingPass = source.clearStencilAfterLightingPass;
		destination.cullingMatrix = source.cullingMatrix;
		destination.depthTextureMode = source.depthTextureMode;
		destination.eventMask = source.eventMask;
		destination.opaqueSortMode = source.opaqueSortMode;

	}

	private void CopyTransformComponent(Transform source, Transform destination) {

		//Transform properties
		destination.position = source.position;
		destination.rotation = source.rotation;
		destination.localScale = source.localScale;

	}

	private void CopyPostProcessLayerComponent(PostProcessLayer source, PostProcessLayer destination) {

		destination.volumeLayer = source.volumeLayer;
		destination.volumeTrigger = source.volumeTrigger;
		destination.antialiasingMode = source.antialiasingMode;
		destination.fastApproximateAntialiasing = source.fastApproximateAntialiasing;
		destination.subpixelMorphologicalAntialiasing = source.subpixelMorphologicalAntialiasing;
		destination.temporalAntialiasing = source.temporalAntialiasing;
		destination.fog = source.fog;
		//destination.dithering = source.dithering;
		destination.stopNaNPropagation = source.stopNaNPropagation;

	}
}
