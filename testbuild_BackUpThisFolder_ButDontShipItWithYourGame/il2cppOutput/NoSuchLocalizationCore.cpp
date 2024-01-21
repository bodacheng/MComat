#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2;
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3;
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3<T1*, T2*, T3*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2, T3* p3)
	{
		void* params[3] = { p1, p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};

// System.Action`1<System.Int32>
struct Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404;
// System.Action`2<System.Int32,System.Int32>
struct Action_2_tD7438462601D3939500ED67463331FE00CFFBDB8;
// System.Threading.AsyncLocal`1<System.Globalization.CultureInfo>
struct AsyncLocal_1_t1D3339EA4C8650D2DEDDF9553E5C932B3DC2CCFD;
// System.Collections.Generic.Dictionary`2<System.Object,System.ValueTuple`2<System.Object,System.Object>>
struct Dictionary_2_t6F1450BD58C4E5A563CB6647A120640FF1708A98;
// System.Collections.Generic.Dictionary`2<System.Object,System.Object>
struct Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA;
// System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>
struct Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD;
// System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>
struct Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07;
// System.Collections.Generic.HashSet`1<System.Int32>
struct HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2;
// System.Collections.Generic.IEqualityComparer`1<System.Int32>
struct IEqualityComparer_1_tDBFC8496F14612776AF930DBF84AFE7D06D1F0E9;
// System.Collections.Generic.IEqualityComparer`1<System.String>
struct IEqualityComparer_1_tAE94C8F24AD5B94D4EE85CA9FC59E3409D41CAF7;
// System.Collections.Generic.IEqualityComparer`1<System.Type>
struct IEqualityComparer_1_t0C79004BFE79D9DBCE6C2250109D31D468A9A68E;
// System.Collections.Generic.Dictionary`2/KeyCollection<System.String,NoSuchStudio.Common.Singleton>
struct KeyCollection_t4F7EC7F5785A0E6C333DB2E7B433459516803D34;
// System.Collections.Generic.Dictionary`2/KeyCollection<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>
struct KeyCollection_t968BE79753B5A54B5F0E5934821EB003E7966297;
// System.Collections.Generic.List`1<System.Int32>
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73;
// System.Collections.Generic.Queue`1<System.Tuple`2<System.String,System.Object[]>>
struct Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C;
// System.Collections.Generic.Queue`1<System.Object>
struct Queue_1_tE9EF546915795972C3BFD68FBB8FA859D3BAF3B5;
// System.Tuple`2<System.Object,System.Object>
struct Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6;
// System.Tuple`2<System.String,System.Object[]>
struct Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16;
// System.Collections.Generic.Dictionary`2/ValueCollection<System.String,NoSuchStudio.Common.Singleton>
struct ValueCollection_tBDDE3AA5C2A7E0A4C846E1BACFD2209D7913066F;
// System.Collections.Generic.Dictionary`2/ValueCollection<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>
struct ValueCollection_t4D9BC6FB351767C1FA6B59EFFDB6AED0DAF7BFA7;
// System.Collections.Generic.Dictionary`2/Entry<System.String,NoSuchStudio.Common.Singleton>[]
struct EntryU5BU5D_t75C1A52488998546F8AA6E0553F36C2C4F0C2EA3;
// System.Collections.Generic.Dictionary`2/Entry<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>[]
struct EntryU5BU5D_t0D006CA7E913B43A877E6F39A3A799631CDBA96F;
// System.Collections.Generic.HashSet`1/Slot<System.Int32>[]
struct SlotU5BU5D_tC4D7CD3E804DC835CCF2F990797BC1D9AE4330D7;
// System.Tuple`2<System.String,System.Object[]>[]
struct Tuple_2U5BU5D_t09118D0EC70917D6B71760D4811C4C367DE9EC7C;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// UnityEngine.GameObject[]
struct GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF;
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// UnityEngine.Touch[]
struct TouchU5BU5D_t242545870BFCA81F368CCF82E00F9E2A7FB523B3;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// UnityEngine.Vector3[]
struct Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C;
// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
// System.ApplicationException
struct ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A;
// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// UnityEngine.Camera
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184;
// UnityEngine.Canvas
struct Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26;
// NoSuchStudio.Common.CanvasTouchVisualizer
struct CanvasTouchVisualizer_t751199DB21BF94CBE211E384F7A7856545FAD1CA;
// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
// UnityEngine.Coroutine
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B;
// System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// NoSuchStudio.Common.EditorUtilities
struct EditorUtilities_tE8DE91375ED2E9E172623F15AA994A6C0CFE3F10;
// NoSuchStudio.Common.Events
struct Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C;
// System.Exception
struct Exception_t;
// System.Threading.ExecutionContext
struct ExecutionContext_t9D6EDFD92F0B2D391751963E2D77A8B03CB81710;
// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
// System.IAsyncResult
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.Collections.IEnumerator
struct IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA;
// UnityEngine.ILogHandler
struct ILogHandler_tC139ADEB099E63CFA289F310D4BE306E16B5EAE1;
// UnityEngine.ILogger
struct ILogger_tD1F573C6DC829FBA987FA1EBA0A5FA64E0C2BC42;
// System.Security.Principal.IPrincipal
struct IPrincipal_tE7AF5096287F6C3472585E124CB38FF2A51EAB5F;
// NoSuchStudio.Common.IllegalStateException
struct IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC;
// NoSuchStudio.Common.InputTouchVisualizer
struct InputTouchVisualizer_t4330BBD81033C73ED064B27D2A9C30AA4C6A12C8;
// System.Threading.InternalThread
struct InternalThread_tF40B7BFCBD60C82BD8475A22FF5186CA10293687;
// System.LocalDataStoreHolder
struct LocalDataStoreHolder_t789DD474AE5141213C2105CE57830ECFC2D3C03F;
// System.LocalDataStoreMgr
struct LocalDataStoreMgr_t205F1783D5CC2B148E829B5882E5406FF9A3AC1E;
// UnityEngine.Logger
struct Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0;
// NoSuchStudio.Common.LoggerConfig
struct LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
// System.MulticastDelegate
struct MulticastDelegate_t;
// NoSuchStudio.Common.NoSuchMonoBehaviour
struct NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4;
// NoSuchStudio.Common.NoSuchScriptableObject
struct NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8;
// System.NotSupportedException
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// UnityEngine.RectTransform
struct RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// NoSuchStudio.Common.Scope
struct Scope_tF552A0888C1C104526116958D4BC266EDB879A0E;
// UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A;
// UnityEngine.UI.ScrollRect
struct ScrollRect_t17D2F2939CA8953110180DF53164CFC3DC88D70E;
// UnityEngine.UI.Scrollbar
struct Scrollbar_t7CDC9B956698D9385A11E4C12964CD51477072C3;
// System.Runtime.Serialization.SerializationInfo
struct SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37;
// NoSuchStudio.Common.Singleton
struct Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75;
// NoSuchStudio.Common.SingletonChildEnabler
struct SingletonChildEnabler_t99ADDFC4CD734D0CF08078CA1CE1674AB472AC6D;
// System.String
struct String_t;
// System.Threading.Thread
struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F;
// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.WaitForSeconds
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3;
// UnityEngine.WaitForSecondsRealtime
struct WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01;
// UnityEngine.Camera/CameraCallback
struct CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD;
// UnityEngine.Canvas/WillRenderCanvases
struct WillRenderCanvases_tA4A6E66DBA797DCB45B995DBA449A9D1D80D0FBC;
// NoSuchStudio.Common.Events/<RaiseEventInternal>d__10
struct U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B;
// NoSuchStudio.Common.Events/EventsDelegate
struct EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416;
// NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0
struct U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769;
// NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1
struct U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53;
// UnityEngine.RectTransform/ReapplyDrivenProperties
struct ReapplyDrivenProperties_t3482EA130A01FF7EE2EEFE37F66A5215D08CFE24;
// UnityEngine.UI.ScrollRect/ScrollRectEvent
struct ScrollRectEvent_t812C011901E6101F2A0FFC34C66AC5F65C0DEC26;

IL2CPP_EXTERN_C RuntimeClass* ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ILogger_tD1F573C6DC829FBA987FA1EBA0A5FA64E0C2BC42_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RectTransformUtility_t65C00A84A72F17D78B81F2E7D88C2AA98AB61244_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral10A105116F1400FFCE661E402C3C12DDCA0D688C;
IL2CPP_EXTERN_C String_t* _stringLiteral1FB9018D8BFC0FACF068B1067EF9E96C35FED1FE;
IL2CPP_EXTERN_C String_t* _stringLiteral23114468D04FA2B7A2DA455B545DB914D0A3ED94;
IL2CPP_EXTERN_C String_t* _stringLiteral2493EF500F7255CBBDEFD73C9C3D3AA6EEC00040;
IL2CPP_EXTERN_C String_t* _stringLiteral2703934E990F4D74F9E97D5985CDF284A870C0E0;
IL2CPP_EXTERN_C String_t* _stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A;
IL2CPP_EXTERN_C String_t* _stringLiteral5FD20D8504182B91A7EE1908D7A191F36ABAEDF1;
IL2CPP_EXTERN_C String_t* _stringLiteral63717794632FEDA33FCF6C202E592B6EA4DBC7F8;
IL2CPP_EXTERN_C String_t* _stringLiteral85BC4A18024062EE8394D71331785A0C1F66BFED;
IL2CPP_EXTERN_C String_t* _stringLiteral961BC57A0E961FF7DA97AB95377745D8766376D7;
IL2CPP_EXTERN_C String_t* _stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73;
IL2CPP_EXTERN_C String_t* _stringLiteralB3F14BF976EFD974E34846B742502C802FABAE9D;
IL2CPP_EXTERN_C String_t* _stringLiteralBF48F5F1A4487D9161428D14DC40A698E4596F3E;
IL2CPP_EXTERN_C String_t* _stringLiteralC087E631060AB76B7C814C0E1B92D5C7C4C4B924;
IL2CPP_EXTERN_C String_t* _stringLiteralD3AC132C0C7B7318DC5A23CCC9BC632A80976F30;
IL2CPP_EXTERN_C String_t* _stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1;
IL2CPP_EXTERN_C String_t* _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
IL2CPP_EXTERN_C String_t* _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC;
IL2CPP_EXTERN_C String_t* _stringLiteralE302AA9BECF9F1CB69CF2A3E5B33E0716BEA97F6;
IL2CPP_EXTERN_C const RuntimeMethod* Component_GetComponent_TisSingleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_m67CFFC259C315C7D32F39708EC5DE1D6B89FCBE2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_m6917FFC8B47B29FC2E7A65BA0C61EAF0C8ABF3F1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_ContainsKey_m700A5670F3CB7E83C52F2590D17EF521324F2430_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Remove_m13CE1B03E096BE40FECC8C7546831E80CD1A8D59_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_m7F749610DCC2068FFABD81A4FAC6522D6C334632_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_mC6AF8829C5C4C4865830344ACF22D1BDF29CF081_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_get_Item_mD046F6B66CAC9023A3AC965DD99BAE431D3F31D4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_set_Item_m45E21CB14A73F58BD606054CB89E38965210E75E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m40384472A2440993E6407EAFAC42C8E5F9E2A679_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m58912CEC7A4655D207EE2E2ACD74ED8AD6F65425_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_m11048A0F71FAE52952E39C32D7C45300444AD80D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Events_Awake_m712AF9922E5E4C4FB9E2E7917D43C42F7F0F5969_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_GetComponent_TisCamera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_m3B3C11550E48AA36AFF82788636EB163CC51FEE6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_GetComponent_TisCanvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26_mE5A2711FA84F57F5EA0876DB106B1A146956CEFE_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Helpers_UniqueRandom_m650C989F0CC5AE36C10D841B99F91795564F8535_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Queue_1_Enqueue_mB0520351271639D3269DC87FCD5AF5ECE4094CAD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Queue_1_GetEnumerator_m4F32C724CB1AA873049A953CC218B23FD86370AE_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Scope_Unapply_m450CB7685D9078B98566A1C8E15C4A7B3AF1E228_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Singleton_OnEnable_m3E4254EB3DA4A8A63C48775A1AD5A76C2B501B4B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Tuple_2_get_Item1_mFE4E6BB2EBDAFBED6CCFAD58B1EF4D1CE5236BA1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Tuple_2_get_Item2_m6EA5B1A59F9501053DAF2ECA63725144E35854B4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Tuple_Create_TisString_t_TisObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_mFFF9A96F99C9F68C88C6B4FBDA62419C4E5307DB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CDelayedCoroutineRealtimeU3Ed__1_System_Collections_IEnumerator_Reset_m323CEAB25061C010FAC5F4A864FDCE172B8E592F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CDelayedCoroutineU3Ed__0_System_Collections_IEnumerator_Reset_m7A9A7E9003837F53021473B482B0C3DDDC6345A9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CRaiseEventInternalU3Ed__10_System_Collections_IEnumerator_Reset_mF5A95EA267EF8058AF7ECD0E737789D999B9B8D0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ValueTuple_2__ctor_m704CDA27B90CDBBAE2DC59E142CCEA85ABCEAD3B_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct TouchU5BU5D_t242545870BFCA81F368CCF82E00F9E2A7FB523B3;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t29AE8F71E6EF060373B2D500B20712A73D84D73C 
{
};

// System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>
struct Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_t75C1A52488998546F8AA6E0553F36C2C4F0C2EA3* ____entries_1;
	// System.Int32 System.Collections.Generic.Dictionary`2::_count
	int32_t ____count_2;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeList
	int32_t ____freeList_3;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeCount
	int32_t ____freeCount_4;
	// System.Int32 System.Collections.Generic.Dictionary`2::_version
	int32_t ____version_5;
	// System.Collections.Generic.IEqualityComparer`1<TKey> System.Collections.Generic.Dictionary`2::_comparer
	RuntimeObject* ____comparer_6;
	// System.Collections.Generic.Dictionary`2/KeyCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_keys
	KeyCollection_t4F7EC7F5785A0E6C333DB2E7B433459516803D34* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_tBDDE3AA5C2A7E0A4C846E1BACFD2209D7913066F* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>
struct Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_t0D006CA7E913B43A877E6F39A3A799631CDBA96F* ____entries_1;
	// System.Int32 System.Collections.Generic.Dictionary`2::_count
	int32_t ____count_2;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeList
	int32_t ____freeList_3;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeCount
	int32_t ____freeCount_4;
	// System.Int32 System.Collections.Generic.Dictionary`2::_version
	int32_t ____version_5;
	// System.Collections.Generic.IEqualityComparer`1<TKey> System.Collections.Generic.Dictionary`2::_comparer
	RuntimeObject* ____comparer_6;
	// System.Collections.Generic.Dictionary`2/KeyCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_keys
	KeyCollection_t968BE79753B5A54B5F0E5934821EB003E7966297* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_t4D9BC6FB351767C1FA6B59EFFDB6AED0DAF7BFA7* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.Collections.Generic.HashSet`1<System.Int32>
struct HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.HashSet`1::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_7;
	// System.Collections.Generic.HashSet`1/Slot<T>[] System.Collections.Generic.HashSet`1::_slots
	SlotU5BU5D_tC4D7CD3E804DC835CCF2F990797BC1D9AE4330D7* ____slots_8;
	// System.Int32 System.Collections.Generic.HashSet`1::_count
	int32_t ____count_9;
	// System.Int32 System.Collections.Generic.HashSet`1::_lastIndex
	int32_t ____lastIndex_10;
	// System.Int32 System.Collections.Generic.HashSet`1::_freeList
	int32_t ____freeList_11;
	// System.Collections.Generic.IEqualityComparer`1<T> System.Collections.Generic.HashSet`1::_comparer
	RuntimeObject* ____comparer_12;
	// System.Int32 System.Collections.Generic.HashSet`1::_version
	int32_t ____version_13;
	// System.Runtime.Serialization.SerializationInfo System.Collections.Generic.HashSet`1::_siInfo
	SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* ____siInfo_14;
};

// System.Collections.Generic.List`1<System.Int32>
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_emptyArray_5;
};

// System.Collections.Generic.Queue`1<System.Tuple`2<System.String,System.Object[]>>
struct Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C  : public RuntimeObject
{
	// T[] System.Collections.Generic.Queue`1::_array
	Tuple_2U5BU5D_t09118D0EC70917D6B71760D4811C4C367DE9EC7C* ____array_0;
	// System.Int32 System.Collections.Generic.Queue`1::_head
	int32_t ____head_1;
	// System.Int32 System.Collections.Generic.Queue`1::_tail
	int32_t ____tail_2;
	// System.Int32 System.Collections.Generic.Queue`1::_size
	int32_t ____size_3;
	// System.Int32 System.Collections.Generic.Queue`1::_version
	int32_t ____version_4;
	// System.Object System.Collections.Generic.Queue`1::_syncRoot
	RuntimeObject* ____syncRoot_5;
};

// System.Tuple`2<System.Object,System.Object>
struct Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6  : public RuntimeObject
{
	// T1 System.Tuple`2::m_Item1
	RuntimeObject* ___m_Item1_0;
	// T2 System.Tuple`2::m_Item2
	RuntimeObject* ___m_Item2_1;
};

// System.Tuple`2<System.String,System.Object[]>
struct Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16  : public RuntimeObject
{
	// T1 System.Tuple`2::m_Item1
	String_t* ___m_Item1_0;
	// T2 System.Tuple`2::m_Item2
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___m_Item2_1;
};
struct Il2CppArrayBounds;

// NoSuchStudio.Common.CollectionExts
struct CollectionExts_t4D2DAEC03760AA6BBE7FFFF07CFA628196521AF2  : public RuntimeObject
{
};

// System.Runtime.ConstrainedExecution.CriticalFinalizerObject
struct CriticalFinalizerObject_t1DCAB623CAEA6529A96F5F3EDE3C7048A6E313C9  : public RuntimeObject
{
};

// UnityEngine.CustomYieldInstruction
struct CustomYieldInstruction_t6B81A50D5D210C1ACAAE247FB53B65CDFFEB7617  : public RuntimeObject
{
};

// UnityEngine.Debug
struct Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F  : public RuntimeObject
{
};

struct Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticFields
{
	// UnityEngine.ILogger UnityEngine.Debug::s_DefaultLogger
	RuntimeObject* ___s_DefaultLogger_0;
	// UnityEngine.ILogger UnityEngine.Debug::s_Logger
	RuntimeObject* ___s_Logger_1;
};

// NoSuchStudio.Common.EditorUtilities
struct EditorUtilities_tE8DE91375ED2E9E172623F15AA994A6C0CFE3F10  : public RuntimeObject
{
};

// NoSuchStudio.Common.ExceptionExts
struct ExceptionExts_t06282EA8878CA6CA06F22B0AD0FFA8382169F687  : public RuntimeObject
{
};

// NoSuchStudio.Common.HSVColor
struct HSVColor_tD9233790BF3F872F989134D34AA1A57162B8D12F  : public RuntimeObject
{
};

// NoSuchStudio.Common.Helpers
struct Helpers_t12E54D25C804972399383D6F8C10A7B04593AA7A  : public RuntimeObject
{
};

// UnityEngine.Logger
struct Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0  : public RuntimeObject
{
	// UnityEngine.ILogHandler UnityEngine.Logger::<logHandler>k__BackingField
	RuntimeObject* ___U3ClogHandlerU3Ek__BackingField_0;
	// System.Boolean UnityEngine.Logger::<logEnabled>k__BackingField
	bool ___U3ClogEnabledU3Ek__BackingField_1;
	// UnityEngine.LogType UnityEngine.Logger::<filterLogType>k__BackingField
	int32_t ___U3CfilterLogTypeU3Ek__BackingField_2;
};

// NoSuchStudio.Common.LoggerConfig
struct LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6  : public RuntimeObject
{
	// System.String NoSuchStudio.Common.LoggerConfig::className
	String_t* ___className_0;
	// System.Boolean NoSuchStudio.Common.LoggerConfig::logClassName
	bool ___logClassName_1;
	// System.Boolean NoSuchStudio.Common.LoggerConfig::logGameObjectName
	bool ___logGameObjectName_2;
	// System.Boolean NoSuchStudio.Common.LoggerConfig::logThreadId
	bool ___logThreadId_3;
	// System.Boolean NoSuchStudio.Common.LoggerConfig::logGameTime
	bool ___logGameTime_4;
};

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// NoSuchStudio.Common.MonoBehaviourRunDelayedExt
struct MonoBehaviourRunDelayedExt_tD8A9DD0839972E2957324CDDE5454F7BC2D71DF8  : public RuntimeObject
{
};

// NoSuchStudio.Common.Scope
struct Scope_tF552A0888C1C104526116958D4BC266EDB879A0E  : public RuntimeObject
{
	// System.String NoSuchStudio.Common.Scope::_scope
	String_t* ____scope_1;
	// System.String NoSuchStudio.Common.Scope::_delimiter
	String_t* ____delimiter_2;
};

struct Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_StaticFields
{
	// NoSuchStudio.Common.Scope NoSuchStudio.Common.Scope::Global
	Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* ___Global_0;
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// NoSuchStudio.Common.ToStringExts
struct ToStringExts_t28B2BBAD1384348BFD6EBDEF2D4D94AD9403390D  : public RuntimeObject
{
};

// NoSuchStudio.Common.TransformExt
struct TransformExt_tD7AE5247622A352469ACE7998CA886CF228C4213  : public RuntimeObject
{
};

// NoSuchStudio.Common.UIExts
struct UIExts_tDF6CD1DA01E849E288E32860BFFB1AB4E0CC793F  : public RuntimeObject
{
};

// NoSuchStudio.Common.UnityObjectLoggerExt
struct UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973  : public RuntimeObject
{
};

struct UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_StaticFields
{
	// System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>> NoSuchStudio.Common.UnityObjectLoggerExt::loggers
	Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* ___loggers_0;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
};

// NoSuchStudio.Common.Events/<RaiseEventInternal>d__10
struct U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B  : public RuntimeObject
{
	// System.Int32 NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::<>1__state
	int32_t ___U3CU3E1__state_0;
	// System.Object NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::<>2__current
	RuntimeObject* ___U3CU3E2__current_1;
	// NoSuchStudio.Common.Events NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::<>4__this
	Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* ___U3CU3E4__this_2;
};

// NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0
struct U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769  : public RuntimeObject
{
	// System.Int32 NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::<>1__state
	int32_t ___U3CU3E1__state_0;
	// System.Object NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::<>2__current
	RuntimeObject* ___U3CU3E2__current_1;
	// System.Single NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::delay
	float ___delay_2;
	// System.Action NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::a
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a_3;
};

// NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1
struct U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53  : public RuntimeObject
{
	// System.Int32 NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::<>1__state
	int32_t ___U3CU3E1__state_0;
	// System.Object NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::<>2__current
	RuntimeObject* ___U3CU3E2__current_1;
	// System.Single NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::delay
	float ___delay_2;
	// System.Action NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::a
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a_3;
};

// System.Collections.Generic.Queue`1/Enumerator<System.Tuple`2<System.String,System.Object[]>>
struct Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626 
{
	// System.Collections.Generic.Queue`1<T> System.Collections.Generic.Queue`1/Enumerator::_q
	Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* ____q_0;
	// System.Int32 System.Collections.Generic.Queue`1/Enumerator::_version
	int32_t ____version_1;
	// System.Int32 System.Collections.Generic.Queue`1/Enumerator::_index
	int32_t ____index_2;
	// T System.Collections.Generic.Queue`1/Enumerator::_currentElement
	Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* ____currentElement_3;
};

// System.Collections.Generic.Queue`1/Enumerator<System.Object>
struct Enumerator_t30E3290EE12437374037B3CF0EE4D614F96D030A 
{
	// System.Collections.Generic.Queue`1<T> System.Collections.Generic.Queue`1/Enumerator::_q
	Queue_1_tE9EF546915795972C3BFD68FBB8FA859D3BAF3B5* ____q_0;
	// System.Int32 System.Collections.Generic.Queue`1/Enumerator::_version
	int32_t ____version_1;
	// System.Int32 System.Collections.Generic.Queue`1/Enumerator::_index
	int32_t ____index_2;
	// T System.Collections.Generic.Queue`1/Enumerator::_currentElement
	RuntimeObject* ____currentElement_3;
};

// System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>
struct ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F 
{
	// T1 System.ValueTuple`2::Item1
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* ___Item1_0;
	// T2 System.ValueTuple`2::Item2
	LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* ___Item2_1;
};

// System.ValueTuple`2<System.Object,System.Object>
struct ValueTuple_2_tC3717D4552EE1E5FC27BFBA3F5155741BC04557A 
{
	// T1 System.ValueTuple`2::Item1
	RuntimeObject* ___Item1_0;
	// T2 System.ValueTuple`2::Item2
	RuntimeObject* ___Item2_1;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// UnityEngine.Color
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	// System.Single UnityEngine.Color::r
	float ___r_0;
	// System.Single UnityEngine.Color::g
	float ___g_1;
	// System.Single UnityEngine.Color::b
	float ___b_2;
	// System.Single UnityEngine.Color::a
	float ___a_3;
};

// UnityEngine.DrivenRectTransformTracker
struct DrivenRectTransformTracker_tFB0706C933E3C68E4F377C204FCEEF091F1EE0B1 
{
	union
	{
		struct
		{
		};
		uint8_t DrivenRectTransformTracker_tFB0706C933E3C68E4F377C204FCEEF091F1EE0B1__padding[1];
	};
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.Threading.Thread
struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F  : public CriticalFinalizerObject_t1DCAB623CAEA6529A96F5F3EDE3C7048A6E313C9
{
	// System.Threading.InternalThread System.Threading.Thread::internal_thread
	InternalThread_tF40B7BFCBD60C82BD8475A22FF5186CA10293687* ___internal_thread_6;
	// System.Object System.Threading.Thread::m_ThreadStartArg
	RuntimeObject* ___m_ThreadStartArg_7;
	// System.Object System.Threading.Thread::pending_exception
	RuntimeObject* ___pending_exception_8;
	// System.MulticastDelegate System.Threading.Thread::m_Delegate
	MulticastDelegate_t* ___m_Delegate_10;
	// System.Threading.ExecutionContext System.Threading.Thread::m_ExecutionContext
	ExecutionContext_t9D6EDFD92F0B2D391751963E2D77A8B03CB81710* ___m_ExecutionContext_11;
	// System.Boolean System.Threading.Thread::m_ExecutionContextBelongsToOuterScope
	bool ___m_ExecutionContextBelongsToOuterScope_12;
	// System.Security.Principal.IPrincipal System.Threading.Thread::principal
	RuntimeObject* ___principal_13;
	// System.Int32 System.Threading.Thread::principal_version
	int32_t ___principal_version_14;
};

struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F_StaticFields
{
	// System.LocalDataStoreMgr System.Threading.Thread::s_LocalDataStoreMgr
	LocalDataStoreMgr_t205F1783D5CC2B148E829B5882E5406FF9A3AC1E* ___s_LocalDataStoreMgr_0;
	// System.Threading.AsyncLocal`1<System.Globalization.CultureInfo> System.Threading.Thread::s_asyncLocalCurrentCulture
	AsyncLocal_1_t1D3339EA4C8650D2DEDDF9553E5C932B3DC2CCFD* ___s_asyncLocalCurrentCulture_4;
	// System.Threading.AsyncLocal`1<System.Globalization.CultureInfo> System.Threading.Thread::s_asyncLocalCurrentUICulture
	AsyncLocal_1_t1D3339EA4C8650D2DEDDF9553E5C932B3DC2CCFD* ___s_asyncLocalCurrentUICulture_5;
};

struct Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F_ThreadStaticFields
{
	// System.LocalDataStoreHolder System.Threading.Thread::s_LocalDataStore
	LocalDataStoreHolder_t789DD474AE5141213C2105CE57830ECFC2D3C03F* ___s_LocalDataStore_1;
	// System.Globalization.CultureInfo System.Threading.Thread::m_CurrentCulture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___m_CurrentCulture_2;
	// System.Globalization.CultureInfo System.Threading.Thread::m_CurrentUICulture
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___m_CurrentUICulture_3;
	// System.Threading.Thread System.Threading.Thread::current_thread
	Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* ___current_thread_9;
};

// UnityEngine.Vector2
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	// System.Single UnityEngine.Vector2::x
	float ___x_0;
	// System.Single UnityEngine.Vector2::y
	float ___y_1;
};

struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_StaticFields
{
	// UnityEngine.Vector2 UnityEngine.Vector2::zeroVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___zeroVector_2;
	// UnityEngine.Vector2 UnityEngine.Vector2::oneVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___oneVector_3;
	// UnityEngine.Vector2 UnityEngine.Vector2::upVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___upVector_4;
	// UnityEngine.Vector2 UnityEngine.Vector2::downVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___downVector_5;
	// UnityEngine.Vector2 UnityEngine.Vector2::leftVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___leftVector_6;
	// UnityEngine.Vector2 UnityEngine.Vector2::rightVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rightVector_7;
	// UnityEngine.Vector2 UnityEngine.Vector2::positiveInfinityVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___positiveInfinityVector_8;
	// UnityEngine.Vector2 UnityEngine.Vector2::negativeInfinityVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___negativeInfinityVector_9;
};

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	// System.Single UnityEngine.Vector3::x
	float ___x_2;
	// System.Single UnityEngine.Vector3::y
	float ___y_3;
	// System.Single UnityEngine.Vector3::z
	float ___z_4;
};

struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields
{
	// UnityEngine.Vector3 UnityEngine.Vector3::zeroVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___zeroVector_5;
	// UnityEngine.Vector3 UnityEngine.Vector3::oneVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___oneVector_6;
	// UnityEngine.Vector3 UnityEngine.Vector3::upVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___upVector_7;
	// UnityEngine.Vector3 UnityEngine.Vector3::downVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___downVector_8;
	// UnityEngine.Vector3 UnityEngine.Vector3::leftVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___leftVector_9;
	// UnityEngine.Vector3 UnityEngine.Vector3::rightVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rightVector_10;
	// UnityEngine.Vector3 UnityEngine.Vector3::forwardVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forwardVector_11;
	// UnityEngine.Vector3 UnityEngine.Vector3::backVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backVector_12;
	// UnityEngine.Vector3 UnityEngine.Vector3::positiveInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___positiveInfinityVector_13;
	// UnityEngine.Vector3 UnityEngine.Vector3::negativeInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___negativeInfinityVector_14;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// UnityEngine.WaitForSeconds
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	// System.Single UnityEngine.WaitForSeconds::m_Seconds
	float ___m_Seconds_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.WaitForSeconds
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	float ___m_Seconds_0;
};
// Native definition for COM marshalling of UnityEngine.WaitForSeconds
struct WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	float ___m_Seconds_0;
};

// UnityEngine.WaitForSecondsRealtime
struct WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01  : public CustomYieldInstruction_t6B81A50D5D210C1ACAAE247FB53B65CDFFEB7617
{
	// System.Single UnityEngine.WaitForSecondsRealtime::<waitTime>k__BackingField
	float ___U3CwaitTimeU3Ek__BackingField_0;
	// System.Single UnityEngine.WaitForSecondsRealtime::m_WaitUntilTime
	float ___m_WaitUntilTime_1;
};

// UnityEngine.Bounds
struct Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 
{
	// UnityEngine.Vector3 UnityEngine.Bounds::m_Center
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Center_0;
	// UnityEngine.Vector3 UnityEngine.Bounds::m_Extents
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Extents_1;
};

// UnityEngine.Coroutine
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	// System.IntPtr UnityEngine.Coroutine::m_Ptr
	intptr_t ___m_Ptr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Coroutine
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
};
// Native definition for COM marshalling of UnityEngine.Coroutine
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	intptr_t ___m_Ptr_0;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};

struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};

struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
};

// UnityEngine.Touch
struct Touch_t03E51455ED508492B3F278903A0114FA0E87B417 
{
	// System.Int32 UnityEngine.Touch::m_FingerId
	int32_t ___m_FingerId_0;
	// UnityEngine.Vector2 UnityEngine.Touch::m_Position
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_Position_1;
	// UnityEngine.Vector2 UnityEngine.Touch::m_RawPosition
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_RawPosition_2;
	// UnityEngine.Vector2 UnityEngine.Touch::m_PositionDelta
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_PositionDelta_3;
	// System.Single UnityEngine.Touch::m_TimeDelta
	float ___m_TimeDelta_4;
	// System.Int32 UnityEngine.Touch::m_TapCount
	int32_t ___m_TapCount_5;
	// UnityEngine.TouchPhase UnityEngine.Touch::m_Phase
	int32_t ___m_Phase_6;
	// UnityEngine.TouchType UnityEngine.Touch::m_Type
	int32_t ___m_Type_7;
	// System.Single UnityEngine.Touch::m_Pressure
	float ___m_Pressure_8;
	// System.Single UnityEngine.Touch::m_maximumPossiblePressure
	float ___m_maximumPossiblePressure_9;
	// System.Single UnityEngine.Touch::m_Radius
	float ___m_Radius_10;
	// System.Single UnityEngine.Touch::m_RadiusVariance
	float ___m_RadiusVariance_11;
	// System.Single UnityEngine.Touch::m_AltitudeAngle
	float ___m_AltitudeAngle_12;
	// System.Single UnityEngine.Touch::m_AzimuthAngle
	float ___m_AzimuthAngle_13;
};

// System.ApplicationException
struct ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A  : public Exception_t
{
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// NoSuchStudio.Common.IllegalStateException
struct IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC  : public Exception_t
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
// Native definition for P/Invoke marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// System.Type
struct Type_t  : public MemberInfo_t
{
	// System.RuntimeTypeHandle System.Type::_impl
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl_8;
};

struct Type_t_StaticFields
{
	// System.Reflection.Binder modreq(System.Runtime.CompilerServices.IsVolatile) System.Type::s_defaultBinder
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder_0;
	// System.Char System.Type::Delimiter
	Il2CppChar ___Delimiter_1;
	// System.Type[] System.Type::EmptyTypes
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes_2;
	// System.Object System.Type::Missing
	RuntimeObject* ___Missing_3;
	// System.Reflection.MemberFilter System.Type::FilterAttribute
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute_4;
	// System.Reflection.MemberFilter System.Type::FilterName
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName_5;
	// System.Reflection.MemberFilter System.Type::FilterNameIgnoreCase
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase_6;
};

// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};

// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C  : public MulticastDelegate_t
{
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// NoSuchStudio.Common.NoSuchScriptableObject
struct NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
};

// System.NotSupportedException
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// NoSuchStudio.Common.Events/EventsDelegate
struct EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416  : public MulticastDelegate_t
{
};

// UnityEngine.Camera
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_StaticFields
{
	// UnityEngine.Camera/CameraCallback UnityEngine.Camera::onPreCull
	CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD* ___onPreCull_4;
	// UnityEngine.Camera/CameraCallback UnityEngine.Camera::onPreRender
	CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD* ___onPreRender_5;
	// UnityEngine.Camera/CameraCallback UnityEngine.Camera::onPostRender
	CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD* ___onPostRender_6;
};

// UnityEngine.Canvas
struct Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

struct Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26_StaticFields
{
	// UnityEngine.Canvas/WillRenderCanvases UnityEngine.Canvas::preWillRenderCanvases
	WillRenderCanvases_tA4A6E66DBA797DCB45B995DBA449A9D1D80D0FBC* ___preWillRenderCanvases_4;
	// UnityEngine.Canvas/WillRenderCanvases UnityEngine.Canvas::willRenderCanvases
	WillRenderCanvases_tA4A6E66DBA797DCB45B995DBA449A9D1D80D0FBC* ___willRenderCanvases_5;
	// System.Action`1<System.Int32> UnityEngine.Canvas::<externBeginRenderOverlays>k__BackingField
	Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___U3CexternBeginRenderOverlaysU3Ek__BackingField_6;
	// System.Action`2<System.Int32,System.Int32> UnityEngine.Canvas::<externRenderOverlaysBefore>k__BackingField
	Action_2_tD7438462601D3939500ED67463331FE00CFFBDB8* ___U3CexternRenderOverlaysBeforeU3Ek__BackingField_7;
	// System.Action`1<System.Int32> UnityEngine.Canvas::<externEndRenderOverlays>k__BackingField
	Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___U3CexternEndRenderOverlaysU3Ek__BackingField_8;
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// UnityEngine.RectTransform
struct RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5  : public Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1
{
};

struct RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5_StaticFields
{
	// UnityEngine.RectTransform/ReapplyDrivenProperties UnityEngine.RectTransform::reapplyDrivenProperties
	ReapplyDrivenProperties_t3482EA130A01FF7EE2EEFE37F66A5215D08CFE24* ___reapplyDrivenProperties_4;
};

// NoSuchStudio.Common.CanvasTouchVisualizer
struct CanvasTouchVisualizer_t751199DB21BF94CBE211E384F7A7856545FAD1CA  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// UnityEngine.Canvas NoSuchStudio.Common.CanvasTouchVisualizer::mainCanvas
	Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* ___mainCanvas_4;
	// UnityEngine.Camera NoSuchStudio.Common.CanvasTouchVisualizer::mainCamera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___mainCamera_5;
	// UnityEngine.GameObject NoSuchStudio.Common.CanvasTouchVisualizer::prefab
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___prefab_6;
	// UnityEngine.GameObject[] NoSuchStudio.Common.CanvasTouchVisualizer::touchVisuals
	GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* ___touchVisuals_7;
	// System.Int32 NoSuchStudio.Common.CanvasTouchVisualizer::maxTouchCount
	int32_t ___maxTouchCount_8;
};

// NoSuchStudio.Common.Events
struct Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// NoSuchStudio.Common.Events/EventsDelegate NoSuchStudio.Common.Events::gEvent
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* ___gEvent_5;
	// System.Boolean NoSuchStudio.Common.Events::_alreadyRaised
	bool ____alreadyRaised_6;
	// System.Collections.Generic.Queue`1<System.Tuple`2<System.String,System.Object[]>> NoSuchStudio.Common.Events::_eventQueue
	Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* ____eventQueue_7;
};

struct Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_StaticFields
{
	// NoSuchStudio.Common.Events NoSuchStudio.Common.Events::gInstance
	Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* ___gInstance_4;
};

// NoSuchStudio.Common.InputTouchVisualizer
struct InputTouchVisualizer_t4330BBD81033C73ED064B27D2A9C30AA4C6A12C8  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// UnityEngine.GameObject NoSuchStudio.Common.InputTouchVisualizer::prefab
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___prefab_4;
	// UnityEngine.GameObject[] NoSuchStudio.Common.InputTouchVisualizer::touchVisuals
	GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* ___touchVisuals_5;
	// System.Int32 NoSuchStudio.Common.InputTouchVisualizer::maxTouchCount
	int32_t ___maxTouchCount_6;
};

// NoSuchStudio.Common.NoSuchMonoBehaviour
struct NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};

// UnityEngine.EventSystems.UIBehaviour
struct UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};

// UnityEngine.UI.ScrollRect
struct ScrollRect_t17D2F2939CA8953110180DF53164CFC3DC88D70E  : public UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D
{
	// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::m_Content
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_Content_4;
	// System.Boolean UnityEngine.UI.ScrollRect::m_Horizontal
	bool ___m_Horizontal_5;
	// System.Boolean UnityEngine.UI.ScrollRect::m_Vertical
	bool ___m_Vertical_6;
	// UnityEngine.UI.ScrollRect/MovementType UnityEngine.UI.ScrollRect::m_MovementType
	int32_t ___m_MovementType_7;
	// System.Single UnityEngine.UI.ScrollRect::m_Elasticity
	float ___m_Elasticity_8;
	// System.Boolean UnityEngine.UI.ScrollRect::m_Inertia
	bool ___m_Inertia_9;
	// System.Single UnityEngine.UI.ScrollRect::m_DecelerationRate
	float ___m_DecelerationRate_10;
	// System.Single UnityEngine.UI.ScrollRect::m_ScrollSensitivity
	float ___m_ScrollSensitivity_11;
	// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::m_Viewport
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_Viewport_12;
	// UnityEngine.UI.Scrollbar UnityEngine.UI.ScrollRect::m_HorizontalScrollbar
	Scrollbar_t7CDC9B956698D9385A11E4C12964CD51477072C3* ___m_HorizontalScrollbar_13;
	// UnityEngine.UI.Scrollbar UnityEngine.UI.ScrollRect::m_VerticalScrollbar
	Scrollbar_t7CDC9B956698D9385A11E4C12964CD51477072C3* ___m_VerticalScrollbar_14;
	// UnityEngine.UI.ScrollRect/ScrollbarVisibility UnityEngine.UI.ScrollRect::m_HorizontalScrollbarVisibility
	int32_t ___m_HorizontalScrollbarVisibility_15;
	// UnityEngine.UI.ScrollRect/ScrollbarVisibility UnityEngine.UI.ScrollRect::m_VerticalScrollbarVisibility
	int32_t ___m_VerticalScrollbarVisibility_16;
	// System.Single UnityEngine.UI.ScrollRect::m_HorizontalScrollbarSpacing
	float ___m_HorizontalScrollbarSpacing_17;
	// System.Single UnityEngine.UI.ScrollRect::m_VerticalScrollbarSpacing
	float ___m_VerticalScrollbarSpacing_18;
	// UnityEngine.UI.ScrollRect/ScrollRectEvent UnityEngine.UI.ScrollRect::m_OnValueChanged
	ScrollRectEvent_t812C011901E6101F2A0FFC34C66AC5F65C0DEC26* ___m_OnValueChanged_19;
	// UnityEngine.Vector2 UnityEngine.UI.ScrollRect::m_PointerStartLocalCursor
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_PointerStartLocalCursor_20;
	// UnityEngine.Vector2 UnityEngine.UI.ScrollRect::m_ContentStartPosition
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_ContentStartPosition_21;
	// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::m_ViewRect
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_ViewRect_22;
	// UnityEngine.Bounds UnityEngine.UI.ScrollRect::m_ContentBounds
	Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 ___m_ContentBounds_23;
	// UnityEngine.Bounds UnityEngine.UI.ScrollRect::m_ViewBounds
	Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 ___m_ViewBounds_24;
	// UnityEngine.Vector2 UnityEngine.UI.ScrollRect::m_Velocity
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_Velocity_25;
	// System.Boolean UnityEngine.UI.ScrollRect::m_Dragging
	bool ___m_Dragging_26;
	// System.Boolean UnityEngine.UI.ScrollRect::m_Scrolling
	bool ___m_Scrolling_27;
	// UnityEngine.Vector2 UnityEngine.UI.ScrollRect::m_PrevPosition
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_PrevPosition_28;
	// UnityEngine.Bounds UnityEngine.UI.ScrollRect::m_PrevContentBounds
	Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 ___m_PrevContentBounds_29;
	// UnityEngine.Bounds UnityEngine.UI.ScrollRect::m_PrevViewBounds
	Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 ___m_PrevViewBounds_30;
	// System.Boolean UnityEngine.UI.ScrollRect::m_HasRebuiltLayout
	bool ___m_HasRebuiltLayout_31;
	// System.Boolean UnityEngine.UI.ScrollRect::m_HSliderExpand
	bool ___m_HSliderExpand_32;
	// System.Boolean UnityEngine.UI.ScrollRect::m_VSliderExpand
	bool ___m_VSliderExpand_33;
	// System.Single UnityEngine.UI.ScrollRect::m_HSliderHeight
	float ___m_HSliderHeight_34;
	// System.Single UnityEngine.UI.ScrollRect::m_VSliderWidth
	float ___m_VSliderWidth_35;
	// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::m_Rect
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_Rect_36;
	// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::m_HorizontalScrollbarRect
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_HorizontalScrollbarRect_37;
	// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::m_VerticalScrollbarRect
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_VerticalScrollbarRect_38;
	// UnityEngine.DrivenRectTransformTracker UnityEngine.UI.ScrollRect::m_Tracker
	DrivenRectTransformTracker_tFB0706C933E3C68E4F377C204FCEEF091F1EE0B1 ___m_Tracker_39;
	// UnityEngine.Vector3[] UnityEngine.UI.ScrollRect::m_Corners
	Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C* ___m_Corners_40;
};

// NoSuchStudio.Common.Singleton
struct Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75  : public NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4
{
	// System.String NoSuchStudio.Common.Singleton::tagName
	String_t* ___tagName_4;
};

struct Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields
{
	// System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton> NoSuchStudio.Common.Singleton::_instances
	Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* ____instances_5;
};

// NoSuchStudio.Common.SingletonChildEnabler
struct SingletonChildEnabler_t99ADDFC4CD734D0CF08078CA1CE1674AB472AC6D  : public NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4
{
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// UnityEngine.GameObject[]
struct GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF  : public RuntimeArray
{
	ALIGN_FIELD (8) GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* m_Items[1];

	inline GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline GameObject_t76FEDD663AB33C991A9C9A23129337651094216F** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline GameObject_t76FEDD663AB33C991A9C9A23129337651094216F** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// UnityEngine.Touch[]
struct TouchU5BU5D_t242545870BFCA81F368CCF82E00F9E2A7FB523B3  : public RuntimeArray
{
	ALIGN_FIELD (8) Touch_t03E51455ED508492B3F278903A0114FA0E87B417 m_Items[1];

	inline Touch_t03E51455ED508492B3F278903A0114FA0E87B417 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Touch_t03E51455ED508492B3F278903A0114FA0E87B417* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Touch_t03E51455ED508492B3F278903A0114FA0E87B417 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Touch_t03E51455ED508492B3F278903A0114FA0E87B417 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Touch_t03E51455ED508492B3F278903A0114FA0E87B417* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Touch_t03E51455ED508492B3F278903A0114FA0E87B417 value)
	{
		m_Items[index] = value;
	}
};
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C  : public RuntimeArray
{
	ALIGN_FIELD (8) int32_t m_Items[1];

	inline int32_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline int32_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, int32_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline int32_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline int32_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, int32_t value)
	{
		m_Items[index] = value;
	}
};


// T UnityEngine.GameObject::GetComponent<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GameObject_GetComponent_TisRuntimeObject_m6EAED4AA356F0F48288F67899E5958792395563B_gshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;
// T UnityEngine.Object::Instantiate<System.Object>(T,UnityEngine.Transform,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Object_Instantiate_TisRuntimeObject_m8784E2833D9F115FD2B2BED6615426E8CD75EE9B_gshared (RuntimeObject* ___original0, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___parent1, bool ___worldPositionStays2, const RuntimeMethod* method) ;
// System.Tuple`2<T1,T2> System.Tuple::Create<System.Object,System.Object>(T1,T2)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6* Tuple_Create_TisRuntimeObject_TisRuntimeObject_m1185C3D02620CC5F26786D9BE962850A28346DD2_gshared (RuntimeObject* ___item10, RuntimeObject* ___item21, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Queue`1<System.Object>::Enqueue(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Queue_1_Enqueue_m5CB8CF3906F1289F92036F0973EC5BE3450402EF_gshared (Queue_1_tE9EF546915795972C3BFD68FBB8FA859D3BAF3B5* __this, RuntimeObject* ___item0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Queue`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Queue_1__ctor_m6E2A5A8173E0CC524496D5155C737DF8FD10D0EB_gshared (Queue_1_tE9EF546915795972C3BFD68FBB8FA859D3BAF3B5* __this, const RuntimeMethod* method) ;
// System.Collections.Generic.Queue`1/Enumerator<T> System.Collections.Generic.Queue`1<System.Object>::GetEnumerator()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t30E3290EE12437374037B3CF0EE4D614F96D030A Queue_1_GetEnumerator_mBF0033C4BCEA408644D24F0B28A81F9145FB97C9_gshared (Queue_1_tE9EF546915795972C3BFD68FBB8FA859D3BAF3B5* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Queue`1/Enumerator<System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m680926A5EFC7099ECBCE9DEF68F8DED03C103955_gshared (Enumerator_t30E3290EE12437374037B3CF0EE4D614F96D030A* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.Queue`1/Enumerator<System.Object>::get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m5F2338F4C35E898DB7231D7E30F30155498FA9D7_gshared (Enumerator_t30E3290EE12437374037B3CF0EE4D614F96D030A* __this, const RuntimeMethod* method) ;
// T1 System.Tuple`2<System.Object,System.Object>::get_Item1()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Tuple_2_get_Item1_mBF34A596062BBB3C1DD2A6CA36810366F445C9FA_gshared_inline (Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6* __this, const RuntimeMethod* method) ;
// T2 System.Tuple`2<System.Object,System.Object>::get_Item2()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Tuple_2_get_Item2_m4C8E8E93C0299E98E046C765CA6ABB544412C1D9_gshared_inline (Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Queue`1/Enumerator<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mABD92CBE05B031E50E316375DDC8B2BDAD3F6F84_gshared (Enumerator_t30E3290EE12437374037B3CF0EE4D614F96D030A* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Int32>::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98_gshared (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___capacity0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.HashSet`1<System.Int32>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF_gshared (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.HashSet`1<System.Int32>::Contains(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1_gshared (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* __this, int32_t ___item0, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.HashSet`1<System.Int32>::Add(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB_gshared (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* __this, int32_t ___item0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Int32>::Add(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___item0, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<System.Int32>::get_Count()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Object>::ContainsKey(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_ContainsKey_m703047C213F7AB55C9DC346596287773A1F670CD_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.Dictionary`2<System.Object,System.Object>::get_Item(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Dictionary_2_get_Item_m4AAAECBE902A211BF2126E6AFA280AEF73A3E0D6_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::set_Item(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_set_Item_m1A840355E8EDAECEA9D0C6F5E51B248FAA449CBD_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Object>::Remove(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_Remove_m5C7C45E75D951A75843F3F7AADD56ECD64F6BC86_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, const RuntimeMethod* method) ;
// T UnityEngine.Component::GetComponent<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Component_GetComponent_TisRuntimeObject_m7181F81CAEC2CF53F5D2BC79B7425C16E1F80D33_gshared (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.ValueTuple`2<System.Object,System.Object>>::ContainsKey(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_ContainsKey_m97373D9B9C73B02C726AC509E99CD3B4D44B6037_gshared (Dictionary_2_t6F1450BD58C4E5A563CB6647A120640FF1708A98* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;
// System.Void System.ValueTuple`2<System.Object,System.Object>::.ctor(T1,T2)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ValueTuple_2__ctor_m4D25F4A0A0085EBE6559B6CC932AA5E267DB554D_gshared (ValueTuple_2_tC3717D4552EE1E5FC27BFBA3F5155741BC04557A* __this, RuntimeObject* ___item10, RuntimeObject* ___item21, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.ValueTuple`2<System.Object,System.Object>>::Add(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_mCF5E047BDC67DF58A3BB3218E820CF9657554131_gshared (Dictionary_2_t6F1450BD58C4E5A563CB6647A120640FF1708A98* __this, RuntimeObject* ___key0, ValueTuple_2_tC3717D4552EE1E5FC27BFBA3F5155741BC04557A ___value1, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.Dictionary`2<System.Object,System.ValueTuple`2<System.Object,System.Object>>::get_Item(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ValueTuple_2_tC3717D4552EE1E5FC27BFBA3F5155741BC04557A Dictionary_2_get_Item_m7CF609C9AAD0B8E317DBD40BC0E67F3AEE7C10C2_gshared (Dictionary_2_t6F1450BD58C4E5A563CB6647A120640FF1708A98* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.ValueTuple`2<System.Object,System.Object>>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_mA5C8ED6FC12E0E2B62A4D3FDDB0336BEC10D192C_gshared (Dictionary_2_t6F1450BD58C4E5A563CB6647A120640FF1708A98* __this, const RuntimeMethod* method) ;

