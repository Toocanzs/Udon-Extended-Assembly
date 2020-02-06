using System;
using System.Collections.Generic;
using VRC.Udon.UAssembly.Assembler;

public class ExceptionTypeResolver : BaseTypeResolver
{
    private static readonly Dictionary<string, Type> _types = new Dictionary<string, Type>()
    {
        {"SystemVoid", typeof(void)},
        {"VRCUdonCommonInterfacesIUdonEventReceiver", typeof(VRC.Udon.Common.Interfaces.IUdonEventReceiver)},
        {"VRCUdonCommonInterfacesIUdonEventReceiverArray", typeof(VRC.Udon.Common.Interfaces.IUdonEventReceiver[])},
        {"SystemChar*", typeof(Char*)},
        {"SystemSByte*", typeof(SByte*)},
        {"SystemTextEncoding", typeof(System.Text.Encoding)},
        {"SystemGlobalizationCalendar", typeof(System.Globalization.Calendar)},
        {"T", typeof(object)},
        {"TArray", typeof(object[])},
        {"ListT", typeof(List<object>)},
        {"UnityEngineRenderingReflectionProbeUsage", typeof(UnityEngine.Rendering.ReflectionProbeUsage)},
        {
            "SystemCollectionsGenericListUnityEngineRenderingReflectionProbeBlendInfo",
            typeof(System.Collections.Generic.List<UnityEngine.Rendering.ReflectionProbeBlendInfo>)
        },
        {"SystemIntPtr", typeof(System.IntPtr)},
        {"UnityEngineRenderingDefaultReflectionMode", typeof(UnityEngine.Rendering.DefaultReflectionMode)},
        {"UnityEngineCameraCameraCallback", typeof(UnityEngine.Camera.CameraCallback)},
        {"UnityEngineAINavMeshOnNavMeshPreUpdate", typeof(UnityEngine.AI.NavMesh.OnNavMeshPreUpdate)},
        {"UnityEngineUIToggleToggleTransition", typeof(UnityEngine.UI.Toggle.ToggleTransition)},
        {"UnityEngineTrackedReference", typeof(UnityEngine.TrackedReference)},
        {"UnityEngineBodyDof", typeof(UnityEngine.BodyDof)},
        {"UnityEngineHeadDof", typeof(UnityEngine.HeadDof)},
        {"UnityEngineLegDof", typeof(UnityEngine.LegDof)},
        {"UnityEngineArmDof", typeof(UnityEngine.ArmDof)},
        {"UnityEngineFingerDof", typeof(UnityEngine.FingerDof)},
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiSystemBoolean",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, System.Boolean>)
        },
        {"VRCSDKBaseVRCPlayerApiSetAnimatorBoolDelegate", typeof(VRC.SDKBase.VRCPlayerApi.SetAnimatorBoolDelegate)},
        {
            "VRCSDKBaseVRCPlayerApiClaimNetworkControlDelegate",
            typeof(VRC.SDKBase.VRCPlayerApi.ClaimNetworkControlDelegate)
        },
        {"VRCSDKBaseVRCPlayerApiGetLookRayDelegate", typeof(VRC.SDKBase.VRCPlayerApi.GetLookRayDelegate)},
        {"VRCSDKBaseVRCPlayerApiBoolDelegate", typeof(VRC.SDKBase.VRCPlayerApi.BoolDelegate)},
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiSystemInt32",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, System.Int32>)
        },
        {
            "SystemFuncUnityEngineGameObjectVRCSDKBaseVRCPlayerApi",
            typeof(System.Func<UnityEngine.GameObject, VRC.SDKBase.VRCPlayerApi>)
        },
        {
            "SystemFuncSystemInt32VRCSDKBaseVRCPlayerApi",
            typeof(System.Func<System.Int32, VRC.SDKBase.VRCPlayerApi>)
        },
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiUnityEngineGameObjectSystemBoolean",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, UnityEngine.GameObject, System.Boolean>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineGameObject",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.GameObject>)
        },
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiVRCSDKBaseVRCPlayerApiTrackingDataTypeVRCSDKBaseVRCPlayerApiTrackingData",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, VRC.SDKBase.VRCPlayerApi.TrackingDataType,
                VRC.SDKBase.VRCPlayerApi.TrackingData>)
        },
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiUnityEngineHumanBodyBonesUnityEngineTransform",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, UnityEngine.HumanBodyBones, UnityEngine.Transform>)
        },
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiVRCSDKBaseVRC_PickupPickupHandVRCSDKBaseVRC_Pickup",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, VRC.SDKBase.VRC_Pickup.PickupHand, VRC.SDKBase.VRC_Pickup>)
        },
        {
            "VRCSDKBaseVRCPlayerApiActionVRCSDKBaseVRCPlayerApiVRCSDKBaseVRC_PickupPickupHandSystemSingleSystemSingleSystemSingle",
            typeof(VRC.SDKBase.VRCPlayerApi.Action<VRC.SDKBase.VRCPlayerApi, VRC.SDKBase.VRC_Pickup.PickupHand,
                System.Single, System.Single, System.Single>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineVector3UnityEngineQuaternion",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.Vector3, UnityEngine.Quaternion>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineVector3UnityEngineQuaternionVRCSDKBaseVRC_SceneDescriptorSpawnOrientation",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.Vector3, UnityEngine.Quaternion,
                VRC.SDKBase.VRC_SceneDescriptor.SpawnOrientation>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiSystemBoolean",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, System.Boolean>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineColor",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.Color>)
        },
        {"SystemActionVRCSDKBaseVRCPlayerApi", typeof(System.Action<VRC.SDKBase.VRCPlayerApi>)},
        {
            "SystemActionVRCSDKBaseVRCPlayerApiSystemStringSystemString",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, System.String, System.String>)
        },
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiSystemStringSystemString",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, System.String, System.String>)
        },
        {
            "SystemFuncSystemStringSystemStringSystemCollectionsGenericListSystemInt32",
            typeof(System.Func<System.String, System.String, System.Collections.Generic.List<System.Int32>>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiSystemBooleanSystemStringSystemString",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, System.Boolean, System.String, System.String>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiSystemInt32SystemStringSystemString",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, System.Int32, System.String, System.String>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineRuntimeAnimatorController",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.RuntimeAnimatorController>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineVector3",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.Vector3>)
        },
        {
            "SystemFuncVRCSDKBaseVRCPlayerApiUnityEngineVector3",
            typeof(System.Func<VRC.SDKBase.VRCPlayerApi, UnityEngine.Vector3>)
        },
        {"SystemFuncUnityEngineGameObjectSystemString", typeof(System.Func<UnityEngine.GameObject, System.String>)},
        {
            "SystemActionVRCSDKBaseVRC_EventHandlerVrcTargetTypeUnityEngineGameObjectSystemStringSystemObjectArray",
            typeof(System.Action<VRC.SDKBase.VRC_EventHandler.VrcTargetType, UnityEngine.GameObject, System.String,
                System.Object[]>)
        },
        {
            "SystemActionVRCSDKBaseVRCPlayerApiUnityEngineGameObjectSystemStringSystemObjectArray",
            typeof(System.Action<VRC.SDKBase.VRCPlayerApi, UnityEngine.GameObject, System.String, System.Object[]>)
        },
        {
            "SystemActionVRCSDKBaseVRC_EventHandlerVrcBroadcastTypeUnityEngineGameObjectSystemString",
            typeof(System.Action<VRC.SDKBase.VRC_EventHandler.VrcBroadcastType, UnityEngine.GameObject,
                System.String>)
        },
        {"SystemFuncSystemBoolean", typeof(System.Func<System.Boolean>)},
        {"SystemFuncVRCSDKBaseVRCPlayerApi", typeof(System.Func<VRC.SDKBase.VRCPlayerApi>)},
        {
            "SystemFuncUnityEngineGameObjectSystemBoolean",
            typeof(System.Func<UnityEngine.GameObject, System.Boolean>)
        },
        {
            "SystemFuncVRCSDKBaseVRC_EventHandlerVrcBroadcastTypeSystemStringUnityEngineVector3UnityEngineQuaternionUnityEngineGameObject",
            typeof(System.Func<VRC.SDKBase.VRC_EventHandler.VrcBroadcastType, System.String, UnityEngine.Vector3,
                UnityEngine.Quaternion, UnityEngine.GameObject>)
        },
        {"SystemFuncSystemObjectArraySystemByteArray", typeof(System.Func<System.Object[], System.Byte[]>)},
        {"SystemFuncSystemByteArraySystemObjectArray", typeof(System.Func<System.Byte[], System.Object[]>)},
        {"SystemActionUnityEngineGameObject", typeof(System.Action<UnityEngine.GameObject>)},
        {"SystemFuncVRCSDKBaseVRC_EventHandler", typeof(System.Func<VRC.SDKBase.VRC_EventHandler>)},
        {"SystemFuncSystemStringSystemBoolean", typeof(System.Func<System.String, System.Boolean>)},
        {"SystemFuncSystemDateTime", typeof(System.Func<System.DateTime>)},
        {"SystemFuncSystemDouble", typeof(System.Func<System.Double>)},
        {"SystemFuncSystemInt32", typeof(System.Func<System.Int32>)},
        {
            "SystemFuncSystemDoubleSystemDoubleSystemDouble",
            typeof(System.Func<System.Double, System.Double, System.Double>)
        },
        {
            "SystemFuncSystemCollectionsIEnumeratorUnityEngineCoroutine",
            typeof(System.Func<System.Collections.IEnumerator, UnityEngine.Coroutine>)
        },
        {"SystemFuncVRCSDKBaseVRC_EventDispatcher", typeof(System.Func<VRC.SDKBase.VRC_EventDispatcher>)},
        {"SystemFuncVRCSDKBaseVRCInputMethod", typeof(System.Func<VRC.SDKBase.VRCInputMethod>)},
        {
            "SystemFuncVRCSDKBaseVRCInputSettingSystemBoolean",
            typeof(System.Func<VRC.SDKBase.VRCInputSetting, System.Boolean>)
        },
        {
            "SystemActionVRCSDKBaseVRCInputSettingSystemBoolean",
            typeof(System.Action<VRC.SDKBase.VRCInputSetting, System.Boolean>)
        },
        {
            "SystemActionUnityEngineRendererSystemBoolean",
            typeof(System.Action<UnityEngine.Renderer, System.Boolean>)
        },
        {"VRCSDKBaseVRC_AudioBankOrder", typeof(VRC.SDKBase.VRC_AudioBank.Order)},
        {"VRCSDKBaseVRC_AudioBankStyle", typeof(VRC.SDKBase.VRC_AudioBank.Style)},
        {"VRCSDKBaseVRC_TriggerCustomTriggerTarget", typeof(VRC.SDKBase.VRC_Trigger.CustomTriggerTarget)},
        {"VRCSDK3ComponentsVRCMirrorReflection", typeof(VRC.SDK3.Components.VRCMirrorReflection)},
        {"VRCSDK3ComponentsVRCMirrorReflectionArray", typeof(VRC.SDK3.Components.VRCMirrorReflection[])},
        {"VRCSDKBaseVRC_PickupPickupOrientation", typeof(VRC.SDKBase.VRC_Pickup.PickupOrientation)},
        {"VRCSDKBaseVRC_PickupAutoHoldMode", typeof(VRC.SDKBase.VRC_Pickup.AutoHoldMode)},
        {"VRCSDKBaseVRC_PortalMarkerVRChatWorld", typeof(VRC.SDKBase.VRC_PortalMarker.VRChatWorld)},
        {"VRCSDKBaseVRC_PortalMarkerSortHeading", typeof(VRC.SDKBase.VRC_PortalMarker.SortHeading)},
        {"VRCSDKBaseVRC_PortalMarkerSortOrder", typeof(VRC.SDKBase.VRC_PortalMarker.SortOrder)},
        {"VRCSDKBaseVRC_StationMobility", typeof(VRC.SDKBase.VRC_Station.Mobility)},
        {"VRCSDKBaseVRC_ObjectApi", typeof(VRC.SDKBase.VRC_ObjectApi)},
        {
            "SystemCollectionsGenericListVRCSDKBaseVRC_StationInputInputPairing",
            typeof(System.Collections.Generic.List<VRC.SDKBase.VRC_StationInput.InputPairing>)
        },
        {"SystemActionVRCSDKBaseVRCPlayerApiSystemSingle", typeof(System.Action<VRC.SDKBase.VRCPlayerApi,System.Single>)},
        {"UnityEngineWheelHit", typeof(UnityEngine.WheelHit)},
        {"UnityEngineAINavMeshHit", typeof(UnityEngine.AI.NavMeshHit)},
        {"UnityEngineUIILayoutElement", typeof(UnityEngine.UI.ILayoutElement)},
        {"SystemCollectionsGenericIEnumerableUnityEngineObject", typeof(IEnumerable<UnityEngine.Object>)},
        {"VRCInstantiate", typeof(VRCInstantiate)},
        {"VRCSDKBaseVRC_StationInputInputPairing", typeof(VRC.SDKBase.VRC_StationInput.InputPairing)},
        {"UnityEngineRenderingReflectionProbeBlendInfo", typeof(UnityEngine.Rendering.ReflectionProbeBlendInfo)},
        {"UnityEngineBoneWeight", typeof(UnityEngine.BoneWeight)},
        {"UnityEngineCombineInstance", typeof(UnityEngine.CombineInstance)},
        {"UnityEngineEventSystemsRaycastResult", typeof(UnityEngine.EventSystems.RaycastResult)},
        {"SystemTimeZoneInfoAdjustmentRule", typeof(System.TimeZoneInfo.AdjustmentRule)},
        {"UnityEngineGizmos", typeof(UnityEngine.Gizmos)},
    };

    protected override Dictionary<string, Type> Types => _types;
}

class VRCInstantiate//Dummy exception class
{
    
}