// UnityEngine.GameObject UnityEngine.GameObject::FindWithTag(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* GameObject_FindWithTag_m8E5D34F652B0A6ECA1A90688726C22E272236C0F (String_t* ___tag0, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::GetComponent<UnityEngine.Canvas>()
inline Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* GameObject_GetComponent_TisCanvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26_mE5A2711FA84F57F5EA0876DB106B1A146956CEFE (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_GetComponent_TisRuntimeObject_m6EAED4AA356F0F48288F67899E5958792395563B_gshared)(__this, method);
}
// T UnityEngine.GameObject::GetComponent<UnityEngine.Camera>()
inline Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* GameObject_GetComponent_TisCamera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_m3B3C11550E48AA36AFF82788636EB163CC51FEE6 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_GetComponent_TisRuntimeObject_m6EAED4AA356F0F48288F67899E5958792395563B_gshared)(__this, method);
}
// UnityEngine.Transform UnityEngine.Component::get_transform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// T UnityEngine.Object::Instantiate<UnityEngine.GameObject>(T,UnityEngine.Transform,System.Boolean)
inline GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___original0, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___parent1, bool ___worldPositionStays2, const RuntimeMethod* method)
{
	return ((  GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1*, bool, const RuntimeMethod*))Object_Instantiate_TisRuntimeObject_m8784E2833D9F115FD2B2BED6615426E8CD75EE9B_gshared)(___original0, ___parent1, ___worldPositionStays2, method);
}
// System.String System.String::Format(System.String,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30 (String_t* ___format0, RuntimeObject* ___arg01, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::set_name(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_set_name_mC79E6DC8FFD72479C90F0C4CC7F42A0FEAF5AE47 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, String_t* ___value0, const RuntimeMethod* method) ;
// UnityEngine.Transform UnityEngine.GameObject::get_transform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::SetAsLastSibling()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_SetAsLastSibling_m848AF1A0B4C7912FE88D8CBCF92B83D57B2B917E (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// UnityEngine.Touch[] UnityEngine.Input::get_touches()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TouchU5BU5D_t242545870BFCA81F368CCF82E00F9E2A7FB523B3* Input_get_touches_m884B92DD9A389F7985AB275A9717AC629C258B6B (const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Touch::get_position()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A (Touch_t03E51455ED508492B3F278903A0114FA0E87B417* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.RectTransformUtility::ScreenPointToWorldPointInRectangle(UnityEngine.RectTransform,UnityEngine.Vector2,UnityEngine.Camera,UnityEngine.Vector3&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool RectTransformUtility_ScreenPointToWorldPointInRectangle_mA37289182AEA7D89BA927C325F82980085D6A882 (RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___rect0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___screenPoint1, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___cam2, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* ___worldPoint3, const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::set_position(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___value0, const RuntimeMethod* method) ;
// System.Void UnityEngine.GameObject::SetActive(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, bool ___value0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Input::get_touchCount()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF (const RuntimeMethod* method) ;
// System.Int32 System.Math::Min(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Math_Min_m1F346FEDDC77AC1EC0C4EF1AC6BA59F0EC7980F8 (int32_t ___val10, int32_t ___val21, const RuntimeMethod* method) ;
// System.Void UnityEngine.MonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Combine(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m8B9D24CED35033C7FC56501DFE650F5CB7FF012C (Delegate_t* ___a0, Delegate_t* ___b1, const RuntimeMethod* method) ;
// System.Delegate System.Delegate::Remove(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m40506877934EC1AD4ADAE57F5E97AF0BC0F96116 (Delegate_t* ___source0, Delegate_t* ___value1, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Equality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___x0, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___y1, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Inequality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___x0, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___y1, const RuntimeMethod* method) ;
// System.Void System.ApplicationException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ApplicationException__ctor_mE51100DFCDB0A0DF23B482CC43EC8E396BE7BE82 (ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.Events/EventsDelegate::Invoke(System.String,System.Object[])
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_inline (EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method) ;
// System.Tuple`2<T1,T2> System.Tuple::Create<System.String,System.Object[]>(T1,T2)
inline Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* Tuple_Create_TisString_t_TisObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_mFFF9A96F99C9F68C88C6B4FBDA62419C4E5307DB (String_t* ___item10, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___item21, const RuntimeMethod* method)
{
	return ((  Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* (*) (String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))Tuple_Create_TisRuntimeObject_TisRuntimeObject_m1185C3D02620CC5F26786D9BE962850A28346DD2_gshared)(___item10, ___item21, method);
}
// System.Void System.Collections.Generic.Queue`1<System.Tuple`2<System.String,System.Object[]>>::Enqueue(T)
inline void Queue_1_Enqueue_mB0520351271639D3269DC87FCD5AF5ECE4094CAD (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* __this, Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* ___item0, const RuntimeMethod* method)
{
	((  void (*) (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C*, Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16*, const RuntimeMethod*))Queue_1_Enqueue_m5CB8CF3906F1289F92036F0973EC5BE3450402EF_gshared)(__this, ___item0, method);
}
// System.Collections.IEnumerator NoSuchStudio.Common.Events::RaiseEventInternal()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Events_RaiseEventInternal_m6AB974BD7A0E607D83EA02682002B80076545B4A (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, const RuntimeMethod* method) ;
// UnityEngine.Coroutine UnityEngine.MonoBehaviour::StartCoroutine(System.Collections.IEnumerator)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* MonoBehaviour_StartCoroutine_m4CAFF732AA28CD3BDC5363B44A863575530EC812 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, RuntimeObject* ___routine0, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CRaiseEventInternalU3Ed__10__ctor_m0C7829792D3BB2E778EC877349D6E921FF5E28EF (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, int32_t ___U3CU3E1__state0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Queue`1<System.Tuple`2<System.String,System.Object[]>>::.ctor()
inline void Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0 (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* __this, const RuntimeMethod* method)
{
	((  void (*) (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C*, const RuntimeMethod*))Queue_1__ctor_m6E2A5A8173E0CC524496D5155C737DF8FD10D0EB_gshared)(__this, method);
}
// System.Collections.Generic.Queue`1/Enumerator<T> System.Collections.Generic.Queue`1<System.Tuple`2<System.String,System.Object[]>>::GetEnumerator()
inline Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626 Queue_1_GetEnumerator_m4F32C724CB1AA873049A953CC218B23FD86370AE (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626 (*) (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C*, const RuntimeMethod*))Queue_1_GetEnumerator_mBF0033C4BCEA408644D24F0B28A81F9145FB97C9_gshared)(__this, method);
}
// System.Void System.Collections.Generic.Queue`1/Enumerator<System.Tuple`2<System.String,System.Object[]>>::Dispose()
inline void Enumerator_Dispose_m40384472A2440993E6407EAFAC42C8E5F9E2A679 (Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626*, const RuntimeMethod*))Enumerator_Dispose_m680926A5EFC7099ECBCE9DEF68F8DED03C103955_gshared)(__this, method);
}
// T System.Collections.Generic.Queue`1/Enumerator<System.Tuple`2<System.String,System.Object[]>>::get_Current()
inline Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* Enumerator_get_Current_m11048A0F71FAE52952E39C32D7C45300444AD80D (Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626* __this, const RuntimeMethod* method)
{
	return ((  Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* (*) (Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626*, const RuntimeMethod*))Enumerator_get_Current_m5F2338F4C35E898DB7231D7E30F30155498FA9D7_gshared)(__this, method);
}
// T1 System.Tuple`2<System.String,System.Object[]>::get_Item1()
inline String_t* Tuple_2_get_Item1_mFE4E6BB2EBDAFBED6CCFAD58B1EF4D1CE5236BA1_inline (Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* __this, const RuntimeMethod* method)
{
	return ((  String_t* (*) (Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16*, const RuntimeMethod*))Tuple_2_get_Item1_mBF34A596062BBB3C1DD2A6CA36810366F445C9FA_gshared_inline)(__this, method);
}
// T2 System.Tuple`2<System.String,System.Object[]>::get_Item2()
inline ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Tuple_2_get_Item2_m6EA5B1A59F9501053DAF2ECA63725144E35854B4_inline (Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* __this, const RuntimeMethod* method)
{
	return ((  ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* (*) (Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16*, const RuntimeMethod*))Tuple_2_get_Item2_m4C8E8E93C0299E98E046C765CA6ABB544412C1D9_gshared_inline)(__this, method);
}
// System.Boolean System.Collections.Generic.Queue`1/Enumerator<System.Tuple`2<System.String,System.Object[]>>::MoveNext()
inline bool Enumerator_MoveNext_m58912CEC7A4655D207EE2E2ACD74ED8AD6F65425 (Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626*, const RuntimeMethod*))Enumerator_MoveNext_mABD92CBE05B031E50E316375DDC8B2BDAD3F6F84_gshared)(__this, method);
}
// System.Void System.NotSupportedException::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotSupportedException__ctor_m1398D0CDE19B36AA3DE9392879738C1EA2439CDF (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* __this, const RuntimeMethod* method) ;
// System.Exception System.Exception::get_InnerException()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Exception_t* Exception_get_InnerException_m0C1BDB339C786BA4DA7D2C1AD214571CFBBB1410_inline (Exception_t* __this, const RuntimeMethod* method) ;
// System.Exception NoSuchStudio.Common.ExceptionExts::RootCause(System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* ExceptionExts_RootCause_mD13EE637045A6A3188B7622DB5A609452001CE4E (Exception_t* ___e0, const RuntimeMethod* method) ;
// System.Void System.Exception::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F (Exception_t* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Application::get_isEditor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Application_get_isEditor_m0377DB707B566C8E21DA3CD99963210F6D57D234 (const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Application::get_isPlaying()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Application_get_isPlaying_m0B3B501E1093739F8887A0DAC5F61D9CB49CC337 (const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Screen::get_width()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C (const RuntimeMethod* method) ;
// System.Single UnityEngine.Screen::get_dpi()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Screen_get_dpi_mD5BB95E605FABD335F0D4736EE4860A0AA98A50D (const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Screen::get_height()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8 (const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m9499958F4B0BB6089C75760AB647AB3CA4D55806 (String_t* ___format0, RuntimeObject* ___arg01, RuntimeObject* ___arg12, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.IllegalStateException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IllegalStateException__ctor_m963B18BC7568D6C372F253B7AF07C1983A40AF36 (IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC* __this, String_t* ___msg0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Int32>::.ctor(System.Int32)
inline void List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98 (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___capacity0, const RuntimeMethod* method)
{
	((  void (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, int32_t, const RuntimeMethod*))List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98_gshared)(__this, ___capacity0, method);
}
// System.Void System.Collections.Generic.HashSet`1<System.Int32>::.ctor()
inline void HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* __this, const RuntimeMethod* method)
{
	((  void (*) (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2*, const RuntimeMethod*))HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF_gshared)(__this, method);
}
// System.Int32 UnityEngine.Random::Range(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Random_Range_mD4D2DEE3D2E75D07740C9A6F93B3088B03BBB8F8 (int32_t ___minInclusive0, int32_t ___maxExclusive1, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.HashSet`1<System.Int32>::Contains(T)
inline bool HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1 (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* __this, int32_t ___item0, const RuntimeMethod* method)
{
	return ((  bool (*) (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2*, int32_t, const RuntimeMethod*))HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1_gshared)(__this, ___item0, method);
}
// System.Boolean System.Collections.Generic.HashSet`1<System.Int32>::Add(T)
inline bool HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* __this, int32_t ___item0, const RuntimeMethod* method)
{
	return ((  bool (*) (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2*, int32_t, const RuntimeMethod*))HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB_gshared)(__this, ___item0, method);
}
// System.Void System.Collections.Generic.List`1<System.Int32>::Add(T)
inline void List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___item0, const RuntimeMethod* method)
{
	((  void (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, int32_t, const RuntimeMethod*))List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_gshared_inline)(__this, ___item0, method);
}
// System.Int32 System.Collections.Generic.List`1<System.Int32>::get_Count()
inline int32_t List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, const RuntimeMethod*))List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_gshared_inline)(__this, method);
}
// System.Void UnityEngine.Color::.ctor(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Color__ctor_mCD6889CDE39F18704CD6EA8E2EFBFA48BA3E13B0_inline (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* __this, float ___r0, float ___g1, float ___b2, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Clamp01(System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline (float ___value0, const RuntimeMethod* method) ;
// UnityEngine.Color NoSuchStudio.Common.HSVColor::hue2rgb(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color_tD001788D726C3A7F1379BEED0260B9591F440C1F HSVColor_hue2rgb_mDA453C7AC96A68A81982754088DE85FFD331E16B (float ___hue0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Vector3::.ctor(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, float ___x0, float ___y1, float ___z2, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_one()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_one_mE6A2D5C6578E94268024613B596BF09F990B1260_inline (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Lerp(UnityEngine.Vector3,UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, float ___t2, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Multiply(UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, float ___d1, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Max(System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline (float ___a0, float ___b1, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Min(System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Min_m4F2A9C5128DC3F9E84865EE7ADA8DB5DA6B8B507_inline (float ___a0, float ___b1, const RuntimeMethod* method) ;
// UnityEngine.Touch UnityEngine.Input::GetTouch(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Touch_t03E51455ED508492B3F278903A0114FA0E87B417 Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4 (int32_t ___index0, const RuntimeMethod* method) ;
// UnityEngine.Camera UnityEngine.Camera::get_main()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* Camera_get_main_mF222B707D3BF8CC9C7544609EFC71CFB62E81D43 (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector2::op_Implicit(UnityEngine.Vector2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___v0, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_forward()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Addition(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Camera::ScreenToWorldPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Camera_ScreenToWorldPoint_m5EA3148F070985EC72127AAC3448D8D6ABE6E7E5 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position0, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineU3Ed__0__ctor_m3CC301E300B7A507D5871F78E203E5CDCF77B2AD (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, int32_t ___U3CU3E1__state0, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineRealtimeU3Ed__1__ctor_mAF980EDFAD6792A2C1DA0C725D7EBDF8B3303E3C (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, int32_t ___U3CU3E1__state0, const RuntimeMethod* method) ;
// System.Collections.IEnumerator NoSuchStudio.Common.MonoBehaviourRunDelayedExt::DelayedCoroutine(System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MonoBehaviourRunDelayedExt_DelayedCoroutine_mA82873EEAA344F29C1AA70758E4281F753284470 (float ___delay0, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a1, const RuntimeMethod* method) ;
// System.Collections.IEnumerator NoSuchStudio.Common.MonoBehaviourRunDelayedExt::DelayedCoroutineRealtime(System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MonoBehaviourRunDelayedExt_DelayedCoroutineRealtime_m9044905016AFFC0207E60DB83DF282AEADF67B45 (float ___delay0, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a1, const RuntimeMethod* method) ;
// System.Void UnityEngine.WaitForSeconds::.ctor(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WaitForSeconds__ctor_m579F95BADEDBAB4B3A7E302C6EE3995926EF2EFC (WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3* __this, float ___seconds0, const RuntimeMethod* method) ;
// System.Void System.Action::Invoke()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.WaitForSecondsRealtime::.ctor(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WaitForSecondsRealtime__ctor_mBFC1E4F0E042D5EC6E7EEB211A2FE5193A8F6D6F (WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01* __this, float ___time0, const RuntimeMethod* method) ;
// System.Type System.Object::GetType()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig> NoSuchStudio.Common.UnityObjectLoggerExt::GetLoggerByType(System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53 (Type_t* ___type0, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogLogFormat(UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogLogFormat_mEF4688871A7D53518B12307F907E452E5D934513 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___format1, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args2, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogLog(UnityEngine.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogLog_mC174F3944DBBF72B5667393163D3CBBFF440AB30 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___msg1, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogWarnFormat(UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogWarnFormat_m18CFBC606E7A4660BCFC38C759271265CA589FB2 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___format1, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args2, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogWarn(UnityEngine.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogWarn_mB1F6307AF886FDE0D443B5AFFF6E674EDBE41EDA (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___msg1, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogErrorFormat(UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogErrorFormat_m40A9D1D33A5FE6D11D78DE280141F87EF9221D81 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___format1, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args2, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogError(UnityEngine.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogError_m364179587BD3CA7C881454C95564305B5A91F612 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___msg1, const RuntimeMethod* method) ;
// UnityEngine.Coroutine NoSuchStudio.Common.MonoBehaviourRunDelayedExt::RunDelayed(UnityEngine.MonoBehaviour,System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* MonoBehaviourRunDelayedExt_RunDelayed_mA8AC65BCCF871A4C82EB2A0A636609F805BB7640 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ___mono0, float ___delay1, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a2, const RuntimeMethod* method) ;
// UnityEngine.Coroutine NoSuchStudio.Common.MonoBehaviourRunDelayedExt::RunDelayedRealtime(UnityEngine.MonoBehaviour,System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* MonoBehaviourRunDelayedExt_RunDelayedRealtime_m48EAEC5B712A6828E57E5377E0576487E88E5A46 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ___mono0, float ___delay1, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a2, const RuntimeMethod* method) ;
// System.Void UnityEngine.ScriptableObject::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF (ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A* __this, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.Scope::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scope__ctor_mAD0B7846C6034EEA565200DD0535F3C0DDAA1C9F (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* __this, String_t* ___scope0, String_t* ___delimiter1, const RuntimeMethod* method) ;
// System.Boolean System.String::StartsWith(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_StartsWith_mF75DBA1EB709811E711B44E26FF919C88A8E65C0 (String_t* __this, String_t* ___value0, const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object,System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m76BF8F3A6AD789E38B708848A2688D400AAC250A (String_t* ___format0, RuntimeObject* ___arg01, RuntimeObject* ___arg12, RuntimeObject* ___arg23, const RuntimeMethod* method) ;
// System.Boolean NoSuchStudio.Common.Scope::Match(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Scope_Match_m83A4301662FB9AD9A7F5D411E5B170211894D67D (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* __this, String_t* ___fullName0, const RuntimeMethod* method) ;
// System.Int32 System.String::get_Length()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
// System.String System.String::Substring(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Substring_m6BA4A3FA3800FE92662D0847CC8E1EEF940DF472 (String_t* __this, int32_t ___startIndex0, const RuntimeMethod* method) ;
// NoSuchStudio.Common.Scope NoSuchStudio.Common.Scope::Create(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* Scope_Create_mA60654609FAF23A2BF31DE2DAABAC41396B990DD (String_t* ___scope0, String_t* ___delimiter1, const RuntimeMethod* method) ;
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A (String_t* ___value0, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>::ContainsKey(TKey)
inline bool Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* __this, String_t* ___key0, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD*, String_t*, const RuntimeMethod*))Dictionary_2_ContainsKey_m703047C213F7AB55C9DC346596287773A1F670CD_gshared)(__this, ___key0, method);
}
// TValue System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>::get_Item(TKey)
inline Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* __this, String_t* ___key0, const RuntimeMethod* method)
{
	return ((  Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* (*) (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD*, String_t*, const RuntimeMethod*))Dictionary_2_get_Item_m4AAAECBE902A211BF2126E6AFA280AEF73A3E0D6_gshared)(__this, ___key0, method);
}
// UnityEngine.GameObject UnityEngine.Component::get_gameObject()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Object::get_name()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::Destroy(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_Destroy_mFCDAE6333522488F60597AF019EA90BB1207A5AA (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___obj0, const RuntimeMethod* method) ;
// System.Void UnityEngine.GameObject::set_tag(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject_set_tag_m0A41528AFD8C83E1CEC5D769921159897CDD2B24 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, String_t* ___value0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>::set_Item(TKey,TValue)
inline void Dictionary_2_set_Item_m45E21CB14A73F58BD606054CB89E38965210E75E (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* __this, String_t* ___key0, Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* ___value1, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD*, String_t*, Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75*, const RuntimeMethod*))Dictionary_2_set_Item_m1A840355E8EDAECEA9D0C6F5E51B248FAA449CBD_gshared)(__this, ___key0, ___value1, method);
}
// System.Void UnityEngine.Object::DontDestroyOnLoad(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_DontDestroyOnLoad_m303AA1C4DC810349F285B4809E426CBBA8F834F9 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___target0, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>::Remove(TKey)
inline bool Dictionary_2_Remove_m13CE1B03E096BE40FECC8C7546831E80CD1A8D59 (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* __this, String_t* ___key0, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD*, String_t*, const RuntimeMethod*))Dictionary_2_Remove_m5C7C45E75D951A75843F3F7AADD56ECD64F6BC86_gshared)(__this, ___key0, method);
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour__ctor_m58F2B53BD2C05B59A51818C9B3656C60AE0C55EE (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.String,NoSuchStudio.Common.Singleton>::.ctor()
inline void Dictionary_2__ctor_m7F749610DCC2068FFABD81A4FAC6522D6C334632 (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD*, const RuntimeMethod*))Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared)(__this, method);
}
// T UnityEngine.Component::GetComponent<NoSuchStudio.Common.Singleton>()
inline Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* Component_GetComponent_TisSingleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_m67CFFC259C315C7D32F39708EC5DE1D6B89FCBE2 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method)
{
	return ((  Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* (*) (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3*, const RuntimeMethod*))Component_GetComponent_TisRuntimeObject_m7181F81CAEC2CF53F5D2BC79B7425C16E1F80D33_gshared)(__this, method);
}
// System.Boolean NoSuchStudio.Common.Singleton::get_IsChosenSingleton()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Singleton_get_IsChosenSingleton_m604CEE054136DBF9D9BE920721E993FAC118730F (Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* __this, const RuntimeMethod* method) ;
// UnityEngine.Transform UnityEngine.Transform::GetChild(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* Transform_GetChild_mE686DF0C7AAC1F7AEF356967B1C04D8B8E240EAF (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Transform::get_childCount()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Transform_get_childCount_mE9C29C702AB662CC540CA053EDE48BDAFA35B4B0 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::DestroyImmediate(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_DestroyImmediate_m8249CABCDF344BE3A67EE765122EBB415DC2BC57 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___obj0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Canvas::ForceUpdateCanvases()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Canvas_ForceUpdateCanvases_m29B1B008CA6C4A3CF623A0A86ACE5C8AA4C2B0C1 (const RuntimeMethod* method) ;
// UnityEngine.RectTransform UnityEngine.UI.ScrollRect::get_viewport()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ScrollRect_get_viewport_m85092216DD476F77E78F5CE50F9C4E70063ECCF9_inline (ScrollRect_t17D2F2939CA8953110180DF53164CFC3DC88D70E* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_localPosition()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_localPosition_mA9C86B990DF0685EA1061A120218993FDCC60A95 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Vector2::op_Implicit(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___v0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Vector2::.ctor(System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, float ___x0, float ___y1, const RuntimeMethod* method) ;
// System.Threading.Thread System.Threading.Thread::get_CurrentThread()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* Thread_get_CurrentThread_m835AD1DF1C0D10BABE1A5427CC4B357C991B25AB (const RuntimeMethod* method) ;
// System.Int32 System.Threading.Thread::get_ManagedThreadId()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Thread_get_ManagedThreadId_m74ACB74A574EE535C2B00B7D64F203A62E796B05 (Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* __this, const RuntimeMethod* method) ;
// System.String System.Int32::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5 (int32_t* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0 (String_t* ___str00, String_t* ___str11, String_t* ___str22, const RuntimeMethod* method) ;
// System.Single UnityEngine.Time::get_time()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Time_get_time_m0BEE9AACD0723FE414465B77C9C64D12263675F3 (const RuntimeMethod* method) ;
// System.String System.Single::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972 (float* __this, const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55 (String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) ;
// System.Void UnityEngine.Logger::LogFormat(UnityEngine.LogType,UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Logger_LogFormat_m776A546E755F914039AB8591E23D08510308DB4C (Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* __this, int32_t ___logType0, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___context1, String_t* ___format2, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args3, const RuntimeMethod* method) ;
// System.Void UnityEngine.Logger::Log(UnityEngine.LogType,System.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Logger_Log_mF8C7E8A8CC31E04732044D73D2CB551D7CCB8995 (Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* __this, int32_t ___logType0, RuntimeObject* ___message1, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___context2, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogFormat(UnityEngine.Object,UnityEngine.LogType,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogFormat_m06BD26D581CBA64E3422A043A782DC663BDB12D3 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, int32_t ___logType1, String_t* ___format2, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args3, const RuntimeMethod* method) ;
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::Log(UnityEngine.Object,UnityEngine.LogType,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_Log_m82862BA4CFCAB632BB2147B63E68274C378C8A31 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, int32_t ___logType1, String_t* ___msg2, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>::ContainsKey(TKey)
inline bool Dictionary_2_ContainsKey_m700A5670F3CB7E83C52F2590D17EF521324F2430 (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* __this, Type_t* ___key0, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07*, Type_t*, const RuntimeMethod*))Dictionary_2_ContainsKey_m97373D9B9C73B02C726AC509E99CD3B4D44B6037_gshared)(__this, ___key0, method);
}
// System.Void NoSuchStudio.Common.LoggerConfig::.ctor(System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LoggerConfig__ctor_m53267D4702C573947E2EA33FBF821B9C8547E303 (LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* __this, String_t* ___className0, bool ___logClassName1, bool ___logGameObjectName2, bool ___logThreadId3, bool ___logGameTime4, const RuntimeMethod* method) ;
// UnityEngine.ILogger UnityEngine.Debug::get_unityLogger()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Debug_get_unityLogger_mA872400E9E585FCD6A2DE1717748A458545DE8A4_inline (const RuntimeMethod* method) ;
// System.Void UnityEngine.Logger::.ctor(UnityEngine.ILogHandler)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Logger__ctor_m3155E21A68AA616431A260A3FCBB4B074DF6FAA2 (Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* __this, RuntimeObject* ___logHandler0, const RuntimeMethod* method) ;
// System.Void System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>::.ctor(T1,T2)
inline void ValueTuple_2__ctor_m704CDA27B90CDBBAE2DC59E142CCEA85ABCEAD3B (ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F* __this, Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* ___item10, LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* ___item21, const RuntimeMethod* method)
{
	((  void (*) (ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F*, Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0*, LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6*, const RuntimeMethod*))ValueTuple_2__ctor_m4D25F4A0A0085EBE6559B6CC932AA5E267DB554D_gshared)(__this, ___item10, ___item21, method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>::Add(TKey,TValue)
inline void Dictionary_2_Add_m6917FFC8B47B29FC2E7A65BA0C61EAF0C8ABF3F1 (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* __this, Type_t* ___key0, ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F ___value1, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07*, Type_t*, ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F, const RuntimeMethod*))Dictionary_2_Add_mCF5E047BDC67DF58A3BB3218E820CF9657554131_gshared)(__this, ___key0, ___value1, method);
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::AddType(System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_AddType_mD9ABC1EB73654B6A28262B40802BF5C857A34E92 (Type_t* ___type0, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>::get_Item(TKey)
inline ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F Dictionary_2_get_Item_mD046F6B66CAC9023A3AC965DD99BAE431D3F31D4 (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* __this, Type_t* ___key0, const RuntimeMethod* method)
{
	return ((  ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F (*) (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07*, Type_t*, const RuntimeMethod*))Dictionary_2_get_Item_m7CF609C9AAD0B8E317DBD40BC0E67F3AEE7C10C2_gshared)(__this, ___key0, method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.Type,System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig>>::.ctor()
inline void Dictionary_2__ctor_mC6AF8829C5C4C4865830344ACF22D1BDF29CF081 (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07*, const RuntimeMethod*))Dictionary_2__ctor_mA5C8ED6FC12E0E2B62A4D3FDDB0336BEC10D192C_gshared)(__this, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.CanvasTouchVisualizer::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CanvasTouchVisualizer_Start_m2F382C43343CEA5E22E7819AA30BBCED17F6DB20 (CanvasTouchVisualizer_t751199DB21BF94CBE211E384F7A7856545FAD1CA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_GetComponent_TisCamera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_m3B3C11550E48AA36AFF82788636EB163CC51FEE6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_GetComponent_TisCanvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26_mE5A2711FA84F57F5EA0876DB106B1A146956CEFE_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5FD20D8504182B91A7EE1908D7A191F36ABAEDF1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral63717794632FEDA33FCF6C202E592B6EA4DBC7F8);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE302AA9BECF9F1CB69CF2A3E5B33E0716BEA97F6);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// mainCanvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0;
		L_0 = GameObject_FindWithTag_m8E5D34F652B0A6ECA1A90688726C22E272236C0F(_stringLiteral63717794632FEDA33FCF6C202E592B6EA4DBC7F8, NULL);
		NullCheck(L_0);
		Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* L_1;
		L_1 = GameObject_GetComponent_TisCanvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26_mE5A2711FA84F57F5EA0876DB106B1A146956CEFE(L_0, GameObject_GetComponent_TisCanvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26_mE5A2711FA84F57F5EA0876DB106B1A146956CEFE_RuntimeMethod_var);
		__this->___mainCanvas_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mainCanvas_4), (void*)L_1);
		// mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_2;
		L_2 = GameObject_FindWithTag_m8E5D34F652B0A6ECA1A90688726C22E272236C0F(_stringLiteralE302AA9BECF9F1CB69CF2A3E5B33E0716BEA97F6, NULL);
		NullCheck(L_2);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_3;
		L_3 = GameObject_GetComponent_TisCamera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_m3B3C11550E48AA36AFF82788636EB163CC51FEE6(L_2, GameObject_GetComponent_TisCamera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184_m3B3C11550E48AA36AFF82788636EB163CC51FEE6_RuntimeMethod_var);
		__this->___mainCamera_5 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mainCamera_5), (void*)L_3);
		// touchVisuals = new GameObject[maxTouchCount];
		int32_t L_4 = __this->___maxTouchCount_8;
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_5 = (GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF*)(GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF*)SZArrayNew(GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF_il2cpp_TypeInfo_var, (uint32_t)L_4);
		__this->___touchVisuals_7 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___touchVisuals_7), (void*)L_5);
		// for (int i = 0; i < maxTouchCount; i++) {
		V_0 = 0;
		goto IL_0091;
	}

IL_003f:
	{
		// touchVisuals[i] = Instantiate(prefab, mainCanvas.transform, false);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_6 = __this->___touchVisuals_7;
		int32_t L_7 = V_0;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_8 = __this->___prefab_6;
		Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* L_9 = __this->___mainCanvas_4;
		NullCheck(L_9);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10;
		L_10 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_9, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_11;
		L_11 = Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5(L_8, L_10, (bool)0, Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5_RuntimeMethod_var);
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, L_11);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*)L_11);
		// touchVisuals[i].name = string.Format("TouchVisualizer{0}", i);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_12 = __this->___touchVisuals_7;
		int32_t L_13 = V_0;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		int32_t L_16 = V_0;
		int32_t L_17 = L_16;
		RuntimeObject* L_18 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_17);
		String_t* L_19;
		L_19 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(_stringLiteral5FD20D8504182B91A7EE1908D7A191F36ABAEDF1, L_18, NULL);
		NullCheck(L_15);
		Object_set_name_mC79E6DC8FFD72479C90F0C4CC7F42A0FEAF5AE47(L_15, L_19, NULL);
		// touchVisuals[i].transform.SetAsLastSibling();
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_20 = __this->___touchVisuals_7;
		int32_t L_21 = V_0;
		NullCheck(L_20);
		int32_t L_22 = L_21;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_23 = (L_20)->GetAt(static_cast<il2cpp_array_size_t>(L_22));
		NullCheck(L_23);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_24;
		L_24 = GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56(L_23, NULL);
		NullCheck(L_24);
		Transform_SetAsLastSibling_m848AF1A0B4C7912FE88D8CBCF92B83D57B2B917E(L_24, NULL);
		// for (int i = 0; i < maxTouchCount; i++) {
		int32_t L_25 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_25, 1));
	}

IL_0091:
	{
		// for (int i = 0; i < maxTouchCount; i++) {
		int32_t L_26 = V_0;
		int32_t L_27 = __this->___maxTouchCount_8;
		if ((((int32_t)L_26) < ((int32_t)L_27)))
		{
			goto IL_003f;
		}
	}
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.CanvasTouchVisualizer::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CanvasTouchVisualizer_Update_mD9A60E4FD54543A79AE0028F38564D999ECEFAE4 (CanvasTouchVisualizer_t751199DB21BF94CBE211E384F7A7856545FAD1CA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RectTransformUtility_t65C00A84A72F17D78B81F2E7D88C2AA98AB61244_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	{
		// for (int i = 0; i < Math.Min(Input.touchCount, maxTouchCount); i++) {
		V_0 = 0;
		goto IL_005f;
	}

IL_0004:
	{
		// Vector3 globalPos = new Vector3();
		il2cpp_codegen_initobj((&V_1), sizeof(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2));
		// RectTransformUtility.ScreenPointToWorldPointInRectangle(mainCanvas.transform as RectTransform, Input.touches[i].position, mainCamera, out globalPos);
		Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* L_0 = __this->___mainCanvas_4;
		NullCheck(L_0);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1;
		L_1 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_0, NULL);
		TouchU5BU5D_t242545870BFCA81F368CCF82E00F9E2A7FB523B3* L_2;
		L_2 = Input_get_touches_m884B92DD9A389F7985AB275A9717AC629C258B6B(NULL);
		int32_t L_3 = V_0;
		NullCheck(L_2);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4;
		L_4 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(((L_2)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_3))), NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = __this->___mainCamera_5;
		il2cpp_codegen_runtime_class_init_inline(RectTransformUtility_t65C00A84A72F17D78B81F2E7D88C2AA98AB61244_il2cpp_TypeInfo_var);
		bool L_6;
		L_6 = RectTransformUtility_ScreenPointToWorldPointInRectangle_mA37289182AEA7D89BA927C325F82980085D6A882(((RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5*)IsInstSealed((RuntimeObject*)L_1, RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5_il2cpp_TypeInfo_var)), L_4, L_5, (&V_1), NULL);
		// touchVisuals[i].transform.position = globalPos;
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_7 = __this->___touchVisuals_7;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		NullCheck(L_10);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11;
		L_11 = GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56(L_10, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = V_1;
		NullCheck(L_11);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_11, L_12, NULL);
		// touchVisuals[i].SetActive(true);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_13 = __this->___touchVisuals_7;
		int32_t L_14 = V_0;
		NullCheck(L_13);
		int32_t L_15 = L_14;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_16 = (L_13)->GetAt(static_cast<il2cpp_array_size_t>(L_15));
		NullCheck(L_16);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_16, (bool)1, NULL);
		// for (int i = 0; i < Math.Min(Input.touchCount, maxTouchCount); i++) {
		int32_t L_17 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_005f:
	{
		// for (int i = 0; i < Math.Min(Input.touchCount, maxTouchCount); i++) {
		int32_t L_18 = V_0;
		int32_t L_19;
		L_19 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		int32_t L_20 = __this->___maxTouchCount_8;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		int32_t L_21;
		L_21 = Math_Min_m1F346FEDDC77AC1EC0C4EF1AC6BA59F0EC7980F8(L_19, L_20, NULL);
		if ((((int32_t)L_18) < ((int32_t)L_21)))
		{
			goto IL_0004;
		}
	}
	{
		// for (int i = Input.touchCount; i < maxTouchCount; i++) {
		int32_t L_22;
		L_22 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		V_2 = L_22;
		goto IL_008c;
	}

IL_007a:
	{
		// touchVisuals[i].SetActive(false);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_23 = __this->___touchVisuals_7;
		int32_t L_24 = V_2;
		NullCheck(L_23);
		int32_t L_25 = L_24;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_26 = (L_23)->GetAt(static_cast<il2cpp_array_size_t>(L_25));
		NullCheck(L_26);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_26, (bool)0, NULL);
		// for (int i = Input.touchCount; i < maxTouchCount; i++) {
		int32_t L_27 = V_2;
		V_2 = ((int32_t)il2cpp_codegen_add(L_27, 1));
	}

IL_008c:
	{
		// for (int i = Input.touchCount; i < maxTouchCount; i++) {
		int32_t L_28 = V_2;
		int32_t L_29 = __this->___maxTouchCount_8;
		if ((((int32_t)L_28) < ((int32_t)L_29)))
		{
			goto IL_007a;
		}
	}
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.CanvasTouchVisualizer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CanvasTouchVisualizer__ctor_m3A1F2933DA9143D36529FAD5804974F2B1E0CEB9 (CanvasTouchVisualizer_t751199DB21BF94CBE211E384F7A7856545FAD1CA* __this, const RuntimeMethod* method) 
{
	{
		// int maxTouchCount = 5;
		__this->___maxTouchCount_8 = 5;
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Boolean NoSuchStudio.Common.EditorUtilities::IsInMainStage(UnityEngine.GameObject)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool EditorUtilities_IsInMainStage_m219A8AE6997134A613319E4E55EEEA9A22A133EA (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___go0, const RuntimeMethod* method) 
{
	{
		// return true;
		return (bool)1;
	}
}
// System.Void NoSuchStudio.Common.EditorUtilities::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EditorUtilities__ctor_m81B0D35D23C701FDA0DF450D9E5C921AF2365826 (EditorUtilities_tE8DE91375ED2E9E172623F15AA994A6C0CFE3F10* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.Events::add_gEvent(NoSuchStudio.Common.Events/EventsDelegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Events_add_gEvent_m5149D3293E4DCDA9696137CA16D42B0B309E1211 (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* V_0 = NULL;
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* V_1 = NULL;
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* V_2 = NULL;
	{
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_0 = __this->___gEvent_5;
		V_0 = L_0;
	}

IL_0007:
	{
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_1 = V_0;
		V_1 = L_1;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_2 = V_1;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_3 = ___value0;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m8B9D24CED35033C7FC56501DFE650F5CB7FF012C(L_2, L_3, NULL);
		V_2 = ((EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*)CastclassSealed((RuntimeObject*)L_4, EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416_il2cpp_TypeInfo_var));
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416** L_5 = (&__this->___gEvent_5);
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_6 = V_2;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_7 = V_1;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_8;
		L_8 = InterlockedCompareExchangeImpl<EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*>(L_5, L_6, L_7);
		V_0 = L_8;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_9 = V_0;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_10 = V_1;
		if ((!(((RuntimeObject*)(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*)L_9) == ((RuntimeObject*)(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// System.Void NoSuchStudio.Common.Events::remove_gEvent(NoSuchStudio.Common.Events/EventsDelegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Events_remove_gEvent_m4D4D008D976213A26914E95D6F54E431E7FD048B (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* V_0 = NULL;
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* V_1 = NULL;
	EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* V_2 = NULL;
	{
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_0 = __this->___gEvent_5;
		V_0 = L_0;
	}

IL_0007:
	{
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_1 = V_0;
		V_1 = L_1;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_2 = V_1;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_3 = ___value0;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m40506877934EC1AD4ADAE57F5E97AF0BC0F96116(L_2, L_3, NULL);
		V_2 = ((EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*)CastclassSealed((RuntimeObject*)L_4, EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416_il2cpp_TypeInfo_var));
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416** L_5 = (&__this->___gEvent_5);
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_6 = V_2;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_7 = V_1;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_8;
		L_8 = InterlockedCompareExchangeImpl<EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*>(L_5, L_6, L_7);
		V_0 = L_8;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_9 = V_0;
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_10 = V_1;
		if ((!(((RuntimeObject*)(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*)L_9) == ((RuntimeObject*)(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// System.Void NoSuchStudio.Common.Events::Awake()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Events_Awake_m712AF9922E5E4C4FB9E2E7917D43C42F7F0F5969 (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (gInstance == null) gInstance = this;
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_0 = ((Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_StaticFields*)il2cpp_codegen_static_fields_for(Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_il2cpp_TypeInfo_var))->___gInstance_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0013;
		}
	}
	{
		// if (gInstance == null) gInstance = this;
		((Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_StaticFields*)il2cpp_codegen_static_fields_for(Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_il2cpp_TypeInfo_var))->___gInstance_4 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&((Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_StaticFields*)il2cpp_codegen_static_fields_for(Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_il2cpp_TypeInfo_var))->___gInstance_4), (void*)__this);
	}

IL_0013:
	{
		// if (gInstance != this)
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_2 = ((Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_StaticFields*)il2cpp_codegen_static_fields_for(Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C_il2cpp_TypeInfo_var))->___gInstance_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_2, __this, NULL);
		if (!L_3)
		{
			goto IL_002b;
		}
	}
	{
		// throw new ApplicationException("Only one events object.");
		ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A* L_4 = (ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		ApplicationException__ctor_mE51100DFCDB0A0DF23B482CC43EC8E396BE7BE82(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral85BC4A18024062EE8394D71331785A0C1F66BFED)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Events_Awake_m712AF9922E5E4C4FB9E2E7917D43C42F7F0F5969_RuntimeMethod_var)));
	}

IL_002b:
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.Events::RaiseEventImmediate(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Events_RaiseEventImmediate_mCE1C779178F9B5C388EC6E7D2119AA8C0042FBBE (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method) 
{
	{
		// gEvent(eventName, eventParams);
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_0 = __this->___gEvent_5;
		String_t* L_1 = ___eventName0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___eventParams1;
		NullCheck(L_0);
		EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_inline(L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.Events::RaiseEvent(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Events_RaiseEvent_m802D91B2F91793C603816824BC1AC1DD747EB6F3 (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Queue_1_Enqueue_mB0520351271639D3269DC87FCD5AF5ECE4094CAD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Tuple_Create_TisString_t_TisObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_mFFF9A96F99C9F68C88C6B4FBDA62419C4E5307DB_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// _eventQueue.Enqueue(Tuple.Create(eventName, eventParams));
		Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* L_0 = __this->____eventQueue_7;
		String_t* L_1 = ___eventName0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___eventParams1;
		Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* L_3;
		L_3 = Tuple_Create_TisString_t_TisObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_mFFF9A96F99C9F68C88C6B4FBDA62419C4E5307DB(L_1, L_2, Tuple_Create_TisString_t_TisObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_mFFF9A96F99C9F68C88C6B4FBDA62419C4E5307DB_RuntimeMethod_var);
		NullCheck(L_0);
		Queue_1_Enqueue_mB0520351271639D3269DC87FCD5AF5ECE4094CAD(L_0, L_3, Queue_1_Enqueue_mB0520351271639D3269DC87FCD5AF5ECE4094CAD_RuntimeMethod_var);
		// if (!_alreadyRaised) {
		bool L_4 = __this->____alreadyRaised_6;
		if (L_4)
		{
			goto IL_0027;
		}
	}
	{
		// StartCoroutine(RaiseEventInternal());
		RuntimeObject* L_5;
		L_5 = Events_RaiseEventInternal_m6AB974BD7A0E607D83EA02682002B80076545B4A(__this, NULL);
		Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* L_6;
		L_6 = MonoBehaviour_StartCoroutine_m4CAFF732AA28CD3BDC5363B44A863575530EC812(__this, L_5, NULL);
	}

IL_0027:
	{
		// }
		return;
	}
}
// System.Collections.IEnumerator NoSuchStudio.Common.Events::RaiseEventInternal()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Events_RaiseEventInternal_m6AB974BD7A0E607D83EA02682002B80076545B4A (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* L_0 = (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B*)il2cpp_codegen_object_new(U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CRaiseEventInternalU3Ed__10__ctor_m0C7829792D3BB2E778EC877349D6E921FF5E28EF(L_0, 0, NULL);
		U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* L_1 = L_0;
		NullCheck(L_1);
		L_1->___U3CU3E4__this_2 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3E4__this_2), (void*)__this);
		return L_1;
	}
}
// System.Void NoSuchStudio.Common.Events::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Events__ctor_m555ABE0546018069B84C5A79EB8EBEAFE4C44E11 (Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private Queue<Tuple<string, object[]>> _eventQueue = new Queue<Tuple<string, object[]>>();
		Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* L_0 = (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C*)il2cpp_codegen_object_new(Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0(L_0, Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0_RuntimeMethod_var);
		__this->____eventQueue_7 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____eventQueue_7), (void*)L_0);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_Multicast(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* currentDelegate = reinterpret_cast<EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, ___eventName0, ___eventParams1, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
}
void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_Open(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___eventName0, ___eventParams1, method);
}
void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_OpenStaticInvoker(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method)
{
	InvokerActionInvoker2< String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* >::Invoke(__this->___method_ptr_0, method, NULL, ___eventName0, ___eventParams1);
}
void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_ClosedStaticInvoker(EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method)
{
	InvokerActionInvoker3< RuntimeObject*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* >::Invoke(__this->___method_ptr_0, method, NULL, __this->___m_target_2, ___eventName0, ___eventParams1);
}
// System.Void NoSuchStudio.Common.Events/EventsDelegate::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EventsDelegate__ctor_m1F9BA9EFEDF69DACAD14EAD77B4F39D5DFCE456C (EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___method1);
	__this->___method_3 = ___method1;
	__this->___m_target_2 = ___object0;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___object0);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___method1);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___method1))
	{
		bool isOpen = parameterCount == 2;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___method1))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_Open;
			else
				{
					__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			__this->___invoke_impl_1 = (intptr_t)&EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_Open;
		}
		else
		{
			__this->___invoke_impl_1 = (intptr_t)__this->___method_ptr_0;
			__this->___method_code_6 = (intptr_t)__this->___m_target_2;
		}
	}
	__this->___extra_arg_5 = (intptr_t)&EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_Multicast;
}
// System.Void NoSuchStudio.Common.Events/EventsDelegate::Invoke(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED (EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___eventName0, ___eventParams1, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
// System.IAsyncResult NoSuchStudio.Common.Events/EventsDelegate::BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* EventsDelegate_BeginInvoke_m6DC5115F4FBF32B6399E715550C415D61B208697 (EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C* ___callback2, RuntimeObject* ___object3, const RuntimeMethod* method) 
{
	void *__d_args[3] = {0};
	__d_args[0] = ___eventName0;
	__d_args[1] = ___eventParams1;
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___callback2, (RuntimeObject*)___object3);
}
// System.Void NoSuchStudio.Common.Events/EventsDelegate::EndInvoke(System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EventsDelegate_EndInvoke_mB676395B8E5F9DE86F587ABFDF479A157445D21A (EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, RuntimeObject* ___result0, const RuntimeMethod* method) 
{
	il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___result0, 0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CRaiseEventInternalU3Ed__10__ctor_m0C7829792D3BB2E778EC877349D6E921FF5E28EF (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, int32_t ___U3CU3E1__state0, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___U3CU3E1__state0;
		__this->___U3CU3E1__state_0 = L_0;
		return;
	}
}
// System.Void NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::System.IDisposable.Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CRaiseEventInternalU3Ed__10_System_IDisposable_Dispose_m7462E098D228651B31D13C39001DE9C3DD1594F2 (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// System.Boolean NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CRaiseEventInternalU3Ed__10_MoveNext_m38639D0856ED90B41ADB4546BE1EEDBFC9DF9464 (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m40384472A2440993E6407EAFAC42C8E5F9E2A679_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m58912CEC7A4655D207EE2E2ACD74ED8AD6F65425_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m11048A0F71FAE52952E39C32D7C45300444AD80D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Queue_1_GetEnumerator_m4F32C724CB1AA873049A953CC218B23FD86370AE_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Tuple_2_get_Item1_mFE4E6BB2EBDAFBED6CCFAD58B1EF4D1CE5236BA1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Tuple_2_get_Item2_m6EA5B1A59F9501053DAF2ECA63725144E35854B4_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* V_1 = NULL;
	Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* V_2 = NULL;
	Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* V_4 = NULL;
	{
		int32_t L_0 = __this->___U3CU3E1__state_0;
		V_0 = L_0;
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_1 = __this->___U3CU3E4__this_2;
		V_1 = L_1;
		int32_t L_2 = V_0;
		if (!L_2)
		{
			goto IL_0017;
		}
	}
	{
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0035;
		}
	}
	{
		return (bool)0;
	}

IL_0017:
	{
		__this->___U3CU3E1__state_0 = (-1);
		// _alreadyRaised = true;
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_4 = V_1;
		NullCheck(L_4);
		L_4->____alreadyRaised_6 = (bool)1;
		// yield return null; // defer to next frame
		__this->___U3CU3E2__current_1 = NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E2__current_1), (void*)NULL);
		__this->___U3CU3E1__state_0 = 1;
		return (bool)1;
	}

IL_0035:
	{
		__this->___U3CU3E1__state_0 = (-1);
		// _alreadyRaised = false;
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_5 = V_1;
		NullCheck(L_5);
		L_5->____alreadyRaised_6 = (bool)0;
		// Queue<Tuple<string, object[]>> curEventBatch = _eventQueue;
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_6 = V_1;
		NullCheck(L_6);
		Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* L_7 = L_6->____eventQueue_7;
		V_2 = L_7;
		// _eventQueue = new Queue<Tuple<string, object[]>>();
		Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_8 = V_1;
		Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* L_9 = (Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C*)il2cpp_codegen_object_new(Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C_il2cpp_TypeInfo_var);
		NullCheck(L_9);
		Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0(L_9, Queue_1__ctor_m38469E1DE424607AA01B5D1E2165132BD08CD1C0_RuntimeMethod_var);
		NullCheck(L_8);
		L_8->____eventQueue_7 = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&L_8->____eventQueue_7), (void*)L_9);
		// foreach (Tuple<string, object[]> t in curEventBatch) {
		Queue_1_t0E83834EB73E45C43479D0948673FE3F13F9B52C* L_10 = V_2;
		NullCheck(L_10);
		Enumerator_t825E75D49AD855611C4891A1DFE9DC80E02E2626 L_11;
		L_11 = Queue_1_GetEnumerator_m4F32C724CB1AA873049A953CC218B23FD86370AE(L_10, Queue_1_GetEnumerator_m4F32C724CB1AA873049A953CC218B23FD86370AE_RuntimeMethod_var);
		V_3 = L_11;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_008b:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m40384472A2440993E6407EAFAC42C8E5F9E2A679((&V_3), Enumerator_Dispose_m40384472A2440993E6407EAFAC42C8E5F9E2A679_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0080_1;
			}

IL_005e_1:
			{
				// foreach (Tuple<string, object[]> t in curEventBatch) {
				Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* L_12;
				L_12 = Enumerator_get_Current_m11048A0F71FAE52952E39C32D7C45300444AD80D((&V_3), Enumerator_get_Current_m11048A0F71FAE52952E39C32D7C45300444AD80D_RuntimeMethod_var);
				V_4 = L_12;
				// gEvent(t.Item1, t.Item2);
				Events_t2C9EFF5778A3A267A8831F4482078817B1A12F6C* L_13 = V_1;
				NullCheck(L_13);
				EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* L_14 = L_13->___gEvent_5;
				Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* L_15 = V_4;
				NullCheck(L_15);
				String_t* L_16;
				L_16 = Tuple_2_get_Item1_mFE4E6BB2EBDAFBED6CCFAD58B1EF4D1CE5236BA1_inline(L_15, Tuple_2_get_Item1_mFE4E6BB2EBDAFBED6CCFAD58B1EF4D1CE5236BA1_RuntimeMethod_var);
				Tuple_2_tD8B8518713CAE8E9EACC0E4FD3CF177A3BE72E16* L_17 = V_4;
				NullCheck(L_17);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_18;
				L_18 = Tuple_2_get_Item2_m6EA5B1A59F9501053DAF2ECA63725144E35854B4_inline(L_17, Tuple_2_get_Item2_m6EA5B1A59F9501053DAF2ECA63725144E35854B4_RuntimeMethod_var);
				NullCheck(L_14);
				EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_inline(L_14, L_16, L_18, NULL);
			}

IL_0080_1:
			{
				// foreach (Tuple<string, object[]> t in curEventBatch) {
				bool L_19;
				L_19 = Enumerator_MoveNext_m58912CEC7A4655D207EE2E2ACD74ED8AD6F65425((&V_3), Enumerator_MoveNext_m58912CEC7A4655D207EE2E2ACD74ED8AD6F65425_RuntimeMethod_var);
				if (L_19)
				{
					goto IL_005e_1;
				}
			}
			{
				goto IL_0099;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0099:
	{
		// }
		return (bool)0;
	}
}
// System.Object NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::System.Collections.Generic.IEnumerator<System.Object>.get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CRaiseEventInternalU3Ed__10_System_Collections_Generic_IEnumeratorU3CSystem_ObjectU3E_get_Current_m46AD4AADBCB8EBE8428F8075A9BDB1FD4081993B (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current_1;
		return L_0;
	}
}
// System.Void NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::System.Collections.IEnumerator.Reset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CRaiseEventInternalU3Ed__10_System_Collections_IEnumerator_Reset_mF5A95EA267EF8058AF7ECD0E737789D999B9B8D0 (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, const RuntimeMethod* method) 
{
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NullCheck(L_0);
		NotSupportedException__ctor_m1398D0CDE19B36AA3DE9392879738C1EA2439CDF(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&U3CRaiseEventInternalU3Ed__10_System_Collections_IEnumerator_Reset_mF5A95EA267EF8058AF7ECD0E737789D999B9B8D0_RuntimeMethod_var)));
	}
}
// System.Object NoSuchStudio.Common.Events/<RaiseEventInternal>d__10::System.Collections.IEnumerator.get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CRaiseEventInternalU3Ed__10_System_Collections_IEnumerator_get_Current_mEED381456C5543ADF1C5B26BFE7F2E35F3DBCD99 (U3CRaiseEventInternalU3Ed__10_t260B5C36C1C6F9FC04F534FF48C4DE12F20D806B* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current_1;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Exception NoSuchStudio.Common.ExceptionExts::RootCause(System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* ExceptionExts_RootCause_mD13EE637045A6A3188B7622DB5A609452001CE4E (Exception_t* ___e0, const RuntimeMethod* method) 
{
	{
		// return e.InnerException != null ? e.InnerException.RootCause() : e;
		Exception_t* L_0 = ___e0;
		NullCheck(L_0);
		Exception_t* L_1;
		L_1 = Exception_get_InnerException_m0C1BDB339C786BA4DA7D2C1AD214571CFBBB1410_inline(L_0, NULL);
		if (L_1)
		{
			goto IL_000a;
		}
	}
	{
		Exception_t* L_2 = ___e0;
		return L_2;
	}

IL_000a:
	{
		Exception_t* L_3 = ___e0;
		NullCheck(L_3);
		Exception_t* L_4;
		L_4 = Exception_get_InnerException_m0C1BDB339C786BA4DA7D2C1AD214571CFBBB1410_inline(L_3, NULL);
		Exception_t* L_5;
		L_5 = ExceptionExts_RootCause_mD13EE637045A6A3188B7622DB5A609452001CE4E(L_4, NULL);
		return L_5;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.IllegalStateException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IllegalStateException__ctor_m963B18BC7568D6C372F253B7AF07C1983A40AF36 (IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC* __this, String_t* ___msg0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public IllegalStateException(string msg) : base(msg)
		String_t* L_0 = ___msg0;
		il2cpp_codegen_runtime_class_init_inline(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(__this, L_0, NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Boolean NoSuchStudio.Common.Helpers::get_IsEditMode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Helpers_get_IsEditMode_m8C0E9CEEC6F9327088F45782694D89E626E069BE (const RuntimeMethod* method) 
{
	{
		// get { return (Application.isEditor && !Application.isPlaying); }
		bool L_0;
		L_0 = Application_get_isEditor_m0377DB707B566C8E21DA3CD99963210F6D57D234(NULL);
		if (!L_0)
		{
			goto IL_0010;
		}
	}
	{
		bool L_1;
		L_1 = Application_get_isPlaying_m0B3B501E1093739F8887A0DAC5F61D9CB49CC337(NULL);
		return (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
	}

IL_0010:
	{
		return (bool)0;
	}
}
// System.Boolean NoSuchStudio.Common.Helpers::IsTablet()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Helpers_IsTablet_m97209656F74CD26E17E2EDAA8F9ACFB1817C0D97 (const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		// float screenWidth = Screen.width / Screen.dpi;
		int32_t L_0;
		L_0 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		float L_1;
		L_1 = Screen_get_dpi_mD5BB95E605FABD335F0D4736EE4860A0AA98A50D(NULL);
		// float screenHeight = Screen.height / Screen.dpi;
		int32_t L_2;
		L_2 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		float L_3;
		L_3 = Screen_get_dpi_mD5BB95E605FABD335F0D4736EE4860A0AA98A50D(NULL);
		V_0 = ((float)(((float)L_2)/L_3));
		// double size = Mathf.Sqrt(screenWidth * screenWidth + screenHeight * screenHeight);
		float L_4 = ((float)(((float)L_0)/L_1));
		float L_5 = V_0;
		float L_6 = V_0;
		float L_7;
		L_7 = sqrtf(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_4, L_4)), ((float)il2cpp_codegen_multiply(L_5, L_6)))));
		// return size >= 6;
		return (bool)((((int32_t)((!(((double)((double)L_7)) >= ((double)(6.0))))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// System.Collections.Generic.List`1<System.Int32> NoSuchStudio.Common.Helpers::UniqueRandom(System.Int32,System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* Helpers_UniqueRandom_m650C989F0CC5AE36C10D841B99F91795564F8535 (int32_t ___c0, int32_t ___min1, int32_t ___max2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* V_0 = NULL;
	HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* V_1 = NULL;
	int32_t V_2 = 0;
	{
		// if (c >= (max - min - 1) / 2) {
		int32_t L_0 = ___c0;
		int32_t L_1 = ___max2;
		int32_t L_2 = ___min1;
		if ((((int32_t)L_0) < ((int32_t)((int32_t)(((int32_t)il2cpp_codegen_subtract(((int32_t)il2cpp_codegen_subtract(L_1, L_2)), 1))/2)))))
		{
			goto IL_0026;
		}
	}
	{
		// throw new IllegalStateException(string.Format("UniqueRandom inefficient for c: {0}, max: {1}", c, max));
		int32_t L_3 = ___c0;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)), &L_4);
		int32_t L_6 = ___max2;
		int32_t L_7 = L_6;
		RuntimeObject* L_8 = Box(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)), &L_7);
		String_t* L_9;
		L_9 = String_Format_m9499958F4B0BB6089C75760AB647AB3CA4D55806(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD3AC132C0C7B7318DC5A23CCC9BC632A80976F30)), L_5, L_8, NULL);
		IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC* L_10 = (IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&IllegalStateException_t7FF8742709012CCBB6F7DBEB31FB5F6D2247FBDC_il2cpp_TypeInfo_var)));
		NullCheck(L_10);
		IllegalStateException__ctor_m963B18BC7568D6C372F253B7AF07C1983A40AF36(L_10, L_9, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_10, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Helpers_UniqueRandom_m650C989F0CC5AE36C10D841B99F91795564F8535_RuntimeMethod_var)));
	}

IL_0026:
	{
		// List<int> ret = new List<int>(c);
		int32_t L_11 = ___c0;
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_12 = (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*)il2cpp_codegen_object_new(List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98(L_12, L_11, List_1__ctor_m30DD6F0F8DFBA9856BF7220A3CDB1C89ECEC0D98_RuntimeMethod_var);
		V_0 = L_12;
		// HashSet<int> curSet = new HashSet<int>();
		HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* L_13 = (HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2*)il2cpp_codegen_object_new(HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2_il2cpp_TypeInfo_var);
		NullCheck(L_13);
		HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF(L_13, HashSet_1__ctor_m90EA29D74B137C5317CDC485AA1D799F0B6726FF_RuntimeMethod_var);
		V_1 = L_13;
		goto IL_0055;
	}

IL_0035:
	{
		// int rand = UnityEngine.Random.Range(min, max);
		int32_t L_14 = ___min1;
		int32_t L_15 = ___max2;
		int32_t L_16;
		L_16 = Random_Range_mD4D2DEE3D2E75D07740C9A6F93B3088B03BBB8F8(L_14, L_15, NULL);
		V_2 = L_16;
		// if (!curSet.Contains(rand)) {
		HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* L_17 = V_1;
		int32_t L_18 = V_2;
		NullCheck(L_17);
		bool L_19;
		L_19 = HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1(L_17, L_18, HashSet_1_Contains_m98A9F88FF94538B5EECB0F87E1E3B3572E02ACA1_RuntimeMethod_var);
		if (L_19)
		{
			goto IL_0055;
		}
	}
	{
		// curSet.Add(rand);
		HashSet_1_t4A2F2B74276D0AD3ED0F873045BD61E9504ECAE2* L_20 = V_1;
		int32_t L_21 = V_2;
		NullCheck(L_20);
		bool L_22;
		L_22 = HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB(L_20, L_21, HashSet_1_Add_m9B0DD9902395EE95D3DC522264BE1EBBBD3513EB_RuntimeMethod_var);
		// ret.Add(rand);
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_23 = V_0;
		int32_t L_24 = V_2;
		NullCheck(L_23);
		List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_inline(L_23, L_24, List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_RuntimeMethod_var);
	}

IL_0055:
	{
		// while (ret.Count < c) {
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_25 = V_0;
		NullCheck(L_25);
		int32_t L_26;
		L_26 = List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_inline(L_25, List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var);
		int32_t L_27 = ___c0;
		if ((((int32_t)L_26) < ((int32_t)L_27)))
		{
			goto IL_0035;
		}
	}
	{
		// return ret;
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_28 = V_0;
		return L_28;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.Color NoSuchStudio.Common.HSVColor::hue2rgb(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color_tD001788D726C3A7F1379BEED0260B9591F440C1F HSVColor_hue2rgb_mDA453C7AC96A68A81982754088DE85FFD331E16B (float ___hue0, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F V_3;
	memset((&V_3), 0, sizeof(V_3));
	{
		// hue = hue - (int)hue; //only use fractional part
		float L_0 = ___hue0;
		float L_1 = ___hue0;
		___hue0 = ((float)il2cpp_codegen_subtract(L_0, ((float)il2cpp_codegen_cast_double_to_int<int32_t>(L_1))));
		// float r = Mathf.Abs(hue * 6 - 3) - 1; //red
		float L_2 = ___hue0;
		float L_3;
		L_3 = fabsf(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_2, (6.0f))), (3.0f))));
		V_0 = ((float)il2cpp_codegen_subtract(L_3, (1.0f)));
		// float g = 2 - Mathf.Abs(hue * 6 - 2); //green
		float L_4 = ___hue0;
		float L_5;
		L_5 = fabsf(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_4, (6.0f))), (2.0f))));
		V_1 = ((float)il2cpp_codegen_subtract((2.0f), L_5));
		// float b = 2 - Mathf.Abs(hue * 6 - 4); //blue
		float L_6 = ___hue0;
		float L_7;
		L_7 = fabsf(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_6, (6.0f))), (4.0f))));
		V_2 = ((float)il2cpp_codegen_subtract((2.0f), L_7));
		// Color rgb = new Color(r, g, b); //combine components
		float L_8 = V_0;
		float L_9 = V_1;
		float L_10 = V_2;
		Color__ctor_mCD6889CDE39F18704CD6EA8E2EFBFA48BA3E13B0_inline((&V_3), L_8, L_9, L_10, NULL);
		// rgb.r = Mathf.Clamp01(rgb.r);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_11 = V_3;
		float L_12 = L_11.___r_0;
		float L_13;
		L_13 = Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline(L_12, NULL);
		(&V_3)->___r_0 = L_13;
		// rgb.g = Mathf.Clamp01(rgb.g);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_14 = V_3;
		float L_15 = L_14.___g_1;
		float L_16;
		L_16 = Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline(L_15, NULL);
		(&V_3)->___g_1 = L_16;
		// rgb.b = Mathf.Clamp01(rgb.b);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_17 = V_3;
		float L_18 = L_17.___b_2;
		float L_19;
		L_19 = Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline(L_18, NULL);
		(&V_3)->___b_2 = L_19;
		// return rgb;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_20 = V_3;
		return L_20;
	}
}
// UnityEngine.Color NoSuchStudio.Common.HSVColor::hsv2rgb(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color_tD001788D726C3A7F1379BEED0260B9591F440C1F HSVColor_hsv2rgb_m20E4B9FF4F64FA17A49B53BEEBD99E79A40E4682 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___hsv0, const RuntimeMethod* method) 
{
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// Color rgb = hue2rgb(hsv.x); //apply hue
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___hsv0;
		float L_1 = L_0.___x_2;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_2;
		L_2 = HSVColor_hue2rgb_mDA453C7AC96A68A81982754088DE85FFD331E16B(L_1, NULL);
		V_0 = L_2;
		// Vector3 rgbVec = new Vector3(rgb.r, rgb.g, rgb.b);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_3 = V_0;
		float L_4 = L_3.___r_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_5 = V_0;
		float L_6 = L_5.___g_1;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_7 = V_0;
		float L_8 = L_7.___b_2;
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&V_1), L_4, L_6, L_8, NULL);
		// rgbVec = Vector3.Lerp(Vector3.one, rgbVec, hsv.y); //apply saturation
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_get_one_mE6A2D5C6578E94268024613B596BF09F990B1260_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = ___hsv0;
		float L_12 = L_11.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_9, L_10, L_12, NULL);
		V_1 = L_13;
		// rgbVec = rgbVec * hsv.z; //apply value
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = ___hsv0;
		float L_16 = L_15.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
		L_17 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_14, L_16, NULL);
		V_1 = L_17;
		// rgb.r = rgbVec.x;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = V_1;
		float L_19 = L_18.___x_2;
		(&V_0)->___r_0 = L_19;
		// rgb.g = rgbVec.y;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20 = V_1;
		float L_21 = L_20.___y_3;
		(&V_0)->___g_1 = L_21;
		// rgb.b = rgbVec.z;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = V_1;
		float L_23 = L_22.___z_4;
		(&V_0)->___b_2 = L_23;
		// return rgb;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_24 = V_0;
		return L_24;
	}
}
// UnityEngine.Vector3 NoSuchStudio.Common.HSVColor::rgb2hsv(UnityEngine.Color)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 HSVColor_rgb2hsv_m8651DFEA32FA70D080508F24CE87E54E38645D1D (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___rgb0, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	float V_4 = 0.0f;
	float V_5 = 0.0f;
	{
		// float maxComponent = Mathf.Max(rgb.r, Mathf.Max(rgb.g, rgb.b));
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_0 = ___rgb0;
		float L_1 = L_0.___r_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_2 = ___rgb0;
		float L_3 = L_2.___g_1;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_4 = ___rgb0;
		float L_5 = L_4.___b_2;
		float L_6;
		L_6 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_3, L_5, NULL);
		float L_7;
		L_7 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_1, L_6, NULL);
		V_0 = L_7;
		// float minComponent = Mathf.Min(rgb.r, Mathf.Min(rgb.g, rgb.b));
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_8 = ___rgb0;
		float L_9 = L_8.___r_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_10 = ___rgb0;
		float L_11 = L_10.___g_1;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_12 = ___rgb0;
		float L_13 = L_12.___b_2;
		float L_14;
		L_14 = Mathf_Min_m4F2A9C5128DC3F9E84865EE7ADA8DB5DA6B8B507_inline(L_11, L_13, NULL);
		float L_15;
		L_15 = Mathf_Min_m4F2A9C5128DC3F9E84865EE7ADA8DB5DA6B8B507_inline(L_9, L_14, NULL);
		V_1 = L_15;
		// float diff = maxComponent - minComponent;
		float L_16 = V_0;
		float L_17 = V_1;
		V_2 = ((float)il2cpp_codegen_subtract(L_16, L_17));
		// float hue = 0;
		V_3 = (0.0f);
		// if (maxComponent == rgb.r) {
		float L_18 = V_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_19 = ___rgb0;
		float L_20 = L_19.___r_0;
		if ((!(((float)L_18) == ((float)L_20))))
		{
			goto IL_0065;
		}
	}
	{
		// hue = 0 + (rgb.g - rgb.b) / diff;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_21 = ___rgb0;
		float L_22 = L_21.___g_1;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_23 = ___rgb0;
		float L_24 = L_23.___b_2;
		float L_25 = V_2;
		V_3 = ((float)il2cpp_codegen_add((0.0f), ((float)(((float)il2cpp_codegen_subtract(L_22, L_24))/L_25))));
		goto IL_00a5;
	}

IL_0065:
	{
		// } else if (maxComponent == rgb.g) {
		float L_26 = V_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_27 = ___rgb0;
		float L_28 = L_27.___g_1;
		if ((!(((float)L_26) == ((float)L_28))))
		{
			goto IL_0086;
		}
	}
	{
		// hue = 2 + (rgb.b - rgb.r) / diff;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_29 = ___rgb0;
		float L_30 = L_29.___b_2;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_31 = ___rgb0;
		float L_32 = L_31.___r_0;
		float L_33 = V_2;
		V_3 = ((float)il2cpp_codegen_add((2.0f), ((float)(((float)il2cpp_codegen_subtract(L_30, L_32))/L_33))));
		goto IL_00a5;
	}

IL_0086:
	{
		// } else if (maxComponent == rgb.b) {
		float L_34 = V_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_35 = ___rgb0;
		float L_36 = L_35.___b_2;
		if ((!(((float)L_34) == ((float)L_36))))
		{
			goto IL_00a5;
		}
	}
	{
		// hue = 4 + (rgb.r - rgb.g) / diff;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_37 = ___rgb0;
		float L_38 = L_37.___r_0;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_39 = ___rgb0;
		float L_40 = L_39.___g_1;
		float L_41 = V_2;
		V_3 = ((float)il2cpp_codegen_add((4.0f), ((float)(((float)il2cpp_codegen_subtract(L_38, L_40))/L_41))));
	}

IL_00a5:
	{
		// hue = (hue / 6) - (int)(hue / 6);
		float L_42 = V_3;
		float L_43 = V_3;
		V_3 = ((float)il2cpp_codegen_subtract(((float)(L_42/(6.0f))), ((float)il2cpp_codegen_cast_double_to_int<int32_t>(((float)(L_43/(6.0f)))))));
		// if (hue < 0) hue = hue + 1;
		float L_44 = V_3;
		if ((!(((float)L_44) < ((float)(0.0f)))))
		{
			goto IL_00c7;
		}
	}
	{
		// if (hue < 0) hue = hue + 1;
		float L_45 = V_3;
		V_3 = ((float)il2cpp_codegen_add(L_45, (1.0f)));
	}

IL_00c7:
	{
		// float saturation = diff / maxComponent;
		float L_46 = V_2;
		float L_47 = V_0;
		V_4 = ((float)(L_46/L_47));
		// float value = maxComponent;
		float L_48 = V_0;
		V_5 = L_48;
		// return new Vector3(hue, saturation, value);
		float L_49 = V_3;
		float L_50 = V_4;
		float L_51 = V_5;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52;
		memset((&L_52), 0, sizeof(L_52));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_52), L_49, L_50, L_51, /*hidden argument*/NULL);
		return L_52;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.InputTouchVisualizer::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputTouchVisualizer_Start_m56EDA1C4B33483CB40FA36AE1C40A8E53AB24FE4 (InputTouchVisualizer_t4330BBD81033C73ED064B27D2A9C30AA4C6A12C8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5FD20D8504182B91A7EE1908D7A191F36ABAEDF1);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// touchVisuals = new GameObject[maxTouchCount];
		int32_t L_0 = __this->___maxTouchCount_6;
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_1 = (GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF*)(GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF*)SZArrayNew(GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF_il2cpp_TypeInfo_var, (uint32_t)L_0);
		__this->___touchVisuals_5 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___touchVisuals_5), (void*)L_1);
		// for (int i = 0; i < maxTouchCount; i++) {
		V_0 = 0;
		goto IL_0062;
	}

IL_0015:
	{
		// touchVisuals[i] = Instantiate(prefab, transform, false);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_2 = __this->___touchVisuals_5;
		int32_t L_3 = V_0;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_4 = __this->___prefab_4;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
		L_5 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_6;
		L_6 = Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5(L_4, L_5, (bool)0, Object_Instantiate_TisGameObject_t76FEDD663AB33C991A9C9A23129337651094216F_m8CC4225774108D732B4BF9D4B204835A2DBA6EC5_RuntimeMethod_var);
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_6);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*)L_6);
		// touchVisuals[i].name = string.Format("TouchVisualizer{0}", i);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_7 = __this->___touchVisuals_5;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		int32_t L_11 = V_0;
		int32_t L_12 = L_11;
		RuntimeObject* L_13 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_12);
		String_t* L_14;
		L_14 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(_stringLiteral5FD20D8504182B91A7EE1908D7A191F36ABAEDF1, L_13, NULL);
		NullCheck(L_10);
		Object_set_name_mC79E6DC8FFD72479C90F0C4CC7F42A0FEAF5AE47(L_10, L_14, NULL);
		// touchVisuals[i].transform.SetAsLastSibling();
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_15 = __this->___touchVisuals_5;
		int32_t L_16 = V_0;
		NullCheck(L_15);
		int32_t L_17 = L_16;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_18 = (L_15)->GetAt(static_cast<il2cpp_array_size_t>(L_17));
		NullCheck(L_18);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19;
		L_19 = GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56(L_18, NULL);
		NullCheck(L_19);
		Transform_SetAsLastSibling_m848AF1A0B4C7912FE88D8CBCF92B83D57B2B917E(L_19, NULL);
		// for (int i = 0; i < maxTouchCount; i++) {
		int32_t L_20 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_20, 1));
	}

IL_0062:
	{
		// for (int i = 0; i < maxTouchCount; i++) {
		int32_t L_21 = V_0;
		int32_t L_22 = __this->___maxTouchCount_6;
		if ((((int32_t)L_21) < ((int32_t)L_22)))
		{
			goto IL_0015;
		}
	}
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.InputTouchVisualizer::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputTouchVisualizer_Update_mD9CEEFFB000EB00F216AE7C7AE354DCBA6331059 (InputTouchVisualizer_t4330BBD81033C73ED064B27D2A9C30AA4C6A12C8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_3;
	memset((&V_3), 0, sizeof(V_3));
	int32_t V_4 = 0;
	{
		// int c = Math.Min(Input.touchCount, maxTouchCount);
		int32_t L_0;
		L_0 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		int32_t L_1 = __this->___maxTouchCount_6;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		int32_t L_2;
		L_2 = Math_Min_m1F346FEDDC77AC1EC0C4EF1AC6BA59F0EC7980F8(L_0, L_1, NULL);
		V_0 = L_2;
		// for (int i = 0; i < c; i++) {
		V_1 = 0;
		goto IL_006c;
	}

IL_0015:
	{
		// Touch t = Input.GetTouch(i);
		int32_t L_3 = V_1;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_4;
		L_4 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(L_3, NULL);
		V_2 = L_4;
		// Vector3 worldPos = Camera.main.ScreenToWorldPoint((Vector3)t.position + Vector3.forward * -1);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5;
		L_5 = Camera_get_main_mF222B707D3BF8CC9C7544609EFC71CFB62E81D43(NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6;
		L_6 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_2), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline(L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_8, (-1.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_7, L_9, NULL);
		NullCheck(L_5);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Camera_ScreenToWorldPoint_m5EA3148F070985EC72127AAC3448D8D6ABE6E7E5(L_5, L_10, NULL);
		V_3 = L_11;
		// touchVisuals[i].transform.position = worldPos;
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_12 = __this->___touchVisuals_5;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_15);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_16;
		L_16 = GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56(L_15, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = V_3;
		NullCheck(L_16);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_16, L_17, NULL);
		// touchVisuals[i].SetActive(true);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_18 = __this->___touchVisuals_5;
		int32_t L_19 = V_1;
		NullCheck(L_18);
		int32_t L_20 = L_19;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_21 = (L_18)->GetAt(static_cast<il2cpp_array_size_t>(L_20));
		NullCheck(L_21);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_21, (bool)1, NULL);
		// for (int i = 0; i < c; i++) {
		int32_t L_22 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_22, 1));
	}

IL_006c:
	{
		// for (int i = 0; i < c; i++) {
		int32_t L_23 = V_1;
		int32_t L_24 = V_0;
		if ((((int32_t)L_23) < ((int32_t)L_24)))
		{
			goto IL_0015;
		}
	}
	{
		// for (int i = c; i < maxTouchCount; i++) {
		int32_t L_25 = V_0;
		V_4 = L_25;
		goto IL_008a;
	}

IL_0075:
	{
		// touchVisuals[i].SetActive(false);
		GameObjectU5BU5D_tFF67550DFCE87096D7A3734EA15B75896B2722CF* L_26 = __this->___touchVisuals_5;
		int32_t L_27 = V_4;
		NullCheck(L_26);
		int32_t L_28 = L_27;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_29 = (L_26)->GetAt(static_cast<il2cpp_array_size_t>(L_28));
		NullCheck(L_29);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_29, (bool)0, NULL);
		// for (int i = c; i < maxTouchCount; i++) {
		int32_t L_30 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_add(L_30, 1));
	}

IL_008a:
	{
		// for (int i = c; i < maxTouchCount; i++) {
		int32_t L_31 = V_4;
		int32_t L_32 = __this->___maxTouchCount_6;
		if ((((int32_t)L_31) < ((int32_t)L_32)))
		{
			goto IL_0075;
		}
	}
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.InputTouchVisualizer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputTouchVisualizer__ctor_m118ECAAC0904112FDD3FF12F7E425BB564836F12 (InputTouchVisualizer_t4330BBD81033C73ED064B27D2A9C30AA4C6A12C8* __this, const RuntimeMethod* method) 
{
	{
		// int maxTouchCount = 10;
		__this->___maxTouchCount_6 = ((int32_t)10);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Collections.IEnumerator NoSuchStudio.Common.MonoBehaviourRunDelayedExt::DelayedCoroutine(System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MonoBehaviourRunDelayedExt_DelayedCoroutine_mA82873EEAA344F29C1AA70758E4281F753284470 (float ___delay0, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* L_0 = (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769*)il2cpp_codegen_object_new(U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CDelayedCoroutineU3Ed__0__ctor_m3CC301E300B7A507D5871F78E203E5CDCF77B2AD(L_0, 0, NULL);
		U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* L_1 = L_0;
		float L_2 = ___delay0;
		NullCheck(L_1);
		L_1->___delay_2 = L_2;
		U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* L_3 = L_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_4 = ___a1;
		NullCheck(L_3);
		L_3->___a_3 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___a_3), (void*)L_4);
		return L_3;
	}
}
// System.Collections.IEnumerator NoSuchStudio.Common.MonoBehaviourRunDelayedExt::DelayedCoroutineRealtime(System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MonoBehaviourRunDelayedExt_DelayedCoroutineRealtime_m9044905016AFFC0207E60DB83DF282AEADF67B45 (float ___delay0, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* L_0 = (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53*)il2cpp_codegen_object_new(U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CDelayedCoroutineRealtimeU3Ed__1__ctor_mAF980EDFAD6792A2C1DA0C725D7EBDF8B3303E3C(L_0, 0, NULL);
		U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* L_1 = L_0;
		float L_2 = ___delay0;
		NullCheck(L_1);
		L_1->___delay_2 = L_2;
		U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* L_3 = L_1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_4 = ___a1;
		NullCheck(L_3);
		L_3->___a_3 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___a_3), (void*)L_4);
		return L_3;
	}
}
// UnityEngine.Coroutine NoSuchStudio.Common.MonoBehaviourRunDelayedExt::RunDelayed(UnityEngine.MonoBehaviour,System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* MonoBehaviourRunDelayedExt_RunDelayed_mA8AC65BCCF871A4C82EB2A0A636609F805BB7640 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ___mono0, float ___delay1, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a2, const RuntimeMethod* method) 
{
	{
		// return mono.StartCoroutine(DelayedCoroutine(delay, a));
		MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* L_0 = ___mono0;
		float L_1 = ___delay1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = ___a2;
		RuntimeObject* L_3;
		L_3 = MonoBehaviourRunDelayedExt_DelayedCoroutine_mA82873EEAA344F29C1AA70758E4281F753284470(L_1, L_2, NULL);
		NullCheck(L_0);
		Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* L_4;
		L_4 = MonoBehaviour_StartCoroutine_m4CAFF732AA28CD3BDC5363B44A863575530EC812(L_0, L_3, NULL);
		return L_4;
	}
}
// UnityEngine.Coroutine NoSuchStudio.Common.MonoBehaviourRunDelayedExt::RunDelayedRealtime(UnityEngine.MonoBehaviour,System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* MonoBehaviourRunDelayedExt_RunDelayedRealtime_m48EAEC5B712A6828E57E5377E0576487E88E5A46 (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* ___mono0, float ___delay1, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a2, const RuntimeMethod* method) 
{
	{
		// return mono.StartCoroutine(DelayedCoroutineRealtime(delay, a));
		MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* L_0 = ___mono0;
		float L_1 = ___delay1;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = ___a2;
		RuntimeObject* L_3;
		L_3 = MonoBehaviourRunDelayedExt_DelayedCoroutineRealtime_m9044905016AFFC0207E60DB83DF282AEADF67B45(L_1, L_2, NULL);
		NullCheck(L_0);
		Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* L_4;
		L_4 = MonoBehaviour_StartCoroutine_m4CAFF732AA28CD3BDC5363B44A863575530EC812(L_0, L_3, NULL);
		return L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineU3Ed__0__ctor_m3CC301E300B7A507D5871F78E203E5CDCF77B2AD (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, int32_t ___U3CU3E1__state0, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___U3CU3E1__state0;
		__this->___U3CU3E1__state_0 = L_0;
		return;
	}
}
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::System.IDisposable.Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineU3Ed__0_System_IDisposable_Dispose_m896A4003E3DD903511478DB04331E0EC14F1F27A (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// System.Boolean NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CDelayedCoroutineU3Ed__0_MoveNext_m0A83FE51796ACDCB34E4B0D9BBD4A65AF0D5A965 (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->___U3CU3E1__state_0;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if (!L_1)
		{
			goto IL_0010;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0031;
		}
	}
	{
		return (bool)0;
	}

IL_0010:
	{
		__this->___U3CU3E1__state_0 = (-1);
		// yield return new WaitForSeconds(delay);
		float L_3 = __this->___delay_2;
		WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3* L_4 = (WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3*)il2cpp_codegen_object_new(WaitForSeconds_tF179DF251655B8DF044952E70A60DF4B358A3DD3_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		WaitForSeconds__ctor_m579F95BADEDBAB4B3A7E302C6EE3995926EF2EFC(L_4, L_3, NULL);
		__this->___U3CU3E2__current_1 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E2__current_1), (void*)L_4);
		__this->___U3CU3E1__state_0 = 1;
		return (bool)1;
	}

IL_0031:
	{
		__this->___U3CU3E1__state_0 = (-1);
		// a();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = __this->___a_3;
		NullCheck(L_5);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_5, NULL);
		// }
		return (bool)0;
	}
}
// System.Object NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::System.Collections.Generic.IEnumerator<System.Object>.get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CDelayedCoroutineU3Ed__0_System_Collections_Generic_IEnumeratorU3CSystem_ObjectU3E_get_Current_mBE4061C0DE05E002D05F966CAA59AB5E8E03D5B5 (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current_1;
		return L_0;
	}
}
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::System.Collections.IEnumerator.Reset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineU3Ed__0_System_Collections_IEnumerator_Reset_m7A9A7E9003837F53021473B482B0C3DDDC6345A9 (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, const RuntimeMethod* method) 
{
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NullCheck(L_0);
		NotSupportedException__ctor_m1398D0CDE19B36AA3DE9392879738C1EA2439CDF(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&U3CDelayedCoroutineU3Ed__0_System_Collections_IEnumerator_Reset_m7A9A7E9003837F53021473B482B0C3DDDC6345A9_RuntimeMethod_var)));
	}
}
// System.Object NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutine>d__0::System.Collections.IEnumerator.get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CDelayedCoroutineU3Ed__0_System_Collections_IEnumerator_get_Current_m4393CA470F6F24B5C64C5D84E0C5E28867A43C3B (U3CDelayedCoroutineU3Ed__0_t405843FAB8114F4693B5BF92BD3D332338D4A769* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current_1;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineRealtimeU3Ed__1__ctor_mAF980EDFAD6792A2C1DA0C725D7EBDF8B3303E3C (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, int32_t ___U3CU3E1__state0, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___U3CU3E1__state0;
		__this->___U3CU3E1__state_0 = L_0;
		return;
	}
}
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::System.IDisposable.Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineRealtimeU3Ed__1_System_IDisposable_Dispose_mA9C105F18F5F5E8C02EA03B375C025389420DF41 (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, const RuntimeMethod* method) 
{
	{
		return;
	}
}
// System.Boolean NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CDelayedCoroutineRealtimeU3Ed__1_MoveNext_mCFB91304001E972EE14E90BE79CD090BB76374CF (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->___U3CU3E1__state_0;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if (!L_1)
		{
			goto IL_0010;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0031;
		}
	}
	{
		return (bool)0;
	}

IL_0010:
	{
		__this->___U3CU3E1__state_0 = (-1);
		// yield return new WaitForSecondsRealtime(delay);
		float L_3 = __this->___delay_2;
		WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01* L_4 = (WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01*)il2cpp_codegen_object_new(WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		WaitForSecondsRealtime__ctor_mBFC1E4F0E042D5EC6E7EEB211A2FE5193A8F6D6F(L_4, L_3, NULL);
		__this->___U3CU3E2__current_1 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CU3E2__current_1), (void*)L_4);
		__this->___U3CU3E1__state_0 = 1;
		return (bool)1;
	}

IL_0031:
	{
		__this->___U3CU3E1__state_0 = (-1);
		// a();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = __this->___a_3;
		NullCheck(L_5);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_5, NULL);
		// }
		return (bool)0;
	}
}
// System.Object NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::System.Collections.Generic.IEnumerator<System.Object>.get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CDelayedCoroutineRealtimeU3Ed__1_System_Collections_Generic_IEnumeratorU3CSystem_ObjectU3E_get_Current_m75572B5B85EA5E1EE9C60B5D92FDA80AA7D81DD6 (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current_1;
		return L_0;
	}
}
// System.Void NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::System.Collections.IEnumerator.Reset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CDelayedCoroutineRealtimeU3Ed__1_System_Collections_IEnumerator_Reset_m323CEAB25061C010FAC5F4A864FDCE172B8E592F (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, const RuntimeMethod* method) 
{
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NullCheck(L_0);
		NotSupportedException__ctor_m1398D0CDE19B36AA3DE9392879738C1EA2439CDF(L_0, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&U3CDelayedCoroutineRealtimeU3Ed__1_System_Collections_IEnumerator_Reset_m323CEAB25061C010FAC5F4A864FDCE172B8E592F_RuntimeMethod_var)));
	}
}
// System.Object NoSuchStudio.Common.MonoBehaviourRunDelayedExt/<DelayedCoroutineRealtime>d__1::System.Collections.IEnumerator.get_Current()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CDelayedCoroutineRealtimeU3Ed__1_System_Collections_IEnumerator_get_Current_m2A4E8C57D46D2FD7A08FA708C4DCB00FBF1D48EB (U3CDelayedCoroutineRealtimeU3Ed__1_t577D3B315AEAE4EB33131D242FA65BBF542D6D53* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CU3E2__current_1;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.Logger NoSuchStudio.Common.NoSuchMonoBehaviour::get_logger()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* NoSuchMonoBehaviour_get_logger_mCEDC0791B7E5ECB57830A66B56C5E3302B2E946C (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Type thisType = GetType();
		Type_t* L_0;
		L_0 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(__this, NULL);
		// return UnityObjectLoggerExt.GetLoggerByType(thisType).logger;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_1;
		L_1 = UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53(L_0, NULL);
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_2 = L_1.___Item1_0;
		return L_2;
	}
}
// NoSuchStudio.Common.LoggerConfig NoSuchStudio.Common.NoSuchMonoBehaviour::get_loggerConfig()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* NoSuchMonoBehaviour_get_loggerConfig_mFEAF2B18B92038DFC08DDCA0596BFB37C740C46E (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Type thisType = GetType();
		Type_t* L_0;
		L_0 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(__this, NULL);
		// return UnityObjectLoggerExt.GetLoggerByType(thisType).loggerConfig;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_1;
		L_1 = UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53(L_0, NULL);
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_2 = L_1.___Item2_1;
		return L_2;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::LogLogFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour_LogLogFormat_mEE472F6D7EA4480C7AB4EEBD760BCA70979922FB (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogLogFormat(this, format, args);
		String_t* L_0 = ___format0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___args1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogLogFormat_mEF4688871A7D53518B12307F907E452E5D934513(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::LogLog(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour_LogLog_mC9123DC97B4D4702BFCA942665A5868B150BF48D (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, String_t* ___msg0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogLog(this, msg);
		String_t* L_0 = ___msg0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogLog_mC174F3944DBBF72B5667393163D3CBBFF440AB30(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::LogWarnFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour_LogWarnFormat_m6F7821658F3067AD1F4A184A883486D1ADE7AAD9 (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogWarnFormat(this, format, args);
		String_t* L_0 = ___format0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___args1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogWarnFormat_m18CFBC606E7A4660BCFC38C759271265CA589FB2(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::LogWarn(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour_LogWarn_m620B552328C736B9FB5EEA9767D8389A5F2F4AB7 (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, String_t* ___log0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogWarn(this, log);
		String_t* L_0 = ___log0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogWarn_mB1F6307AF886FDE0D443B5AFFF6E674EDBE41EDA(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::LogErrorFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour_LogErrorFormat_mEDCCB17EBE83356438E3EF6E04A759B2C1511878 (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogErrorFormat(this, format, args);
		String_t* L_0 = ___format0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___args1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogErrorFormat_m40A9D1D33A5FE6D11D78DE280141F87EF9221D81(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::LogError(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour_LogError_mD4B1F040F07649A9AB287D52F2FC3720941D45E9 (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, String_t* ___log0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogError(this, log);
		String_t* L_0 = ___log0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogError_m364179587BD3CA7C881454C95564305B5A91F612(__this, L_0, NULL);
		// }
		return;
	}
}
// UnityEngine.Coroutine NoSuchStudio.Common.NoSuchMonoBehaviour::RunDelayed(System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* NoSuchMonoBehaviour_RunDelayed_mCA30E45DD28525DE86864FCA735DF8C6B18B33CA (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, float ___delay0, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a1, const RuntimeMethod* method) 
{
	{
		// return MonoBehaviourRunDelayedExt.RunDelayed(this, delay, a);
		float L_0 = ___delay0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = ___a1;
		Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* L_2;
		L_2 = MonoBehaviourRunDelayedExt_RunDelayed_mA8AC65BCCF871A4C82EB2A0A636609F805BB7640(__this, L_0, L_1, NULL);
		return L_2;
	}
}
// UnityEngine.Coroutine NoSuchStudio.Common.NoSuchMonoBehaviour::RunDelayedRealtime(System.Single,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* NoSuchMonoBehaviour_RunDelayedRealtime_m1466AAF33E41AEA0582F26659EBEA786D2313219 (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, float ___delay0, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___a1, const RuntimeMethod* method) 
{
	{
		// return MonoBehaviourRunDelayedExt.RunDelayedRealtime(this, delay, a);
		float L_0 = ___delay0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = ___a1;
		Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* L_2;
		L_2 = MonoBehaviourRunDelayedExt_RunDelayedRealtime_m48EAEC5B712A6828E57E5377E0576487E88E5A46(__this, L_0, L_1, NULL);
		return L_2;
	}
}
// System.Void NoSuchStudio.Common.NoSuchMonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchMonoBehaviour__ctor_m58F2B53BD2C05B59A51818C9B3656C60AE0C55EE (NoSuchMonoBehaviour_t72F9694F459199DD31BECA86FE2DF60298CA92F4* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.Logger NoSuchStudio.Common.NoSuchScriptableObject::get_logger()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* NoSuchScriptableObject_get_logger_mC248326C49809BC086267D5A940B60F0F8273C34 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Type thisType = GetType();
		Type_t* L_0;
		L_0 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(__this, NULL);
		// return UnityObjectLoggerExt.GetLoggerByType(thisType).logger;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_1;
		L_1 = UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53(L_0, NULL);
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_2 = L_1.___Item1_0;
		return L_2;
	}
}
// NoSuchStudio.Common.LoggerConfig NoSuchStudio.Common.NoSuchScriptableObject::get_loggerConfig()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* NoSuchScriptableObject_get_loggerConfig_m26E1AB2ADAD2E3B6E59FB819B19AE1870AEC4DB0 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Type thisType = GetType();
		Type_t* L_0;
		L_0 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(__this, NULL);
		// return UnityObjectLoggerExt.GetLoggerByType(thisType).loggerConfig;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_1;
		L_1 = UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53(L_0, NULL);
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_2 = L_1.___Item2_1;
		return L_2;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::LogLogFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject_LogLogFormat_m9DF95E142828C9F9965B945515F5FC64F10CD16F (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogLogFormat(this, format, args);
		String_t* L_0 = ___format0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___args1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogLogFormat_mEF4688871A7D53518B12307F907E452E5D934513(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::LogLog(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject_LogLog_m755ACD14D13ACC8711B50AD9377FEDA6E3EE47F3 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, String_t* ___log0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogLog(this, log);
		String_t* L_0 = ___log0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogLog_mC174F3944DBBF72B5667393163D3CBBFF440AB30(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::LogWarnFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject_LogWarnFormat_m047557A16151B2D2B27886CF63E1873CB04EE970 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogWarnFormat(this, format, args);
		String_t* L_0 = ___format0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___args1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogWarnFormat_m18CFBC606E7A4660BCFC38C759271265CA589FB2(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::LogWarn(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject_LogWarn_m81AEB79BB4681A020BDFAE5C5E22152C58A37EB1 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, String_t* ___log0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogWarn(this, log);
		String_t* L_0 = ___log0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogWarn_mB1F6307AF886FDE0D443B5AFFF6E674EDBE41EDA(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::LogErrorFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject_LogErrorFormat_m1B2CE5D8EF23858C14F0FFA3A296C5F04494A306 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogErrorFormat(this, format, args);
		String_t* L_0 = ___format0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___args1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogErrorFormat_m40A9D1D33A5FE6D11D78DE280141F87EF9221D81(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::LogError(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject_LogError_mE3E49B50707F2CA0C0E1E569412EEBA1569DC365 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, String_t* ___log0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityObjectLoggerExt.LogError(this, log);
		String_t* L_0 = ___log0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogError_m364179587BD3CA7C881454C95564305B5A91F612(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.NoSuchScriptableObject::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NoSuchScriptableObject__ctor_mFF04A25BF1D8EE7EABD56E0099717EE65C998BF7 (NoSuchScriptableObject_t51C69F9A4AE86654CC6AD007C57859F2848702D8* __this, const RuntimeMethod* method) 
{
	{
		ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// NoSuchStudio.Common.Scope NoSuchStudio.Common.Scope::Create(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* Scope_Create_mA60654609FAF23A2BF31DE2DAABAC41396B990DD (String_t* ___scope0, String_t* ___delimiter1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return new Scope(scope, delimiter);
		String_t* L_0 = ___scope0;
		String_t* L_1 = ___delimiter1;
		Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* L_2 = (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E*)il2cpp_codegen_object_new(Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		Scope__ctor_mAD0B7846C6034EEA565200DD0535F3C0DDAA1C9F(L_2, L_0, L_1, NULL);
		return L_2;
	}
}
// System.Void NoSuchStudio.Common.Scope::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scope__ctor_mAD0B7846C6034EEA565200DD0535F3C0DDAA1C9F (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* __this, String_t* ___scope0, String_t* ___delimiter1, const RuntimeMethod* method) 
{
	{
		// private Scope(string scope, string delimiter) {
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// _scope = scope;
		String_t* L_0 = ___scope0;
		__this->____scope_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____scope_1), (void*)L_0);
		// _delimiter = delimiter;
		String_t* L_1 = ___delimiter1;
		__this->____delimiter_2 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____delimiter_2), (void*)L_1);
		// }
		return;
	}
}
// System.Boolean NoSuchStudio.Common.Scope::Match(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Scope_Match_m83A4301662FB9AD9A7F5D411E5B170211894D67D (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* __this, String_t* ___fullName0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1FB9018D8BFC0FACF068B1067EF9E96C35FED1FE);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return fullName.StartsWith(string.Format("{0}{1}", _scope, _delimiter));
		String_t* L_0 = ___fullName0;
		String_t* L_1 = __this->____scope_1;
		String_t* L_2 = __this->____delimiter_2;
		String_t* L_3;
		L_3 = String_Format_m9499958F4B0BB6089C75760AB647AB3CA4D55806(_stringLiteral1FB9018D8BFC0FACF068B1067EF9E96C35FED1FE, L_1, L_2, NULL);
		NullCheck(L_0);
		bool L_4;
		L_4 = String_StartsWith_mF75DBA1EB709811E711B44E26FF919C88A8E65C0(L_0, L_3, NULL);
		return L_4;
	}
}
// System.String NoSuchStudio.Common.Scope::Apply(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Scope_Apply_m9466E3DCC16A6C20BBAAD15E186A493C7ECFB6B2 (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* __this, String_t* ___partialName0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2703934E990F4D74F9E97D5985CDF284A870C0E0);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return string.Format("{0}{1}{2}", _scope, _delimiter, partialName);
		String_t* L_0 = __this->____scope_1;
		String_t* L_1 = __this->____delimiter_2;
		String_t* L_2 = ___partialName0;
		String_t* L_3;
		L_3 = String_Format_m76BF8F3A6AD789E38B708848A2688D400AAC250A(_stringLiteral2703934E990F4D74F9E97D5985CDF284A870C0E0, L_0, L_1, L_2, NULL);
		return L_3;
	}
}
// System.String NoSuchStudio.Common.Scope::Unapply(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Scope_Unapply_m450CB7685D9078B98566A1C8E15C4A7B3AF1E228 (Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* __this, String_t* ___fullName0, const RuntimeMethod* method) 
{
	{
		// if (!Match(fullName)) throw new ApplicationException(string.Format("Cannot unapply {0}{1} to {2}", _scope, _delimiter, fullName));
		String_t* L_0 = ___fullName0;
		bool L_1;
		L_1 = Scope_Match_m83A4301662FB9AD9A7F5D411E5B170211894D67D(__this, L_0, NULL);
		if (L_1)
		{
			goto IL_0026;
		}
	}
	{
		// if (!Match(fullName)) throw new ApplicationException(string.Format("Cannot unapply {0}{1} to {2}", _scope, _delimiter, fullName));
		String_t* L_2 = __this->____scope_1;
		String_t* L_3 = __this->____delimiter_2;
		String_t* L_4 = ___fullName0;
		String_t* L_5;
		L_5 = String_Format_m76BF8F3A6AD789E38B708848A2688D400AAC250A(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2493EF500F7255CBBDEFD73C9C3D3AA6EEC00040)), L_2, L_3, L_4, NULL);
		ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A* L_6 = (ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A_il2cpp_TypeInfo_var)));
		NullCheck(L_6);
		ApplicationException__ctor_mE51100DFCDB0A0DF23B482CC43EC8E396BE7BE82(L_6, L_5, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_6, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Scope_Unapply_m450CB7685D9078B98566A1C8E15C4A7B3AF1E228_RuntimeMethod_var)));
	}

IL_0026:
	{
		// return fullName.Substring(_scope.Length + _delimiter.Length);
		String_t* L_7 = ___fullName0;
		String_t* L_8 = __this->____scope_1;
		NullCheck(L_8);
		int32_t L_9;
		L_9 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_8, NULL);
		String_t* L_10 = __this->____delimiter_2;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_10, NULL);
		NullCheck(L_7);
		String_t* L_12;
		L_12 = String_Substring_m6BA4A3FA3800FE92662D0847CC8E1EEF940DF472(L_7, ((int32_t)il2cpp_codegen_add(L_9, L_11)), NULL);
		return L_12;
	}
}
// System.Void NoSuchStudio.Common.Scope::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scope__cctor_m830B2934F67F237E41BD6778B49392A9416E70A0 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static readonly Scope Global = Scope.Create("", "");
		Scope_tF552A0888C1C104526116958D4BC266EDB879A0E* L_0;
		L_0 = Scope_Create_mA60654609FAF23A2BF31DE2DAABAC41396B990DD(_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709, _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709, NULL);
		((Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_StaticFields*)il2cpp_codegen_static_fields_for(Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_il2cpp_TypeInfo_var))->___Global_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_StaticFields*)il2cpp_codegen_static_fields_for(Scope_tF552A0888C1C104526116958D4BC266EDB879A0E_il2cpp_TypeInfo_var))->___Global_0), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Boolean NoSuchStudio.Common.Singleton::get_IsChosenSingleton()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Singleton_get_IsChosenSingleton_m604CEE054136DBF9D9BE920721E993FAC118730F (Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return !string.IsNullOrEmpty(tagName)
		//     && _instances.ContainsKey(tagName)
		//     && _instances[tagName] == this;
		String_t* L_0 = __this->___tagName_4;
		bool L_1;
		L_1 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_0, NULL);
		if (L_1)
		{
			goto IL_0036;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_2 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		String_t* L_3 = __this->___tagName_4;
		NullCheck(L_2);
		bool L_4;
		L_4 = Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A(L_2, L_3, Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var);
		if (!L_4)
		{
			goto IL_0036;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_5 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		String_t* L_6 = __this->___tagName_4;
		NullCheck(L_5);
		Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* L_7;
		L_7 = Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B(L_5, L_6, Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B_RuntimeMethod_var);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_8;
		L_8 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_7, __this, NULL);
		return L_8;
	}

IL_0036:
	{
		return (bool)0;
	}
}
// System.Void NoSuchStudio.Common.Singleton::OnEnable()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Singleton_OnEnable_m3E4254EB3DA4A8A63C48775A1AD5A76C2B501B4B (Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_set_Item_m45E21CB14A73F58BD606054CB89E38965210E75E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (string.IsNullOrEmpty(tagName)) throw new ApplicationException(string.Format("Singleton tagName empty for object {0}", gameObject.name));
		String_t* L_0 = __this->___tagName_4;
		bool L_1;
		L_1 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_0, NULL);
		if (!L_1)
		{
			goto IL_0028;
		}
	}
	{
		// if (string.IsNullOrEmpty(tagName)) throw new ApplicationException(string.Format("Singleton tagName empty for object {0}", gameObject.name));
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_2;
		L_2 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		NullCheck(L_2);
		String_t* L_3;
		L_3 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(L_2, NULL);
		String_t* L_4;
		L_4 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral961BC57A0E961FF7DA97AB95377745D8766376D7)), L_3, NULL);
		ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A* L_5 = (ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ApplicationException_tA744BED4E90266BD255285CD4CF909BAB3EE811A_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ApplicationException__ctor_mE51100DFCDB0A0DF23B482CC43EC8E396BE7BE82(L_5, L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Singleton_OnEnable_m3E4254EB3DA4A8A63C48775A1AD5A76C2B501B4B_RuntimeMethod_var)));
	}

IL_0028:
	{
		// if (_instances.ContainsKey(tagName)) {
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_6 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		String_t* L_7 = __this->___tagName_4;
		NullCheck(L_6);
		bool L_8;
		L_8 = Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A(L_6, L_7, Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var);
		if (!L_8)
		{
			goto IL_0052;
		}
	}
	{
		// gameObject.SetActive(false);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_9;
		L_9 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		NullCheck(L_9);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_9, (bool)0, NULL);
		// Destroy(gameObject);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_10;
		L_10 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object_Destroy_mFCDAE6333522488F60597AF019EA90BB1207A5AA(L_10, NULL);
		return;
	}

IL_0052:
	{
		// gameObject.tag = tagName;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_11;
		L_11 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		String_t* L_12 = __this->___tagName_4;
		NullCheck(L_11);
		GameObject_set_tag_m0A41528AFD8C83E1CEC5D769921159897CDD2B24(L_11, L_12, NULL);
		// _instances[tagName] = this;
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_13 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		String_t* L_14 = __this->___tagName_4;
		NullCheck(L_13);
		Dictionary_2_set_Item_m45E21CB14A73F58BD606054CB89E38965210E75E(L_13, L_14, __this, Dictionary_2_set_Item_m45E21CB14A73F58BD606054CB89E38965210E75E_RuntimeMethod_var);
		// DontDestroyOnLoad(gameObject);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_15;
		L_15 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object_DontDestroyOnLoad_m303AA1C4DC810349F285B4809E426CBBA8F834F9(L_15, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.Singleton::OnDisable()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Singleton_OnDisable_m88BAEF57F153E27B1D8A47E20C0941B54034C48B (Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Remove_m13CE1B03E096BE40FECC8C7546831E80CD1A8D59_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBF48F5F1A4487D9161428D14DC40A698E4596F3E);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (_instances.ContainsKey("tagName") && _instances["tagName"] == this) {
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_0 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		NullCheck(L_0);
		bool L_1;
		L_1 = Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A(L_0, _stringLiteralBF48F5F1A4487D9161428D14DC40A698E4596F3E, Dictionary_2_ContainsKey_m9E18F1C6B7E329F19DDAE894D141AAA2F389B53A_RuntimeMethod_var);
		if (!L_1)
		{
			goto IL_0038;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_2 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		NullCheck(L_2);
		Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* L_3;
		L_3 = Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B(L_2, _stringLiteralBF48F5F1A4487D9161428D14DC40A698E4596F3E, Dictionary_2_get_Item_m92DBFC7298A7882F4B1EC6C1D0B49A90C9E5D79B_RuntimeMethod_var);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_4;
		L_4 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_3, __this, NULL);
		if (!L_4)
		{
			goto IL_0038;
		}
	}
	{
		// _instances.Remove("tagName");
		il2cpp_codegen_runtime_class_init_inline(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_5 = ((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5;
		NullCheck(L_5);
		bool L_6;
		L_6 = Dictionary_2_Remove_m13CE1B03E096BE40FECC8C7546831E80CD1A8D59(L_5, _stringLiteralBF48F5F1A4487D9161428D14DC40A698E4596F3E, Dictionary_2_Remove_m13CE1B03E096BE40FECC8C7546831E80CD1A8D59_RuntimeMethod_var);
	}

IL_0038:
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.Singleton::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Singleton__ctor_m014A8776BF7712061F3E93C777A2B2B97B03C242 (Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* __this, const RuntimeMethod* method) 
{
	{
		NoSuchMonoBehaviour__ctor_m58F2B53BD2C05B59A51818C9B3656C60AE0C55EE(__this, NULL);
		return;
	}
}
// System.Void NoSuchStudio.Common.Singleton::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Singleton__cctor_m53395643FBAED887F156550B9B45737E22BBB1A1 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m7F749610DCC2068FFABD81A4FAC6522D6C334632_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static Dictionary<string, Singleton> _instances = new Dictionary<string, Singleton>();
		Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD* L_0 = (Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD*)il2cpp_codegen_object_new(Dictionary_2_t3C34AB338C8C7F30F6B42B792EF04010310EDBCD_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Dictionary_2__ctor_m7F749610DCC2068FFABD81A4FAC6522D6C334632(L_0, Dictionary_2__ctor_m7F749610DCC2068FFABD81A4FAC6522D6C334632_RuntimeMethod_var);
		((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_StaticFields*)il2cpp_codegen_static_fields_for(Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_il2cpp_TypeInfo_var))->____instances_5), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.SingletonChildEnabler::OnEnable()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SingletonChildEnabler_OnEnable_mBB7EDE340B592653479556C6B69789072C92A369 (SingletonChildEnabler_t99ADDFC4CD734D0CF08078CA1CE1674AB472AC6D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Component_GetComponent_TisSingleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_m67CFFC259C315C7D32F39708EC5DE1D6B89FCBE2_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// if (!GetComponent<Singleton>().IsChosenSingleton) return;
		Singleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75* L_0;
		L_0 = Component_GetComponent_TisSingleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_m67CFFC259C315C7D32F39708EC5DE1D6B89FCBE2(__this, Component_GetComponent_TisSingleton_tE43D82C470F8B690205FF8D782AADA11B2BBCB75_m67CFFC259C315C7D32F39708EC5DE1D6B89FCBE2_RuntimeMethod_var);
		NullCheck(L_0);
		bool L_1;
		L_1 = Singleton_get_IsChosenSingleton_m604CEE054136DBF9D9BE920721E993FAC118730F(L_0, NULL);
		if (L_1)
		{
			goto IL_000e;
		}
	}
	{
		// if (!GetComponent<Singleton>().IsChosenSingleton) return;
		return;
	}

IL_000e:
	{
		// for (int i = 0; i < transform.childCount; i++) {
		V_0 = 0;
		goto IL_002d;
	}

IL_0012:
	{
		// transform.GetChild(i).gameObject.SetActive(true);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
		L_2 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		int32_t L_3 = V_0;
		NullCheck(L_2);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4;
		L_4 = Transform_GetChild_mE686DF0C7AAC1F7AEF356967B1C04D8B8E240EAF(L_2, L_3, NULL);
		NullCheck(L_4);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_5;
		L_5 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_4, NULL);
		NullCheck(L_5);
		GameObject_SetActive_m638E92E1E75E519E5B24CF150B08CA8E0CDFAB92(L_5, (bool)1, NULL);
		// for (int i = 0; i < transform.childCount; i++) {
		int32_t L_6 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_6, 1));
	}

IL_002d:
	{
		// for (int i = 0; i < transform.childCount; i++) {
		int32_t L_7 = V_0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8;
		L_8 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(__this, NULL);
		NullCheck(L_8);
		int32_t L_9;
		L_9 = Transform_get_childCount_mE9C29C702AB662CC540CA053EDE48BDAFA35B4B0(L_8, NULL);
		if ((((int32_t)L_7) < ((int32_t)L_9)))
		{
			goto IL_0012;
		}
	}
	{
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.SingletonChildEnabler::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SingletonChildEnabler__ctor_m24372497155C225768129B8AE0EF729727FBF065 (SingletonChildEnabler_t99ADDFC4CD734D0CF08078CA1CE1674AB472AC6D* __this, const RuntimeMethod* method) 
{
	{
		NoSuchMonoBehaviour__ctor_m58F2B53BD2C05B59A51818C9B3656C60AE0C55EE(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.TransformExt::ClearChildren(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TransformExt_ClearChildren_m87F939A08B4A82E61EDC2F7A565514D877879E02 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___t0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		// if (Application.isEditor && !Application.isPlaying) {
		bool L_0;
		L_0 = Application_get_isEditor_m0377DB707B566C8E21DA3CD99963210F6D57D234(NULL);
		if (!L_0)
		{
			goto IL_0033;
		}
	}
	{
		bool L_1;
		L_1 = Application_get_isPlaying_m0B3B501E1093739F8887A0DAC5F61D9CB49CC337(NULL);
		if (L_1)
		{
			goto IL_0033;
		}
	}
	{
		// for (int i = t.childCount - 1; i >= 0; i--) {
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = ___t0;
		NullCheck(L_2);
		int32_t L_3;
		L_3 = Transform_get_childCount_mE9C29C702AB662CC540CA053EDE48BDAFA35B4B0(L_2, NULL);
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_3, 1));
		goto IL_002e;
	}

IL_0019:
	{
		// GameObject.DestroyImmediate(t.GetChild(i).gameObject);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4 = ___t0;
		int32_t L_5 = V_0;
		NullCheck(L_4);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
		L_6 = Transform_GetChild_mE686DF0C7AAC1F7AEF356967B1C04D8B8E240EAF(L_4, L_5, NULL);
		NullCheck(L_6);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_7;
		L_7 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_6, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object_DestroyImmediate_m8249CABCDF344BE3A67EE765122EBB415DC2BC57(L_7, NULL);
		// for (int i = t.childCount - 1; i >= 0; i--) {
		int32_t L_8 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, 1));
	}

IL_002e:
	{
		// for (int i = t.childCount - 1; i >= 0; i--) {
		int32_t L_9 = V_0;
		if ((((int32_t)L_9) >= ((int32_t)0)))
		{
			goto IL_0019;
		}
	}
	{
		return;
	}

IL_0033:
	{
		// for (int i = t.childCount - 1; i >= 0; i--) {
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = ___t0;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = Transform_get_childCount_mE9C29C702AB662CC540CA053EDE48BDAFA35B4B0(L_10, NULL);
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_11, 1));
		goto IL_0053;
	}

IL_003e:
	{
		// GameObject.Destroy(t.GetChild(i).gameObject);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_12 = ___t0;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14;
		L_14 = Transform_GetChild_mE686DF0C7AAC1F7AEF356967B1C04D8B8E240EAF(L_12, L_13, NULL);
		NullCheck(L_14);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_15;
		L_15 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_14, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object_Destroy_mFCDAE6333522488F60597AF019EA90BB1207A5AA(L_15, NULL);
		// for (int i = t.childCount - 1; i >= 0; i--) {
		int32_t L_16 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_16, 1));
	}

IL_0053:
	{
		// for (int i = t.childCount - 1; i >= 0; i--) {
		int32_t L_17 = V_1;
		if ((((int32_t)L_17) >= ((int32_t)0)))
		{
			goto IL_003e;
		}
	}
	{
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// UnityEngine.Vector2 NoSuchStudio.Common.UIExts::GetSnapToPositionToBringChildIntoView(UnityEngine.UI.ScrollRect,UnityEngine.RectTransform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 UIExts_GetSnapToPositionToBringChildIntoView_mA2DCCFA22A1B5AE7919262E04B04B9B5423F3C9E (ScrollRect_t17D2F2939CA8953110180DF53164CFC3DC88D70E* ___instance0, RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___child1, const RuntimeMethod* method) 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// Canvas.ForceUpdateCanvases();
		Canvas_ForceUpdateCanvases_m29B1B008CA6C4A3CF623A0A86ACE5C8AA4C2B0C1(NULL);
		// Vector2 viewportLocalPosition = instance.viewport.localPosition;
		ScrollRect_t17D2F2939CA8953110180DF53164CFC3DC88D70E* L_0 = ___instance0;
		NullCheck(L_0);
		RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* L_1;
		L_1 = ScrollRect_get_viewport_m85092216DD476F77E78F5CE50F9C4E70063ECCF9_inline(L_0, NULL);
		NullCheck(L_1);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Transform_get_localPosition_mA9C86B990DF0685EA1061A120218993FDCC60A95(L_1, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_3;
		L_3 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_2, NULL);
		V_0 = L_3;
		// Vector2 childLocalPosition = child.localPosition;
		RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* L_4 = ___child1;
		NullCheck(L_4);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_get_localPosition_mA9C86B990DF0685EA1061A120218993FDCC60A95(L_4, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6;
		L_6 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_5, NULL);
		V_1 = L_6;
		// Vector2 result = new Vector2(
		//     0 - (viewportLocalPosition.x + childLocalPosition.x),
		//     0 - (viewportLocalPosition.y + childLocalPosition.y)
		// );
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_7 = V_0;
		float L_8 = L_7.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_9 = V_1;
		float L_10 = L_9.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_11 = V_0;
		float L_12 = L_11.___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_13 = V_1;
		float L_14 = L_13.___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_15;
		memset((&L_15), 0, sizeof(L_15));
		Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&L_15), ((float)il2cpp_codegen_subtract((0.0f), ((float)il2cpp_codegen_add(L_8, L_10)))), ((float)il2cpp_codegen_subtract((0.0f), ((float)il2cpp_codegen_add(L_12, L_14)))), /*hidden argument*/NULL);
		// return result;
		return L_15;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.LoggerConfig::.ctor(System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LoggerConfig__ctor_m53267D4702C573947E2EA33FBF821B9C8547E303 (LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* __this, String_t* ___className0, bool ___logClassName1, bool ___logGameObjectName2, bool ___logThreadId3, bool ___logGameTime4, const RuntimeMethod* method) 
{
	{
		// public LoggerConfig(string className, bool logClassName = true, bool logGameObjectName = true, bool logThreadId = true, bool logGameTime = true) {
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// this.className = className;
		String_t* L_0 = ___className0;
		__this->___className_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___className_0), (void*)L_0);
		// this.logClassName = logClassName;
		bool L_1 = ___logClassName1;
		__this->___logClassName_1 = L_1;
		// this.logGameObjectName = logGameObjectName;
		bool L_2 = ___logGameObjectName2;
		__this->___logGameObjectName_2 = L_2;
		// this.logThreadId = logThreadId;
		bool L_3 = ___logThreadId3;
		__this->___logThreadId_3 = L_3;
		// this.logGameTime = logGameTime;
		bool L_4 = ___logGameTime4;
		__this->___logGameTime_4 = L_4;
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogFormat(UnityEngine.Object,UnityEngine.LogType,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogFormat_m06BD26D581CBA64E3422A043A782DC663BDB12D3 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, int32_t ___logType1, String_t* ___format2, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral10A105116F1400FFCE661E402C3C12DDCA0D688C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB3F14BF976EFD974E34846B742502C802FABAE9D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC087E631060AB76B7C814C0E1B92D5C7C4C4B924);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC);
		s_Il2CppMethodInitialized = true;
	}
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* V_0 = NULL;
	LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* V_1 = NULL;
	int32_t V_2 = 0;
	float V_3 = 0.0f;
	int32_t G_B2_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B2_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B2_2 = NULL;
	String_t* G_B2_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B2_4 = NULL;
	int32_t G_B2_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B2_6 = NULL;
	int32_t G_B1_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B1_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B1_2 = NULL;
	String_t* G_B1_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B1_4 = NULL;
	int32_t G_B1_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B1_6 = NULL;
	String_t* G_B3_0 = NULL;
	int32_t G_B3_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B3_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B3_3 = NULL;
	String_t* G_B3_4 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B3_5 = NULL;
	int32_t G_B3_6 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B3_7 = NULL;
	int32_t G_B5_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B5_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B5_2 = NULL;
	String_t* G_B5_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B5_4 = NULL;
	int32_t G_B5_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B5_6 = NULL;
	int32_t G_B4_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B4_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B4_2 = NULL;
	String_t* G_B4_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B4_4 = NULL;
	int32_t G_B4_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B4_6 = NULL;
	String_t* G_B6_0 = NULL;
	int32_t G_B6_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B6_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B6_3 = NULL;
	String_t* G_B6_4 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B6_5 = NULL;
	int32_t G_B6_6 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B6_7 = NULL;
	int32_t G_B8_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B8_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B8_2 = NULL;
	String_t* G_B8_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B8_4 = NULL;
	int32_t G_B8_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B8_6 = NULL;
	int32_t G_B7_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B7_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B7_2 = NULL;
	String_t* G_B7_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B7_4 = NULL;
	int32_t G_B7_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B7_6 = NULL;
	String_t* G_B9_0 = NULL;
	int32_t G_B9_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B9_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B9_3 = NULL;
	String_t* G_B9_4 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B9_5 = NULL;
	int32_t G_B9_6 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B9_7 = NULL;
	int32_t G_B11_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B11_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B11_2 = NULL;
	String_t* G_B11_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B11_4 = NULL;
	int32_t G_B11_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B11_6 = NULL;
	int32_t G_B10_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B10_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B10_2 = NULL;
	String_t* G_B10_3 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B10_4 = NULL;
	int32_t G_B10_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B10_6 = NULL;
	String_t* G_B12_0 = NULL;
	int32_t G_B12_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B12_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B12_3 = NULL;
	String_t* G_B12_4 = NULL;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* G_B12_5 = NULL;
	int32_t G_B12_6 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B12_7 = NULL;
	{
		// (Logger logger, LoggerConfig lc) = GetLoggerByType(unityObj.GetType());
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		NullCheck(L_0);
		Type_t* L_1;
		L_1 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(L_0, NULL);
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_2;
		L_2 = UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53(L_1, NULL);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_3 = L_2;
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_4 = L_3.___Item1_0;
		V_0 = L_4;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_5 = L_3.___Item2_1;
		V_1 = L_5;
		// logger.LogFormat(logType, unityObj, string.Format("{0}{1}{2}{3}{4}",
		//     lc.logThreadId ? "[" + Thread.CurrentThread.ManagedThreadId.ToString() + "] " : "",
		//     lc.logGameTime ? "[" + Time.time + "]" : "",
		//     lc.logClassName ? "(" + lc.className + ")" : "",
		//     lc.logGameObjectName ? "(" + unityObj.name + ") " : "",
		//     format),
		//     args);
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_6 = V_0;
		int32_t L_7 = ___logType1;
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_8 = ___unityObj0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)5);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_10 = L_9;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_11 = V_1;
		NullCheck(L_11);
		bool L_12 = L_11->___logThreadId_3;
		G_B1_0 = 0;
		G_B1_1 = L_10;
		G_B1_2 = L_10;
		G_B1_3 = _stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A;
		G_B1_4 = L_8;
		G_B1_5 = L_7;
		G_B1_6 = L_6;
		if (L_12)
		{
			G_B2_0 = 0;
			G_B2_1 = L_10;
			G_B2_2 = L_10;
			G_B2_3 = _stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A;
			G_B2_4 = L_8;
			G_B2_5 = L_7;
			G_B2_6 = L_6;
			goto IL_0037;
		}
	}
	{
		G_B3_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B3_1 = G_B1_0;
		G_B3_2 = G_B1_1;
		G_B3_3 = G_B1_2;
		G_B3_4 = G_B1_3;
		G_B3_5 = G_B1_4;
		G_B3_6 = G_B1_5;
		G_B3_7 = G_B1_6;
		goto IL_0058;
	}

IL_0037:
	{
		Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* L_13;
		L_13 = Thread_get_CurrentThread_m835AD1DF1C0D10BABE1A5427CC4B357C991B25AB(NULL);
		NullCheck(L_13);
		int32_t L_14;
		L_14 = Thread_get_ManagedThreadId_m74ACB74A574EE535C2B00B7D64F203A62E796B05(L_13, NULL);
		V_2 = L_14;
		String_t* L_15;
		L_15 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_2), NULL);
		String_t* L_16;
		L_16 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1, L_15, _stringLiteral10A105116F1400FFCE661E402C3C12DDCA0D688C, NULL);
		G_B3_0 = L_16;
		G_B3_1 = G_B2_0;
		G_B3_2 = G_B2_1;
		G_B3_3 = G_B2_2;
		G_B3_4 = G_B2_3;
		G_B3_5 = G_B2_4;
		G_B3_6 = G_B2_5;
		G_B3_7 = G_B2_6;
	}

IL_0058:
	{
		NullCheck(G_B3_2);
		ArrayElementTypeCheck (G_B3_2, G_B3_0);
		(G_B3_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B3_1), (RuntimeObject*)G_B3_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_17 = G_B3_3;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_18 = V_1;
		NullCheck(L_18);
		bool L_19 = L_18->___logGameTime_4;
		G_B4_0 = 1;
		G_B4_1 = L_17;
		G_B4_2 = L_17;
		G_B4_3 = G_B3_4;
		G_B4_4 = G_B3_5;
		G_B4_5 = G_B3_6;
		G_B4_6 = G_B3_7;
		if (L_19)
		{
			G_B5_0 = 1;
			G_B5_1 = L_17;
			G_B5_2 = L_17;
			G_B5_3 = G_B3_4;
			G_B5_4 = G_B3_5;
			G_B5_5 = G_B3_6;
			G_B5_6 = G_B3_7;
			goto IL_006a;
		}
	}
	{
		G_B6_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B6_1 = G_B4_0;
		G_B6_2 = G_B4_1;
		G_B6_3 = G_B4_2;
		G_B6_4 = G_B4_3;
		G_B6_5 = G_B4_4;
		G_B6_6 = G_B4_5;
		G_B6_7 = G_B4_6;
		goto IL_0086;
	}

IL_006a:
	{
		float L_20;
		L_20 = Time_get_time_m0BEE9AACD0723FE414465B77C9C64D12263675F3(NULL);
		V_3 = L_20;
		String_t* L_21;
		L_21 = Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972((&V_3), NULL);
		String_t* L_22;
		L_22 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1, L_21, _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC, NULL);
		G_B6_0 = L_22;
		G_B6_1 = G_B5_0;
		G_B6_2 = G_B5_1;
		G_B6_3 = G_B5_2;
		G_B6_4 = G_B5_3;
		G_B6_5 = G_B5_4;
		G_B6_6 = G_B5_5;
		G_B6_7 = G_B5_6;
	}

IL_0086:
	{
		NullCheck(G_B6_2);
		ArrayElementTypeCheck (G_B6_2, G_B6_0);
		(G_B6_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B6_1), (RuntimeObject*)G_B6_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_23 = G_B6_3;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_24 = V_1;
		NullCheck(L_24);
		bool L_25 = L_24->___logClassName_1;
		G_B7_0 = 2;
		G_B7_1 = L_23;
		G_B7_2 = L_23;
		G_B7_3 = G_B6_4;
		G_B7_4 = G_B6_5;
		G_B7_5 = G_B6_6;
		G_B7_6 = G_B6_7;
		if (L_25)
		{
			G_B8_0 = 2;
			G_B8_1 = L_23;
			G_B8_2 = L_23;
			G_B8_3 = G_B6_4;
			G_B8_4 = G_B6_5;
			G_B8_5 = G_B6_6;
			G_B8_6 = G_B6_7;
			goto IL_0098;
		}
	}
	{
		G_B9_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B9_1 = G_B7_0;
		G_B9_2 = G_B7_1;
		G_B9_3 = G_B7_2;
		G_B9_4 = G_B7_3;
		G_B9_5 = G_B7_4;
		G_B9_6 = G_B7_5;
		G_B9_7 = G_B7_6;
		goto IL_00ad;
	}

IL_0098:
	{
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_26 = V_1;
		NullCheck(L_26);
		String_t* L_27 = L_26->___className_0;
		String_t* L_28;
		L_28 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73, L_27, _stringLiteralB3F14BF976EFD974E34846B742502C802FABAE9D, NULL);
		G_B9_0 = L_28;
		G_B9_1 = G_B8_0;
		G_B9_2 = G_B8_1;
		G_B9_3 = G_B8_2;
		G_B9_4 = G_B8_3;
		G_B9_5 = G_B8_4;
		G_B9_6 = G_B8_5;
		G_B9_7 = G_B8_6;
	}

IL_00ad:
	{
		NullCheck(G_B9_2);
		ArrayElementTypeCheck (G_B9_2, G_B9_0);
		(G_B9_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B9_1), (RuntimeObject*)G_B9_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_29 = G_B9_3;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_30 = V_1;
		NullCheck(L_30);
		bool L_31 = L_30->___logGameObjectName_2;
		G_B10_0 = 3;
		G_B10_1 = L_29;
		G_B10_2 = L_29;
		G_B10_3 = G_B9_4;
		G_B10_4 = G_B9_5;
		G_B10_5 = G_B9_6;
		G_B10_6 = G_B9_7;
		if (L_31)
		{
			G_B11_0 = 3;
			G_B11_1 = L_29;
			G_B11_2 = L_29;
			G_B11_3 = G_B9_4;
			G_B11_4 = G_B9_5;
			G_B11_5 = G_B9_6;
			G_B11_6 = G_B9_7;
			goto IL_00bf;
		}
	}
	{
		G_B12_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B12_1 = G_B10_0;
		G_B12_2 = G_B10_1;
		G_B12_3 = G_B10_2;
		G_B12_4 = G_B10_3;
		G_B12_5 = G_B10_4;
		G_B12_6 = G_B10_5;
		G_B12_7 = G_B10_6;
		goto IL_00d4;
	}

IL_00bf:
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_32 = ___unityObj0;
		NullCheck(L_32);
		String_t* L_33;
		L_33 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(L_32, NULL);
		String_t* L_34;
		L_34 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73, L_33, _stringLiteralC087E631060AB76B7C814C0E1B92D5C7C4C4B924, NULL);
		G_B12_0 = L_34;
		G_B12_1 = G_B11_0;
		G_B12_2 = G_B11_1;
		G_B12_3 = G_B11_2;
		G_B12_4 = G_B11_3;
		G_B12_5 = G_B11_4;
		G_B12_6 = G_B11_5;
		G_B12_7 = G_B11_6;
	}

IL_00d4:
	{
		NullCheck(G_B12_2);
		ArrayElementTypeCheck (G_B12_2, G_B12_0);
		(G_B12_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B12_1), (RuntimeObject*)G_B12_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_35 = G_B12_3;
		String_t* L_36 = ___format2;
		NullCheck(L_35);
		ArrayElementTypeCheck (L_35, L_36);
		(L_35)->SetAt(static_cast<il2cpp_array_size_t>(4), (RuntimeObject*)L_36);
		String_t* L_37;
		L_37 = String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55(G_B12_4, L_35, NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_38 = ___args3;
		NullCheck(G_B12_7);
		Logger_LogFormat_m776A546E755F914039AB8591E23D08510308DB4C(G_B12_7, G_B12_6, G_B12_5, L_37, L_38, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::Log(UnityEngine.Object,UnityEngine.LogType,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_Log_m82862BA4CFCAB632BB2147B63E68274C378C8A31 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, int32_t ___logType1, String_t* ___msg2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral10A105116F1400FFCE661E402C3C12DDCA0D688C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB3F14BF976EFD974E34846B742502C802FABAE9D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC087E631060AB76B7C814C0E1B92D5C7C4C4B924);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC);
		s_Il2CppMethodInitialized = true;
	}
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* V_0 = NULL;
	LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* V_1 = NULL;
	int32_t V_2 = 0;
	float V_3 = 0.0f;
	int32_t G_B2_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B2_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B2_2 = NULL;
	String_t* G_B2_3 = NULL;
	int32_t G_B2_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B2_5 = NULL;
	int32_t G_B1_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B1_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B1_2 = NULL;
	String_t* G_B1_3 = NULL;
	int32_t G_B1_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B1_5 = NULL;
	String_t* G_B3_0 = NULL;
	int32_t G_B3_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B3_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B3_3 = NULL;
	String_t* G_B3_4 = NULL;
	int32_t G_B3_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B3_6 = NULL;
	int32_t G_B5_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B5_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B5_2 = NULL;
	String_t* G_B5_3 = NULL;
	int32_t G_B5_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B5_5 = NULL;
	int32_t G_B4_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B4_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B4_2 = NULL;
	String_t* G_B4_3 = NULL;
	int32_t G_B4_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B4_5 = NULL;
	String_t* G_B6_0 = NULL;
	int32_t G_B6_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B6_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B6_3 = NULL;
	String_t* G_B6_4 = NULL;
	int32_t G_B6_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B6_6 = NULL;
	int32_t G_B8_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B8_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B8_2 = NULL;
	String_t* G_B8_3 = NULL;
	int32_t G_B8_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B8_5 = NULL;
	int32_t G_B7_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B7_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B7_2 = NULL;
	String_t* G_B7_3 = NULL;
	int32_t G_B7_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B7_5 = NULL;
	String_t* G_B9_0 = NULL;
	int32_t G_B9_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B9_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B9_3 = NULL;
	String_t* G_B9_4 = NULL;
	int32_t G_B9_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B9_6 = NULL;
	int32_t G_B11_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B11_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B11_2 = NULL;
	String_t* G_B11_3 = NULL;
	int32_t G_B11_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B11_5 = NULL;
	int32_t G_B10_0 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B10_1 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B10_2 = NULL;
	String_t* G_B10_3 = NULL;
	int32_t G_B10_4 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B10_5 = NULL;
	String_t* G_B12_0 = NULL;
	int32_t G_B12_1 = 0;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B12_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* G_B12_3 = NULL;
	String_t* G_B12_4 = NULL;
	int32_t G_B12_5 = 0;
	Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* G_B12_6 = NULL;
	{
		// (Logger logger, LoggerConfig lc) = GetLoggerByType(unityObj.GetType());
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		NullCheck(L_0);
		Type_t* L_1;
		L_1 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(L_0, NULL);
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_2;
		L_2 = UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53(L_1, NULL);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_3 = L_2;
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_4 = L_3.___Item1_0;
		V_0 = L_4;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_5 = L_3.___Item2_1;
		V_1 = L_5;
		// logger.Log(logType, (object)string.Format("{0}{1}{2}{3}{4}",
		//         lc.logThreadId ? "[" + Thread.CurrentThread.ManagedThreadId.ToString() + "] " : "",
		//         lc.logGameTime ? "[" + Time.time + "]" : "",
		//         lc.logClassName ? "(" + lc.className + ")" : "",
		//         lc.logGameObjectName ? "(" + unityObj.name + ") " : "",
		//         msg),
		//     unityObj);
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_6 = V_0;
		int32_t L_7 = ___logType1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_8 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)5);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_8;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_10 = V_1;
		NullCheck(L_10);
		bool L_11 = L_10->___logThreadId_3;
		G_B1_0 = 0;
		G_B1_1 = L_9;
		G_B1_2 = L_9;
		G_B1_3 = _stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A;
		G_B1_4 = L_7;
		G_B1_5 = L_6;
		if (L_11)
		{
			G_B2_0 = 0;
			G_B2_1 = L_9;
			G_B2_2 = L_9;
			G_B2_3 = _stringLiteral3673FF002279CCCC33A113C482FD3DB9FE3D429A;
			G_B2_4 = L_7;
			G_B2_5 = L_6;
			goto IL_0036;
		}
	}
	{
		G_B3_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B3_1 = G_B1_0;
		G_B3_2 = G_B1_1;
		G_B3_3 = G_B1_2;
		G_B3_4 = G_B1_3;
		G_B3_5 = G_B1_4;
		G_B3_6 = G_B1_5;
		goto IL_0057;
	}

IL_0036:
	{
		Thread_t0A773B9DE873D2DCAA7D229EAB36757B500E207F* L_12;
		L_12 = Thread_get_CurrentThread_m835AD1DF1C0D10BABE1A5427CC4B357C991B25AB(NULL);
		NullCheck(L_12);
		int32_t L_13;
		L_13 = Thread_get_ManagedThreadId_m74ACB74A574EE535C2B00B7D64F203A62E796B05(L_12, NULL);
		V_2 = L_13;
		String_t* L_14;
		L_14 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_2), NULL);
		String_t* L_15;
		L_15 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1, L_14, _stringLiteral10A105116F1400FFCE661E402C3C12DDCA0D688C, NULL);
		G_B3_0 = L_15;
		G_B3_1 = G_B2_0;
		G_B3_2 = G_B2_1;
		G_B3_3 = G_B2_2;
		G_B3_4 = G_B2_3;
		G_B3_5 = G_B2_4;
		G_B3_6 = G_B2_5;
	}

IL_0057:
	{
		NullCheck(G_B3_2);
		ArrayElementTypeCheck (G_B3_2, G_B3_0);
		(G_B3_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B3_1), (RuntimeObject*)G_B3_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_16 = G_B3_3;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_17 = V_1;
		NullCheck(L_17);
		bool L_18 = L_17->___logGameTime_4;
		G_B4_0 = 1;
		G_B4_1 = L_16;
		G_B4_2 = L_16;
		G_B4_3 = G_B3_4;
		G_B4_4 = G_B3_5;
		G_B4_5 = G_B3_6;
		if (L_18)
		{
			G_B5_0 = 1;
			G_B5_1 = L_16;
			G_B5_2 = L_16;
			G_B5_3 = G_B3_4;
			G_B5_4 = G_B3_5;
			G_B5_5 = G_B3_6;
			goto IL_0069;
		}
	}
	{
		G_B6_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B6_1 = G_B4_0;
		G_B6_2 = G_B4_1;
		G_B6_3 = G_B4_2;
		G_B6_4 = G_B4_3;
		G_B6_5 = G_B4_4;
		G_B6_6 = G_B4_5;
		goto IL_0085;
	}

IL_0069:
	{
		float L_19;
		L_19 = Time_get_time_m0BEE9AACD0723FE414465B77C9C64D12263675F3(NULL);
		V_3 = L_19;
		String_t* L_20;
		L_20 = Single_ToString_mE282EDA9CA4F7DF88432D807732837A629D04972((&V_3), NULL);
		String_t* L_21;
		L_21 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralD9691C4FD8A1F6B09DB1147CA32B442772FB46A1, L_20, _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC, NULL);
		G_B6_0 = L_21;
		G_B6_1 = G_B5_0;
		G_B6_2 = G_B5_1;
		G_B6_3 = G_B5_2;
		G_B6_4 = G_B5_3;
		G_B6_5 = G_B5_4;
		G_B6_6 = G_B5_5;
	}

IL_0085:
	{
		NullCheck(G_B6_2);
		ArrayElementTypeCheck (G_B6_2, G_B6_0);
		(G_B6_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B6_1), (RuntimeObject*)G_B6_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_22 = G_B6_3;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_23 = V_1;
		NullCheck(L_23);
		bool L_24 = L_23->___logClassName_1;
		G_B7_0 = 2;
		G_B7_1 = L_22;
		G_B7_2 = L_22;
		G_B7_3 = G_B6_4;
		G_B7_4 = G_B6_5;
		G_B7_5 = G_B6_6;
		if (L_24)
		{
			G_B8_0 = 2;
			G_B8_1 = L_22;
			G_B8_2 = L_22;
			G_B8_3 = G_B6_4;
			G_B8_4 = G_B6_5;
			G_B8_5 = G_B6_6;
			goto IL_0097;
		}
	}
	{
		G_B9_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B9_1 = G_B7_0;
		G_B9_2 = G_B7_1;
		G_B9_3 = G_B7_2;
		G_B9_4 = G_B7_3;
		G_B9_5 = G_B7_4;
		G_B9_6 = G_B7_5;
		goto IL_00ac;
	}

IL_0097:
	{
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_25 = V_1;
		NullCheck(L_25);
		String_t* L_26 = L_25->___className_0;
		String_t* L_27;
		L_27 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73, L_26, _stringLiteralB3F14BF976EFD974E34846B742502C802FABAE9D, NULL);
		G_B9_0 = L_27;
		G_B9_1 = G_B8_0;
		G_B9_2 = G_B8_1;
		G_B9_3 = G_B8_2;
		G_B9_4 = G_B8_3;
		G_B9_5 = G_B8_4;
		G_B9_6 = G_B8_5;
	}

IL_00ac:
	{
		NullCheck(G_B9_2);
		ArrayElementTypeCheck (G_B9_2, G_B9_0);
		(G_B9_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B9_1), (RuntimeObject*)G_B9_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_28 = G_B9_3;
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_29 = V_1;
		NullCheck(L_29);
		bool L_30 = L_29->___logGameObjectName_2;
		G_B10_0 = 3;
		G_B10_1 = L_28;
		G_B10_2 = L_28;
		G_B10_3 = G_B9_4;
		G_B10_4 = G_B9_5;
		G_B10_5 = G_B9_6;
		if (L_30)
		{
			G_B11_0 = 3;
			G_B11_1 = L_28;
			G_B11_2 = L_28;
			G_B11_3 = G_B9_4;
			G_B11_4 = G_B9_5;
			G_B11_5 = G_B9_6;
			goto IL_00be;
		}
	}
	{
		G_B12_0 = _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
		G_B12_1 = G_B10_0;
		G_B12_2 = G_B10_1;
		G_B12_3 = G_B10_2;
		G_B12_4 = G_B10_3;
		G_B12_5 = G_B10_4;
		G_B12_6 = G_B10_5;
		goto IL_00d3;
	}

IL_00be:
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_31 = ___unityObj0;
		NullCheck(L_31);
		String_t* L_32;
		L_32 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(L_31, NULL);
		String_t* L_33;
		L_33 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(_stringLiteralA3DFC0C77ACADE0EE48DCC73E795A597D0270A73, L_32, _stringLiteralC087E631060AB76B7C814C0E1B92D5C7C4C4B924, NULL);
		G_B12_0 = L_33;
		G_B12_1 = G_B11_0;
		G_B12_2 = G_B11_1;
		G_B12_3 = G_B11_2;
		G_B12_4 = G_B11_3;
		G_B12_5 = G_B11_4;
		G_B12_6 = G_B11_5;
	}

IL_00d3:
	{
		NullCheck(G_B12_2);
		ArrayElementTypeCheck (G_B12_2, G_B12_0);
		(G_B12_2)->SetAt(static_cast<il2cpp_array_size_t>(G_B12_1), (RuntimeObject*)G_B12_0);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_34 = G_B12_3;
		String_t* L_35 = ___msg2;
		NullCheck(L_34);
		ArrayElementTypeCheck (L_34, L_35);
		(L_34)->SetAt(static_cast<il2cpp_array_size_t>(4), (RuntimeObject*)L_35);
		String_t* L_36;
		L_36 = String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55(G_B12_4, L_34, NULL);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_37 = ___unityObj0;
		NullCheck(G_B12_6);
		Logger_Log_mF8C7E8A8CC31E04732044D73D2CB551D7CCB8995(G_B12_6, G_B12_5, L_36, L_37, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogLogFormat(UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogLogFormat_mEF4688871A7D53518B12307F907E452E5D934513 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___format1, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// LogFormat(unityObj, LogType.Log, format, args);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		String_t* L_1 = ___format1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___args2;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogFormat_m06BD26D581CBA64E3422A043A782DC663BDB12D3(L_0, 3, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogLog(UnityEngine.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogLog_mC174F3944DBBF72B5667393163D3CBBFF440AB30 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___msg1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Log(unityObj, LogType.Log, msg);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		String_t* L_1 = ___msg1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_Log_m82862BA4CFCAB632BB2147B63E68274C378C8A31(L_0, 3, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogWarnFormat(UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogWarnFormat_m18CFBC606E7A4660BCFC38C759271265CA589FB2 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___format1, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// LogFormat(unityObj, LogType.Warning, format, args);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		String_t* L_1 = ___format1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___args2;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogFormat_m06BD26D581CBA64E3422A043A782DC663BDB12D3(L_0, 2, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogWarn(UnityEngine.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogWarn_mB1F6307AF886FDE0D443B5AFFF6E674EDBE41EDA (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___msg1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Log(unityObj, LogType.Warning, msg);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		String_t* L_1 = ___msg1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_Log_m82862BA4CFCAB632BB2147B63E68274C378C8A31(L_0, 2, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogErrorFormat(UnityEngine.Object,System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogErrorFormat_m40A9D1D33A5FE6D11D78DE280141F87EF9221D81 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___format1, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// LogFormat(unityObj, LogType.Error, format, args);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		String_t* L_1 = ___format1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___args2;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_LogFormat_m06BD26D581CBA64E3422A043A782DC663BDB12D3(L_0, 0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::LogError(UnityEngine.Object,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_LogError_m364179587BD3CA7C881454C95564305B5A91F612 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___unityObj0, String_t* ___msg1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Log(unityObj, LogType.Error, msg);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = ___unityObj0;
		String_t* L_1 = ___msg1;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_Log_m82862BA4CFCAB632BB2147B63E68274C378C8A31(L_0, 0, L_1, NULL);
		// }
		return;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::AddType(System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt_AddType_mD9ABC1EB73654B6A28262B40802BF5C857A34E92 (Type_t* ___type0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_m6917FFC8B47B29FC2E7A65BA0C61EAF0C8ABF3F1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m700A5670F3CB7E83C52F2590D17EF521324F2430_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILogger_tD1F573C6DC829FBA987FA1EBA0A5FA64E0C2BC42_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ValueTuple_2__ctor_m704CDA27B90CDBBAE2DC59E142CCEA85ABCEAD3B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral23114468D04FA2B7A2DA455B545DB914D0A3ED94);
		s_Il2CppMethodInitialized = true;
	}
	LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* V_0 = NULL;
	{
		// if (!loggers.ContainsKey(type)) {
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* L_0 = ((UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_StaticFields*)il2cpp_codegen_static_fields_for(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var))->___loggers_0;
		Type_t* L_1 = ___type0;
		NullCheck(L_0);
		bool L_2;
		L_2 = Dictionary_2_ContainsKey_m700A5670F3CB7E83C52F2590D17EF521324F2430(L_0, L_1, Dictionary_2_ContainsKey_m700A5670F3CB7E83C52F2590D17EF521324F2430_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0047;
		}
	}
	{
		// LoggerConfig lc = new LoggerConfig(string.Format("{0}", type.Name));
		Type_t* L_3 = ___type0;
		NullCheck(L_3);
		String_t* L_4;
		L_4 = VirtualFuncInvoker0< String_t* >::Invoke(8 /* System.String System.Reflection.MemberInfo::get_Name() */, L_3);
		String_t* L_5;
		L_5 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(_stringLiteral23114468D04FA2B7A2DA455B545DB914D0A3ED94, L_4, NULL);
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_6 = (LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6*)il2cpp_codegen_object_new(LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6_il2cpp_TypeInfo_var);
		NullCheck(L_6);
		LoggerConfig__ctor_m53267D4702C573947E2EA33FBF821B9C8547E303(L_6, L_5, (bool)1, (bool)1, (bool)1, (bool)1, NULL);
		V_0 = L_6;
		// loggers.Add(type, (new Logger(Debug.unityLogger.logHandler), lc));
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* L_7 = ((UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_StaticFields*)il2cpp_codegen_static_fields_for(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var))->___loggers_0;
		Type_t* L_8 = ___type0;
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		RuntimeObject* L_9;
		L_9 = Debug_get_unityLogger_mA872400E9E585FCD6A2DE1717748A458545DE8A4_inline(NULL);
		NullCheck(L_9);
		RuntimeObject* L_10;
		L_10 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* UnityEngine.ILogHandler UnityEngine.ILogger::get_logHandler() */, ILogger_tD1F573C6DC829FBA987FA1EBA0A5FA64E0C2BC42_il2cpp_TypeInfo_var, L_9);
		Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0* L_11 = (Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0*)il2cpp_codegen_object_new(Logger_t608FFEA1E140B6BE2CCB01C86ACB219533C172A0_il2cpp_TypeInfo_var);
		NullCheck(L_11);
		Logger__ctor_m3155E21A68AA616431A260A3FCBB4B074DF6FAA2(L_11, L_10, NULL);
		LoggerConfig_tBCB9CE13E8F0BB1CD91600C4539729D9ECB95BE6* L_12 = V_0;
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_13;
		memset((&L_13), 0, sizeof(L_13));
		ValueTuple_2__ctor_m704CDA27B90CDBBAE2DC59E142CCEA85ABCEAD3B((&L_13), L_11, L_12, /*hidden argument*/ValueTuple_2__ctor_m704CDA27B90CDBBAE2DC59E142CCEA85ABCEAD3B_RuntimeMethod_var);
		NullCheck(L_7);
		Dictionary_2_Add_m6917FFC8B47B29FC2E7A65BA0C61EAF0C8ABF3F1(L_7, L_8, L_13, Dictionary_2_Add_m6917FFC8B47B29FC2E7A65BA0C61EAF0C8ABF3F1_RuntimeMethod_var);
	}

IL_0047:
	{
		// }
		return;
	}
}
// System.ValueTuple`2<UnityEngine.Logger,NoSuchStudio.Common.LoggerConfig> NoSuchStudio.Common.UnityObjectLoggerExt::GetLoggerByType(System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F UnityObjectLoggerExt_GetLoggerByType_mE98B03C02E105837B6770FBFE64347352E241B53 (Type_t* ___type0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Item_mD046F6B66CAC9023A3AC965DD99BAE431D3F31D4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// AddType(type);
		Type_t* L_0 = ___type0;
		il2cpp_codegen_runtime_class_init_inline(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		UnityObjectLoggerExt_AddType_mD9ABC1EB73654B6A28262B40802BF5C857A34E92(L_0, NULL);
		// return loggers[type];
		Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* L_1 = ((UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_StaticFields*)il2cpp_codegen_static_fields_for(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var))->___loggers_0;
		Type_t* L_2 = ___type0;
		NullCheck(L_1);
		ValueTuple_2_t34ACE0964D1CAAF9D09B5AB2CC706220CD1C1D7F L_3;
		L_3 = Dictionary_2_get_Item_mD046F6B66CAC9023A3AC965DD99BAE431D3F31D4(L_1, L_2, Dictionary_2_get_Item_mD046F6B66CAC9023A3AC965DD99BAE431D3F31D4_RuntimeMethod_var);
		return L_3;
	}
}
// System.Void NoSuchStudio.Common.UnityObjectLoggerExt::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnityObjectLoggerExt__cctor_m7BDA7526A4FD1EEF24A32046452FBF3C3A043780 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_mC6AF8829C5C4C4865830344ACF22D1BDF29CF081_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static readonly Dictionary<Type, (Logger, LoggerConfig)> loggers = new Dictionary<Type, (Logger, LoggerConfig)>();
		Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07* L_0 = (Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07*)il2cpp_codegen_object_new(Dictionary_2_t831F499284FF95737996957BDD974D6A74D92C07_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Dictionary_2__ctor_mC6AF8829C5C4C4865830344ACF22D1BDF29CF081(L_0, Dictionary_2__ctor_mC6AF8829C5C4C4865830344ACF22D1BDF29CF081_RuntimeMethod_var);
		((UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_StaticFields*)il2cpp_codegen_static_fields_for(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var))->___loggers_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_StaticFields*)il2cpp_codegen_static_fields_for(UnityObjectLoggerExt_t06C4FF4C516B332F1BE82A54C07505AE514F1973_il2cpp_TypeInfo_var))->___loggers_0), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void EventsDelegate_Invoke_mF682224CF4C297269F19FAF17E190F3D56791DED_inline (EventsDelegate_tAB8A5A313FE5AAFDE012DD09D18194D7CF52E416* __this, String_t* ___eventName0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___eventParams1, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___eventName0, ___eventParams1, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Exception_t* Exception_get_InnerException_m0C1BDB339C786BA4DA7D2C1AD214571CFBBB1410_inline (Exception_t* __this, const RuntimeMethod* method) 
{
	{
		Exception_t* L_0 = __this->____innerException_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Color__ctor_mCD6889CDE39F18704CD6EA8E2EFBFA48BA3E13B0_inline (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* __this, float ___r0, float ___g1, float ___b2, const RuntimeMethod* method) 
{
	{
		float L_0 = ___r0;
		__this->___r_0 = L_0;
		float L_1 = ___g1;
		__this->___g_1 = L_1;
		float L_2 = ___b2;
		__this->___b_2 = L_2;
		__this->___a_3 = (1.0f);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline (float ___value0, const RuntimeMethod* method) 
{
	bool V_0 = false;
	float V_1 = 0.0f;
	bool V_2 = false;
	{
		float L_0 = ___value0;
		V_0 = (bool)((((float)L_0) < ((float)(0.0f)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		V_1 = (0.0f);
		goto IL_002d;
	}

IL_0015:
	{
		float L_2 = ___value0;
		V_2 = (bool)((((float)L_2) > ((float)(1.0f)))? 1 : 0);
		bool L_3 = V_2;
		if (!L_3)
		{
			goto IL_0029;
		}
	}
	{
		V_1 = (1.0f);
		goto IL_002d;
	}

IL_0029:
	{
		float L_4 = ___value0;
		V_1 = L_4;
		goto IL_002d;
	}

IL_002d:
	{
		float L_5 = V_1;
		return L_5;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, float ___x0, float ___y1, float ___z2, const RuntimeMethod* method) 
{
	{
		float L_0 = ___x0;
		__this->___x_2 = L_0;
		float L_1 = ___y1;
		__this->___y_3 = L_1;
		float L_2 = ___z2;
		__this->___z_4 = L_2;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_one_mE6A2D5C6578E94268024613B596BF09F990B1260_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___oneVector_6;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, float ___t2, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		float L_0 = ___t2;
		float L_1;
		L_1 = Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline(L_0, NULL);
		___t2 = L_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___a0;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___b1;
		float L_5 = L_4.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___a0;
		float L_7 = L_6.___x_2;
		float L_8 = ___t2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = ___a0;
		float L_10 = L_9.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = ___b1;
		float L_12 = L_11.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = ___a0;
		float L_14 = L_13.___y_3;
		float L_15 = ___t2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = ___a0;
		float L_17 = L_16.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = ___b1;
		float L_19 = L_18.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20 = ___a0;
		float L_21 = L_20.___z_4;
		float L_22 = ___t2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		memset((&L_23), 0, sizeof(L_23));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_23), ((float)il2cpp_codegen_add(L_3, ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_subtract(L_5, L_7)), L_8)))), ((float)il2cpp_codegen_add(L_10, ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_subtract(L_12, L_14)), L_15)))), ((float)il2cpp_codegen_add(L_17, ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_subtract(L_19, L_21)), L_22)))), /*hidden argument*/NULL);
		V_0 = L_23;
		goto IL_0053;
	}

IL_0053:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = V_0;
		return L_24;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, float ___d1, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___a0;
		float L_1 = L_0.___x_2;
		float L_2 = ___d1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = ___a0;
		float L_4 = L_3.___y_3;
		float L_5 = ___d1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___a0;
		float L_7 = L_6.___z_4;
		float L_8 = ___d1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		memset((&L_9), 0, sizeof(L_9));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_9), ((float)il2cpp_codegen_multiply(L_1, L_2)), ((float)il2cpp_codegen_multiply(L_4, L_5)), ((float)il2cpp_codegen_multiply(L_7, L_8)), /*hidden argument*/NULL);
		V_0 = L_9;
		goto IL_0021;
	}

IL_0021:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_0;
		return L_10;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline (float ___a0, float ___b1, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float G_B3_0 = 0.0f;
	{
		float L_0 = ___a0;
		float L_1 = ___b1;
		if ((((float)L_0) > ((float)L_1)))
		{
			goto IL_0008;
		}
	}
	{
		float L_2 = ___b1;
		G_B3_0 = L_2;
		goto IL_0009;
	}

IL_0008:
	{
		float L_3 = ___a0;
		G_B3_0 = L_3;
	}

IL_0009:
	{
		V_0 = G_B3_0;
		goto IL_000c;
	}

IL_000c:
	{
		float L_4 = V_0;
		return L_4;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Min_m4F2A9C5128DC3F9E84865EE7ADA8DB5DA6B8B507_inline (float ___a0, float ___b1, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float G_B3_0 = 0.0f;
	{
		float L_0 = ___a0;
		float L_1 = ___b1;
		if ((((float)L_0) < ((float)L_1)))
		{
			goto IL_0008;
		}
	}
	{
		float L_2 = ___b1;
		G_B3_0 = L_2;
		goto IL_0009;
	}

IL_0008:
	{
		float L_3 = ___a0;
		G_B3_0 = L_3;
	}

IL_0009:
	{
		V_0 = G_B3_0;
		goto IL_000c;
	}

IL_000c:
	{
		float L_4 = V_0;
		return L_4;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___v0, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = ___v0;
		float L_1 = L_0.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_2 = ___v0;
		float L_3 = L_2.___y_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		memset((&L_4), 0, sizeof(L_4));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_4), L_1, L_3, (0.0f), /*hidden argument*/NULL);
		V_0 = L_4;
		goto IL_001a;
	}

IL_001a:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = V_0;
		return L_5;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___forwardVector_11;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___a0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___b1;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___a0;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___b1;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___a0;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___b1;
		float L_11 = L_10.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		memset((&L_12), 0, sizeof(L_12));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_12), ((float)il2cpp_codegen_add(L_1, L_3)), ((float)il2cpp_codegen_add(L_5, L_7)), ((float)il2cpp_codegen_add(L_9, L_11)), /*hidden argument*/NULL);
		V_0 = L_12;
		goto IL_0030;
	}

IL_0030:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		return L_13;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ScrollRect_get_viewport_m85092216DD476F77E78F5CE50F9C4E70063ECCF9_inline (ScrollRect_t17D2F2939CA8953110180DF53164CFC3DC88D70E* __this, const RuntimeMethod* method) 
{
	{
		// public RectTransform viewport { get { return m_Viewport; } set { m_Viewport = value; SetDirtyCaching(); } }
		RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* L_0 = __this->___m_Viewport_12;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___v0, const RuntimeMethod* method) 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___v0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___v0;
		float L_3 = L_2.___y_3;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4;
		memset((&L_4), 0, sizeof(L_4));
		Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&L_4), L_1, L_3, /*hidden argument*/NULL);
		V_0 = L_4;
		goto IL_0015;
	}

IL_0015:
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_5 = V_0;
		return L_5;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, float ___x0, float ___y1, const RuntimeMethod* method) 
{
	{
		float L_0 = ___x0;
		__this->___x_0 = L_0;
		float L_1 = ___y1;
		__this->___y_1 = L_1;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Debug_get_unityLogger_mA872400E9E585FCD6A2DE1717748A458545DE8A4_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		RuntimeObject* L_0 = ((Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticFields*)il2cpp_codegen_static_fields_for(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var))->___s_Logger_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Tuple_2_get_Item1_mBF34A596062BBB3C1DD2A6CA36810366F445C9FA_gshared_inline (Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___m_Item1_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Tuple_2_get_Item2_m4C8E8E93C0299E98E046C765CA6ABB544412C1D9_gshared_inline (Tuple_2_t4B75F18A57363D88671568DEF504983C60E18AC6* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___m_Item2_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___item0, const RuntimeMethod* method) 
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = (int32_t)__this->____version_3;
		__this->____version_3 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_1 = (Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C*)__this->____items_1;
		V_0 = L_1;
		int32_t L_2 = (int32_t)__this->____size_2;
		V_1 = L_2;
		int32_t L_3 = V_1;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size_2 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_6 = V_0;
		int32_t L_7 = V_1;
		int32_t L_8 = ___item0;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (int32_t)L_8);
		return;
	}

IL_0034:
	{
		int32_t L_9 = ___item0;
		((  void (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)))(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = (int32_t)__this->____size_2;
		return L_0;
	}
}
