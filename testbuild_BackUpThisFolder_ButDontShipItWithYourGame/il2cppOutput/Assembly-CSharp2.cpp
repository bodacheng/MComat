#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
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
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
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
template <typename R, typename T1, typename T2>
struct InterfaceFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};

// DG.Tweening.Plugins.Core.ABSTweenPlugin`3<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions>
struct ABSTweenPlugin_3_t56BEDD6B006DC2E8D499101DE8A2339425AE6A10;
// DG.Tweening.Plugins.Core.ABSTweenPlugin`3<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions>
struct ABSTweenPlugin_3_t60F4DE5120CFD5986925189A0E775FAEAB4C59B9;
// DG.Tweening.Plugins.Core.ABSTweenPlugin`3<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>
struct ABSTweenPlugin_3_tE5A78BE46D046C07A6356B8AB596B2D00F9295E7;
// DG.Tweening.Core.DOGetter`1<UnityEngine.Quaternion>
struct DOGetter_1_tB89DD12456B8E79576BB70E1CA6DF899686410D3;
// DG.Tweening.Core.DOGetter`1<System.Single>
struct DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03;
// DG.Tweening.Core.DOGetter`1<UnityEngine.Vector3>
struct DOGetter_1_t709462C08281F3AA5DFEF36CAF91404B1004C338;
// DG.Tweening.Core.DOSetter`1<UnityEngine.Quaternion>
struct DOSetter_1_t9EFF8DD70A15F455A6FE698A22BD0FE9683AC28E;
// DG.Tweening.Core.DOSetter`1<System.Single>
struct DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200;
// DG.Tweening.Core.DOSetter`1<UnityEngine.Vector3>
struct DOSetter_1_t02E8F9920F174322F1CF5AC8BCDEAABD14A03358;
// System.Collections.Generic.Dictionary`2<C_Mode,CameraMode>
struct Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44;
// System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager>
struct Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A;
// System.Collections.Generic.Dictionary`2<System.Int32Enum,System.Object>
struct Dictionary_2_t514396B90715EDD83BB0470C76C2F426F9381C71;
// System.Collections.Generic.Dictionary`2<System.Object,System.Object>
struct Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA;
// System.Collections.Generic.IDictionary`2<C_Mode,CameraMode>
struct IDictionary_2_t70FD88AC6FC219AD37685F0B48E299147C3767CA;
// System.Collections.Generic.IDictionary`2<UnityEngine.Collider,HittingDetection.HitPointPara>
struct IDictionary_2_t9985861B09259F8CC49356B143D2E1A0A80D8A00;
// System.Collections.Generic.IEnumerable`1<System.Object>
struct IEnumerable_1_tF95C9E01A913DD50575531C8305932628663D9E9;
// System.Collections.Generic.IEnumerable`1<UnityEngine.Transform>
struct IEnumerable_1_t4980F9E076B96A4E697C2E09671204DD70B5573F;
// System.Collections.Generic.IEqualityComparer`1<C_Mode>
struct IEqualityComparer_1_t3ECFF85B5D2F3ED016E78C76EFBEA199E5E133E3;
// System.Collections.Generic.IEqualityComparer`1<UnityEngine.Collider>
struct IEqualityComparer_1_t8B0F38FEDBDCD41E8338626B9114DF3410322BAD;
// System.Collections.Generic.Dictionary`2/KeyCollection<C_Mode,CameraMode>
struct KeyCollection_t1DF0FB109B2BD8BB35C33107037C3D6E7C09E865;
// System.Collections.Generic.Dictionary`2/KeyCollection<UnityEngine.Collider,HittingDetection.HitBoxManager>
struct KeyCollection_tD8347EBCD834312A91381EF7F282DEAA6696C7BA;
// System.Collections.Generic.List`1<System.String[]>
struct List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED;
// System.Collections.Generic.List`1<DG.Tweening.Core.ABSSequentiable>
struct List_1_t0C6BF1E3B166E9D2A63FC3291C519D61B950BFDC;
// System.Collections.Generic.List`1<UnityEngine.CanvasGroup>
struct List_1_t2CDCA768E7F493F5EDEBC75AEB200FD621354E35;
// System.Collections.Generic.List`1<Decomposition>
struct List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69;
// System.Collections.Generic.List`1<HittingDetection.Marker>
struct List_1_tB084CC07F0D61ECD66AAB6B593690873EBF70AA1;
// System.Collections.Generic.List`1<UnityEngine.MeshRenderer>
struct List_1_t558592816DA880773C8A60C1EB777F3B092B68EC;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<System.String>
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
// System.Collections.Generic.List`1<UnityEngine.Transform>
struct List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D;
// System.Collections.Generic.List`1<DG.Tweening.Tween>
struct List_1_tDA2C18E15C40590123A37DABB6D0D9AEB77A3BBD;
// System.Collections.Generic.List`1<UnityEngine.Events.UnityAction>
struct List_1_t81DD6D8E3F2D498C5E128E9488F7CC05E1881C4D;
// System.Collections.Generic.List`1<HittingDetection.V_Damage>
struct List_1_t6449D5997D9677B34BE44A31FB5155C097352DE2;
// System.Collections.Generic.List`1<UnityEngine.Vector3>
struct List_1_t77B94703E05C519A9010DD0614F757F974E1CD8B;
// System.Collections.Generic.List`1<SampleTable/Row>
struct List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED;
// System.Predicate`1<System.Object>
struct Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12;
// System.Predicate`1<SampleTable/Row>
struct Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70;
// DG.Tweening.TweenCallback`1<System.Int32>
struct TweenCallback_1_tF0ADCA0C226C9C243ACB55E67D852E4BB53AEB67;
// UnityEngine.UI.CoroutineTween.TweenRunner`1<UnityEngine.UI.CoroutineTween.ColorTween>
struct TweenRunner_1_t5BB0582F926E75E2FE795492679A6CF55A4B4BC4;
// DG.Tweening.Core.TweenerCore`3<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions>
struct TweenerCore_3_t9A48A35EB4763F174321ED1A1BE49A67BC0A5C6F;
// DG.Tweening.Core.TweenerCore`3<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions>
struct TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1;
// DG.Tweening.Core.TweenerCore`3<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>
struct TweenerCore_3_tCD82DFC45FB71C681FA8659EA63A7D7D16BFFE77;
// System.Collections.Generic.Dictionary`2/ValueCollection<C_Mode,CameraMode>
struct ValueCollection_t98B14FD9F90D91C7205FC6432948EDDC1BE03C6E;
// System.Collections.Generic.Dictionary`2/ValueCollection<UnityEngine.Collider,HittingDetection.HitBoxManager>
struct ValueCollection_t1B125C66A8A33D69CE9DD60AE26A7EE8CC54B961;
// System.Collections.Generic.Dictionary`2/Entry<C_Mode,CameraMode>[]
struct EntryU5BU5D_tBE56D5A2844478483F0CF3D03A5278C0FA3D36DF;
// System.Collections.Generic.Dictionary`2/Entry<UnityEngine.Collider,HittingDetection.HitBoxManager>[]
struct EntryU5BU5D_tD283906550672BFBCC475D8669DFE2B6019883A3;
// System.String[][]
struct StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
// Decomposition[]
struct DecompositionU5BU5D_t81A24D51CED220B75B54F4D22D2587199C540F2D;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// UnityEngine.UI.Selectable[]
struct SelectableU5BU5D_t4160E135F02A40F75A63F787D36F31FEC6FE91A9;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// UnityEngine.Transform[]
struct TransformU5BU5D_tBB9C5F5686CAE82E3D97D43DF0F3D68ABF75EC24;
// UnityEngine.UIVertex[]
struct UIVertexU5BU5D_tBC532486B45D071A520751A90E819C77BA4E3D2F;
// UnityEngine.Vector2[]
struct Vector2U5BU5D_tFEBBC94BCC6C9C88277BA04047D2B3FDB6ED7FDA;
// UnityEngine.Vector3[]
struct Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C;
// SampleTable/Row[]
struct RowU5BU5D_t62AD3FB240F5BB1F11CD86A17C2FC320AFF4FF8C;
// UnityEngine.UI.AnimationTriggers
struct AnimationTriggers_tA0DC06F89C5280C6DD972F6F4C8A56D7F4F79074;
// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
// UnityEngine.AudioSource
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299;
// BO_Ani_E
struct BO_Ani_E_tE52B3FFFAF6137845E7FCAF01A1A84991BAF3F6D;
// BO_Limb
struct BO_Limb_t34AE66D5B61AEC630DDA942E1000BC4247901966;
// C2TDemo
struct C2TDemo_tE3F1FE59F2C6D939EA56302F04A6CAC556272F57;
// UnityEngine.Camera
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184;
// CameraManager
struct CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB;
// CameraMode
struct CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97;
// UnityEngine.Canvas
struct Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26;
// UnityEngine.CanvasRenderer
struct CanvasRenderer_tAB9A55A976C4E3B2B37D0CE5616E5685A8B43860;
// CenterSurroundCamera
struct CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C;
// CertainYAntiVabration
struct CertainYAntiVabration_tC3F93440371E35297502C0F0431F942257E24AC9;
// CertainYAntiVibrationCamera
struct CertainYAntiVibrationCamera_tBE98E18C6C6A13DE240FEEAA67D2CC0074175BC6;
// ChatGptFix
struct ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD;
// ChatGptFix2
struct ChatGptFix2_tA67A0EB8B87FEFA6B8FB70BF923327A8D5BEC9D3;
// UnityEngine.Collider
struct Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76;
// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
// UnityEngine.Coroutine
struct Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B;
// CsvParser
struct CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607;
// Decomposition
struct Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53;
// DecompositionPool
struct DecompositionPool_tB2DB3E05F320A6D6F54A6482A7BB94C11ACEC229;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// DG.Tweening.EaseFunction
struct EaseFunction_t0F945D9D726B0915C5FBF30862E987EC3AC12A04;
// UnityEngine.Event
struct Event_tEBC6F24B56CE22B9C9AD1AC6C24A6B83BC3860CB;
// FightParamsReference
struct FightParamsReference_tF64DF89060040FE893FB00338DA2E6500E44A629;
// UnityEngine.UI.FontData
struct FontData_tB8E562846C6CB59C43260F69AE346B9BF3157224;
// GodPlayerCertainY
struct GodPlayerCertainY_t2757B4570A2E99A42AFDDAFADFBC705E58F4F37A;
// GodplayerCamera
struct GodplayerCamera_t69B1E62878C9B321870DEB22A717E76037545DB2;
// UnityEngine.UI.Graphic
struct Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931;
// HittingDetection.HitBoxManager
struct HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC;
// HitBoxesProcesser
struct HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A;
// System.IAsyncResult
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
// UnityEngine.UI.InputField
struct InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140;
// LerpToCertainDistance
struct LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD;
// MCamera
struct MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF;
// UnityEngine.Material
struct Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3;
// UnityEngine.Mesh
struct Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
// New2021
struct New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5;
// New2022
struct New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8;
// New2023
struct New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// OneVOneMode
struct OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822;
// OneVOneModeNew
struct OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB;
// OneVOneMode_failed
struct OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F;
// UnityEngine.ParticleSystem
struct ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1;
// PinchZoom
struct PinchZoom_t94309269E7A55D4CA4DDD01EB4CB93A0B47CFEDD;
// UnityEngine.Animations.PositionConstraint
struct PositionConstraint_t574BE070FD49E61B0DC8B4CA53486634FD30377B;
// UnityEngine.UI.RectMask2D
struct RectMask2D_tACF92BE999C791A665BD1ADEABF5BCEB82846670;
// UnityEngine.RectTransform
struct RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5;
// SampleTable
struct SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321;
// ScreenSaverC
struct ScreenSaverC_t57D260260EAF244CB16B9345A74371B1CAB86AE7;
// UnityEngine.UI.Selectable
struct Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712;
// DG.Tweening.Sequence
struct Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C;
// UnityEngine.Sprite
struct Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99;
// StartToEndMode
struct StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338;
// System.String
struct String_t;
// System.Text.StringBuilder
struct StringBuilder_t;
// System.IO.StringReader
struct StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8;
// TeamConfig
struct TeamConfig_t9B18EF1FD184E83A5BF2F9A59AF6A3B6876D715E;
// TeamEditCamera
struct TeamEditCamera_tCD912CE0B0950259EDA0CEA3351EF1FEAA085D9F;
// UnityEngine.UI.Text
struct Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62;
// UnityEngine.TextAsset
struct TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69;
// UnityEngine.TextGenerator
struct TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC;
// System.IO.TextReader
struct TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7;
// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
// TopDownWatchCamera
struct TopDownWatchCamera_tB31CB6E39C34F1D87B22F4B4D6E2171F68934989;
// UnityEngine.TouchScreenKeyboard
struct TouchScreenKeyboard_tE87B78A3DAED69816B44C99270A734682E093E7A;
// TouchTopDownCamera
struct TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2;
// TrackControl
struct TrackControl_t6E36A5D737F65778D9B7BAEA6B3F42C479FD9566;
// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1;
// DG.Tweening.Tween
struct Tween_t8CB06EBC48A5B6F5065C490E4F4909C18CE7983C;
// DG.Tweening.TweenCallback
struct TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24;
// System.Type
struct Type_t;
// UnityEngine.Events.UnityAction
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7;
// UnityEngine.UI.VertexHelper
struct VertexHelper_tB905FCB02AE67CBEE5F265FE37A5938FC5D136FE;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.WaitForSecondsRealtime
struct WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01;
// WatchOverCamera
struct WatchOverCamera_t0B35C99643DDFF8D8D63153C55E4862BAEC5C7F6;
// keepTargetLeftCamera
struct keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95;
// UnityEngine.Camera/CameraCallback
struct CameraCallback_t844E527BFE37BC0495E7F67993E43C07642DA9DD;
// CsvParser/LineStartState
struct LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB;
// CsvParser/ParserContext
struct ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9;
// CsvParser/ParserState
struct ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3;
// CsvParser/QuoteState
struct QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E;
// CsvParser/QuotedValueState
struct QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA;
// CsvParser/ValueStartState
struct ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B;
// CsvParser/ValueState
struct ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44;
// UnityEngine.UI.InputField/EndEditEvent
struct EndEditEvent_t946A962BA13CF60BB0BE7AD091DA041FD788E655;
// UnityEngine.UI.InputField/OnChangeEvent
struct OnChangeEvent_tE4829F88300B0E0E0D1B78B453AF25FC1AA55E2F;
// UnityEngine.UI.InputField/OnValidateInput
struct OnValidateInput_t48916A4E9C9FD6204401FF0808C2B7A93D73418B;
// UnityEngine.UI.InputField/SubmitEvent
struct SubmitEvent_t1E0F5A2AB28D0DB55AE18E8DA99147D86492DD5D;
// UnityEngine.UI.MaskableGraphic/CullStateChangedEvent
struct CullStateChangedEvent_t6073CD0D951EC1256BF74B8F9107D68FC89B99B8;
// SampleTable/<>c__DisplayClass10_0
struct U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790;
// SampleTable/<>c__DisplayClass11_0
struct U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9;
// SampleTable/<>c__DisplayClass12_0
struct U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8;
// SampleTable/<>c__DisplayClass13_0
struct U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778;
// SampleTable/<>c__DisplayClass14_0
struct U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517;
// SampleTable/<>c__DisplayClass15_0
struct U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869;
// SampleTable/<>c__DisplayClass16_0
struct U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292;
// SampleTable/<>c__DisplayClass17_0
struct U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D;
// SampleTable/<>c__DisplayClass8_0
struct U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565;
// SampleTable/<>c__DisplayClass9_0
struct U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D;
// SampleTable/Row
struct Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC;
// TouchTopDownCamera/<>c__DisplayClass21_0
struct U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66;

IL2CPP_EXTERN_C RuntimeClass* CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDictionary_2_t70FD88AC6FC219AD37685F0B48E299147C3767CA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_1_t00EAEB29218994CE734A3A26D94870DCCC8089A2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_1_tDB9241AA672FBAD41B38A33A2A3D720DB45A70D5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0;
IL2CPP_EXTERN_C String_t* _stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B;
IL2CPP_EXTERN_C String_t* _stringLiteral265E15F1F86F1C766555899D5771CF29055DE75A;
IL2CPP_EXTERN_C String_t* _stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E;
IL2CPP_EXTERN_C String_t* _stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7;
IL2CPP_EXTERN_C String_t* _stringLiteral8AF7B9D6121033ED1DE80EFA3688A7998521AB1F;
IL2CPP_EXTERN_C String_t* _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709;
IL2CPP_EXTERN_C String_t* _stringLiteralF616212F742C2A1A279331136CF869CE0847A0C0;
IL2CPP_EXTERN_C String_t* _stringLiteralFC6687DC37346CD2569888E29764F727FAF530E0;
IL2CPP_EXTERN_C const RuntimeMethod* ChatGptFix_U3CEnterU3Eb__25_0_m13ADE7F9BD5A6AEA1EC212EED8AC53991D5BC74C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ChatGptFix_U3CEnterU3Eb__25_1_mD0AD2D3F2C19FFBC7674083BD3C4C225118CD816_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_m7A3E3FD907B5C5FC6FACEDF50DC5C1C6A6C67F19_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_ContainsKey_m19FC3A712B339AC1EC6CC0D81D8BB425B022B97C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_TryGetValue_m3335583D1D1EE1BECD3037C120CA3B3BEBDD9D71_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_m665BD95251217FF9BEAAE59FB36F09C3CB9E2012_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_m9C0EC68028100E8C91D57975D3FA9279791E676F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* KeyValuePair_2_get_Value_m9B30E68334E34583A8C40B04DEB897A4800203F9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m9761F0D2ADF7CB1D17354DDC09E8F08DB70897EF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_mBFF86E22A26E9ED0D216F526BFBF7A7546991F38_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Clear_mAF59287F15E95C0F18D3E325B64FCAC82A7610A9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Contains_mDE448B160DBA47CFE50F34A3524289C69870B992_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_ToArray_m0FF88E5645F74AB2208E8BA2A85973B21E5FADA0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m362AED7E17D370D578FF476B1FF74A9236A96783_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mA9A28D7BDA09426757EEB0C6020D5BE0CC7A9584_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_m82D8E1795C4DF42DA74D17354A985E517168F936_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_m8EAA91B4CE37CBB6C720FD238E4505097B29FFDA_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MCamera_U3CEnterU3Eb__26_0_mE9E17032F8267383045DC9F016A5ADAFFE645D1F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MCamera_U3CEnterU3Eb__26_1_mD60F8A737B8E0DD31DCE54F573CB8595E3A1FB29_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* New2022_U3CEnterU3Eb__21_0_m0C421AE011466B048D269224F7612BF40625DE24_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* New2022_U3CEnterU3Eb__21_1_mCF650825B73C01987D7E65A95573F15A7FB91FE4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* New2023_U3CEnterU3Eb__21_0_mE3340875199190BBAF23F0AC6F440F86703AC54E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* New2023_U3CEnterU3Eb__21_1_mE5C4A357F019129F1A6CFB77C1E342F78C026A43_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TweenExtensions_Play_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mAE376A6BE21D1F94CE5EAA4DA0C1683A7D6DFDE7_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TweenSettingsExtensions_OnStart_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mCCE914E78193AFF17F77999963371587BAD452E5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass10_0_U3CFind_MakeU3Eb__0_m9690D15F7018B534711F77ACE95ED4A3C9EAEA53_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass11_0_U3CFindAll_MakeU3Eb__0_m176F964725AF0CAFDAB226E789B460DF024ABAA9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass12_0_U3CFind_ModelU3Eb__0_m8BAD4E720795A1A54FB128AECF0C50B86E6C2FF5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass13_0_U3CFindAll_ModelU3Eb__0_mD2258DCB18D2FEC0702A063156864A12E36208CF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass14_0_U3CFind_DescriptionU3Eb__0_mB6C5A693A3EC33C1F95D6E524CCDE48DD46AE39E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass15_0_U3CFindAll_DescriptionU3Eb__0_mD4398A3E548390DDED491E828A9E706FD17D6019_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass16_0_U3CFind_PriceU3Eb__0_mC46DC08CEE1619582B2E05F661CAEB2BA1845131_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass17_0_U3CFindAll_PriceU3Eb__0_m14CBD99784ED2E431E7D88416BBA6186F4E65BB1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__0_m3186DC723944338480A2B848E7B36DE2F5ABB70E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__1_m9F7688B45302FA836246EB0B35829E085E4185D3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass8_0_U3CFind_YearU3Eb__0_m015A2A5E12E7BDE55B522CC486803B07E9B4F795_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass9_0_U3CFindAll_YearU3Eb__0_m3C1B30422DB8F2F130ED68EE884F821AEFF219BC_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Collections.Generic.Dictionary`2<C_Mode,CameraMode>
struct Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_tBE56D5A2844478483F0CF3D03A5278C0FA3D36DF* ____entries_1;
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
	KeyCollection_t1DF0FB109B2BD8BB35C33107037C3D6E7C09E865* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_t98B14FD9F90D91C7205FC6432948EDDC1BE03C6E* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager>
struct Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_tD283906550672BFBCC475D8669DFE2B6019883A3* ____entries_1;
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
	KeyCollection_tD8347EBCD834312A91381EF7F282DEAA6696C7BA* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_t1B125C66A8A33D69CE9DD60AE26A7EE8CC54B961* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.Collections.Generic.List`1<System.String[]>
struct List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<Decomposition>
struct List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	DecompositionU5BU5D_t81A24D51CED220B75B54F4D22D2587199C540F2D* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	DecompositionU5BU5D_t81A24D51CED220B75B54F4D22D2587199C540F2D* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.Transform>
struct List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	TransformU5BU5D_tBB9C5F5686CAE82E3D97D43DF0F3D68ABF75EC24* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	TransformU5BU5D_tBB9C5F5686CAE82E3D97D43DF0F3D68ABF75EC24* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<SampleTable/Row>
struct List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	RowU5BU5D_t62AD3FB240F5BB1F11CD86A17C2FC320AFF4FF8C* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	RowU5BU5D_t62AD3FB240F5BB1F11CD86A17C2FC320AFF4FF8C* ___s_emptyArray_5;
};

// DG.Tweening.Core.ABSSequentiable
struct ABSSequentiable_t05DF85FC63E3650D2D4CF6ABBA0F43263EB8CE89  : public RuntimeObject
{
	// DG.Tweening.TweenType DG.Tweening.Core.ABSSequentiable::tweenType
	int32_t ___tweenType_0;
	// System.Single DG.Tweening.Core.ABSSequentiable::sequencedPosition
	float ___sequencedPosition_1;
	// System.Single DG.Tweening.Core.ABSSequentiable::sequencedEndPosition
	float ___sequencedEndPosition_2;
	// DG.Tweening.TweenCallback DG.Tweening.Core.ABSSequentiable::onStart
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onStart_3;
};
struct Il2CppArrayBounds;

// CameraMode
struct CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97  : public RuntimeObject
{
	// CameraManager CameraMode::cameraManager
	CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* ___cameraManager_0;
	// UnityEngine.Transform CameraMode::meCenter
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___meCenter_1;
	// UnityEngine.Transform CameraMode::target
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___target_2;
	// System.Collections.Generic.List`1<UnityEngine.Transform> CameraMode::myTeamTargets
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ___myTeamTargets_3;
	// System.Collections.Generic.List`1<UnityEngine.Transform> CameraMode::targets
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ___targets_4;
	// System.Boolean CameraMode::auto
	bool ___auto_5;
	// System.Single CameraMode::speed
	float ___speed_6;
	// System.Single CameraMode::XZDis
	float ___XZDis_7;
	// System.Single CameraMode::YDis
	float ___YDis_8;
	// System.Single CameraMode::XZrosOffset
	float ___XZrosOffset_9;
	// System.Single CameraMode::YrosOffset
	float ___YrosOffset_10;
	// System.Single CameraMode::duration
	float ___duration_11;
	// System.Single CameraMode::fieldOfView
	float ___fieldOfView_12;
};

// CsvParser
struct CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607  : public RuntimeObject
{
};

// System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE  : public RuntimeObject
{
	// System.Object System.MarshalByRefObject::_identity
	RuntimeObject* ____identity_0;
};
// Native definition for P/Invoke marshalling of System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_pinvoke
{
	Il2CppIUnknown* ____identity_0;
};
// Native definition for COM marshalling of System.MarshalByRefObject
struct MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE_marshaled_com
{
	Il2CppIUnknown* ____identity_0;
};

// SampleTable
struct SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321  : public RuntimeObject
{
	// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::rowList
	List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* ___rowList_0;
	// System.Boolean SampleTable::isLoaded
	bool ___isLoaded_1;
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

// CsvParser/ParserContext
struct ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9  : public RuntimeObject
{
	// System.Text.StringBuilder CsvParser/ParserContext::_currentValue
	StringBuilder_t* ____currentValue_0;
	// System.Collections.Generic.List`1<System.String[]> CsvParser/ParserContext::_lines
	List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED* ____lines_1;
	// System.Collections.Generic.List`1<System.String> CsvParser/ParserContext::_currentLine
	List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* ____currentLine_2;
};

// CsvParser/ParserState
struct ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3  : public RuntimeObject
{
};

struct ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields
{
	// CsvParser/LineStartState CsvParser/ParserState::LineStartState
	LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* ___LineStartState_0;
	// CsvParser/ValueStartState CsvParser/ParserState::ValueStartState
	ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* ___ValueStartState_1;
	// CsvParser/ValueState CsvParser/ParserState::ValueState
	ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* ___ValueState_2;
	// CsvParser/QuotedValueState CsvParser/ParserState::QuotedValueState
	QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA* ___QuotedValueState_3;
	// CsvParser/QuoteState CsvParser/ParserState::QuoteState
	QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E* ___QuoteState_4;
};

// SampleTable/<>c__DisplayClass10_0
struct U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass10_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass11_0
struct U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass11_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass12_0
struct U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass12_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass13_0
struct U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass13_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass14_0
struct U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass14_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass15_0
struct U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass15_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass16_0
struct U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass16_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass17_0
struct U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass17_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass8_0
struct U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass8_0::find
	String_t* ___find_0;
};

// SampleTable/<>c__DisplayClass9_0
struct U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D  : public RuntimeObject
{
	// System.String SampleTable/<>c__DisplayClass9_0::find
	String_t* ___find_0;
};

// SampleTable/Row
struct Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC  : public RuntimeObject
{
	// System.String SampleTable/Row::Year
	String_t* ___Year_0;
	// System.String SampleTable/Row::Make
	String_t* ___Make_1;
	// System.String SampleTable/Row::Model
	String_t* ___Model_2;
	// System.String SampleTable/Row::Description
	String_t* ___Description_3;
	// System.String SampleTable/Row::Price
	String_t* ___Price_4;
};

// System.Collections.Generic.List`1/Enumerator<System.Object>
struct Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	RuntimeObject* ____current_3;
};

// System.Collections.Generic.List`1/Enumerator<UnityEngine.Transform>
struct Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____current_3;
};

// System.Collections.Generic.KeyValuePair`2<C_Mode,CameraMode>
struct KeyValuePair_2_t16437782916F5E7884151CEF28CCC71F0FDEBAE4 
{
	// TKey System.Collections.Generic.KeyValuePair`2::key
	int32_t ___key_0;
	// TValue System.Collections.Generic.KeyValuePair`2::value
	CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* ___value_1;
};

// System.Collections.Generic.KeyValuePair`2<System.Int32Enum,System.Object>
struct KeyValuePair_2_tF70DDE0C5A349727371FB070D433FA147032A13B 
{
	// TKey System.Collections.Generic.KeyValuePair`2::key
	int32_t ___key_0;
	// TValue System.Collections.Generic.KeyValuePair`2::value
	RuntimeObject* ___value_1;
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

// System.Char
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	// System.Char System.Char::m_value
	Il2CppChar ___m_value_0;
};

struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	// System.Byte[] System.Char::s_categoryForLatin1
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1_3;
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

// System.Decimal
struct Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			// System.Int32 System.Decimal::flags
			int32_t ___flags_8;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___flags_8_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___hi_9_OffsetPadding[4];
			// System.Int32 System.Decimal::hi
			int32_t ___hi_9;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___hi_9_OffsetPadding_forAlignmentOnly[4];
			int32_t ___hi_9_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___lo_10_OffsetPadding[8];
			// System.Int32 System.Decimal::lo
			int32_t ___lo_10;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___lo_10_OffsetPadding_forAlignmentOnly[8];
			int32_t ___lo_10_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___mid_11_OffsetPadding[12];
			// System.Int32 System.Decimal::mid
			int32_t ___mid_11;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___mid_11_OffsetPadding_forAlignmentOnly[12];
			int32_t ___mid_11_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___ulomidLE_12_OffsetPadding[8];
			// System.UInt64 System.Decimal::ulomidLE
			uint64_t ___ulomidLE_12;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___ulomidLE_12_OffsetPadding_forAlignmentOnly[8];
			uint64_t ___ulomidLE_12_forAlignmentOnly;
		};
	};
};

struct Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_StaticFields
{
	// System.Decimal System.Decimal::Zero
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___Zero_3;
	// System.Decimal System.Decimal::One
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___One_4;
	// System.Decimal System.Decimal::MinusOne
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MinusOne_5;
	// System.Decimal System.Decimal::MaxValue
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MaxValue_6;
	// System.Decimal System.Decimal::MinValue
	Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___MinValue_7;
};

// System.Double
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	// System.Double System.Double::m_value
	double ___m_value_0;
};

// DG.Tweening.Plugins.Options.FloatOptions
struct FloatOptions_t8A9B05DB7CF6CC90A27F300C2977D91A48B3FEF5 
{
	// System.Boolean DG.Tweening.Plugins.Options.FloatOptions::snapping
	bool ___snapping_0;
};
// Native definition for P/Invoke marshalling of DG.Tweening.Plugins.Options.FloatOptions
struct FloatOptions_t8A9B05DB7CF6CC90A27F300C2977D91A48B3FEF5_marshaled_pinvoke
{
	int32_t ___snapping_0;
};
// Native definition for COM marshalling of DG.Tweening.Plugins.Options.FloatOptions
struct FloatOptions_t8A9B05DB7CF6CC90A27F300C2977D91A48B3FEF5_marshaled_com
{
	int32_t ___snapping_0;
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

// UnityEngine.UI.Navigation
struct Navigation_t4D2E201D65749CF4E104E8AC1232CF1D6F14795C 
{
	// UnityEngine.UI.Navigation/Mode UnityEngine.UI.Navigation::m_Mode
	int32_t ___m_Mode_0;
	// System.Boolean UnityEngine.UI.Navigation::m_WrapAround
	bool ___m_WrapAround_1;
	// UnityEngine.UI.Selectable UnityEngine.UI.Navigation::m_SelectOnUp
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnUp_2;
	// UnityEngine.UI.Selectable UnityEngine.UI.Navigation::m_SelectOnDown
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnDown_3;
	// UnityEngine.UI.Selectable UnityEngine.UI.Navigation::m_SelectOnLeft
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnLeft_4;
	// UnityEngine.UI.Selectable UnityEngine.UI.Navigation::m_SelectOnRight
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnRight_5;
};
// Native definition for P/Invoke marshalling of UnityEngine.UI.Navigation
struct Navigation_t4D2E201D65749CF4E104E8AC1232CF1D6F14795C_marshaled_pinvoke
{
	int32_t ___m_Mode_0;
	int32_t ___m_WrapAround_1;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnUp_2;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnDown_3;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnLeft_4;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnRight_5;
};
// Native definition for COM marshalling of UnityEngine.UI.Navigation
struct Navigation_t4D2E201D65749CF4E104E8AC1232CF1D6F14795C_marshaled_com
{
	int32_t ___m_Mode_0;
	int32_t ___m_WrapAround_1;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnUp_2;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnDown_3;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnLeft_4;
	Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712* ___m_SelectOnRight_5;
};

// DG.Tweening.Plugins.Options.NoOptions
struct NoOptions_t2B4A2CA3C472B5AC37AACC090B1D0B27BCF4307E 
{
	union
	{
		struct
		{
		};
		uint8_t NoOptions_t2B4A2CA3C472B5AC37AACC090B1D0B27BCF4307E__padding[1];
	};
};

// UnityEngine.Quaternion
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 
{
	// System.Single UnityEngine.Quaternion::x
	float ___x_0;
	// System.Single UnityEngine.Quaternion::y
	float ___y_1;
	// System.Single UnityEngine.Quaternion::z
	float ___z_2;
	// System.Single UnityEngine.Quaternion::w
	float ___w_3;
};

struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974_StaticFields
{
	// UnityEngine.Quaternion UnityEngine.Quaternion::identityQuaternion
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___identityQuaternion_4;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// UnityEngine.UI.SpriteState
struct SpriteState_tC8199570BE6337FB5C49347C97892B4222E5AACD 
{
	// UnityEngine.Sprite UnityEngine.UI.SpriteState::m_HighlightedSprite
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_HighlightedSprite_0;
	// UnityEngine.Sprite UnityEngine.UI.SpriteState::m_PressedSprite
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_PressedSprite_1;
	// UnityEngine.Sprite UnityEngine.UI.SpriteState::m_SelectedSprite
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_SelectedSprite_2;
	// UnityEngine.Sprite UnityEngine.UI.SpriteState::m_DisabledSprite
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_DisabledSprite_3;
};
// Native definition for P/Invoke marshalling of UnityEngine.UI.SpriteState
struct SpriteState_tC8199570BE6337FB5C49347C97892B4222E5AACD_marshaled_pinvoke
{
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_HighlightedSprite_0;
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_PressedSprite_1;
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_SelectedSprite_2;
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_DisabledSprite_3;
};
// Native definition for COM marshalling of UnityEngine.UI.SpriteState
struct SpriteState_tC8199570BE6337FB5C49347C97892B4222E5AACD_marshaled_com
{
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_HighlightedSprite_0;
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_PressedSprite_1;
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_SelectedSprite_2;
	Sprite_tAFF74BC83CD68037494CB0B4F28CBDF8971CAB99* ___m_DisabledSprite_3;
};

// System.IO.TextReader
struct TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7  : public MarshalByRefObject_t8C2F4C5854177FD60439EB1FCCFC1B3CFAFE8DCE
{
};

struct TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7_StaticFields
{
	// System.IO.TextReader System.IO.TextReader::Null
	TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7* ___Null_1;
};

// DG.Tweening.Tween
struct Tween_t8CB06EBC48A5B6F5065C490E4F4909C18CE7983C  : public ABSSequentiable_t05DF85FC63E3650D2D4CF6ABBA0F43263EB8CE89
{
	// System.Single DG.Tweening.Tween::timeScale
	float ___timeScale_4;
	// System.Boolean DG.Tweening.Tween::isBackwards
	bool ___isBackwards_5;
	// System.Object DG.Tweening.Tween::id
	RuntimeObject* ___id_6;
	// System.String DG.Tweening.Tween::stringId
	String_t* ___stringId_7;
	// System.Int32 DG.Tweening.Tween::intId
	int32_t ___intId_8;
	// System.Object DG.Tweening.Tween::target
	RuntimeObject* ___target_9;
	// DG.Tweening.UpdateType DG.Tweening.Tween::updateType
	int32_t ___updateType_10;
	// System.Boolean DG.Tweening.Tween::isIndependentUpdate
	bool ___isIndependentUpdate_11;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onPlay
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onPlay_12;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onPause
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onPause_13;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onRewind
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onRewind_14;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onUpdate
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onUpdate_15;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onStepComplete
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onStepComplete_16;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onComplete
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onComplete_17;
	// DG.Tweening.TweenCallback DG.Tweening.Tween::onKill
	TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___onKill_18;
	// DG.Tweening.TweenCallback`1<System.Int32> DG.Tweening.Tween::onWaypointChange
	TweenCallback_1_tF0ADCA0C226C9C243ACB55E67D852E4BB53AEB67* ___onWaypointChange_19;
	// System.Boolean DG.Tweening.Tween::isFrom
	bool ___isFrom_20;
	// System.Boolean DG.Tweening.Tween::isBlendable
	bool ___isBlendable_21;
	// System.Boolean DG.Tweening.Tween::isRecyclable
	bool ___isRecyclable_22;
	// System.Boolean DG.Tweening.Tween::isSpeedBased
	bool ___isSpeedBased_23;
	// System.Boolean DG.Tweening.Tween::autoKill
	bool ___autoKill_24;
	// System.Single DG.Tweening.Tween::duration
	float ___duration_25;
	// System.Int32 DG.Tweening.Tween::loops
	int32_t ___loops_26;
	// DG.Tweening.LoopType DG.Tweening.Tween::loopType
	int32_t ___loopType_27;
	// System.Single DG.Tweening.Tween::delay
	float ___delay_28;
	// System.Boolean DG.Tweening.Tween::<isRelative>k__BackingField
	bool ___U3CisRelativeU3Ek__BackingField_29;
	// DG.Tweening.Ease DG.Tweening.Tween::easeType
	int32_t ___easeType_30;
	// DG.Tweening.EaseFunction DG.Tweening.Tween::customEase
	EaseFunction_t0F945D9D726B0915C5FBF30862E987EC3AC12A04* ___customEase_31;
	// System.Single DG.Tweening.Tween::easeOvershootOrAmplitude
	float ___easeOvershootOrAmplitude_32;
	// System.Single DG.Tweening.Tween::easePeriod
	float ___easePeriod_33;
	// System.Type DG.Tweening.Tween::typeofT1
	Type_t* ___typeofT1_34;
	// System.Type DG.Tweening.Tween::typeofT2
	Type_t* ___typeofT2_35;
	// System.Type DG.Tweening.Tween::typeofTPlugOptions
	Type_t* ___typeofTPlugOptions_36;
	// System.Boolean DG.Tweening.Tween::<active>k__BackingField
	bool ___U3CactiveU3Ek__BackingField_37;
	// System.Boolean DG.Tweening.Tween::isSequenced
	bool ___isSequenced_38;
	// DG.Tweening.Sequence DG.Tweening.Tween::sequenceParent
	Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___sequenceParent_39;
	// System.Int32 DG.Tweening.Tween::activeId
	int32_t ___activeId_40;
	// DG.Tweening.Core.Enums.SpecialStartupMode DG.Tweening.Tween::specialStartupMode
	int32_t ___specialStartupMode_41;
	// System.Boolean DG.Tweening.Tween::creationLocked
	bool ___creationLocked_42;
	// System.Boolean DG.Tweening.Tween::startupDone
	bool ___startupDone_43;
	// System.Boolean DG.Tweening.Tween::<playedOnce>k__BackingField
	bool ___U3CplayedOnceU3Ek__BackingField_44;
	// System.Single DG.Tweening.Tween::<position>k__BackingField
	float ___U3CpositionU3Ek__BackingField_45;
	// System.Single DG.Tweening.Tween::fullDuration
	float ___fullDuration_46;
	// System.Int32 DG.Tweening.Tween::completedLoops
	int32_t ___completedLoops_47;
	// System.Boolean DG.Tweening.Tween::isPlaying
	bool ___isPlaying_48;
	// System.Boolean DG.Tweening.Tween::isComplete
	bool ___isComplete_49;
	// System.Single DG.Tweening.Tween::elapsedDelay
	float ___elapsedDelay_50;
	// System.Boolean DG.Tweening.Tween::delayComplete
	bool ___delayComplete_51;
	// System.Int32 DG.Tweening.Tween::miscInt
	int32_t ___miscInt_52;
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

// UnityEngine.Vector4
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 
{
	// System.Single UnityEngine.Vector4::x
	float ___x_1;
	// System.Single UnityEngine.Vector4::y
	float ___y_2;
	// System.Single UnityEngine.Vector4::z
	float ___z_3;
	// System.Single UnityEngine.Vector4::w
	float ___w_4;
};

struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3_StaticFields
{
	// UnityEngine.Vector4 UnityEngine.Vector4::zeroVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___zeroVector_5;
	// UnityEngine.Vector4 UnityEngine.Vector4::oneVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___oneVector_6;
	// UnityEngine.Vector4 UnityEngine.Vector4::positiveInfinityVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___positiveInfinityVector_7;
	// UnityEngine.Vector4 UnityEngine.Vector4::negativeInfinityVector
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___negativeInfinityVector_8;
};

// DG.Tweening.Plugins.Options.VectorOptions
struct VectorOptions_t2814CC842518C92C9DFC5DE6F7A73824758D3EF9 
{
	// DG.Tweening.AxisConstraint DG.Tweening.Plugins.Options.VectorOptions::axisConstraint
	int32_t ___axisConstraint_0;
	// System.Boolean DG.Tweening.Plugins.Options.VectorOptions::snapping
	bool ___snapping_1;
};
// Native definition for P/Invoke marshalling of DG.Tweening.Plugins.Options.VectorOptions
struct VectorOptions_t2814CC842518C92C9DFC5DE6F7A73824758D3EF9_marshaled_pinvoke
{
	int32_t ___axisConstraint_0;
	int32_t ___snapping_1;
};
// Native definition for COM marshalling of DG.Tweening.Plugins.Options.VectorOptions
struct VectorOptions_t2814CC842518C92C9DFC5DE6F7A73824758D3EF9_marshaled_com
{
	int32_t ___axisConstraint_0;
	int32_t ___snapping_1;
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

// CsvParser/LineStartState
struct LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB  : public ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3
{
};

// CsvParser/QuoteState
struct QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E  : public ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3
{
};

// CsvParser/QuotedValueState
struct QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA  : public ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3
{
};

// CsvParser/ValueState
struct ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44  : public ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3
{
};

// MCamera/<>c__DisplayClass39_0
struct U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D 
{
	// UnityEngine.Camera MCamera/<>c__DisplayClass39_0::camera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera_0;
	// MCamera MCamera/<>c__DisplayClass39_0::<>4__this
	MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* ___U3CU3E4__this_1;
};

// TouchTopDownCamera/<>c__DisplayClass24_0
struct U3CU3Ec__DisplayClass24_0_tF67F393E60EBEB75B219167CBCB8A8DC6B7F9C3B 
{
	// System.Single TouchTopDownCamera/<>c__DisplayClass24_0::RotateCameraH
	float ___RotateCameraH_0;
	// System.Single TouchTopDownCamera/<>c__DisplayClass24_0::RotateCameraV
	float ___RotateCameraV_1;
};

// CenterSurroundCamera
struct CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 CenterSurroundCamera::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 CenterSurroundCamera::focuscenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___focuscenter_14;
	// UnityEngine.Quaternion CenterSurroundCamera::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 CenterSurroundCamera::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_16;
	// UnityEngine.Vector3 CenterSurroundCamera::xzOff_onstartrecord
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_onstartrecord_17;
	// System.Single CenterSurroundCamera::h
	float ___h_18;
	// System.Single CenterSurroundCamera::xAngleTemp
	float ___xAngleTemp_19;
	// UnityEngine.Vector3 CenterSurroundCamera::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_20;
};

// CertainYAntiVabration
struct CertainYAntiVabration_tC3F93440371E35297502C0F0431F942257E24AC9  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 CertainYAntiVabration::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 CertainYAntiVabration::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Quaternion CertainYAntiVabration::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 CertainYAntiVabration::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector3 CertainYAntiVabration::temp
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___temp_17;
	// UnityEngine.Vector2 CertainYAntiVabration::mescreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___mescreenpos_18;
	// UnityEngine.Vector2 CertainYAntiVabration::enemyscreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyscreenpos_19;
	// UnityEngine.Vector3 CertainYAntiVabration::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_20;
	// System.Single CertainYAntiVabration::fixy
	float ___fixy_21;
	// System.Single CertainYAntiVabration::h
	float ___h_22;
};

// CertainYAntiVibrationCamera
struct CertainYAntiVibrationCamera_tBE98E18C6C6A13DE240FEEAA67D2CC0074175BC6  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::enemiescenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiescenter_14;
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::focuscenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___focuscenter_15;
	// UnityEngine.Quaternion CertainYAntiVibrationCamera::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_16;
	// UnityEngine.Vector2 CertainYAntiVibrationCamera::screenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___screenpos_17;
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_18;
	// System.Single CertainYAntiVibrationCamera::angele
	float ___angele_19;
	// System.Single CertainYAntiVibrationCamera::h
	float ___h_20;
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::FirstPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___FirstPoint_21;
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::SecondPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___SecondPoint_22;
	// UnityEngine.Vector3 CertainYAntiVibrationCamera::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_23;
};

// ChatGptFix
struct ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 ChatGptFix::cameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___cameraTargetPos_13;
	// UnityEngine.Vector3 ChatGptFix::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Vector3 ChatGptFix::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_15;
	// UnityEngine.Vector2 ChatGptFix::meScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___meScreenPos_16;
	// UnityEngine.Vector2 ChatGptFix::enemyScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyScreenPos_17;
	// UnityEngine.Vector3 ChatGptFix::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_18;
	// UnityEngine.Vector3 ChatGptFix::lookPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lookPoint_19;
	// UnityEngine.Vector3 ChatGptFix::frontWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___frontWPos_20;
	// UnityEngine.Vector3 ChatGptFix::backWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backWPos_21;
	// UnityEngine.Quaternion ChatGptFix::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_22;
	// System.Single ChatGptFix::autoChangeAngleLimit
	float ___autoChangeAngleLimit_23;
	// System.Single ChatGptFix::autoRotateSpeed
	float ___autoRotateSpeed_24;
	// System.Single ChatGptFix::_changeSpeed
	float ____changeSpeed_25;
	// System.Single ChatGptFix::_transitionSpeedPara
	float ____transitionSpeedPara_26;
	// System.Single ChatGptFix::_lookPointHeight
	float ____lookPointHeight_27;
	// System.Single ChatGptFix::_minXZ
	float ____minXZ_28;
	// System.Single ChatGptFix::fieldOfView
	float ___fieldOfView_29;
	// System.Single ChatGptFix::screenDifferForRotate
	float ___screenDifferForRotate_30;
	// System.Single ChatGptFix::h
	float ___h_31;
	// System.Single ChatGptFix::ePosX
	float ___ePosX_32;
	// System.Single ChatGptFix::ePosY
	float ___ePosY_33;
	// System.Single ChatGptFix::mPosX
	float ___mPosX_34;
	// System.Single ChatGptFix::mPosY
	float ___mPosY_35;
	// System.Boolean ChatGptFix::_canSetH
	bool ____canSetH_36;
	// UnityEngine.Vector3 ChatGptFix::mePos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___mePos_37;
	// System.Single ChatGptFix::_autoRotateTimer
	float ____autoRotateTimer_38;
	// System.Boolean ChatGptFix::_currentRotateClockWiseDirection
	bool ____currentRotateClockWiseDirection_39;
};

// ChatGptFix2
struct ChatGptFix2_tA67A0EB8B87FEFA6B8FB70BF923327A8D5BEC9D3  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// System.Single ChatGptFix2::radius
	float ___radius_13;
	// System.Single ChatGptFix2::height
	float ___height_14;
	// System.Single ChatGptFix2::angle
	float ___angle_15;
	// System.Single ChatGptFix2::rotationSpeed
	float ___rotationSpeed_16;
	// System.Single ChatGptFix2::panSpeed
	float ___panSpeed_17;
	// UnityEngine.Vector3 ChatGptFix2::offset
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___offset_18;
	// UnityEngine.Vector3 ChatGptFix2::circleCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___circleCenter_19;
	// System.Single ChatGptFix2::rotationY
	float ___rotationY_20;
};

// UnityEngine.UI.ColorBlock
struct ColorBlock_tDD7C62E7AFE442652FC98F8D058CE8AE6BFD7C11 
{
	// UnityEngine.Color UnityEngine.UI.ColorBlock::m_NormalColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_NormalColor_0;
	// UnityEngine.Color UnityEngine.UI.ColorBlock::m_HighlightedColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_HighlightedColor_1;
	// UnityEngine.Color UnityEngine.UI.ColorBlock::m_PressedColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_PressedColor_2;
	// UnityEngine.Color UnityEngine.UI.ColorBlock::m_SelectedColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_SelectedColor_3;
	// UnityEngine.Color UnityEngine.UI.ColorBlock::m_DisabledColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_DisabledColor_4;
	// System.Single UnityEngine.UI.ColorBlock::m_ColorMultiplier
	float ___m_ColorMultiplier_5;
	// System.Single UnityEngine.UI.ColorBlock::m_FadeDuration
	float ___m_FadeDuration_6;
};

struct ColorBlock_tDD7C62E7AFE442652FC98F8D058CE8AE6BFD7C11_StaticFields
{
	// UnityEngine.UI.ColorBlock UnityEngine.UI.ColorBlock::defaultColorBlock
	ColorBlock_tDD7C62E7AFE442652FC98F8D058CE8AE6BFD7C11 ___defaultColorBlock_7;
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

// GodPlayerCertainY
struct GodPlayerCertainY_t2757B4570A2E99A42AFDDAFADFBC705E58F4F37A  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 GodPlayerCertainY::Xi
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___Xi_13;
	// UnityEngine.Vector3 GodPlayerCertainY::center
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___center_14;
	// UnityEngine.Quaternion GodPlayerCertainY::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
};

// GodplayerCamera
struct GodplayerCamera_t69B1E62878C9B321870DEB22A717E76037545DB2  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// System.Single GodplayerCamera::distance_use
	float ___distance_use_13;
	// System.Single GodplayerCamera::distance
	float ___distance_14;
	// System.Single GodplayerCamera::zoom_range
	float ___zoom_range_15;
	// System.Single GodplayerCamera::x
	float ___x_16;
	// System.Single GodplayerCamera::y
	float ___y_17;
	// System.Single GodplayerCamera::perspectiveZoomSpeed
	float ___perspectiveZoomSpeed_18;
	// System.Single GodplayerCamera::orthoZoomSpeed
	float ___orthoZoomSpeed_19;
	// UnityEngine.Vector3 GodplayerCamera::direction
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___direction_20;
	// UnityEngine.Vector3 GodplayerCamera::center
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___center_21;
};

// LerpToCertainDistance
struct LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// System.Single LerpToCertainDistance::distancefromtarget
	float ___distancefromtarget_13;
	// UnityEngine.Vector3 LerpToCertainDistance::targetcenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___targetcenter_14;
};

// MCamera
struct MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 MCamera::cameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___cameraTargetPos_13;
	// UnityEngine.Vector3 MCamera::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Vector3 MCamera::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_15;
	// UnityEngine.Vector2 MCamera::meScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___meScreenPos_16;
	// UnityEngine.Vector2 MCamera::enemyScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyScreenPos_17;
	// UnityEngine.Vector3 MCamera::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_18;
	// UnityEngine.Vector3 MCamera::lookPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lookPoint_19;
	// UnityEngine.Vector3 MCamera::frontWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___frontWPos_20;
	// UnityEngine.Vector3 MCamera::backWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backWPos_21;
	// UnityEngine.Quaternion MCamera::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_22;
	// System.Single MCamera::autoChangeAngleLimit
	float ___autoChangeAngleLimit_23;
	// System.Single MCamera::autoRotateSpeed
	float ___autoRotateSpeed_24;
	// System.Single MCamera::_changeSpeed
	float ____changeSpeed_25;
	// System.Single MCamera::_transitionSpeedPara
	float ____transitionSpeedPara_26;
	// System.Single MCamera::_lookPointHeight
	float ____lookPointHeight_27;
	// System.Single MCamera::_minXZ
	float ____minXZ_28;
	// System.Single MCamera::fieldOfView
	float ___fieldOfView_29;
	// System.Single MCamera::screenDifferForRotate
	float ___screenDifferForRotate_30;
	// System.Single MCamera::disToH
	float ___disToH_31;
	// System.Single MCamera::h
	float ___h_32;
	// System.Single MCamera::ePosX
	float ___ePosX_33;
	// System.Single MCamera::ePosY
	float ___ePosY_34;
	// System.Single MCamera::mPosX
	float ___mPosX_35;
	// System.Single MCamera::mPosY
	float ___mPosY_36;
	// System.Boolean MCamera::_canSetH
	bool ____canSetH_37;
	// UnityEngine.Vector3 MCamera::mePos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___mePos_38;
	// System.Single MCamera::_autoRotateTimer
	float ____autoRotateTimer_39;
	// System.Boolean MCamera::_currentRotateClockWiseDirection
	bool ____currentRotateClockWiseDirection_40;
};

// New2021
struct New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 New2021::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 New2021::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Quaternion New2021::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 New2021::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector3 New2021::temp
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___temp_17;
	// UnityEngine.Vector2 New2021::mescreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___mescreenpos_18;
	// UnityEngine.Vector2 New2021::enemyscreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyscreenpos_19;
	// UnityEngine.Vector3 New2021::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_20;
	// System.Single New2021::fixy
	float ___fixy_21;
	// System.Single New2021::h
	float ___h_22;
};

// New2022
struct New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 New2022::cameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___cameraTargetPos_13;
	// UnityEngine.Vector3 New2022::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Quaternion New2022::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 New2022::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector2 New2022::meScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___meScreenPos_17;
	// UnityEngine.Vector2 New2022::enemyScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyScreenPos_18;
	// UnityEngine.Vector3 New2022::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_19;
	// UnityEngine.Vector3 New2022::lookPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lookPoint_20;
	// UnityEngine.Vector3 New2022::frontWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___frontWPos_21;
	// UnityEngine.Vector3 New2022::backWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backWPos_22;
	// UnityEngine.Vector2 New2022::frontScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___frontScreenPos_23;
	// UnityEngine.Vector2 New2022::backScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___backScreenPos_24;
	// System.Single New2022::changeSpeed
	float ___changeSpeed_25;
	// System.Single New2022::minXZ
	float ___minXZ_26;
	// System.Single New2022::time_counter
	float ___time_counter_27;
	// System.Single New2022::autoRotateDelay
	float ___autoRotateDelay_28;
	// System.Single New2022::transitionSpeedPara
	float ___transitionSpeedPara_29;
	// System.Single New2022::fixY
	float ___fixY_30;
	// System.Single New2022::h
	float ___h_31;
	// System.Single New2022::rate
	float ___rate_32;
	// System.Single New2022::c_offSet
	float ___c_offSet_33;
};

// New2023
struct New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 New2023::cameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___cameraTargetPos_13;
	// UnityEngine.Vector3 New2023::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Vector3 New2023::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_15;
	// UnityEngine.Vector2 New2023::meScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___meScreenPos_16;
	// UnityEngine.Vector2 New2023::enemyScreenPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyScreenPos_17;
	// UnityEngine.Vector3 New2023::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_18;
	// UnityEngine.Vector3 New2023::lookPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lookPoint_19;
	// UnityEngine.Vector3 New2023::frontWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___frontWPos_20;
	// UnityEngine.Vector3 New2023::backWPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backWPos_21;
	// UnityEngine.Quaternion New2023::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_22;
	// System.Single New2023::_changeSpeed
	float ____changeSpeed_23;
	// System.Single New2023::_transitionSpeedPara
	float ____transitionSpeedPara_24;
	// System.Single New2023::_lookPointHeight
	float ____lookPointHeight_25;
	// System.Single New2023::_minXZ
	float ____minXZ_26;
	// System.Single New2023::h
	float ___h_27;
	// System.Single New2023::ePosX
	float ___ePosX_28;
	// System.Single New2023::ePosY
	float ___ePosY_29;
	// System.Single New2023::mPosX
	float ___mPosX_30;
	// System.Single New2023::mPosY
	float ___mPosY_31;
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

// OneVOneMode
struct OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 OneVOneMode::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 OneVOneMode::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Quaternion OneVOneMode::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 OneVOneMode::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector2 OneVOneMode::mescreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___mescreenpos_17;
	// UnityEngine.Vector2 OneVOneMode::enemyscreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyscreenpos_18;
	// UnityEngine.Vector3 OneVOneMode::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_19;
	// System.Single OneVOneMode::startAutoRotateRange
	float ___startAutoRotateRange_20;
	// System.Single OneVOneMode::xzMax
	float ___xzMax_21;
	// System.Single OneVOneMode::lookdownDegree
	float ___lookdownDegree_22;
	// System.Single OneVOneMode::zoomAcc
	float ___zoomAcc_23;
	// System.Single OneVOneMode::zoomcounter
	float ___zoomcounter_24;
	// System.Single OneVOneMode::zoomChangeInter
	float ___zoomChangeInter_25;
	// System.Single OneVOneMode::heightOfXZRate
	float ___heightOfXZRate_26;
	// System.Single OneVOneMode::xzd
	float ___xzd_27;
	// System.Boolean OneVOneMode::justEnterdThisMode
	bool ___justEnterdThisMode_28;
	// System.Single OneVOneMode::h
	float ___h_29;
	// System.Single OneVOneMode::maxheight
	float ___maxheight_30;
	// System.Single OneVOneMode::temp
	float ___temp_31;
	// System.Boolean OneVOneMode::zoomDirection
	bool ___zoomDirection_32;
	// UnityEngine.Vector3 OneVOneMode::SlerpCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___SlerpCenter_33;
	// UnityEngine.Vector3 OneVOneMode::tempV3
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___tempV3_34;
};

// OneVOneModeNew
struct OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 OneVOneModeNew::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 OneVOneModeNew::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Quaternion OneVOneModeNew::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 OneVOneModeNew::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector2 OneVOneModeNew::mescreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___mescreenpos_17;
	// UnityEngine.Vector2 OneVOneModeNew::enemyscreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyscreenpos_18;
	// UnityEngine.Vector3 OneVOneModeNew::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_19;
	// System.Single OneVOneModeNew::xzMax
	float ___xzMax_20;
	// System.Single OneVOneModeNew::lookdownDegree
	float ___lookdownDegree_21;
	// System.Single OneVOneModeNew::zoomAcc
	float ___zoomAcc_22;
	// System.Single OneVOneModeNew::zoomcounter
	float ___zoomcounter_23;
	// System.Single OneVOneModeNew::zoomChangeInter
	float ___zoomChangeInter_24;
	// System.Single OneVOneModeNew::heightOfXZRate
	float ___heightOfXZRate_25;
	// System.Single OneVOneModeNew::xzd
	float ___xzd_26;
	// System.Boolean OneVOneModeNew::justEnterdThisMode
	bool ___justEnterdThisMode_27;
	// System.Single OneVOneModeNew::h
	float ___h_28;
	// System.Single OneVOneModeNew::maxheight
	float ___maxheight_29;
	// System.Single OneVOneModeNew::temp
	float ___temp_30;
	// System.Boolean OneVOneModeNew::zoomDirection
	bool ___zoomDirection_31;
	// UnityEngine.Vector3 OneVOneModeNew::SlerpCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___SlerpCenter_32;
	// UnityEngine.Vector3 OneVOneModeNew::tempV3
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___tempV3_33;
};

// OneVOneMode_failed
struct OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 OneVOneMode_failed::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 OneVOneMode_failed::enemiesCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiesCenter_14;
	// UnityEngine.Quaternion OneVOneMode_failed::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 OneVOneMode_failed::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector2 OneVOneMode_failed::mescreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___mescreenpos_17;
	// UnityEngine.Vector2 OneVOneMode_failed::enemyscreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyscreenpos_18;
	// UnityEngine.Vector3 OneVOneMode_failed::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_19;
	// System.Single OneVOneMode_failed::autoRotateXZOffRange
	float ___autoRotateXZOffRange_20;
	// System.Single OneVOneMode_failed::autoRotateXZOffRangeMaxSpeed
	float ___autoRotateXZOffRangeMaxSpeed_21;
	// System.Single OneVOneMode_failed::xzMax
	float ___xzMax_22;
	// System.Single OneVOneMode_failed::lookdownDegree
	float ___lookdownDegree_23;
	// System.Single OneVOneMode_failed::zoomAcc
	float ___zoomAcc_24;
	// System.Single OneVOneMode_failed::heightOfXZRate
	float ___heightOfXZRate_25;
	// System.Single OneVOneMode_failed::xzd
	float ___xzd_26;
	// System.Single OneVOneMode_failed::h
	float ___h_27;
	// System.Single OneVOneMode_failed::maxheight
	float ___maxheight_28;
	// System.Boolean OneVOneMode_failed::zoomDirection
	bool ___zoomDirection_29;
};

// ScreenSaverC
struct ScreenSaverC_t57D260260EAF244CB16B9345A74371B1CAB86AE7  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 ScreenSaverC::CameraTargetPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___CameraTargetPos_13;
	// UnityEngine.Vector3 ScreenSaverC::enemiescenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___enemiescenter_14;
	// UnityEngine.Quaternion ScreenSaverC::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_15;
	// UnityEngine.Vector3 ScreenSaverC::rotateToDirection
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rotateToDirection_16;
	// UnityEngine.Vector2 ScreenSaverC::mescreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___mescreenpos_17;
	// UnityEngine.Vector2 ScreenSaverC::enemyscreenpos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___enemyscreenpos_18;
	// UnityEngine.Vector3 ScreenSaverC::xzOff
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___xzOff_19;
	// System.Single ScreenSaverC::h
	float ___h_20;
};

// DG.Tweening.Sequence
struct Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C  : public Tween_t8CB06EBC48A5B6F5065C490E4F4909C18CE7983C
{
	// System.Collections.Generic.List`1<DG.Tweening.Tween> DG.Tweening.Sequence::sequencedTweens
	List_1_tDA2C18E15C40590123A37DABB6D0D9AEB77A3BBD* ___sequencedTweens_53;
	// System.Collections.Generic.List`1<DG.Tweening.Core.ABSSequentiable> DG.Tweening.Sequence::_sequencedObjs
	List_1_t0C6BF1E3B166E9D2A63FC3291C519D61B950BFDC* ____sequencedObjs_54;
	// System.Single DG.Tweening.Sequence::lastTweenInsertTime
	float ___lastTweenInsertTime_55;
};

// StartToEndMode
struct StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 StartToEndMode::obj_position
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___obj_position_13;
	// UnityEngine.Quaternion StartToEndMode::obj_quaternion
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___obj_quaternion_14;
};

// System.IO.StringReader
struct StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8  : public TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7
{
	// System.String System.IO.StringReader::_s
	String_t* ____s_2;
	// System.Int32 System.IO.StringReader::_pos
	int32_t ____pos_3;
	// System.Int32 System.IO.StringReader::_length
	int32_t ____length_4;
};

// TeamEditCamera
struct TeamEditCamera_tCD912CE0B0950259EDA0CEA3351EF1FEAA085D9F  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// System.Single TeamEditCamera::distance
	float ___distance_13;
	// System.Single TeamEditCamera::height
	float ___height_14;
	// UnityEngine.Vector3 TeamEditCamera::direction
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___direction_15;
};

// TopDownWatchCamera
struct TopDownWatchCamera_tB31CB6E39C34F1D87B22F4B4D6E2171F68934989  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// System.Single TopDownWatchCamera::height
	float ___height_13;
	// System.Single TopDownWatchCamera::minRotation
	float ___minRotation_14;
	// System.Single TopDownWatchCamera::maxRotation
	float ___maxRotation_15;
	// System.Single TopDownWatchCamera::turnSmoothing
	float ___turnSmoothing_16;
	// System.Single TopDownWatchCamera::smoothX
	float ___smoothX_17;
	// System.Single TopDownWatchCamera::smoothY
	float ___smoothY_18;
	// System.Single TopDownWatchCamera::smoothXVelocity
	float ___smoothXVelocity_19;
	// System.Single TopDownWatchCamera::smoothYVelocity
	float ___smoothYVelocity_20;
	// UnityEngine.Transform TopDownWatchCamera::pivot
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___pivot_21;
	// System.Single TopDownWatchCamera::lookRotation
	float ___lookRotation_22;
	// System.Single TopDownWatchCamera::tiltRotation
	float ___tiltRotation_23;
	// UnityEngine.Vector3 TopDownWatchCamera::pos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___pos_24;
	// System.Single TopDownWatchCamera::h
	float ___h_25;
	// System.Single TopDownWatchCamera::v
	float ___v_26;
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

// TouchTopDownCamera
struct TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// System.Single TouchTopDownCamera::startPosSetDuration
	float ___startPosSetDuration_13;
	// System.Single TouchTopDownCamera::height
	float ___height_14;
	// System.Single TouchTopDownCamera::battlefieldDiameter
	float ___battlefieldDiameter_15;
	// UnityEngine.Vector3 TouchTopDownCamera::firstPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___firstPoint_16;
	// UnityEngine.Vector3 TouchTopDownCamera::secondPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___secondPoint_17;
	// UnityEngine.Vector3 TouchTopDownCamera::startFromPointWhenDrag
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___startFromPointWhenDrag_18;
	// DG.Tweening.Sequence TouchTopDownCamera::mainSequence
	Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___mainSequence_19;
	// System.Boolean TouchTopDownCamera::canTouch
	bool ___canTouch_20;
	// System.Single TouchTopDownCamera::groundHeight
	float ___groundHeight_21;
	// System.Single TouchTopDownCamera::followTargetSpeed
	float ___followTargetSpeed_22;
	// System.Single TouchTopDownCamera::rotationSpeed
	float ___rotationSpeed_23;
	// System.Boolean TouchTopDownCamera::isRotating
	bool ___isRotating_24;
	// System.Single TouchTopDownCamera::disAwayFromFront
	float ___disAwayFromFront_25;
	// System.Single TouchTopDownCamera::zoomScreenDis
	float ___zoomScreenDis_26;
	// System.Single TouchTopDownCamera::zoomSpeed
	float ___zoomSpeed_27;
	// System.Single TouchTopDownCamera::fieldOfView
	float ___fieldOfView_28;
	// UnityEngine.Vector3 TouchTopDownCamera::sameHeightCenter
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___sameHeightCenter_29;
	// System.Single TouchTopDownCamera::backDist
	float ___backDist_30;
	// System.Single TouchTopDownCamera::startCameraHeight
	float ___startCameraHeight_31;
};

// DG.Tweening.Tweener
struct Tweener_tD38633F1A42EDF47A73CE3BF1894D946E830E140  : public Tween_t8CB06EBC48A5B6F5065C490E4F4909C18CE7983C
{
	// System.Boolean DG.Tweening.Tweener::hasManuallySetStartValue
	bool ___hasManuallySetStartValue_53;
	// System.Boolean DG.Tweening.Tweener::isFromAllowed
	bool ___isFromAllowed_54;
};

// WatchOverCamera
struct WatchOverCamera_t0B35C99643DDFF8D8D63153C55E4862BAEC5C7F6  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 WatchOverCamera::direction
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___direction_13;
	// UnityEngine.Quaternion WatchOverCamera::ToRotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___ToRotation_14;
	// UnityEngine.Vector3 WatchOverCamera::center
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___center_15;
};

// keepTargetLeftCamera
struct keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95  : public CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97
{
	// UnityEngine.Vector3 keepTargetLeftCamera::center
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___center_13;
	// UnityEngine.Quaternion keepTargetLeftCamera::torotation
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___torotation_14;
};

// CsvParser/ValueStartState
struct ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B  : public LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB
{
};

// TouchTopDownCamera/<>c__DisplayClass21_0
struct U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66  : public RuntimeObject
{
	// TouchTopDownCamera TouchTopDownCamera/<>c__DisplayClass21_0::<>4__this
	TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* ___U3CU3E4__this_0;
	// UnityEngine.Vector3 TouchTopDownCamera/<>c__DisplayClass21_0::temp
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___temp_1;
	// UnityEngine.Camera TouchTopDownCamera/<>c__DisplayClass21_0::_camera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera_2;
};

// DG.Tweening.Core.TweenerCore`3<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions>
struct TweenerCore_3_t9A48A35EB4763F174321ED1A1BE49A67BC0A5C6F  : public Tweener_tD38633F1A42EDF47A73CE3BF1894D946E830E140
{
	// T2 DG.Tweening.Core.TweenerCore`3::startValue
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___startValue_55;
	// T2 DG.Tweening.Core.TweenerCore`3::endValue
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___endValue_56;
	// T2 DG.Tweening.Core.TweenerCore`3::changeValue
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___changeValue_57;
	// TPlugOptions DG.Tweening.Core.TweenerCore`3::plugOptions
	NoOptions_t2B4A2CA3C472B5AC37AACC090B1D0B27BCF4307E ___plugOptions_58;
	// DG.Tweening.Core.DOGetter`1<T1> DG.Tweening.Core.TweenerCore`3::getter
	DOGetter_1_tB89DD12456B8E79576BB70E1CA6DF899686410D3* ___getter_59;
	// DG.Tweening.Core.DOSetter`1<T1> DG.Tweening.Core.TweenerCore`3::setter
	DOSetter_1_t9EFF8DD70A15F455A6FE698A22BD0FE9683AC28E* ___setter_60;
	// DG.Tweening.Plugins.Core.ABSTweenPlugin`3<T1,T2,TPlugOptions> DG.Tweening.Core.TweenerCore`3::tweenPlugin
	ABSTweenPlugin_3_t56BEDD6B006DC2E8D499101DE8A2339425AE6A10* ___tweenPlugin_61;
};

// DG.Tweening.Core.TweenerCore`3<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions>
struct TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1  : public Tweener_tD38633F1A42EDF47A73CE3BF1894D946E830E140
{
	// T2 DG.Tweening.Core.TweenerCore`3::startValue
	float ___startValue_55;
	// T2 DG.Tweening.Core.TweenerCore`3::endValue
	float ___endValue_56;
	// T2 DG.Tweening.Core.TweenerCore`3::changeValue
	float ___changeValue_57;
	// TPlugOptions DG.Tweening.Core.TweenerCore`3::plugOptions
	FloatOptions_t8A9B05DB7CF6CC90A27F300C2977D91A48B3FEF5 ___plugOptions_58;
	// DG.Tweening.Core.DOGetter`1<T1> DG.Tweening.Core.TweenerCore`3::getter
	DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* ___getter_59;
	// DG.Tweening.Core.DOSetter`1<T1> DG.Tweening.Core.TweenerCore`3::setter
	DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* ___setter_60;
	// DG.Tweening.Plugins.Core.ABSTweenPlugin`3<T1,T2,TPlugOptions> DG.Tweening.Core.TweenerCore`3::tweenPlugin
	ABSTweenPlugin_3_t60F4DE5120CFD5986925189A0E775FAEAB4C59B9* ___tweenPlugin_61;
};

// DG.Tweening.Core.TweenerCore`3<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>
struct TweenerCore_3_tCD82DFC45FB71C681FA8659EA63A7D7D16BFFE77  : public Tweener_tD38633F1A42EDF47A73CE3BF1894D946E830E140
{
	// T2 DG.Tweening.Core.TweenerCore`3::startValue
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___startValue_55;
	// T2 DG.Tweening.Core.TweenerCore`3::endValue
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___endValue_56;
	// T2 DG.Tweening.Core.TweenerCore`3::changeValue
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___changeValue_57;
	// TPlugOptions DG.Tweening.Core.TweenerCore`3::plugOptions
	VectorOptions_t2814CC842518C92C9DFC5DE6F7A73824758D3EF9 ___plugOptions_58;
	// DG.Tweening.Core.DOGetter`1<T1> DG.Tweening.Core.TweenerCore`3::getter
	DOGetter_1_t709462C08281F3AA5DFEF36CAF91404B1004C338* ___getter_59;
	// DG.Tweening.Core.DOSetter`1<T1> DG.Tweening.Core.TweenerCore`3::setter
	DOSetter_1_t02E8F9920F174322F1CF5AC8BCDEAABD14A03358* ___setter_60;
	// DG.Tweening.Plugins.Core.ABSTweenPlugin`3<T1,T2,TPlugOptions> DG.Tweening.Core.TweenerCore`3::tweenPlugin
	ABSTweenPlugin_3_tE5A78BE46D046C07A6356B8AB596B2D00F9295E7* ___tweenPlugin_61;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
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

// PinchZoom
struct PinchZoom_t94309269E7A55D4CA4DDD01EB4CB93A0B47CFEDD  : public RuntimeObject
{
	// UnityEngine.Camera PinchZoom::camera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera_0;
	// System.Single PinchZoom::_perspectiveZoomSpeed
	float ____perspectiveZoomSpeed_1;
	// System.Single PinchZoom::_orthoZoomSpeed
	float ____orthoZoomSpeed_2;
	// UnityEngine.Touch PinchZoom::_touchZero
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 ____touchZero_3;
	// UnityEngine.Touch PinchZoom::_touchOne
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 ____touchOne_4;
	// System.Single PinchZoom::_touchZeroScreenPosX
	float ____touchZeroScreenPosX_5;
	// System.Single PinchZoom::_touchZeroScreenPosY
	float ____touchZeroScreenPosY_6;
	// UnityEngine.Vector2 PinchZoom::_touchZeroPrevPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ____touchZeroPrevPos_7;
	// UnityEngine.Vector2 PinchZoom::_touchOnePrevPos
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ____touchOnePrevPos_8;
	// System.Single PinchZoom::_prevTouchDeltaMag
	float ____prevTouchDeltaMag_9;
	// System.Single PinchZoom::_touchDeltaMag
	float ____touchDeltaMag_10;
	// System.Single PinchZoom::_deltaMagnitudeDiff
	float ____deltaMagnitudeDiff_11;
};

// UnityEngine.TextAsset
struct TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// DG.Tweening.Core.DOGetter`1<System.Single>
struct DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03  : public MulticastDelegate_t
{
};

// DG.Tweening.Core.DOSetter`1<System.Single>
struct DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200  : public MulticastDelegate_t
{
};

// System.Predicate`1<SampleTable/Row>
struct Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70  : public MulticastDelegate_t
{
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.Collider
struct Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// DG.Tweening.TweenCallback
struct TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24  : public MulticastDelegate_t
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

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// C2TDemo
struct C2TDemo_tE3F1FE59F2C6D939EA56302F04A6CAC556272F57  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// UnityEngine.TextAsset C2TDemo::csv
	TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___csv_4;
	// UnityEngine.UI.InputField C2TDemo::input
	InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* ___input_5;
	// UnityEngine.UI.Text C2TDemo::output
	Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* ___output_6;
};

// CameraManager
struct CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// UnityEngine.Camera CameraManager::mainCamera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___mainCamera_6;
	// UnityEngine.Camera CameraManager::subCamera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___subCamera_7;
	// UnityEngine.Transform CameraManager::StartPosRef
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___StartPosRef_8;
	// UnityEngine.Transform CameraManager::topDownModeEndRef
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___topDownModeEndRef_9;
	// CameraMode CameraManager::CurrentMode
	CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* ___CurrentMode_10;
	// System.Collections.Generic.IDictionary`2<C_Mode,CameraMode> CameraManager::CModeDic
	RuntimeObject* ___CModeDic_11;
};

struct CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields
{
	// UnityEngine.Camera CameraManager::_camera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera_4;
	// UnityEngine.Camera CameraManager::_subCamera
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____subCamera_5;
};

// Decomposition
struct Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// HittingDetection.HitBoxManager Decomposition::_HitBox
	HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* ____HitBox_4;
	// TrackControl Decomposition::TrackControl
	TrackControl_t6E36A5D737F65778D9B7BAEA6B3F42C479FD9566* ___TrackControl_5;
	// System.Single Decomposition::DestructionDelay
	float ___DestructionDelay_6;
	// System.Single Decomposition::stop_emission_delay
	float ___stop_emission_delay_7;
	// System.Collections.Generic.List`1<UnityEngine.MeshRenderer> Decomposition::to_be_faded_renderers
	List_1_t558592816DA880773C8A60C1EB777F3B092B68EC* ___to_be_faded_renderers_8;
	// System.String[] Decomposition::Attachments
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___Attachments_9;
	// DecompositionPool Decomposition::pool
	DecompositionPool_tB2DB3E05F320A6D6F54A6482A7BB94C11ACEC229* ___pool_10;
	// UnityEngine.Animations.PositionConstraint Decomposition::positionConstraint
	PositionConstraint_t574BE070FD49E61B0DC8B4CA53486634FD30377B* ___positionConstraint_11;
	// BO_Ani_E Decomposition::BO_Ani_E
	BO_Ani_E_tE52B3FFFAF6137845E7FCAF01A1A84991BAF3F6D* ___BO_Ani_E_12;
	// UnityEngine.ParticleSystem Decomposition::to_be_stop_emissions
	ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1* ___to_be_stop_emissions_13;
	// System.Single Decomposition::Counter
	float ___Counter_14;
	// System.Int32 Decomposition::<Phase>k__BackingField
	int32_t ___U3CPhaseU3Ek__BackingField_15;
	// System.Boolean Decomposition::<IsWeapon>k__BackingField
	bool ___U3CIsWeaponU3Ek__BackingField_16;
	// System.Boolean Decomposition::<hasParticle>k__BackingField
	bool ___U3ChasParticleU3Ek__BackingField_17;
	// UnityEngine.AudioSource Decomposition::<AudioSource>k__BackingField
	AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___U3CAudioSourceU3Ek__BackingField_18;
	// UnityEngine.Vector3 Decomposition::_tempPos
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____tempPos_19;
	// System.Single Decomposition::_disFromCenter
	float ____disFromCenter_20;
};

// HittingDetection.HitBoxManager
struct HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// FightParamsReference HittingDetection.HitBoxManager::_Raw_Target_Instance
	FightParamsReference_tF64DF89060040FE893FB00338DA2E6500E44A629* ____Raw_Target_Instance_4;
	// BO_Limb HittingDetection.HitBoxManager::_boHitBox
	BO_Limb_t34AE66D5B61AEC630DDA942E1000BC4247901966* ____boHitBox_5;
	// UnityEngine.Vector3 HittingDetection.HitBoxManager::_TrailModeStartPoint
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____TrailModeStartPoint_6;
	// System.Collections.Generic.IDictionary`2<UnityEngine.Collider,HittingDetection.HitPointPara> HittingDetection.HitBoxManager::_ballDetectHitPool
	RuntimeObject* ____ballDetectHitPool_7;
	// System.Collections.Generic.List`1<UnityEngine.Events.UnityAction> HittingDetection.HitBoxManager::_weaponEnergyExhaustMissions
	List_1_t81DD6D8E3F2D498C5E128E9488F7CC05E1881C4D* ____weaponEnergyExhaustMissions_8;
	// System.Single HittingDetection.HitBoxManager::_ContinuousDamage_Timer
	float ____ContinuousDamage_Timer_9;
	// System.Single HittingDetection.HitBoxManager::dh
	float ___dh_10;
	// System.Single HittingDetection.HitBoxManager::ds1
	float ___ds1_11;
	// System.Single HittingDetection.HitBoxManager::ds2
	float ___ds2_12;
	// System.Single HittingDetection.HitBoxManager::ds3
	float ___ds3_13;
	// System.Single HittingDetection.HitBoxManager::ds4
	float ___ds4_14;
	// System.Single HittingDetection.HitBoxManager::ds5
	float ___ds5_15;
	// System.Single HittingDetection.HitBoxManager::ds6
	float ___ds6_16;
	// System.Single HittingDetection.HitBoxManager::ds7
	float ___ds7_17;
	// System.Single HittingDetection.HitBoxManager::ds8
	float ___ds8_18;
	// System.Single HittingDetection.HitBoxManager::ds9
	float ___ds9_19;
	// System.Single HittingDetection.HitBoxManager::ActivateAfterTime
	float ___ActivateAfterTime_20;
	// HittingDetection.SpecificTarget HittingDetection.HitBoxManager::SpecificTarget
	int32_t ___SpecificTarget_21;
	// HittingDetection.DamageType HittingDetection.HitBoxManager::damage_type
	int32_t ___damage_type_22;
	// HittingDetection.WeaponMode HittingDetection.HitBoxManager::_WeaponMode
	int32_t ____WeaponMode_23;
	// System.Single HittingDetection.HitBoxManager::AT_weight
	float ___AT_weight_24;
	// System.Int32 HittingDetection.HitBoxManager::weaponHP
	int32_t ___weaponHP_25;
	// System.Int32 HittingDetection.HitBoxManager::heavyLevel
	int32_t ___heavyLevel_26;
	// System.Boolean HittingDetection.HitBoxManager::onGroundMagic
	bool ___onGroundMagic_27;
	// System.Boolean HittingDetection.HitBoxManager::effectSpreadOnBody
	bool ___effectSpreadOnBody_28;
	// Element HittingDetection.HitBoxManager::element
	int32_t ___element_29;
	// System.String HittingDetection.HitBoxManager::muzzle
	String_t* ___muzzle_30;
	// System.String HittingDetection.HitBoxManager::hitEffect
	String_t* ___hitEffect_31;
	// System.String HittingDetection.HitBoxManager::ExplosionEffect
	String_t* ___ExplosionEffect_32;
	// System.Boolean HittingDetection.HitBoxManager::ContinuousDamage
	bool ___ContinuousDamage_33;
	// System.Single HittingDetection.HitBoxManager::ContinuousDamageInterval
	float ___ContinuousDamageInterval_34;
	// System.Boolean HittingDetection.HitBoxManager::_enabled
	bool ____enabled_35;
	// System.Single HittingDetection.HitBoxManager::<CurrentHP>k__BackingField
	float ___U3CCurrentHPU3Ek__BackingField_36;
	// FightParamsReference HittingDetection.HitBoxManager::_attackerRef
	FightParamsReference_tF64DF89060040FE893FB00338DA2E6500E44A629* ____attackerRef_37;
	// TeamConfig HittingDetection.HitBoxManager::teamConfig
	TeamConfig_t9B18EF1FD184E83A5BF2F9A59AF6A3B6876D715E* ___teamConfig_38;
	// UnityEngine.Transform HittingDetection.HitBoxManager::_WeaponHolderCenter
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ____WeaponHolderCenter_39;
	// System.Boolean HittingDetection.HitBoxManager::HitFlesh
	bool ___HitFlesh_40;
	// System.Boolean HittingDetection.HitBoxManager::HitShield
	bool ___HitShield_41;
	// System.Collections.Generic.List`1<HittingDetection.Marker> HittingDetection.HitBoxManager::_markers
	List_1_tB084CC07F0D61ECD66AAB6B593690873EBF70AA1* ____markers_42;
	// System.Collections.Generic.List`1<UnityEngine.Transform> HittingDetection.HitBoxManager::_usedTargets
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ____usedTargets_43;
	// System.Collections.Generic.List`1<UnityEngine.Transform> HittingDetection.HitBoxManager::_Targets_Raw_Hit
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ____Targets_Raw_Hit_44;
	// System.Collections.Generic.List`1<UnityEngine.Transform> HittingDetection.HitBoxManager::_shieldsHit
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ____shieldsHit_45;
	// System.Collections.Generic.List`1<UnityEngine.Vector3> HittingDetection.HitBoxManager::_shieldHitPos
	List_1_t77B94703E05C519A9010DD0614F757F974E1CD8B* ____shieldHitPos_46;
	// System.Collections.Generic.List`1<HittingDetection.V_Damage> HittingDetection.HitBoxManager::hitsOnHealthBody
	List_1_t6449D5997D9677B34BE44A31FB5155C097352DE2* ___hitsOnHealthBody_47;
	// System.Boolean HittingDetection.HitBoxManager::_traditionalDefendMode
	bool ____traditionalDefendMode_48;
	// System.String HittingDetection.HitBoxManager::<GeneratedByStateKey>k__BackingField
	String_t* ___U3CGeneratedByStateKeyU3Ek__BackingField_49;
	// Log.HitBoxLifeEnding HittingDetection.HitBoxManager::hitBoxLifeEnding
	int32_t ___hitBoxLifeEnding_50;
	// System.Single HittingDetection.HitBoxManager::AT
	float ___AT_51;
	// UnityEngine.Coroutine HittingDetection.HitBoxManager::_delayEnableMarkers
	Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* ____delayEnableMarkers_52;
};

// HitBoxesProcesser
struct HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// System.Collections.Generic.List`1<Decomposition> HitBoxesProcesser::_processingDecompositions
	List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* ____processingDecompositions_6;
};

struct HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields
{
	// HitBoxesProcesser HitBoxesProcesser::Instance
	HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* ___Instance_4;
	// System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager> HitBoxesProcesser::ColliderHitBox
	Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* ___ColliderHitBox_5;
};

// UnityEngine.EventSystems.UIBehaviour
struct UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};

// UnityEngine.UI.Graphic
struct Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931  : public UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D
{
	// UnityEngine.Material UnityEngine.UI.Graphic::m_Material
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___m_Material_6;
	// UnityEngine.Color UnityEngine.UI.Graphic::m_Color
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_Color_7;
	// System.Boolean UnityEngine.UI.Graphic::m_SkipLayoutUpdate
	bool ___m_SkipLayoutUpdate_8;
	// System.Boolean UnityEngine.UI.Graphic::m_SkipMaterialUpdate
	bool ___m_SkipMaterialUpdate_9;
	// System.Boolean UnityEngine.UI.Graphic::m_RaycastTarget
	bool ___m_RaycastTarget_10;
	// UnityEngine.Vector4 UnityEngine.UI.Graphic::m_RaycastPadding
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___m_RaycastPadding_11;
	// UnityEngine.RectTransform UnityEngine.UI.Graphic::m_RectTransform
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___m_RectTransform_12;
	// UnityEngine.CanvasRenderer UnityEngine.UI.Graphic::m_CanvasRenderer
	CanvasRenderer_tAB9A55A976C4E3B2B37D0CE5616E5685A8B43860* ___m_CanvasRenderer_13;
	// UnityEngine.Canvas UnityEngine.UI.Graphic::m_Canvas
	Canvas_t2DB4CEFDFF732884866C83F11ABF75F5AE8FFB26* ___m_Canvas_14;
	// System.Boolean UnityEngine.UI.Graphic::m_VertsDirty
	bool ___m_VertsDirty_15;
	// System.Boolean UnityEngine.UI.Graphic::m_MaterialDirty
	bool ___m_MaterialDirty_16;
	// UnityEngine.Events.UnityAction UnityEngine.UI.Graphic::m_OnDirtyLayoutCallback
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___m_OnDirtyLayoutCallback_17;
	// UnityEngine.Events.UnityAction UnityEngine.UI.Graphic::m_OnDirtyVertsCallback
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___m_OnDirtyVertsCallback_18;
	// UnityEngine.Events.UnityAction UnityEngine.UI.Graphic::m_OnDirtyMaterialCallback
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___m_OnDirtyMaterialCallback_19;
	// UnityEngine.Mesh UnityEngine.UI.Graphic::m_CachedMesh
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___m_CachedMesh_22;
	// UnityEngine.Vector2[] UnityEngine.UI.Graphic::m_CachedUvs
	Vector2U5BU5D_tFEBBC94BCC6C9C88277BA04047D2B3FDB6ED7FDA* ___m_CachedUvs_23;
	// UnityEngine.UI.CoroutineTween.TweenRunner`1<UnityEngine.UI.CoroutineTween.ColorTween> UnityEngine.UI.Graphic::m_ColorTweenRunner
	TweenRunner_1_t5BB0582F926E75E2FE795492679A6CF55A4B4BC4* ___m_ColorTweenRunner_24;
	// System.Boolean UnityEngine.UI.Graphic::<useLegacyMeshGeneration>k__BackingField
	bool ___U3CuseLegacyMeshGenerationU3Ek__BackingField_25;
};

struct Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931_StaticFields
{
	// UnityEngine.Material UnityEngine.UI.Graphic::s_DefaultUI
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___s_DefaultUI_4;
	// UnityEngine.Texture2D UnityEngine.UI.Graphic::s_WhiteTexture
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___s_WhiteTexture_5;
	// UnityEngine.Mesh UnityEngine.UI.Graphic::s_Mesh
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___s_Mesh_20;
	// UnityEngine.UI.VertexHelper UnityEngine.UI.Graphic::s_VertexHelper
	VertexHelper_tB905FCB02AE67CBEE5F265FE37A5938FC5D136FE* ___s_VertexHelper_21;
};

// UnityEngine.UI.Selectable
struct Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712  : public UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D
{
	// System.Boolean UnityEngine.UI.Selectable::m_EnableCalled
	bool ___m_EnableCalled_6;
	// UnityEngine.UI.Navigation UnityEngine.UI.Selectable::m_Navigation
	Navigation_t4D2E201D65749CF4E104E8AC1232CF1D6F14795C ___m_Navigation_7;
	// UnityEngine.UI.Selectable/Transition UnityEngine.UI.Selectable::m_Transition
	int32_t ___m_Transition_8;
	// UnityEngine.UI.ColorBlock UnityEngine.UI.Selectable::m_Colors
	ColorBlock_tDD7C62E7AFE442652FC98F8D058CE8AE6BFD7C11 ___m_Colors_9;
	// UnityEngine.UI.SpriteState UnityEngine.UI.Selectable::m_SpriteState
	SpriteState_tC8199570BE6337FB5C49347C97892B4222E5AACD ___m_SpriteState_10;
	// UnityEngine.UI.AnimationTriggers UnityEngine.UI.Selectable::m_AnimationTriggers
	AnimationTriggers_tA0DC06F89C5280C6DD972F6F4C8A56D7F4F79074* ___m_AnimationTriggers_11;
	// System.Boolean UnityEngine.UI.Selectable::m_Interactable
	bool ___m_Interactable_12;
	// UnityEngine.UI.Graphic UnityEngine.UI.Selectable::m_TargetGraphic
	Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931* ___m_TargetGraphic_13;
	// System.Boolean UnityEngine.UI.Selectable::m_GroupsAllowInteraction
	bool ___m_GroupsAllowInteraction_14;
	// System.Int32 UnityEngine.UI.Selectable::m_CurrentIndex
	int32_t ___m_CurrentIndex_15;
	// System.Boolean UnityEngine.UI.Selectable::<isPointerInside>k__BackingField
	bool ___U3CisPointerInsideU3Ek__BackingField_16;
	// System.Boolean UnityEngine.UI.Selectable::<isPointerDown>k__BackingField
	bool ___U3CisPointerDownU3Ek__BackingField_17;
	// System.Boolean UnityEngine.UI.Selectable::<hasSelection>k__BackingField
	bool ___U3ChasSelectionU3Ek__BackingField_18;
	// System.Collections.Generic.List`1<UnityEngine.CanvasGroup> UnityEngine.UI.Selectable::m_CanvasGroupCache
	List_1_t2CDCA768E7F493F5EDEBC75AEB200FD621354E35* ___m_CanvasGroupCache_19;
};

struct Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712_StaticFields
{
	// UnityEngine.UI.Selectable[] UnityEngine.UI.Selectable::s_Selectables
	SelectableU5BU5D_t4160E135F02A40F75A63F787D36F31FEC6FE91A9* ___s_Selectables_4;
	// System.Int32 UnityEngine.UI.Selectable::s_SelectableCount
	int32_t ___s_SelectableCount_5;
};

// UnityEngine.UI.InputField
struct InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140  : public Selectable_t3251808068A17B8E92FB33590A4C2FA66D456712
{
	// UnityEngine.TouchScreenKeyboard UnityEngine.UI.InputField::m_Keyboard
	TouchScreenKeyboard_tE87B78A3DAED69816B44C99270A734682E093E7A* ___m_Keyboard_20;
	// UnityEngine.UI.Text UnityEngine.UI.InputField::m_TextComponent
	Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* ___m_TextComponent_24;
	// UnityEngine.UI.Graphic UnityEngine.UI.InputField::m_Placeholder
	Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931* ___m_Placeholder_25;
	// UnityEngine.UI.InputField/ContentType UnityEngine.UI.InputField::m_ContentType
	int32_t ___m_ContentType_26;
	// UnityEngine.UI.InputField/InputType UnityEngine.UI.InputField::m_InputType
	int32_t ___m_InputType_27;
	// System.Char UnityEngine.UI.InputField::m_AsteriskChar
	Il2CppChar ___m_AsteriskChar_28;
	// UnityEngine.TouchScreenKeyboardType UnityEngine.UI.InputField::m_KeyboardType
	int32_t ___m_KeyboardType_29;
	// UnityEngine.UI.InputField/LineType UnityEngine.UI.InputField::m_LineType
	int32_t ___m_LineType_30;
	// System.Boolean UnityEngine.UI.InputField::m_HideMobileInput
	bool ___m_HideMobileInput_31;
	// UnityEngine.UI.InputField/CharacterValidation UnityEngine.UI.InputField::m_CharacterValidation
	int32_t ___m_CharacterValidation_32;
	// System.Int32 UnityEngine.UI.InputField::m_CharacterLimit
	int32_t ___m_CharacterLimit_33;
	// UnityEngine.UI.InputField/SubmitEvent UnityEngine.UI.InputField::m_OnSubmit
	SubmitEvent_t1E0F5A2AB28D0DB55AE18E8DA99147D86492DD5D* ___m_OnSubmit_34;
	// UnityEngine.UI.InputField/EndEditEvent UnityEngine.UI.InputField::m_OnDidEndEdit
	EndEditEvent_t946A962BA13CF60BB0BE7AD091DA041FD788E655* ___m_OnDidEndEdit_35;
	// UnityEngine.UI.InputField/OnChangeEvent UnityEngine.UI.InputField::m_OnValueChanged
	OnChangeEvent_tE4829F88300B0E0E0D1B78B453AF25FC1AA55E2F* ___m_OnValueChanged_36;
	// UnityEngine.UI.InputField/OnValidateInput UnityEngine.UI.InputField::m_OnValidateInput
	OnValidateInput_t48916A4E9C9FD6204401FF0808C2B7A93D73418B* ___m_OnValidateInput_37;
	// UnityEngine.Color UnityEngine.UI.InputField::m_CaretColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_CaretColor_38;
	// System.Boolean UnityEngine.UI.InputField::m_CustomCaretColor
	bool ___m_CustomCaretColor_39;
	// UnityEngine.Color UnityEngine.UI.InputField::m_SelectionColor
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_SelectionColor_40;
	// System.String UnityEngine.UI.InputField::m_Text
	String_t* ___m_Text_41;
	// System.Single UnityEngine.UI.InputField::m_CaretBlinkRate
	float ___m_CaretBlinkRate_42;
	// System.Int32 UnityEngine.UI.InputField::m_CaretWidth
	int32_t ___m_CaretWidth_43;
	// System.Boolean UnityEngine.UI.InputField::m_ReadOnly
	bool ___m_ReadOnly_44;
	// System.Boolean UnityEngine.UI.InputField::m_ShouldActivateOnSelect
	bool ___m_ShouldActivateOnSelect_45;
	// System.Int32 UnityEngine.UI.InputField::m_CaretPosition
	int32_t ___m_CaretPosition_46;
	// System.Int32 UnityEngine.UI.InputField::m_CaretSelectPosition
	int32_t ___m_CaretSelectPosition_47;
	// UnityEngine.RectTransform UnityEngine.UI.InputField::caretRectTrans
	RectTransform_t6C5DA5E41A89E0F488B001E45E58963480E543A5* ___caretRectTrans_48;
	// UnityEngine.UIVertex[] UnityEngine.UI.InputField::m_CursorVerts
	UIVertexU5BU5D_tBC532486B45D071A520751A90E819C77BA4E3D2F* ___m_CursorVerts_49;
	// UnityEngine.TextGenerator UnityEngine.UI.InputField::m_InputTextCache
	TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC* ___m_InputTextCache_50;
	// UnityEngine.CanvasRenderer UnityEngine.UI.InputField::m_CachedInputRenderer
	CanvasRenderer_tAB9A55A976C4E3B2B37D0CE5616E5685A8B43860* ___m_CachedInputRenderer_51;
	// System.Boolean UnityEngine.UI.InputField::m_PreventFontCallback
	bool ___m_PreventFontCallback_52;
	// UnityEngine.Mesh UnityEngine.UI.InputField::m_Mesh
	Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* ___m_Mesh_53;
	// System.Boolean UnityEngine.UI.InputField::m_AllowInput
	bool ___m_AllowInput_54;
	// System.Boolean UnityEngine.UI.InputField::m_ShouldActivateNextUpdate
	bool ___m_ShouldActivateNextUpdate_55;
	// System.Boolean UnityEngine.UI.InputField::m_UpdateDrag
	bool ___m_UpdateDrag_56;
	// System.Boolean UnityEngine.UI.InputField::m_DragPositionOutOfBounds
	bool ___m_DragPositionOutOfBounds_57;
	// System.Boolean UnityEngine.UI.InputField::m_CaretVisible
	bool ___m_CaretVisible_60;
	// UnityEngine.Coroutine UnityEngine.UI.InputField::m_BlinkCoroutine
	Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* ___m_BlinkCoroutine_61;
	// System.Single UnityEngine.UI.InputField::m_BlinkStartTime
	float ___m_BlinkStartTime_62;
	// System.Int32 UnityEngine.UI.InputField::m_DrawStart
	int32_t ___m_DrawStart_63;
	// System.Int32 UnityEngine.UI.InputField::m_DrawEnd
	int32_t ___m_DrawEnd_64;
	// UnityEngine.Coroutine UnityEngine.UI.InputField::m_DragCoroutine
	Coroutine_t85EA685566A254C23F3FD77AB5BDFFFF8799596B* ___m_DragCoroutine_65;
	// System.String UnityEngine.UI.InputField::m_OriginalText
	String_t* ___m_OriginalText_66;
	// System.Boolean UnityEngine.UI.InputField::m_WasCanceled
	bool ___m_WasCanceled_67;
	// System.Boolean UnityEngine.UI.InputField::m_HasDoneFocusTransition
	bool ___m_HasDoneFocusTransition_68;
	// UnityEngine.WaitForSecondsRealtime UnityEngine.UI.InputField::m_WaitForSecondsRealtime
	WaitForSecondsRealtime_tA8CE0AAB4B0C872B843E7973637037D17682BA01* ___m_WaitForSecondsRealtime_69;
	// System.Boolean UnityEngine.UI.InputField::m_TouchKeyboardAllowsInPlaceEditing
	bool ___m_TouchKeyboardAllowsInPlaceEditing_70;
	// UnityEngine.Event UnityEngine.UI.InputField::m_ProcessingEvent
	Event_tEBC6F24B56CE22B9C9AD1AC6C24A6B83BC3860CB* ___m_ProcessingEvent_73;
};

struct InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140_StaticFields
{
	// System.Char[] UnityEngine.UI.InputField::kSeparators
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___kSeparators_21;
	// System.Boolean UnityEngine.UI.InputField::s_IsQuestDeviceEvaluated
	bool ___s_IsQuestDeviceEvaluated_22;
	// System.Boolean UnityEngine.UI.InputField::s_IsQuestDevice
	bool ___s_IsQuestDevice_23;
};

// UnityEngine.UI.MaskableGraphic
struct MaskableGraphic_tFC5B6BE351C90DE53744DF2A70940242774B361E  : public Graphic_tCBFCA4585A19E2B75465AECFEAC43F4016BF7931
{
	// System.Boolean UnityEngine.UI.MaskableGraphic::m_ShouldRecalculateStencil
	bool ___m_ShouldRecalculateStencil_26;
	// UnityEngine.Material UnityEngine.UI.MaskableGraphic::m_MaskMaterial
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___m_MaskMaterial_27;
	// UnityEngine.UI.RectMask2D UnityEngine.UI.MaskableGraphic::m_ParentMask
	RectMask2D_tACF92BE999C791A665BD1ADEABF5BCEB82846670* ___m_ParentMask_28;
	// System.Boolean UnityEngine.UI.MaskableGraphic::m_Maskable
	bool ___m_Maskable_29;
	// System.Boolean UnityEngine.UI.MaskableGraphic::m_IsMaskingGraphic
	bool ___m_IsMaskingGraphic_30;
	// System.Boolean UnityEngine.UI.MaskableGraphic::m_IncludeForMasking
	bool ___m_IncludeForMasking_31;
	// UnityEngine.UI.MaskableGraphic/CullStateChangedEvent UnityEngine.UI.MaskableGraphic::m_OnCullStateChanged
	CullStateChangedEvent_t6073CD0D951EC1256BF74B8F9107D68FC89B99B8* ___m_OnCullStateChanged_32;
	// System.Boolean UnityEngine.UI.MaskableGraphic::m_ShouldRecalculate
	bool ___m_ShouldRecalculate_33;
	// System.Int32 UnityEngine.UI.MaskableGraphic::m_StencilValue
	int32_t ___m_StencilValue_34;
	// UnityEngine.Vector3[] UnityEngine.UI.MaskableGraphic::m_Corners
	Vector3U5BU5D_tFF1859CCE176131B909E2044F76443064254679C* ___m_Corners_35;
};

// UnityEngine.UI.Text
struct Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62  : public MaskableGraphic_tFC5B6BE351C90DE53744DF2A70940242774B361E
{
	// UnityEngine.UI.FontData UnityEngine.UI.Text::m_FontData
	FontData_tB8E562846C6CB59C43260F69AE346B9BF3157224* ___m_FontData_36;
	// System.String UnityEngine.UI.Text::m_Text
	String_t* ___m_Text_37;
	// UnityEngine.TextGenerator UnityEngine.UI.Text::m_TextCache
	TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC* ___m_TextCache_38;
	// UnityEngine.TextGenerator UnityEngine.UI.Text::m_TextCacheForLayout
	TextGenerator_t85D00417640A53953556C01F9D4E7DDE1ABD8FEC* ___m_TextCacheForLayout_39;
	// System.Boolean UnityEngine.UI.Text::m_DisableFontTextureRebuiltCallback
	bool ___m_DisableFontTextureRebuiltCallback_41;
	// UnityEngine.UIVertex[] UnityEngine.UI.Text::m_TempVerts
	UIVertexU5BU5D_tBC532486B45D071A520751A90E819C77BA4E3D2F* ___m_TempVerts_42;
};

struct Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62_StaticFields
{
	// UnityEngine.Material UnityEngine.UI.Text::s_DefaultText
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___s_DefaultText_40;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.String[][]
struct StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF  : public RuntimeArray
{
	ALIGN_FIELD (8) StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* m_Items[1];

	inline StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
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


// System.Void System.Collections.Generic.List`1<System.Object>::Clear()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Object>::TryGetValue(TKey,TValue&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_TryGetValue_mD15380A4ED7CDEE99EA45881577D26BA9CE1B849_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, RuntimeObject** ___value1, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Object>::ContainsKey(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_ContainsKey_m703047C213F7AB55C9DC346596287773A1F670CD_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::Add(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_m93FFFABE8FCE7FA9793F0915E2A8842C7CD0C0C1_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___key0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<System.Object>::get_Count()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1<System.Object>::get_Item(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1<System.Object>::Contains(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m4C9139C2A6B23E9343D3F87807B32C6E2CFE660D_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::Add(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.KeyValuePair`2<System.Int32Enum,System.Object>::get_Value()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* KeyValuePair_2_get_Value_m415A21240AEF58C2E0A2FBA97E2BB75637781DB5_gshared_inline (KeyValuePair_2_tF70DDE0C5A349727371FB070D433FA147032A13B* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Int32Enum,System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_mCC9983804D8DC41E938E080075F9EA7BDD0C7059_gshared (Dictionary_2_t514396B90715EDD83BB0470C76C2F426F9381C71* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Int32Enum,System.Object>::Add(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_mC515884C0546021A29DC0A00DBCABD89B1B65872_gshared (Dictionary_2_t514396B90715EDD83BB0470C76C2F426F9381C71* __this, int32_t ___key0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.Object>::GetEnumerator()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1/Enumerator<System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1/Enumerator<System.Object>::get_Current()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Void DG.Tweening.Core.DOGetter`1<System.Single>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA_gshared (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Void DG.Tweening.Core.DOSetter`1<System.Single>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E_gshared (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::AddRange(System.Collections.Generic.IEnumerable`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m1F76B300133150E6046C5FED00E88B5DE0A02E17_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___collection0, const RuntimeMethod* method) ;
// T DG.Tweening.TweenSettingsExtensions::OnStart<System.Object>(T,DG.Tweening.TweenCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* TweenSettingsExtensions_OnStart_TisRuntimeObject_m520A807423D9F89B8401A562D0941BAC0060C802_gshared (RuntimeObject* ___t0, TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___action1, const RuntimeMethod* method) ;
// T DG.Tweening.TweenExtensions::Play<System.Object>(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* TweenExtensions_Play_TisRuntimeObject_m9C5B8B16699BA91E6605510B84969F71F944D46F_gshared (RuntimeObject* ___t0, const RuntimeMethod* method) ;
// System.Void System.Predicate`1<System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Predicate_1__ctor_m3E007299121A15DF80F4A210FF8C20E5DF688F20_gshared (Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1<System.Object>::Find(System.Predicate`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_Find_m5E78A210541B0D844FE27B94F509313623BE33D3_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* ___match0, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1<System.Object>::FindAll(System.Predicate`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* List_1_FindAll_m87FB5AB35229967D01B9DF933BF70D470B32F0AF_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* ___match0, const RuntimeMethod* method) ;
// T[] System.Collections.Generic.List`1<System.Object>::ToArray()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* List_1_ToArray_mD7E4F8E7C11C3C67CB5739FCC0A6E86106A6291F_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;

// System.Void System.Collections.Generic.List`1<Decomposition>::Clear()
inline void List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_inline (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*, const RuntimeMethod*))List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline)(__this, method);
}
// System.Boolean System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager>::TryGetValue(TKey,TValue&)
inline bool Dictionary_2_TryGetValue_m3335583D1D1EE1BECD3037C120CA3B3BEBDD9D71 (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* __this, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* ___key0, HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC** ___value1, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A*, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76*, HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC**, const RuntimeMethod*))Dictionary_2_TryGetValue_mD15380A4ED7CDEE99EA45881577D26BA9CE1B849_gshared)(__this, ___key0, ___value1, method);
}
// System.Boolean UnityEngine.Object::op_Inequality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___x0, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___y1, const RuntimeMethod* method) ;
// System.Void HitBoxesProcesser::AddToHitBoxesProcessorList(Decomposition)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_AddToHitBoxesProcessorList_m52EC3EDCF36BE2874F5A7F3A229A4E09876C12E4 (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* ___poolObject0, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager>::ContainsKey(TKey)
inline bool Dictionary_2_ContainsKey_m19FC3A712B339AC1EC6CC0D81D8BB425B022B97C (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* __this, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* ___key0, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A*, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76*, const RuntimeMethod*))Dictionary_2_ContainsKey_m703047C213F7AB55C9DC346596287773A1F670CD_gshared)(__this, ___key0, method);
}
// System.Void System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager>::Add(TKey,TValue)
inline void Dictionary_2_Add_m7A3E3FD907B5C5FC6FACEDF50DC5C1C6A6C67F19 (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* __this, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* ___key0, HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* ___value1, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A*, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76*, HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC*, const RuntimeMethod*))Dictionary_2_Add_m93FFFABE8FCE7FA9793F0915E2A8842C7CD0C0C1_gshared)(__this, ___key0, ___value1, method);
}
// System.Int32 System.Collections.Generic.List`1<Decomposition>::get_Count()
inline int32_t List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_inline (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
// T System.Collections.Generic.List`1<Decomposition>::get_Item(System.Int32)
inline Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5 (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* __this, int32_t ___index0, const RuntimeMethod* method)
{
	return ((  Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* (*) (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*, int32_t, const RuntimeMethod*))List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared)(__this, ___index0, method);
}
// System.Void Decomposition::Step1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Decomposition_Step1_m957D30E318BDC2C1853E6782E73A915835412277 (Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* __this, const RuntimeMethod* method) ;
// System.Void Decomposition::Step2()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Decomposition_Step2_m6E52C741631ADEC4861C6AAE5EBEF9EAACCF1303 (Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* __this, const RuntimeMethod* method) ;
// System.Void Decomposition::Life()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Decomposition_Life_m7DC57919FD13B2B01F2A32CC2B5554CDEAD99D62 (Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1<Decomposition>::Contains(T)
inline bool List_1_Contains_mDE448B160DBA47CFE50F34A3524289C69870B992 (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* __this, Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* ___item0, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*, Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53*, const RuntimeMethod*))List_1_Contains_m4C9139C2A6B23E9343D3F87807B32C6E2CFE660D_gshared)(__this, ___item0, method);
}
// System.Void System.Collections.Generic.List`1<Decomposition>::Add(T)
inline void List_1_Add_mBFF86E22A26E9ED0D216F526BFBF7A7546991F38_inline (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* __this, Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* ___item0, const RuntimeMethod* method)
{
	((  void (*) (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*, Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___item0, method);
}
// System.Void System.Collections.Generic.List`1<Decomposition>::.ctor()
inline void List_1__ctor_m362AED7E17D370D578FF476B1FF74A9236A96783 (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void UnityEngine.MonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<UnityEngine.Collider,HittingDetection.HitBoxManager>::.ctor()
inline void Dictionary_2__ctor_m9C0EC68028100E8C91D57975D3FA9279791E676F (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A*, const RuntimeMethod*))Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared)(__this, method);
}
// UnityEngine.Transform UnityEngine.Component::get_transform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371 (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_position()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::set_position(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___value0, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Transform::get_rotation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::set_rotation(UnityEngine.Quaternion)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___value0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Camera::set_depthTextureMode(UnityEngine.DepthTextureMode)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Camera_set_depthTextureMode_mE722389E4DF8B3DF7F6100DB142E4DBAF698F6BF (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, int32_t ___value0, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.KeyValuePair`2<C_Mode,CameraMode>::get_Value()
inline CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* KeyValuePair_2_get_Value_m9B30E68334E34583A8C40B04DEB897A4800203F9_inline (KeyValuePair_2_t16437782916F5E7884151CEF28CCC71F0FDEBAE4* __this, const RuntimeMethod* method)
{
	return ((  CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* (*) (KeyValuePair_2_t16437782916F5E7884151CEF28CCC71F0FDEBAE4*, const RuntimeMethod*))KeyValuePair_2_get_Value_m415A21240AEF58C2E0A2FBA97E2BB75637781DB5_gshared_inline)(__this, method);
}
// System.Void CameraMode::SetMeCenter(UnityEngine.Transform)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CameraMode_SetMeCenter_m7EF634EA83FBD929B8E52E998076BCE50F5AB33D_inline (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___meCenter0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<C_Mode,CameraMode>::.ctor()
inline void Dictionary_2__ctor_m665BD95251217FF9BEAAE59FB36F09C3CB9E2012 (Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44*, const RuntimeMethod*))Dictionary_2__ctor_mCC9983804D8DC41E938E080075F9EA7BDD0C7059_gshared)(__this, method);
}
// System.Void ChatGptFix::.ctor(System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix__ctor_m71A6397CC055AD4595AF6204E084B90CDC9DE526 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___XZDis0, float ___YDis1, float ___fieldOfView2, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<C_Mode,CameraMode>::Add(TKey,TValue)
inline void Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B (Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* __this, int32_t ___key0, CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* ___value1, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44*, int32_t, CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*, const RuntimeMethod*))Dictionary_2_Add_mC515884C0546021A29DC0A00DBCABD89B1B65872_gshared)(__this, ___key0, ___value1, method);
}
// System.Void LerpToCertainDistance::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LerpToCertainDistance__ctor_mAB95B31D424196399B9CC64124D6A8B0554663B7 (LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD* __this, float ___distance0, float ___speed1, const RuntimeMethod* method) ;
// System.Void keepTargetLeftCamera::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void keepTargetLeftCamera__ctor_m05CDA1EFB71DFD240F297A24586B20B01DB78BBA (keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95* __this, const RuntimeMethod* method) ;
// System.Void MCamera::.ctor(System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera__ctor_m0C7A5B62FD9724E9A21B55C82C19887BA48A8623 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___XZDis0, float ___YDis1, float ___fieldOfView2, const RuntimeMethod* method) ;
// System.Void StartToEndMode::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StartToEndMode__ctor_m6737649C2652021444D48FCEAAE9DE93C67D8234 (StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338* __this, const RuntimeMethod* method) ;
// System.Void CenterSurroundCamera::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CenterSurroundCamera__ctor_m250239560EAEA5F20A97A7E4709B079CCB0819A5 (CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) ;
// System.Void TouchTopDownCamera::.ctor(System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera__ctor_m937F9275C5485EB3574AABC647DD50ABCC1A430B (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, float ___height0, float ___battlefieldDiameter1, float ___fieldOfView2, const RuntimeMethod* method) ;
// System.Void New2023::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023__ctor_m1A3A1B18E2487893683BED1513A0B328793D69E6 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_zero()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_up()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline (const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::AngleAxis(System.Single,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8 (float ___angle0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___axis1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_left()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_left_mA75C525C1E78B5BB99E9B7A63EF68C731043FE18_inline (const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::op_Multiply(UnityEngine.Quaternion,UnityEngine.Quaternion)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_op_Multiply_m5AC8B39C55015059BDD09122E04E47D4BFAB2276_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___lhs0, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___rhs1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Quaternion::op_Multiply(UnityEngine.Quaternion,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0 (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___rotation0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___point1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_normalized()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_forward()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline (const RuntimeMethod* method) ;
// System.Void CameraMode::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8 (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Input::GetAxis(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4 (String_t* ___axisName0, const RuntimeMethod* method) ;
// System.Single UltimateJoystick::GetHorizontalAxis(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A (String_t* ___joystickName0, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Multiply(UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, float ___d1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Addition(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Subtraction(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_down()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_down_m19EB5B5B0EDFE9C272BD7BCC6923C4A9D616F771_inline (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Division(UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, float ___d1, const RuntimeMethod* method) ;
// System.Single UnityEngine.Time::get_deltaTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D (const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Lerp(UnityEngine.Vector3,UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, float ___t2, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::LookRotation(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forward0, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::Slerp(UnityEngine.Quaternion,UnityEngine.Quaternion,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949 (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___a0, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___b1, float ___t2, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<UnityEngine.Transform>::get_Count()
inline int32_t List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<UnityEngine.Transform>::GetEnumerator()
inline Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5 (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D (*) (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1/Enumerator<UnityEngine.Transform>::Dispose()
inline void Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5 (Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
// T System.Collections.Generic.List`1/Enumerator<UnityEngine.Transform>::get_Current()
inline Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline (Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D* __this, const RuntimeMethod* method)
{
	return ((  Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* (*) (Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
// System.Boolean System.Collections.Generic.List`1/Enumerator<UnityEngine.Transform>::MoveNext()
inline bool Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87 (Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
// UnityEngine.Vector3 UnityEngine.Camera::WorldToViewportPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position0, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Vector2::op_Implicit(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___v0, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::RotateTowards(UnityEngine.Vector3,UnityEngine.Vector3,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___current0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___target1, float ___maxRadiansDelta2, float ___maxMagnitudeDelta3, const RuntimeMethod* method) ;
// UnityEngine.Vector3 CameraMode::GetVerticalDir(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____dir0, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Clamp(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline (float ___value0, float ___min1, float ___max2, const RuntimeMethod* method) ;
// System.Void UnityEngine.Vector3::.ctor(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, float ___x0, float ___y1, float ___z2, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Angle(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___from0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___to1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_Multiply(System.Single,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m29F4414A9D30B7C0CD8455C4B2F049E8CCF66745_inline (float ___d0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a1, const RuntimeMethod* method) ;
// System.Void ChatGptFix::set_CanSetH(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_set_CanSetH_m55511D4FFCF8219BB2B27872D20A93E46FABAC6F (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, bool ___value0, const RuntimeMethod* method) ;
// System.Void UnityEngine.Camera::set_fieldOfView(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, float ___value0, const RuntimeMethod* method) ;
// System.Void ChatGptFix::set_TransitionSpeedPara(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_set_TransitionSpeedPara_mFC400679B27EB46F538B55D31A3912F8F6358CDB (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___value0, const RuntimeMethod* method) ;
// System.Void DG.Tweening.Core.DOGetter`1<System.Single>::.ctor(System.Object,System.IntPtr)
inline void DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03*, RuntimeObject*, intptr_t, const RuntimeMethod*))DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA_gshared)(__this, ___object0, ___method1, method);
}
// System.Void DG.Tweening.Core.DOSetter`1<System.Single>::.ctor(System.Object,System.IntPtr)
inline void DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200*, RuntimeObject*, intptr_t, const RuntimeMethod*))DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E_gshared)(__this, ___object0, ___method1, method);
}
// DG.Tweening.Core.TweenerCore`3<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DG.Tweening.DOTween::To(DG.Tweening.Core.DOGetter`1<System.Single>,DG.Tweening.Core.DOSetter`1<System.Single>,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* DOTween_To_mEF916279231A76EB7217D421308E489B2B19E85D (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* ___getter0, DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* ___setter1, float ___endValue2, float ___duration3, const RuntimeMethod* method) ;
// System.Single ChatGptFix::get_TransitionSpeedPara()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float ChatGptFix_get_TransitionSpeedPara_m409A745620888BFEB116DF710455916A9882F9A8_inline (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Camera::WorldToScreenPoint(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___position0, const RuntimeMethod* method) ;
// System.Boolean ChatGptFix::get_CanSetH()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool ChatGptFix_get_CanSetH_m1B1804C59790DF4A933DDB76290FB78C66A40869_inline (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector2::Distance(UnityEngine.Vector2,UnityEngine.Vector2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_Distance_m220B2ADBE9F87426BEEE291263560DFE78F835B5_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___a0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___b1, const RuntimeMethod* method) ;
// System.Single ChatGptFix::<LocalUpdate>g__CheckNeedForAutoRotate|38_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float ChatGptFix_U3CLocalUpdateU3Eg__CheckNeedForAutoRotateU7C38_0_m3DBBCD619B68FAC4E0E2E4CE474B4D143DD24217 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) ;
// System.Boolean ChatGptFix::<LocalUpdate>g__Clock|38_1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ChatGptFix_U3CLocalUpdateU3Eg__ClockU7C38_1_m96C230C57C5E989514BCE8FC1D9488D22706E10D (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::Euler(System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline (float ___x0, float ___y1, float ___z2, const RuntimeMethod* method) ;
// System.Decimal System.Decimal::op_Explicit(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC (float ___value0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Screen::get_width()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C (const RuntimeMethod* method) ;
// System.Decimal System.Decimal::op_Implicit(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7 (int32_t ___value0, const RuntimeMethod* method) ;
// System.Decimal System.Decimal::op_Division(System.Decimal,System.Decimal)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A (Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___d10, Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___d21, const RuntimeMethod* method) ;
// System.Single System.Decimal::op_Explicit(System.Decimal)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93 (Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F ___value0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Screen::get_height()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8 (const RuntimeMethod* method) ;
// System.Single ChatGptFix::get_XZDistance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float ChatGptFix_get_XZDistance_mB4A7F32E31E49E7F23F5088D645A76646325902C_inline (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) ;
// System.Void ChatGptFix::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_set_XZDistance_m6ECC3A6C2DE49FD4578047AC7946584D6DE56A64 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___value0, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Vector2::op_Subtraction(UnityEngine.Vector2,UnityEngine.Vector2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___a0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___b1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_right()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_right_m13B7C3EAA64DC921EC23346C56A5A597B5481FF5_inline (const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector2::Angle(UnityEngine.Vector2,UnityEngine.Vector2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_Angle_m9668B13074D1664DD192669C14B3A8FC01676299_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___from0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___to1, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::op_UnaryNegation(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Input::GetMouseButton(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Input_GetMouseButton_mE545CF4B790C6E202808B827E3141BEC3330DB70 (int32_t ___button0, const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Input::get_touchCount()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF (const RuntimeMethod* method) ;
// UnityEngine.Touch UnityEngine.Input::GetTouch(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Touch_t03E51455ED508492B3F278903A0114FA0E87B417 Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4 (int32_t ___index0, const RuntimeMethod* method) ;
// UnityEngine.TouchPhase UnityEngine.Touch::get_phase()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0 (Touch_t03E51455ED508492B3F278903A0114FA0E87B417* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::ClampMagnitude(UnityEngine.Vector3,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_ClampMagnitude_mDEF1E073986286F6EFA1552A5D0E1A0F6CBF4500_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___vector0, float ___maxLength1, const RuntimeMethod* method) ;
// UnityEngine.RuntimePlatform UnityEngine.Application::get_platform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D (const RuntimeMethod* method) ;
// UnityEngine.Vector3 CameraMode::GetDirection(UnityEngine.Vector3,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CameraMode_GetDirection_m09F7279A566D19CC5889EEDD7AD3487C4E842707 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___original0, float ___offsetAngle1, float ___chuizhiangle2, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Touch::get_position()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A (Touch_t03E51455ED508492B3F278903A0114FA0E87B417* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Touch::get_deltaPosition()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299 (Touch_t03E51455ED508492B3F278903A0114FA0E87B417* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector2::get_magnitude()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Camera::get_orthographic()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Camera_get_orthographic_m904DEFC76C54DA4E30C20A62A86D5D87B7D4DD8F (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Camera::get_orthographicSize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Camera_get_orthographicSize_m7950C5627086253E02992A43ADFE59039DB473F8 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Camera::set_orthographicSize(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Camera_set_orthographicSize_m76DD021032ACB3DDBD052B75EC66DCE3A7295A5C (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Max(System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline (float ___a0, float ___b1, const RuntimeMethod* method) ;
// System.Single UnityEngine.Camera::get_fieldOfView()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Camera_get_fieldOfView_m9A93F17BBF89F496AE231C21817AFD1C1E833FBB (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Vector3::op_Inequality(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Vector3_op_Inequality_m6A7FB1C9E9DE194708997BFA24C6E238D92D908E_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lhs0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rhs1, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Distance(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::LookAt(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_LookAt_mBD38EDB5E915C5DA6C5A79D191DEE2C826A9FC2C (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldPosition0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldUp1, const RuntimeMethod* method) ;
// DG.Tweening.Core.TweenerCore`3<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DG.Tweening.ShortcutExtensions::DOOrthoSize(UnityEngine.Camera,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* ShortcutExtensions_DOOrthoSize_m12DBC3D9BB3AEE9AC4D59C422E2514D74FD27A66 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___target0, float ___endValue1, float ___duration2, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::LookRotation(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_LookRotation_mE6859FEBE85BC0AE72A14159988151FF69BF4401 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forward0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___upwards1, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1<UnityEngine.Transform>::get_Item(System.Int32)
inline Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* List_1_get_Item_m8EAA91B4CE37CBB6C720FD238E4505097B29FFDA (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* __this, int32_t ___index0, const RuntimeMethod* method)
{
	return ((  Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* (*) (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*, int32_t, const RuntimeMethod*))List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared)(__this, ___index0, method);
}
// System.Void MCamera::set_CanSetH(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_set_CanSetH_m346CCA645BA5CD5AB107DBCB5AF9DC89E3292DE4 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, bool ___value0, const RuntimeMethod* method) ;
// System.Void MCamera::set_TransitionSpeedPara(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_set_TransitionSpeedPara_mABF7D9976C8AE3EB09015BA3397A2C8B955514AA (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single MCamera::get_TransitionSpeedPara()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float MCamera_get_TransitionSpeedPara_mFB4C4B859D16E59A9AB98B4D9A30365E9E1B97B6_inline (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) ;
// System.Boolean MCamera::get_CanSetH()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool MCamera_get_CanSetH_mBC65ADE59DB394E41A9CA17B9EE12EC94C2FC0A2_inline (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<UnityEngine.Transform>::.ctor()
inline void List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268 (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1<UnityEngine.Transform>::AddRange(System.Collections.Generic.IEnumerable`1<T>)
inline void List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* __this, RuntimeObject* ___collection0, const RuntimeMethod* method)
{
	((  void (*) (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*, RuntimeObject*, const RuntimeMethod*))List_1_AddRange_m1F76B300133150E6046C5FED00E88B5DE0A02E17_gshared)(__this, ___collection0, method);
}
// System.Void MCamera::<LocalUpdate>g__AdjustXZDis|39_0(System.Collections.Generic.List`1<UnityEngine.Transform>,MCamera/<>c__DisplayClass39_0&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_U3CLocalUpdateU3Eg__AdjustXZDisU7C39_0_m2097013F510348BD939DBCDE82665C916935C422 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ___targets0, U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D* p1, const RuntimeMethod* method) ;
// System.Single MCamera::get_XZDistance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74_inline (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) ;
// System.Void MCamera::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_set_XZDistance_m86188E66D0BD6C6CF43DE81C0118F9B6C37B2DB7 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single New2022::get_XZDistance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E_inline (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, const RuntimeMethod* method) ;
// System.Void New2022::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2022_set_XZDistance_m5933B8E8F03EFBB9069AF87219780AE58A44F2DB (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, float ___value0, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.Vector2::get_right()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_get_right_mCE2D0142663361ED4B48C36873786986D25A6E0A_inline (const RuntimeMethod* method) ;
// System.Void New2023::set_TransitionSpeedPara(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_set_TransitionSpeedPara_mCF7B4B51B2ECC018DAC036D41F57C01F8F995574 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single New2023::get_TransitionSpeedPara()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float New2023_get_TransitionSpeedPara_mD8F056A7B4BE13EAB7939DD695AC3C9FB023C20D_inline (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Vector2::.ctor(System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, float ___x0, float ___y1, const RuntimeMethod* method) ;
// System.Single New2023::get_XZDistance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float New2023_get_XZDistance_mA2235920C05176006556D76DFD5AA4CB4F8A524D_inline (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) ;
// System.Void New2023::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_set_XZDistance_m1AF28070B513316C2813470EDBE3306CEA9ECF20 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___value0, const RuntimeMethod* method) ;
// System.Void OneVOneMode::set_XZ_distance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_set_XZ_distance_mBC8F29816E166A3E50FA64A563D67BBD9A3221F9 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single OneVOneMode::get_XZ_distance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_get_XZ_distance_m85DE561AE9FA16B6C72CDA1FFE2F829EA7773A66_inline (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Equality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___x0, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___y1, const RuntimeMethod* method) ;
// System.Void OneVOneMode::<LocalUpdate>g__ZoomIn|31_1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_U3CLocalUpdateU3Eg__ZoomInU7C31_1_mB67A1D66A9AB976A1AA5249150BA3B5C8F844E0A (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) ;
// System.Void OneVOneMode::<LocalUpdate>g__ZoomOut|31_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_U3CLocalUpdateU3Eg__ZoomOutU7C31_0_m9EA9BF9EDC0B1356BF74185472C0B18DC71D8621 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) ;
// System.Single OneVOneMode::get_ZoomAcc()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_get_ZoomAcc_m16B63CB81DADC371768C54722F124AA88B49A8C2_inline (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Slerp(UnityEngine.Vector3,UnityEngine.Vector3,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Slerp_mBA32C7EAC64C56C7D68480549FA9A892FA5C1728 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, float ___t2, const RuntimeMethod* method) ;
// System.Void OneVOneMode::set_ZoomAcc(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_set_ZoomAcc_m197C408CBFCFF375517137E2313466548467F32C (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, float ___value0, const RuntimeMethod* method) ;
// System.Void OneVOneModeNew::set_XZ_distance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_set_XZ_distance_m8B3047AAB0CBAD0A685986D15A5105B201EE66CB (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single OneVOneModeNew::get_XZ_distance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneModeNew_get_XZ_distance_m94590253CF56035E61E827B63683A58B9867CE56_inline (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) ;
// System.Void OneVOneModeNew::<LocalUpdate>g__ZoomOut|30_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_U3CLocalUpdateU3Eg__ZoomOutU7C30_0_m24C5476405E7756753082CBC408EBF90DA323A2D (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) ;
// System.Void OneVOneModeNew::<LocalUpdate>g__ZoomIn|30_1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_U3CLocalUpdateU3Eg__ZoomInU7C30_1_m57A815BD423E9F7D78E04FFF2DC4B968A3D269D2 (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) ;
// System.Single OneVOneModeNew::get_ZoomAcc()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneModeNew_get_ZoomAcc_m11AEA3902A80D7C00F4B8A277CD3D59461F12537_inline (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) ;
// System.Void OneVOneModeNew::set_ZoomAcc(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_set_ZoomAcc_m056925C278FA5621495BEBB29E03D35D235D3A4F (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, float ___value0, const RuntimeMethod* method) ;
// System.Void OneVOneMode_failed::set_XZ_distance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed_set_XZ_distance_m38063BD537DA620E0FF817F793410D6D1DB397B0 (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single OneVOneMode_failed::get_XZ_distance()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C_inline (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, const RuntimeMethod* method) ;
// System.Void OneVOneMode_failed::set_ZoomAcc(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed_set_ZoomAcc_m2E392D5A7F6577A690A1D1C6EA758A4327774E53 (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, float ___value0, const RuntimeMethod* method) ;
// System.Single OneVOneMode_failed::get_ZoomAcc()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_failed_get_ZoomAcc_m0188D1030DC38FCBAACF2CEAE851C047382B2CFC_inline (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, const RuntimeMethod* method) ;
// DG.Tweening.Core.TweenerCore`3<System.Single,System.Single,DG.Tweening.Plugins.Options.FloatOptions> DG.Tweening.ShortcutExtensions::DOFieldOfView(UnityEngine.Camera,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* ShortcutExtensions_DOFieldOfView_m82327EC4821621EBF7957C8DE04B0E7C93778220 (Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___target0, float ___endValue1, float ___duration2, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_forward()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::FromToRotation(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_FromToRotation_m041093DBB23CB3641118310881D6B7746E3B8418 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___fromDirection0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___toDirection1, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::Lerp(UnityEngine.Quaternion,UnityEngine.Quaternion,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_Lerp_m7BE5A2D8FA33A15A5145B2F5261707CA17C3E792 (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___a0, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___b1, float ___t2, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_eulerAngles()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_eulerAngles_mCAAF48EFCF628F1ED91C2FFE75A4FD19C039DD6A (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// System.Single UltimateJoystick::GetVerticalAxis(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float UltimateJoystick_GetVerticalAxis_mEE877C1F115E2601643900464D8C1093AE878798 (String_t* ___joystickName0, const RuntimeMethod* method) ;
// System.Void TopDownWatchCamera::RotateCamera(System.Single,System.Single,System.Single,UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TopDownWatchCamera_RotateCamera_m891C60B0C0936BC0DDCBAADBB44C2513714F54BD (TopDownWatchCamera_tB31CB6E39C34F1D87B22F4B4D6E2171F68934989* __this, float ___vert0, float ___horz1, float ___camTargetSpeed2, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera3, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::SmoothDamp(System.Single,System.Single,System.Single&,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_SmoothDamp_m4B8C5AACFEBF58E93FF2A33832C27EF1E5AF7AFD_inline (float ___current0, float ___target1, float* ___currentVelocity2, float ___smoothTime3, const RuntimeMethod* method) ;
// System.Void TouchTopDownCamera/<>c__DisplayClass21_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0__ctor_m30817E4037C2E0D5D9F8F414B0C960212FB0E61B (U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* __this, const RuntimeMethod* method) ;
// UnityEngine.Transform CameraManager::get_TopDownModeEndRef()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CameraManager_get_TopDownModeEndRef_mC510D9320204B96C91DBBBEE4EB2835E31B41327_inline (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) ;
// System.Void TouchTopDownCamera::set_Height(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_set_Height_mE0D463B145814F3ADFCF49B7399251756717CBCA (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, float ___value0, const RuntimeMethod* method) ;
// DG.Tweening.Sequence DG.Tweening.DOTween::Sequence()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* DOTween_Sequence_m57CE12901581E3C5832EAFFB11C1417270E01754 (const RuntimeMethod* method) ;
// System.Void DG.Tweening.TweenCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TweenCallback__ctor_m68CC9304423CBDE43001F9B1413B5DAAF70DB621 (TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method) ;
// T DG.Tweening.TweenSettingsExtensions::OnStart<DG.Tweening.Sequence>(T,DG.Tweening.TweenCallback)
inline Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* TweenSettingsExtensions_OnStart_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mCCE914E78193AFF17F77999963371587BAD452E5 (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___t0, TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___action1, const RuntimeMethod* method)
{
	return ((  Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* (*) (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C*, TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24*, const RuntimeMethod*))TweenSettingsExtensions_OnStart_TisRuntimeObject_m520A807423D9F89B8401A562D0941BAC0060C802_gshared)(___t0, ___action1, method);
}
// DG.Tweening.Core.TweenerCore`3<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions> DG.Tweening.ShortcutExtensions::DOMove(UnityEngine.Transform,UnityEngine.Vector3,System.Single,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TweenerCore_3_tCD82DFC45FB71C681FA8659EA63A7D7D16BFFE77* ShortcutExtensions_DOMove_m32C4BD3E44498A3C651F30108F0D3402416B868B (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___target0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___endValue1, float ___duration2, bool ___snapping3, const RuntimeMethod* method) ;
// DG.Tweening.Sequence DG.Tweening.TweenSettingsExtensions::Append(DG.Tweening.Sequence,DG.Tweening.Tween)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* TweenSettingsExtensions_Append_mB8CDE24E0410A61DA0D5AD083F8047C18AED3D68 (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___s0, Tween_t8CB06EBC48A5B6F5065C490E4F4909C18CE7983C* ___t1, const RuntimeMethod* method) ;
// DG.Tweening.Core.TweenerCore`3<UnityEngine.Quaternion,UnityEngine.Quaternion,DG.Tweening.Plugins.Options.NoOptions> DG.Tweening.ShortcutExtensions::DORotateQuaternion(UnityEngine.Transform,UnityEngine.Quaternion,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TweenerCore_3_t9A48A35EB4763F174321ED1A1BE49A67BC0A5C6F* ShortcutExtensions_DORotateQuaternion_m18A2982A27F3B18F3D738CEFEB15DED04EB6E9AA (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___target0, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___endValue1, float ___duration2, const RuntimeMethod* method) ;
// DG.Tweening.Sequence DG.Tweening.TweenSettingsExtensions::Join(DG.Tweening.Sequence,DG.Tweening.Tween)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* TweenSettingsExtensions_Join_m197C0D892B0D9763AE9E4C09F2A9EBFFC2882EA0 (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___s0, Tween_t8CB06EBC48A5B6F5065C490E4F4909C18CE7983C* ___t1, const RuntimeMethod* method) ;
// DG.Tweening.Sequence DG.Tweening.TweenSettingsExtensions::AppendCallback(DG.Tweening.Sequence,DG.Tweening.TweenCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* TweenSettingsExtensions_AppendCallback_m0AF8553D233D9803D3C45C2AC976D363EF42EB91 (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___s0, TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* ___callback1, const RuntimeMethod* method) ;
// T DG.Tweening.TweenExtensions::Play<DG.Tweening.Sequence>(T)
inline Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* TweenExtensions_Play_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mAE376A6BE21D1F94CE5EAA4DA0C1683A7D6DFDE7 (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* ___t0, const RuntimeMethod* method)
{
	return ((  Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* (*) (Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C*, const RuntimeMethod*))TweenExtensions_Play_TisRuntimeObject_m9C5B8B16699BA91E6605510B84969F71F944D46F_gshared)(___t0, method);
}
// UnityEngine.Vector3 UnityEngine.Vector2::op_Implicit(UnityEngine.Vector2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___v0, const RuntimeMethod* method) ;
// System.Single TouchTopDownCamera::get_Height()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float TouchTopDownCamera_get_Height_m6A6A94345B3716F3AA84538F7D4B6F03E4CCD4D2_inline (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, const RuntimeMethod* method) ;
// System.Void TouchTopDownCamera::CameraDrag(UnityEngine.Camera,UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_CameraDrag_mAA66A76D28AD22E4AC983C8FD04698415DFB32CE (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___startPoint1, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____firstPoint2, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____secondPoint3, const RuntimeMethod* method) ;
// System.Boolean TouchTopDownCamera::<LocalUpdate>g__OnPad|24_0(TouchTopDownCamera/<>c__DisplayClass24_0&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TouchTopDownCamera_U3CLocalUpdateU3Eg__OnPadU7C24_0_m647D9C5EC4AC9A7477BCD57EAEDAD9E6E6DAE73B (U3CU3Ec__DisplayClass24_0_tF67F393E60EBEB75B219167CBCB8A8DC6B7F9C3B* p0, const RuntimeMethod* method) ;
// System.Void TouchTopDownCamera::CameraRotate(UnityEngine.Camera,UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_CameraRotate_m30F7A499D44B8F2EA6B024D9B4013B590F20EF35 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____firstPoint1, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____secondPoint2, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Input::GetMouseButtonDown(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Input_GetMouseButtonDown_m33522C56A54C402FE6DED802DD7E53435C27A5DE (int32_t ___button0, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Input::get_mousePosition()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Input_get_mousePosition_m2414B43222ED0C5FAB960D393964189AFD21EEAD (const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Input::GetMouseButtonUp(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Input_GetMouseButtonUp_m69FCCF4E6D2F0E4E9B310D1ED2AD5A6927A8C081 (int32_t ___button0, const RuntimeMethod* method) ;
// UnityEngine.Vector3 TouchTopDownCamera::GetCenterScreenGroundPoint(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 TouchTopDownCamera_GetCenterScreenGroundPoint_mAF6D401F535BF52FA7B7947C80FDD6E2DB5CA407 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Transform::get_right()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Transform_get_right_mC6DC057C23313802E2186A9E0DB760D795A758A4 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::RotateAround(UnityEngine.Vector3,UnityEngine.Vector3,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_RotateAround_m489C5BE8B8B15D0A5F4863DE6D23FF2CC8FA76C6 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___point0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___axis1, float ___angle2, const RuntimeMethod* method) ;
// System.Void UnityEngine.UI.InputField::set_text(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InputField_set_text_m28B1C806BBCAC44F3ACCDC3B550509CA0C7D257F (InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* __this, String_t* ___value0, const RuntimeMethod* method) ;
// System.String UnityEngine.TextAsset::get_text()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TextAsset_get_text_m36846042E3CF3D9DD337BF3F8B2B1902D10C8FD9 (TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.UI.InputField::get_text()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* InputField_get_text_m6E0796350FF559505E4DF17311803962699D6704_inline (InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A (String_t* ___value0, const RuntimeMethod* method) ;
// System.String TableCodeGen::Generate(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TableCodeGen_Generate_m1F932F34B8A82A84D17E39F9F49BBFFA3B38928D (String_t* ___csvText0, String_t* ___className1, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<SampleTable/Row>::Clear()
inline void List_1_Clear_mAF59287F15E95C0F18D3E325B64FCAC82A7610A9_inline (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, const RuntimeMethod*))List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline)(__this, method);
}
// System.String[][] CsvParser2::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* CsvParser2_Parse_mD97CB56798836B1C073FCEDE3A2371BD0D870617 (String_t* ___input0, const RuntimeMethod* method) ;
// System.Void SampleTable/Row::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Row__ctor_m2D008B8DB9286F8856252DE30136CFC350484D59 (Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<SampleTable/Row>::Add(T)
inline void List_1_Add_m9761F0D2ADF7CB1D17354DDC09E8F08DB70897EF_inline (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___item0, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___item0, method);
}
// System.Int32 System.Collections.Generic.List`1<SampleTable/Row>::get_Count()
inline int32_t List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_inline (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
// T System.Collections.Generic.List`1<SampleTable/Row>::get_Item(System.Int32)
inline Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* List_1_get_Item_m82D8E1795C4DF42DA74D17354A985E517168F936 (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, int32_t ___index0, const RuntimeMethod* method)
{
	return ((  Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, int32_t, const RuntimeMethod*))List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared)(__this, ___index0, method);
}
// System.Void SampleTable/<>c__DisplayClass8_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass8_0__ctor_m05D46DB0D640A36C7E789205C80DFDA027987C55 (U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* __this, const RuntimeMethod* method) ;
// System.Void System.Predicate`1<SampleTable/Row>::.ctor(System.Object,System.IntPtr)
inline void Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* __this, RuntimeObject* ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	((  void (*) (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*, RuntimeObject*, intptr_t, const RuntimeMethod*))Predicate_1__ctor_m3E007299121A15DF80F4A210FF8C20E5DF688F20_gshared)(__this, ___object0, ___method1, method);
}
// T System.Collections.Generic.List`1<SampleTable/Row>::Find(System.Predicate`1<T>)
inline Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4 (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* ___match0, const RuntimeMethod* method)
{
	return ((  Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*, const RuntimeMethod*))List_1_Find_m5E78A210541B0D844FE27B94F509313623BE33D3_gshared)(__this, ___match0, method);
}
// System.Void SampleTable/<>c__DisplayClass9_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass9_0__ctor_mD75892DF63C0FC2BD408AEA656B48DDB7BD1AA20 (U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* __this, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1<SampleTable/Row>::FindAll(System.Predicate`1<T>)
inline List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* ___match0, const RuntimeMethod* method)
{
	return ((  List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*, const RuntimeMethod*))List_1_FindAll_m87FB5AB35229967D01B9DF933BF70D470B32F0AF_gshared)(__this, ___match0, method);
}
// System.Void SampleTable/<>c__DisplayClass10_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass10_0__ctor_m6367426A77E7F16A373755C9B26D381B494A3C95 (U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass11_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass11_0__ctor_m9E53FC74DCBB7CFE404ABBB2195902FE0C1D7601 (U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass12_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass12_0__ctor_m7F5C378544EFF8780DC07E78158AD5F5F08B71EC (U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass13_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass13_0__ctor_m21277AF189A8868A3EC82B351B6C0CA613D051B3 (U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass14_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass14_0__ctor_m8E169C1967B6403C0B81E90478611CC21A976665 (U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass15_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass15_0__ctor_m86FB91648371CB01C88E55065B451D90C7FDE67E (U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass16_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass16_0__ctor_m561B4EA43806B0351A0D59EEEE5B775D105088BD (U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* __this, const RuntimeMethod* method) ;
// System.Void SampleTable/<>c__DisplayClass17_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass17_0__ctor_mC61D7D40BFF5A0ACFF9FDF1C0868B232A2336627 (U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<SampleTable/Row>::.ctor()
inline void List_1__ctor_mA9A28D7BDA09426757EEB0C6020D5BE0CC7A9584 (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Void CsvParser/ParserContext::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserContext__ctor_m5C1CC4A3CC2996F41AE08533A717CB09B41434F5 (ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* __this, const RuntimeMethod* method) ;
// System.Char System.String::get_Chars(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3 (String_t* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Int32 System.String::get_Length()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1<System.String[]> CsvParser/ParserContext::GetAllLines()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED* ParserContext_GetAllLines_m9BAC583BCE4D3F83A00586053638EE19CFA91877 (ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* __this, const RuntimeMethod* method) ;
// T[] System.Collections.Generic.List`1<System.String[]>::ToArray()
inline StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* List_1_ToArray_m0FF88E5645F74AB2208E8BA2A85973B21E5FADA0 (List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED* __this, const RuntimeMethod* method)
{
	return ((  StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* (*) (List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED*, const RuntimeMethod*))List_1_ToArray_mD7E4F8E7C11C3C67CB5739FCC0A6E86106A6291F_gshared)(__this, method);
}
// System.Void CsvParser::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CsvParser__ctor_mD72DA8A14830DDE8F2E56A277475FEAD5FFBFC71 (CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* __this, const RuntimeMethod* method) ;
// System.Void System.IO.StringReader::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringReader__ctor_m72556EC1062F49E05CF41B0825AC7FA2DB2A81C0 (StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* __this, String_t* ___s0, const RuntimeMethod* method) ;
// System.String[][] CsvParser::Parse(System.IO.TextReader)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* CsvParser_Parse_m786BE4DAC73F7BF7DF882A8E4BE04787C38704F2 (CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* __this, TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7* ___reader0, const RuntimeMethod* method) ;
// System.Void CsvParser/LineStartState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LineStartState__ctor_m3872C17D29CC13EBA595997F0B13AE5ECB486566 (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/ValueStartState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ValueStartState__ctor_m9377B0723C0983042911EEE9864E494594C6EDA4 (ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/ValueState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ValueState__ctor_mB055972E5EB17FC0809F30AE4ACF7AC1F868EE59 (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/QuotedValueState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void QuotedValueState__ctor_mF9D1202E965D9C87E7D2F1DF19A30F4FCD913C6A (QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/QuoteState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void QuoteState__ctor_m1D17BE9C37042852DB1C63C5E1DB4EE125B9C1C8 (QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/ParserContext::AddChar(System.Char)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserContext_AddChar_mE8B2A52474CF912A2B135402C52432B47CF68039 (ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* __this, Il2CppChar ___ch0, const RuntimeMethod* method) ;
// System.Void CsvParser/ParserContext::AddValue()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserContext_AddValue_m971336036E0386C8DC559534A9AEFA04DCFEB3F4 (ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/ParserContext::AddLine()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserContext_AddLine_mABF6B3D83F1F738C84CEBED3F90753244F060ED9 (ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* __this, const RuntimeMethod* method) ;
// System.Void CsvParser/ParserState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserState__ctor_m1C3840E87C5C72B85E675F2E22026412DB87C705 (ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Quaternion::.ctor(System.Single,System.Single,System.Single,System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Quaternion__ctor_m868FD60AA65DD5A8AC0C5DEB0608381A8D85FCD8_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* __this, float ___x0, float ___y1, float ___z2, float ___w3, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::Normalize(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Normalize_m6120F119433C5B60BBB28731D3D4A0DA50A84DDD_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___value0, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Clamp01(System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp01_mD921B23F47F5347996C56DC789D1DE16EE27D9B1_inline (float ___value0, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::get_sqrMagnitude()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Dot(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Dot_m4688A1A524306675DBDB1E6D483F35E85E3CE6D8_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lhs0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rhs1, const RuntimeMethod* method) ;
// UnityEngine.Quaternion UnityEngine.Quaternion::Internal_FromEulerRad(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_Internal_FromEulerRad_m2842B9FFB31CDC0F80B7C2172E22831D11D91E93 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___euler0, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector2::get_sqrMagnitude()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_get_sqrMagnitude_mA16336720C14EEF8BA9B55AE33B98C9EE2082BDC_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector2::Dot(UnityEngine.Vector2,UnityEngine.Vector2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_Dot_mBF0FA0B529C821F4733DDC3AD366B07CD27625F4_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___lhs0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rhs1, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Vector3::op_Equality(UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Vector3_op_Equality_m15951D1B53E3BE36C9D265E229090020FBD72EBB_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lhs0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rhs1, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::SmoothDamp(System.Single,System.Single,System.Single&,System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Mathf_SmoothDamp_m00E482452BCED3FE0F16B4033B2B5323C7E30829 (float ___current0, float ___target1, float* ___currentVelocity2, float ___smoothTime3, float ___maxSpeed4, float ___deltaTime5, const RuntimeMethod* method) ;
// System.Void System.Array::Clear(System.Array,System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Clear_m48B57EC27CADC3463CA98A33373D557DA587FF1B (RuntimeArray* ___array0, int32_t ___index1, int32_t ___length2, const RuntimeMethod* method) ;
// System.Single UnityEngine.Vector3::Magnitude(UnityEngine.Vector3)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Magnitude_m6AD0BEBF88AAF98188A851E62D7A32CB5B7830EF_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___vector0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void HitBoxesProcesser::Awake()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_Awake_mBCAB5A5D5A8B9DBFE7591C0DA308E6792BC9DED8 (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Instance = this;
		il2cpp_codegen_runtime_class_init_inline(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___Instance_4 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___Instance_4), (void*)__this);
		// }
		return;
	}
}
// System.Void HitBoxesProcesser::Clear()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_Clear_mFB9CD524A61223C37895C5AFFE4B12B6D0506AB0 (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// _processingDecompositions.Clear();
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_0 = __this->____processingDecompositions_6;
		NullCheck(L_0);
		List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_inline(L_0, List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_RuntimeMethod_var);
		// }
		return;
	}
}
// HittingDetection.HitBoxManager HitBoxesProcesser::GetHitBox(UnityEngine.Collider)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* HitBoxesProcesser_GetHitBox_m49C4DB68A8A0BE1A3A8ED6449C01F91B7777685A (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* ___c0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_m3335583D1D1EE1BECD3037C120CA3B3BEBDD9D71_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* V_0 = NULL;
	{
		// ColliderHitBox.TryGetValue(c, out var hitBox);
		il2cpp_codegen_runtime_class_init_inline(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* L_0 = ((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___ColliderHitBox_5;
		Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* L_1 = ___c0;
		NullCheck(L_0);
		bool L_2;
		L_2 = Dictionary_2_TryGetValue_m3335583D1D1EE1BECD3037C120CA3B3BEBDD9D71(L_0, L_1, (&V_0), Dictionary_2_TryGetValue_m3335583D1D1EE1BECD3037C120CA3B3BEBDD9D71_RuntimeMethod_var);
		// return hitBox;
		HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* L_3 = V_0;
		return L_3;
	}
}
// System.Void HitBoxesProcesser::AddToDecompositionProcessorList(Decomposition)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_AddToDecompositionProcessorList_m977603F54B6C0CB9B5AF3960BBA5E2614132ACC4 (Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* ___poolObject0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (Instance != null)
		il2cpp_codegen_runtime_class_init_inline(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* L_0 = ((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___Instance_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0018;
		}
	}
	{
		// Instance.AddToHitBoxesProcessorList(poolObject);
		il2cpp_codegen_runtime_class_init_inline(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* L_2 = ((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___Instance_4;
		Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* L_3 = ___poolObject0;
		NullCheck(L_2);
		HitBoxesProcesser_AddToHitBoxesProcessorList_m52EC3EDCF36BE2874F5A7F3A229A4E09876C12E4(L_2, L_3, NULL);
	}

IL_0018:
	{
		// }
		return;
	}
}
// System.Void HitBoxesProcesser::AddToColliderHitBoxDic(UnityEngine.Collider,HittingDetection.HitBoxManager)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_AddToColliderHitBoxDic_m40012D60BE722F81121F25C6F245960E0F10B908 (Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* ___collider0, HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* ___boHitbox1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_m7A3E3FD907B5C5FC6FACEDF50DC5C1C6A6C67F19_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m19FC3A712B339AC1EC6CC0D81D8BB425B022B97C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!ColliderHitBox.ContainsKey(collider))
		il2cpp_codegen_runtime_class_init_inline(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* L_0 = ((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___ColliderHitBox_5;
		Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* L_1 = ___collider0;
		NullCheck(L_0);
		bool L_2;
		L_2 = Dictionary_2_ContainsKey_m19FC3A712B339AC1EC6CC0D81D8BB425B022B97C(L_0, L_1, Dictionary_2_ContainsKey_m19FC3A712B339AC1EC6CC0D81D8BB425B022B97C_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0019;
		}
	}
	{
		// ColliderHitBox.Add(collider, boHitbox);
		il2cpp_codegen_runtime_class_init_inline(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* L_3 = ((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___ColliderHitBox_5;
		Collider_t1CC3163924FCD6C4CC2E816373A929C1E3D55E76* L_4 = ___collider0;
		HitBoxManager_t2AD5674FE48CAD87DFAFEFB6C293E94DC56E7EFC* L_5 = ___boHitbox1;
		NullCheck(L_3);
		Dictionary_2_Add_m7A3E3FD907B5C5FC6FACEDF50DC5C1C6A6C67F19(L_3, L_4, L_5, Dictionary_2_Add_m7A3E3FD907B5C5FC6FACEDF50DC5C1C6A6C67F19_RuntimeMethod_var);
	}

IL_0019:
	{
		// }
		return;
	}
}
// System.Void HitBoxesProcesser::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_Update_mF3CA1B394E6B9207A41B9E1984CB96C81FBA67C3 (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		// if (_processingDecompositions.Count > 0)
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_0 = __this->____processingDecompositions_6;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_inline(L_0, List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_RuntimeMethod_var);
		if ((((int32_t)L_1) <= ((int32_t)0)))
		{
			goto IL_0091;
		}
	}
	{
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		V_0 = 0;
		goto IL_002a;
	}

IL_0015:
	{
		// _processingDecompositions[i].Step1();
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_2 = __this->____processingDecompositions_6;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* L_4;
		L_4 = List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5(L_2, L_3, List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5_RuntimeMethod_var);
		NullCheck(L_4);
		Decomposition_Step1_m957D30E318BDC2C1853E6782E73A915835412277(L_4, NULL);
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		int32_t L_5 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_5, 1));
	}

IL_002a:
	{
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		int32_t L_6 = V_0;
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_7 = __this->____processingDecompositions_6;
		NullCheck(L_7);
		int32_t L_8;
		L_8 = List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_inline(L_7, List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_RuntimeMethod_var);
		if ((((int32_t)L_6) < ((int32_t)L_8)))
		{
			goto IL_0015;
		}
	}
	{
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		V_1 = 0;
		goto IL_0051;
	}

IL_003c:
	{
		// _processingDecompositions[i].Step2();
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_9 = __this->____processingDecompositions_6;
		int32_t L_10 = V_1;
		NullCheck(L_9);
		Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* L_11;
		L_11 = List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5(L_9, L_10, List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5_RuntimeMethod_var);
		NullCheck(L_11);
		Decomposition_Step2_m6E52C741631ADEC4861C6AAE5EBEF9EAACCF1303(L_11, NULL);
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		int32_t L_12 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
	}

IL_0051:
	{
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		int32_t L_13 = V_1;
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_14 = __this->____processingDecompositions_6;
		NullCheck(L_14);
		int32_t L_15;
		L_15 = List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_inline(L_14, List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_RuntimeMethod_var);
		if ((((int32_t)L_13) < ((int32_t)L_15)))
		{
			goto IL_003c;
		}
	}
	{
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		V_2 = 0;
		goto IL_0078;
	}

IL_0063:
	{
		// _processingDecompositions[i].Life();
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_16 = __this->____processingDecompositions_6;
		int32_t L_17 = V_2;
		NullCheck(L_16);
		Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* L_18;
		L_18 = List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5(L_16, L_17, List_1_get_Item_mA4024F2C24E9991B11B93CD217F724BCCC073CE5_RuntimeMethod_var);
		NullCheck(L_18);
		Decomposition_Life_m7DC57919FD13B2B01F2A32CC2B5554CDEAD99D62(L_18, NULL);
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		int32_t L_19 = V_2;
		V_2 = ((int32_t)il2cpp_codegen_add(L_19, 1));
	}

IL_0078:
	{
		// for (var i = 0; i < _processingDecompositions.Count; i++)
		int32_t L_20 = V_2;
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_21 = __this->____processingDecompositions_6;
		NullCheck(L_21);
		int32_t L_22;
		L_22 = List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_inline(L_21, List_1_get_Count_mDF94CEBEDBB4461059B7747F1CDEE0D6E4DFE9CB_RuntimeMethod_var);
		if ((((int32_t)L_20) < ((int32_t)L_22)))
		{
			goto IL_0063;
		}
	}
	{
		// _processingDecompositions.Clear();
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_23 = __this->____processingDecompositions_6;
		NullCheck(L_23);
		List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_inline(L_23, List_1_Clear_m3E53DAB853850ADF1E9626C79235E3E80FCDC50C_RuntimeMethod_var);
	}

IL_0091:
	{
		// }
		return;
	}
}
// System.Void HitBoxesProcesser::AddToHitBoxesProcessorList(Decomposition)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser_AddToHitBoxesProcessorList_m52EC3EDCF36BE2874F5A7F3A229A4E09876C12E4 (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* ___poolObject0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_mBFF86E22A26E9ED0D216F526BFBF7A7546991F38_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Contains_mDE448B160DBA47CFE50F34A3524289C69870B992_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!_processingDecompositions.Contains(poolObject))
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_0 = __this->____processingDecompositions_6;
		Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* L_1 = ___poolObject0;
		NullCheck(L_0);
		bool L_2;
		L_2 = List_1_Contains_mDE448B160DBA47CFE50F34A3524289C69870B992(L_0, L_1, List_1_Contains_mDE448B160DBA47CFE50F34A3524289C69870B992_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_001a;
		}
	}
	{
		// _processingDecompositions.Add(poolObject);
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_3 = __this->____processingDecompositions_6;
		Decomposition_t7ADDF334303197BECF9A53AA5867E41358CC9D53* L_4 = ___poolObject0;
		NullCheck(L_3);
		List_1_Add_mBFF86E22A26E9ED0D216F526BFBF7A7546991F38_inline(L_3, L_4, List_1_Add_mBFF86E22A26E9ED0D216F526BFBF7A7546991F38_RuntimeMethod_var);
	}

IL_001a:
	{
		// }
		return;
	}
}
// System.Void HitBoxesProcesser::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser__ctor_m96477B7CA55E9B098C3F2D61B411E252FA5957BE (HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m362AED7E17D370D578FF476B1FF74A9236A96783_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private readonly List<Decomposition> _processingDecompositions = new List<Decomposition>();
		List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69* L_0 = (List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69*)il2cpp_codegen_object_new(List_1_tE02644AC0B2DCEEACE22E3223CD856CED9899C69_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_m362AED7E17D370D578FF476B1FF74A9236A96783(L_0, List_1__ctor_m362AED7E17D370D578FF476B1FF74A9236A96783_RuntimeMethod_var);
		__this->____processingDecompositions_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____processingDecompositions_6), (void*)L_0);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
// System.Void HitBoxesProcesser::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void HitBoxesProcesser__cctor_m9D0F1361F4BA51B22C158055A395A8B20BD9B473 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m9C0EC68028100E8C91D57975D3FA9279791E676F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private static readonly Dictionary<Collider, HitBoxManager> ColliderHitBox = new Dictionary<Collider, HitBoxManager>();
		Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A* L_0 = (Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A*)il2cpp_codegen_object_new(Dictionary_2_t5F3EC44469BF7D1BD4924422C21A1D07C31BA37A_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Dictionary_2__ctor_m9C0EC68028100E8C91D57975D3FA9279791E676F(L_0, Dictionary_2__ctor_m9C0EC68028100E8C91D57975D3FA9279791E676F_RuntimeMethod_var);
		((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___ColliderHitBox_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_StaticFields*)il2cpp_codegen_static_fields_for(HitBoxesProcesser_t172D945ADBF9182E421541B9BB5567BCF1B03F1A_il2cpp_TypeInfo_var))->___ColliderHitBox_5), (void*)L_0);
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
// UnityEngine.Transform CameraManager::get_TopDownModeEndRef()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CameraManager_get_TopDownModeEndRef_mC510D9320204B96C91DBBBEE4EB2835E31B41327 (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) 
{
	{
		// public Transform TopDownModeEndRef => topDownModeEndRef;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->___topDownModeEndRef_9;
		return L_0;
	}
}
// CameraMode CameraManager::GetMode(C_Mode)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* CameraManager_GetMode_m302EC26A1AFF8BBCA767ED53AF9A0EF1BBA702B0 (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, int32_t ___mode0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDictionary_2_t70FD88AC6FC219AD37685F0B48E299147C3767CA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* V_0 = NULL;
	{
		// CModeDic.TryGetValue(mode, out var c);
		RuntimeObject* L_0 = __this->___CModeDic_11;
		int32_t L_1 = ___mode0;
		NullCheck(L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker2< bool, int32_t, CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97** >::Invoke(7 /* System.Boolean System.Collections.Generic.IDictionary`2<C_Mode,CameraMode>::TryGetValue(TKey,TValue&) */, IDictionary_2_t70FD88AC6FC219AD37685F0B48E299147C3767CA_il2cpp_TypeInfo_var, L_0, L_1, (&V_0));
		// return c;
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_3 = V_0;
		return L_3;
	}
}
// System.Void CameraManager::SetPosToStart()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraManager_SetPosToStart_m6EB6598ACC5CEF19823C5528C7C755BC8B1D4FE1 (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// _camera.transform.position = StartPosRef.position;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4;
		NullCheck(L_0);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1;
		L_1 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_0, NULL);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = __this->___StartPosRef_8;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_2, NULL);
		NullCheck(L_1);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_1, L_3, NULL);
		// _camera.transform.rotation = StartPosRef.rotation;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_4 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4;
		NullCheck(L_4);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
		L_5 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_4, NULL);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6 = __this->___StartPosRef_8;
		NullCheck(L_6);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7;
		L_7 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_6, NULL);
		NullCheck(L_5);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_5, L_7, NULL);
		// }
		return;
	}
}
// System.Void CameraManager::Awake()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraManager_Awake_m8C26B088D4AAD67BC12BF89FBCFAC501813DAE7C (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_t00EAEB29218994CE734A3A26D94870DCCC8089A2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_tDB9241AA672FBAD41B38A33A2A3D720DB45A70D5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&KeyValuePair_2_get_Value_m9B30E68334E34583A8C40B04DEB897A4800203F9_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	KeyValuePair_2_t16437782916F5E7884151CEF28CCC71F0FDEBAE4 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// _camera = mainCamera;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = __this->___mainCamera_6;
		((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4), (void*)L_0);
		// _subCamera = subCamera;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_1 = __this->___subCamera_7;
		((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____subCamera_5 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____subCamera_5), (void*)L_1);
		// _camera.depthTextureMode = DepthTextureMode.Depth;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_2 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4;
		NullCheck(L_2);
		Camera_set_depthTextureMode_mE722389E4DF8B3DF7F6100DB142E4DBAF698F6BF(L_2, 1, NULL);
		// foreach (var kv in CModeDic)
		RuntimeObject* L_3 = __this->___CModeDic_11;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Collections.Generic.KeyValuePair`2<C_Mode,CameraMode>>::GetEnumerator() */, IEnumerable_1_t00EAEB29218994CE734A3A26D94870DCCC8089A2_il2cpp_TypeInfo_var, L_3);
		V_0 = L_4;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_004d:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_5 = V_0;
					if (!L_5)
					{
						goto IL_0056;
					}
				}
				{
					RuntimeObject* L_6 = V_0;
					NullCheck(L_6);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_6);
				}

IL_0056:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0043_1;
			}

IL_002f_1:
			{
				// foreach (var kv in CModeDic)
				RuntimeObject* L_7 = V_0;
				NullCheck(L_7);
				KeyValuePair_2_t16437782916F5E7884151CEF28CCC71F0FDEBAE4 L_8;
				L_8 = InterfaceFuncInvoker0< KeyValuePair_2_t16437782916F5E7884151CEF28CCC71F0FDEBAE4 >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Collections.Generic.KeyValuePair`2<C_Mode,CameraMode>>::get_Current() */, IEnumerator_1_tDB9241AA672FBAD41B38A33A2A3D720DB45A70D5_il2cpp_TypeInfo_var, L_7);
				V_1 = L_8;
				// kv.Value.cameraManager = this;
				CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_9;
				L_9 = KeyValuePair_2_get_Value_m9B30E68334E34583A8C40B04DEB897A4800203F9_inline((&V_1), KeyValuePair_2_get_Value_m9B30E68334E34583A8C40B04DEB897A4800203F9_RuntimeMethod_var);
				NullCheck(L_9);
				L_9->___cameraManager_0 = __this;
				Il2CppCodeGenWriteBarrier((void**)(&L_9->___cameraManager_0), (void*)__this);
			}

IL_0043_1:
			{
				// foreach (var kv in CModeDic)
				RuntimeObject* L_10 = V_0;
				NullCheck(L_10);
				bool L_11;
				L_11 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_10);
				if (L_11)
				{
					goto IL_002f_1;
				}
			}
			{
				goto IL_0057;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0057:
	{
		// }
		return;
	}
}
// System.Void CameraManager::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraManager_Update_mFEAA795BD3D87142A921143886E152D376D2298D (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (CurrentMode != null)
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_0 = __this->___CurrentMode_10;
		if (!L_0)
		{
			goto IL_0018;
		}
	}
	{
		// CurrentMode.LocalUpdate(_camera);
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_1 = __this->___CurrentMode_10;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_2 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4;
		NullCheck(L_1);
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(6 /* System.Void CameraMode::LocalUpdate(UnityEngine.Camera) */, L_1, L_2);
	}

IL_0018:
	{
		// }
		return;
	}
}
// System.Void CameraManager::Assign_Camera(C_Mode,UnityEngine.Transform,System.Collections.Generic.List`1<UnityEngine.Transform>,System.Collections.Generic.List`1<UnityEngine.Transform>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraManager_Assign_Camera_m8C561159A0967F7CAD11C953FA0929418DF74ACB (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, int32_t ___num0, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___me1, List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ___targets2, List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ___mes3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDictionary_2_t70FD88AC6FC219AD37685F0B48E299147C3767CA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* G_B2_0 = NULL;
	CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* G_B1_0 = NULL;
	{
		// CurrentMode?.Exit(_camera);
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_0 = __this->___CurrentMode_10;
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		goto IL_0016;
	}

IL_000c:
	{
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_2 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4;
		NullCheck(G_B2_0);
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(5 /* System.Void CameraMode::Exit(UnityEngine.Camera) */, G_B2_0, L_2);
	}

IL_0016:
	{
		// CModeDic.TryGetValue(num, out CurrentMode);
		RuntimeObject* L_3 = __this->___CModeDic_11;
		int32_t L_4 = ___num0;
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97** L_5 = (&__this->___CurrentMode_10);
		NullCheck(L_3);
		bool L_6;
		L_6 = InterfaceFuncInvoker2< bool, int32_t, CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97** >::Invoke(7 /* System.Boolean System.Collections.Generic.IDictionary`2<C_Mode,CameraMode>::TryGetValue(TKey,TValue&) */, IDictionary_2_t70FD88AC6FC219AD37685F0B48E299147C3767CA_il2cpp_TypeInfo_var, L_3, L_4, L_5);
		// if (CurrentMode != null)
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_7 = __this->___CurrentMode_10;
		if (!L_7)
		{
			goto IL_0066;
		}
	}
	{
		// CurrentMode.SetMeCenter(me);
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_8 = __this->___CurrentMode_10;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9 = ___me1;
		NullCheck(L_8);
		CameraMode_SetMeCenter_m7EF634EA83FBD929B8E52E998076BCE50F5AB33D_inline(L_8, L_9, NULL);
		// CurrentMode.targets = targets;
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_10 = __this->___CurrentMode_10;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_11 = ___targets2;
		NullCheck(L_10);
		L_10->___targets_4 = L_11;
		Il2CppCodeGenWriteBarrier((void**)(&L_10->___targets_4), (void*)L_11);
		// CurrentMode.myTeamTargets = mes;
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_12 = __this->___CurrentMode_10;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_13 = ___mes3;
		NullCheck(L_12);
		L_12->___myTeamTargets_3 = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&L_12->___myTeamTargets_3), (void*)L_13);
		// CurrentMode.Enter(_camera);
		CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* L_14 = __this->___CurrentMode_10;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_15 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____camera_4;
		NullCheck(L_14);
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(4 /* System.Void CameraMode::Enter(UnityEngine.Camera) */, L_14, L_15);
	}

IL_0066:
	{
		// }
		return;
	}
}
// System.Void CameraManager::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraManager__ctor_m765956D95C636CA8C1829BF6C0892A8AF76739C0 (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m665BD95251217FF9BEAAE59FB36F09C3CB9E2012_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// readonly IDictionary<C_Mode, CameraMode> CModeDic = new Dictionary<C_Mode, CameraMode>()
		// {
		//     {C_Mode.CertainYAntiVibration, new ChatGptFix(15f, 5.5f, 30f)},
		//     //{C_Mode.CertainYAntiVibration, new New2023(8.8f, 5f)},
		//     {C_Mode.ApproachToCertainDis,  new LerpToCertainDistance(5f, 1f)},
		//     {C_Mode.keepTargetLeft, new keepTargetLeftCamera()},
		//     {C_Mode.WatchOver, new MCamera(20f, 10f, 30f)},
		//     {C_Mode.StartAndEnd, new StartToEndMode()},
		//     {C_Mode.RoundBoundary, new CenterSurroundCamera(25f, 10f)},
		//     {C_Mode.TopDown, new TouchTopDownCamera(12f, 20f, 25)},
		//     {C_Mode.ScreenSaver, new New2023(8.8f, 5f)}//new ScreenSaverC(8.8f, 8.8f)}
		// };
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_0 = (Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44*)il2cpp_codegen_object_new(Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Dictionary_2__ctor_m665BD95251217FF9BEAAE59FB36F09C3CB9E2012(L_0, Dictionary_2__ctor_m665BD95251217FF9BEAAE59FB36F09C3CB9E2012_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_1 = L_0;
		ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* L_2 = (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD*)il2cpp_codegen_object_new(ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		ChatGptFix__ctor_m71A6397CC055AD4595AF6204E084B90CDC9DE526(L_2, (15.0f), (5.5f), (30.0f), NULL);
		NullCheck(L_1);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_1, ((int32_t)12), L_2, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_3 = L_1;
		LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD* L_4 = (LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD*)il2cpp_codegen_object_new(LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		LerpToCertainDistance__ctor_mAB95B31D424196399B9CC64124D6A8B0554663B7(L_4, (5.0f), (1.0f), NULL);
		NullCheck(L_3);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_3, ((int32_t)14), L_4, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_5 = L_3;
		keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95* L_6 = (keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95*)il2cpp_codegen_object_new(keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95_il2cpp_TypeInfo_var);
		NullCheck(L_6);
		keepTargetLeftCamera__ctor_m05CDA1EFB71DFD240F297A24586B20B01DB78BBA(L_6, NULL);
		NullCheck(L_5);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_5, ((int32_t)13), L_6, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_7 = L_5;
		MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* L_8 = (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF*)il2cpp_codegen_object_new(MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF_il2cpp_TypeInfo_var);
		NullCheck(L_8);
		MCamera__ctor_m0C7A5B62FD9724E9A21B55C82C19887BA48A8623(L_8, (20.0f), (10.0f), (30.0f), NULL);
		NullCheck(L_7);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_7, 8, L_8, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_9 = L_7;
		StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338* L_10 = (StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338*)il2cpp_codegen_object_new(StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338_il2cpp_TypeInfo_var);
		NullCheck(L_10);
		StartToEndMode__ctor_m6737649C2652021444D48FCEAAE9DE93C67D8234(L_10, NULL);
		NullCheck(L_9);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_9, 1, L_10, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_11 = L_9;
		CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C* L_12 = (CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C*)il2cpp_codegen_object_new(CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		CenterSurroundCamera__ctor_m250239560EAEA5F20A97A7E4709B079CCB0819A5(L_12, (25.0f), (10.0f), NULL);
		NullCheck(L_11);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_11, 0, L_12, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_13 = L_11;
		TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* L_14 = (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2*)il2cpp_codegen_object_new(TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2_il2cpp_TypeInfo_var);
		NullCheck(L_14);
		TouchTopDownCamera__ctor_m937F9275C5485EB3574AABC647DD50ABCC1A430B(L_14, (12.0f), (20.0f), (25.0f), NULL);
		NullCheck(L_13);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_13, 2, L_14, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		Dictionary_2_t2D2D3E7DBCDEA2EAE0C1C9169CFCE0FE8C2A1B44* L_15 = L_13;
		New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* L_16 = (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31*)il2cpp_codegen_object_new(New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31_il2cpp_TypeInfo_var);
		NullCheck(L_16);
		New2023__ctor_m1A3A1B18E2487893683BED1513A0B328793D69E6(L_16, (8.80000019f), (5.0f), NULL);
		NullCheck(L_15);
		Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B(L_15, 3, L_16, Dictionary_2_Add_mC58915B2507A2BE05E570AA0C30AA0C1038EC19B_RuntimeMethod_var);
		__this->___CModeDic_11 = L_15;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___CModeDic_11), (void*)L_15);
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
// System.Void CameraMode::SetMeCenter(UnityEngine.Transform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraMode_SetMeCenter_m7EF634EA83FBD929B8E52E998076BCE50F5AB33D (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___meCenter0, const RuntimeMethod* method) 
{
	{
		// this.meCenter = meCenter;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___meCenter0;
		__this->___meCenter_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___meCenter_1), (void*)L_0);
		// }
		return;
	}
}
// System.Void CameraMode::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraMode_Enter_mBB221F38A8AA8C6764240DB1D5E60224ACF708F9 (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// }
		return;
	}
}
// System.Void CameraMode::Exit(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraMode_Exit_m6FA30E515A4E37839C03427B2DABF52B2A5AE19C (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// }
		return;
	}
}
// System.Void CameraMode::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraMode_LocalUpdate_m7BC66684A293A9FA490CF60283C8A1E4A5560689 (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// }
		return;
	}
}
// UnityEngine.Vector3 CameraMode::GetDirection(UnityEngine.Vector3,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CameraMode_GetDirection_m09F7279A566D19CC5889EEDD7AD3487C4E842707 (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___original0, float ___offsetAngle1, float ___chuizhiangle2, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// Vector3 targetDir = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		V_0 = L_0;
		// Quaternion offsetRot = Quaternion.AngleAxis(offsetAngle, Vector3.up);
		float L_1 = ___offsetAngle1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_3;
		L_3 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(L_1, L_2, NULL);
		V_1 = L_3;
		// Quaternion offsetRot2 = Quaternion.AngleAxis(chuizhiangle, Vector3.left);
		float L_4 = ___chuizhiangle2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_get_left_mA75C525C1E78B5BB99E9B7A63EF68C731043FE18_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6;
		L_6 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(L_4, L_5, NULL);
		// targetDir = offsetRot2 * offsetRot * original;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_7 = V_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_8;
		L_8 = Quaternion_op_Multiply_m5AC8B39C55015059BDD09122E04E47D4BFAB2276_inline(L_6, L_7, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = ___original0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_8, L_9, NULL);
		V_0 = L_10;
		// return targetDir.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_0), NULL);
		return L_11;
	}
}
// UnityEngine.Vector3 CameraMode::GetVerticalDir(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____dir0, const RuntimeMethod* method) 
{
	{
		// _dir.y = 0;
		(&____dir0)->___y_3 = (0.0f);
		// _dir = Quaternion.AngleAxis(90, Vector3.up) * _dir;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_1;
		L_1 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8((90.0f), L_0, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ____dir0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_1, L_2, NULL);
		____dir0 = L_3;
		// return _dir;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ____dir0;
		return L_4;
	}
}
// System.Void CameraMode::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8 (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, const RuntimeMethod* method) 
{
	{
		// protected bool auto = true;
		__this->___auto_5 = (bool)1;
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
// System.Void CenterSurroundCamera::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CenterSurroundCamera__ctor_m250239560EAEA5F20A97A7E4709B079CCB0819A5 (CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// Vector3 focuscenter = Vector3.zero; //???????????????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___focuscenter_14 = L_0;
		// Vector3 xzOff = Vector3.forward; //???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		__this->___xzOff_16 = L_1;
		// public CenterSurroundCamera(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZDis = XZDis;
		float L_2 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_2;
		// this.YDis = YDis;
		float L_3 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_3;
		// }
		return;
	}
}
// System.Void CenterSurroundCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CenterSurroundCamera_LocalUpdate_mFE5B339EC2463BF88034CD22C0CECBF59EE27AEF (CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	{
		// h = UnityEngine.Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_0;
		L_0 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_1;
		L_1 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_18 = ((float)il2cpp_codegen_add(L_0, L_1));
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_2 = __this->___h_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4;
		L_4 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_2, (1.5f))), L_3, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = __this->___xzOff_16;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_4, L_5, NULL);
		__this->___xzOff_16 = L_6;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_7 = (&__this->___xzOff_16);
		L_7->___y_3 = (0.0f);
		// CameraTargetPos = focuscenter + xzOff.normalized * XZDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = __this->___focuscenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_9 = (&__this->___xzOff_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_9, NULL);
		float L_11 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_10, L_11, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_8, L_12, NULL);
		__this->___CameraTargetPos_13 = L_13;
		// CameraTargetPos.y = YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_14 = (&__this->___CameraTargetPos_13);
		float L_15 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		L_14->___y_3 = L_15;
		// rotateToDirection = focuscenter - CameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = __this->___focuscenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_16, L_17, NULL);
		__this->___rotateToDirection_20 = L_18;
		// rotateToDirection.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_19 = (&__this->___rotateToDirection_20);
		L_19->___y_3 = (0.0f);
		// rotateToDirection = rotateToDirection.normalized + Vector3.down/2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_20 = (&__this->___rotateToDirection_20);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_20, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_get_down_m19EB5B5B0EDFE9C272BD7BCC6923C4A9D616F771_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_22, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_21, L_23, NULL);
		__this->___rotateToDirection_20 = L_24;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//????????????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_25 = ____camera0;
		NullCheck(L_25);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_26;
		L_26 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_25, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_27 = ____camera0;
		NullCheck(L_27);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_28;
		L_28 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_27, NULL);
		NullCheck(L_28);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_28, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = __this->___CameraTargetPos_13;
		float L_31;
		L_31 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_32;
		L_32 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_29, L_30, ((float)(L_31/((float)il2cpp_codegen_add((0.200000003f), L_32)))), NULL);
		NullCheck(L_26);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_26, L_33, NULL);
		// ToRotation = Quaternion.LookRotation(rotateToDirection);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34 = __this->___rotateToDirection_20;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_35;
		L_35 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_34, NULL);
		__this->___ToRotation_15 = L_35;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_36 = ____camera0;
		NullCheck(L_36);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_37;
		L_37 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_36, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_38 = ____camera0;
		NullCheck(L_38);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_39;
		L_39 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_38, NULL);
		NullCheck(L_39);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_40;
		L_40 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_39, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_41 = __this->___ToRotation_15;
		float L_42;
		L_42 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_43;
		L_43 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_44;
		L_44 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_40, L_41, ((float)(L_42/((float)il2cpp_codegen_add((0.200000003f), L_43)))), NULL);
		NullCheck(L_37);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_37, L_44, NULL);
		// }
		return;
	}
}
// System.Void CenterSurroundCamera::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CenterSurroundCamera_Enter_m752748D436BD683F7C062C48E7AAAEFA220D4B75 (CenterSurroundCamera_t01BA4F3457AB8525D83AF144FE066D6F7DED299C* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
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
// System.Void CertainYAntiVabration::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CertainYAntiVabration__ctor_m01C13E75963F7BECFD624E8394746C87FF5A3D80 (CertainYAntiVabration_tC3F93440371E35297502C0F0431F942257E24AC9* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		__this->___xzOff_20 = L_0;
		// public CertainYAntiVabration(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// }
		return;
	}
}
// System.Void CertainYAntiVabration::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CertainYAntiVabration_LocalUpdate_m691F8380B571984427D8415800FFA3D9FC64CDC0 (CertainYAntiVabration_tC3F93440371E35297502C0F0431F942257E24AC9* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_0;
		L_0 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_1;
		L_1 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_22 = ((float)il2cpp_codegen_add(L_0, L_1));
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_2 = __this->___h_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4;
		L_4 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_2, (1.5f))), L_3, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = __this->___xzOff_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_4, L_5, NULL);
		__this->___xzOff_20 = L_6;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_7 = (&__this->___xzOff_20);
		L_7->___y_3 = (0.0f);
		// if (auto)
		bool L_8 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_8)
		{
			goto IL_025e;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_9 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_9)
		{
			goto IL_025e;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_10 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_10, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_11) <= ((int32_t)0)))
		{
			goto IL_025e;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_12;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_13 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_13);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_14;
		L_14 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_13, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_14;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ca:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_00bf_1;
			}

IL_0092_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_15;
				L_15 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_15;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_16 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_17;
				L_17 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_16, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_17)
				{
					goto IL_00bf_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19 = V_1;
				NullCheck(L_19);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_20;
				L_20 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_19, NULL);
				NullCheck(L_20);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
				L_21 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_20, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
				L_22 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_18, L_21, NULL);
				__this->___enemiesCenter_14 = L_22;
			}

IL_00bf_1:
			{
				// foreach (Transform o in targets)
				bool L_23;
				L_23 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_23)
				{
					goto IL_0092_1;
				}
			}
			{
				goto IL_00d8;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00d8:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_25 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_25);
		int32_t L_26;
		L_26 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_25, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_24, ((float)L_26), NULL);
		__this->___enemiesCenter_14 = L_27;
		// enemiesCenter.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_28 = (&__this->___enemiesCenter_14);
		L_28->___y_3 = (0.0f);
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_29 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = __this->___enemiesCenter_14;
		NullCheck(L_29);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31;
		L_31 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_29, L_30, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_32;
		L_32 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_31, NULL);
		__this->___enemyscreenpos_19 = L_32;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_33 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_34 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_34);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
		L_35 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_34, NULL);
		NullCheck(L_33);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_36;
		L_36 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_33, L_35, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_37;
		L_37 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_36, NULL);
		__this->___mescreenpos_18 = L_37;
		// if (enemyscreenpos.x < 0.08 || enemyscreenpos.x > 0.92 || enemyscreenpos.y < 0.1)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___enemyscreenpos_19);
		float L_39 = L_38->___x_0;
		if ((((double)((double)L_39)) < ((double)(0.080000000000000002))))
		{
			goto IL_017d;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_40 = (&__this->___enemyscreenpos_19);
		float L_41 = L_40->___x_0;
		if ((((double)((double)L_41)) > ((double)(0.92000000000000004))))
		{
			goto IL_017d;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_42 = (&__this->___enemyscreenpos_19);
		float L_43 = L_42->___y_1;
		if ((!(((double)((double)L_43)) < ((double)(0.10000000000000001)))))
		{
			goto IL_01b9;
		}
	}

IL_017d:
	{
		// xzOff = Vector3.RotateTowards(xzOff, meCenter.position - enemiesCenter, 4 * Time.deltaTime, 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44 = __this->___xzOff_20;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_45 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_45);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_45, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_46, L_47, NULL);
		float L_49;
		L_49 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		L_50 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_44, L_48, ((float)il2cpp_codegen_multiply((4.0f), L_49)), (0.0f), NULL);
		__this->___xzOff_20 = L_50;
		goto IL_025e;
	}

IL_01b9:
	{
		// if (Mathf.Abs(mescreenpos.x - enemyscreenpos.x) < (Mathf.Abs(mescreenpos.y - enemyscreenpos.y) + 0.2f) &&
		//     (enemyscreenpos.x > 0.35 && enemyscreenpos.x < 0.65))
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_51 = (&__this->___mescreenpos_18);
		float L_52 = L_51->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_53 = (&__this->___enemyscreenpos_19);
		float L_54 = L_53->___x_0;
		float L_55;
		L_55 = fabsf(((float)il2cpp_codegen_subtract(L_52, L_54)));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_56 = (&__this->___mescreenpos_18);
		float L_57 = L_56->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_58 = (&__this->___enemyscreenpos_19);
		float L_59 = L_58->___y_1;
		float L_60;
		L_60 = fabsf(((float)il2cpp_codegen_subtract(L_57, L_59)));
		if ((!(((float)L_55) < ((float)((float)il2cpp_codegen_add(L_60, (0.200000003f)))))))
		{
			goto IL_025e;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_61 = (&__this->___enemyscreenpos_19);
		float L_62 = L_61->___x_0;
		if ((!(((double)((double)L_62)) > ((double)(0.34999999999999998)))))
		{
			goto IL_025e;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_63 = (&__this->___enemyscreenpos_19);
		float L_64 = L_63->___x_0;
		if ((!(((double)((double)L_64)) < ((double)(0.65000000000000002)))))
		{
			goto IL_025e;
		}
	}
	{
		// xzOff = Vector3.RotateTowards(xzOff, GetVerticalDir(meCenter.position - enemiesCenter), Time.deltaTime, 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_65 = __this->___xzOff_20;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_66 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_66);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_67;
		L_67 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_66, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_68 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_69;
		L_69 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_67, L_68, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_70;
		L_70 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_69, NULL);
		float L_71;
		L_71 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_72;
		L_72 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_65, L_70, L_71, (0.0f), NULL);
		__this->___xzOff_20 = L_72;
	}

IL_025e:
	{
		// CameraTargetPos = meCenter.position + xzOff.normalized * XZDis;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_73 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_73);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_74;
		L_74 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_73, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_75 = (&__this->___xzOff_20);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_76;
		L_76 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_75, NULL);
		float L_77 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78;
		L_78 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_76, L_77, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_79;
		L_79 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_74, L_78, NULL);
		__this->___CameraTargetPos_13 = L_79;
		// CameraTargetPos += Vector3.up * YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_80 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_81;
		L_81 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		float L_82 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_83;
		L_83 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_81, L_82, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_84;
		L_84 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_80, L_83, NULL);
		__this->___CameraTargetPos_13 = L_84;
		// fixy = Mathf.Clamp(CameraTargetPos.y, YDis, CameraTargetPos.y);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_85 = (&__this->___CameraTargetPos_13);
		float L_86 = L_85->___y_3;
		float L_87 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_88 = (&__this->___CameraTargetPos_13);
		float L_89 = L_88->___y_3;
		float L_90;
		L_90 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_86, L_87, L_89, NULL);
		__this->___fixy_21 = L_90;
		// CameraTargetPos.y = fixy;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_91 = (&__this->___CameraTargetPos_13);
		float L_92 = __this->___fixy_21;
		L_91->___y_3 = L_92;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//????????????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_93 = ____camera0;
		NullCheck(L_93);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_94;
		L_94 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_93, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_95 = ____camera0;
		NullCheck(L_95);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_96;
		L_96 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_95, NULL);
		NullCheck(L_96);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_97;
		L_97 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_96, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_98 = __this->___CameraTargetPos_13;
		float L_99;
		L_99 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_100;
		L_100 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_101;
		L_101 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_97, L_98, ((float)(L_99/((float)il2cpp_codegen_add((0.200000003f), L_100)))), NULL);
		NullCheck(L_94);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_94, L_101, NULL);
		// temp = (meCenter.position + Vector3.up * 2f);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_102 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_102);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_103;
		L_103 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_102, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_104;
		L_104 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105;
		L_105 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_104, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_106;
		L_106 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_103, L_105, NULL);
		__this->___temp_17 = L_106;
		// h = Mathf.Clamp(temp.y, 1f, 8f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_107 = (&__this->___temp_17);
		float L_108 = L_107->___y_3;
		float L_109;
		L_109 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_108, (1.0f), (8.0f), NULL);
		__this->___h_22 = L_109;
		// temp = new Vector3(temp.x, h, temp.z);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_110 = (&__this->___temp_17);
		float L_111 = L_110->___x_2;
		float L_112 = __this->___h_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_113 = (&__this->___temp_17);
		float L_114 = L_113->___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_115;
		memset((&L_115), 0, sizeof(L_115));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_115), L_111, L_112, L_114, /*hidden argument*/NULL);
		__this->___temp_17 = L_115;
		// rotateToDirection = temp - CameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_116 = __this->___temp_17;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_117 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118;
		L_118 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_116, L_117, NULL);
		__this->___rotateToDirection_16 = L_118;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_119 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120;
		L_120 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_119, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_121;
		L_121 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_120, NULL);
		__this->___ToRotation_15 = L_121;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, (Time.deltaTime) / (0.2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_122 = ____camera0;
		NullCheck(L_122);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_123;
		L_123 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_122, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_124 = ____camera0;
		NullCheck(L_124);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_125;
		L_125 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_124, NULL);
		NullCheck(L_125);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_126;
		L_126 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_125, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_127 = __this->___ToRotation_15;
		float L_128;
		L_128 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_129;
		L_129 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_130;
		L_130 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_126, L_127, ((float)(L_128/((float)il2cpp_codegen_add((0.200000003f), L_129)))), NULL);
		NullCheck(L_123);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_123, L_130, NULL);
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
// System.Void CertainYAntiVibrationCamera::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CertainYAntiVibrationCamera__ctor_m64A38B7A07D63C08DDBDAB5322761CBC7AAC0502 (CertainYAntiVibrationCamera_tBE98E18C6C6A13DE240FEEAA67D2CC0074175BC6* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		__this->___xzOff_18 = L_0;
		// public CertainYAntiVibrationCamera(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// }
		return;
	}
}
// System.Void CertainYAntiVibrationCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CertainYAntiVibrationCamera_LocalUpdate_m4491315954DB7269B9C41D63616B0E7B8193A551 (CertainYAntiVibrationCamera_tBE98E18C6C6A13DE240FEEAA67D2CC0074175BC6* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// screenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_1);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_1, NULL);
		NullCheck(L_0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_0, L_2, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4;
		L_4 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_3, NULL);
		__this->___screenpos_17 = L_4;
		// if ((screenpos.x < 0.3 || screenpos.x > 0.7) && YDis < 9)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_5 = (&__this->___screenpos_17);
		float L_6 = L_5->___x_0;
		if ((((double)((double)L_6)) < ((double)(0.29999999999999999))))
		{
			goto IL_004a;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_7 = (&__this->___screenpos_17);
		float L_8 = L_7->___x_0;
		if ((!(((double)((double)L_8)) > ((double)(0.69999999999999996)))))
		{
			goto IL_007b;
		}
	}

IL_004a:
	{
		float L_9 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		if ((!(((float)L_9) < ((float)(9.0f)))))
		{
			goto IL_007b;
		}
	}
	{
		// XZDis += 1f;
		float L_10 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = ((float)il2cpp_codegen_add(L_10, (1.0f)));
		// YDis += 1f;
		float L_11 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_add(L_11, (1.0f)));
	}

IL_007b:
	{
		// if ((screenpos.x > 0.4 && screenpos.x < 0.6 && screenpos.y > 0.4 && screenpos.y < 0.6) && XZDis > 12f)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_12 = (&__this->___screenpos_17);
		float L_13 = L_12->___x_0;
		if ((!(((double)((double)L_13)) > ((double)(0.40000000000000002)))))
		{
			goto IL_0108;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_14 = (&__this->___screenpos_17);
		float L_15 = L_14->___x_0;
		if ((!(((double)((double)L_15)) < ((double)(0.59999999999999998)))))
		{
			goto IL_0108;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_16 = (&__this->___screenpos_17);
		float L_17 = L_16->___y_1;
		if ((!(((double)((double)L_17)) > ((double)(0.40000000000000002)))))
		{
			goto IL_0108;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_18 = (&__this->___screenpos_17);
		float L_19 = L_18->___y_1;
		if ((!(((double)((double)L_19)) < ((double)(0.59999999999999998)))))
		{
			goto IL_0108;
		}
	}
	{
		float L_20 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		if ((!(((float)L_20) > ((float)(12.0f)))))
		{
			goto IL_0108;
		}
	}
	{
		// XZDis -= 1f;
		float L_21 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = ((float)il2cpp_codegen_subtract(L_21, (1.0f)));
		// YDis -= 1f;
		float L_22 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_subtract(L_22, (1.0f)));
	}

IL_0108:
	{
		// if (targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_23 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_23);
		int32_t L_24;
		L_24 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_23, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_24) <= ((int32_t)0)))
		{
			goto IL_02d3;
		}
	}
	{
		// enemiescenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiescenter_14 = L_25;
		// foreach (Transform o in this.targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_26 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_26);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_27;
		L_27 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_26, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_27;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0266:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0258_1;
			}

IL_0135_1:
			{
				// foreach (Transform o in this.targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_28;
				L_28 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_28;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_29 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_30;
				L_30 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_29, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_30)
				{
					goto IL_0162_1;
				}
			}
			{
				// enemiescenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31 = __this->___enemiescenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_32 = V_1;
				NullCheck(L_32);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_33;
				L_33 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_32, NULL);
				NullCheck(L_33);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
				L_34 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_33, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
				L_35 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_31, L_34, NULL);
				__this->___enemiescenter_14 = L_35;
			}

IL_0162_1:
			{
				// screenpos = _camera.WorldToViewportPoint(o.position);
				Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_36 = ____camera0;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_37 = V_1;
				NullCheck(L_37);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38;
				L_38 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_37, NULL);
				NullCheck(L_36);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
				L_39 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_36, L_38, NULL);
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_40;
				L_40 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_39, NULL);
				__this->___screenpos_17 = L_40;
				// if (screenpos.x < 0.2 || screenpos.x > 0.8)
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_41 = (&__this->___screenpos_17);
				float L_42 = L_41->___x_0;
				if ((((double)((double)L_42)) < ((double)(0.20000000000000001))))
				{
					goto IL_01a7_1;
				}
			}
			{
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_43 = (&__this->___screenpos_17);
				float L_44 = L_43->___x_0;
				if ((!(((double)((double)L_44)) > ((double)(0.80000000000000004)))))
				{
					goto IL_01cb_1;
				}
			}

IL_01a7_1:
			{
				// XZDis += 0.4f;
				float L_45 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
				((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = ((float)il2cpp_codegen_add(L_45, (0.400000006f)));
				// YDis += 0.4f;
				float L_46 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
				((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_add(L_46, (0.400000006f)));
			}

IL_01cb_1:
			{
				// if ((screenpos.x > 0.4 && screenpos.x < 0.6 && screenpos.y > 0.4 && screenpos.y < 0.6) && XZDis > 10f)
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_47 = (&__this->___screenpos_17);
				float L_48 = L_47->___x_0;
				if ((!(((double)((double)L_48)) > ((double)(0.40000000000000002)))))
				{
					goto IL_0258_1;
				}
			}
			{
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_49 = (&__this->___screenpos_17);
				float L_50 = L_49->___x_0;
				if ((!(((double)((double)L_50)) < ((double)(0.59999999999999998)))))
				{
					goto IL_0258_1;
				}
			}
			{
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_51 = (&__this->___screenpos_17);
				float L_52 = L_51->___y_1;
				if ((!(((double)((double)L_52)) > ((double)(0.40000000000000002)))))
				{
					goto IL_0258_1;
				}
			}
			{
				Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_53 = (&__this->___screenpos_17);
				float L_54 = L_53->___y_1;
				if ((!(((double)((double)L_54)) < ((double)(0.59999999999999998)))))
				{
					goto IL_0258_1;
				}
			}
			{
				float L_55 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
				if ((!(((float)L_55) > ((float)(10.0f)))))
				{
					goto IL_0258_1;
				}
			}
			{
				// XZDis -= 0.4f;
				float L_56 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
				((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = ((float)il2cpp_codegen_subtract(L_56, (0.400000006f)));
				// YDis -= 0.4f;
				float L_57 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
				((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_subtract(L_57, (0.400000006f)));
			}

IL_0258_1:
			{
				// foreach (Transform o in this.targets)
				bool L_58;
				L_58 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_58)
				{
					goto IL_0135_1;
				}
			}
			{
				goto IL_0274;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0274:
	{
		// enemiescenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_59 = __this->___enemiescenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_60 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_60);
		int32_t L_61;
		L_61 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_60, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_62;
		L_62 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_59, ((float)L_61), NULL);
		__this->___enemiescenter_14 = L_62;
		// focuscenter = meCenter.position + (enemiescenter - meCenter.position) * 1 / 2;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_63 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_63);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_64;
		L_64 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_63, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_65 = __this->___enemiescenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_66 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_66);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_67;
		L_67 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_66, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_68;
		L_68 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_65, L_67, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_69;
		L_69 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_68, (1.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_70;
		L_70 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_69, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_71;
		L_71 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_64, L_70, NULL);
		__this->___focuscenter_15 = L_71;
		goto IL_02e4;
	}

IL_02d3:
	{
		// focuscenter = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_72 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_72);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_73;
		L_73 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_72, NULL);
		__this->___focuscenter_15 = L_73;
	}

IL_02e4:
	{
		// enemiescenter.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_74 = (&__this->___enemiescenter_14);
		L_74->___y_3 = (0.0f);
		// angele = Vector3.Angle(meCenter.position - Vector3.zero, enemiescenter - Vector3.zero);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_75 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_75);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_76;
		L_76 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_75, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_77;
		L_77 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78;
		L_78 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_76, L_77, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_79 = __this->___enemiescenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_80;
		L_80 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_81;
		L_81 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_79, L_80, NULL);
		float L_82;
		L_82 = Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline(L_78, L_81, NULL);
		__this->___angele_19 = L_82;
		// if (auto)
		bool L_83 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_83)
		{
			goto IL_039c;
		}
	}
	{
		// xzOff = -(1 - (angele / (180f - angele))) * (meCenter.position + enemiescenter) + (angele / (180f - angele)) * (meCenter.position - enemiescenter);
		float L_84 = __this->___angele_19;
		float L_85 = __this->___angele_19;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_86 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_86);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_87;
		L_87 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_86, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_88 = __this->___enemiescenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_89;
		L_89 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_87, L_88, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_90;
		L_90 = Vector3_op_Multiply_m29F4414A9D30B7C0CD8455C4B2F049E8CCF66745_inline(((-((float)il2cpp_codegen_subtract((1.0f), ((float)(L_84/((float)il2cpp_codegen_subtract((180.0f), L_85)))))))), L_89, NULL);
		float L_91 = __this->___angele_19;
		float L_92 = __this->___angele_19;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_93 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_93);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_94;
		L_94 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_93, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_95 = __this->___enemiescenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_96;
		L_96 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_94, L_95, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_97;
		L_97 = Vector3_op_Multiply_m29F4414A9D30B7C0CD8455C4B2F049E8CCF66745_inline(((float)(L_91/((float)il2cpp_codegen_subtract((180.0f), L_92)))), L_96, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_98;
		L_98 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_90, L_97, NULL);
		__this->___xzOff_18 = L_98;
		goto IL_03de;
	}

IL_039c:
	{
		// h = UnityEngine.Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_99;
		L_99 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_100;
		L_100 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_20 = ((float)il2cpp_codegen_add(L_99, L_100));
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_101 = __this->___h_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_102;
		L_102 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_103;
		L_103 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_101, (1.5f))), L_102, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_104 = __this->___xzOff_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105;
		L_105 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_103, L_104, NULL);
		__this->___xzOff_18 = L_105;
	}

IL_03de:
	{
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_106 = (&__this->___xzOff_18);
		L_106->___y_3 = (0.0f);
		// CameraTargetPos = focuscenter + xzOff.normalized * XZDis;//focuscenter + xzOff.normalized * XZDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_107 = __this->___focuscenter_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_108 = (&__this->___xzOff_18);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_109;
		L_109 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_108, NULL);
		float L_110 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_111;
		L_111 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_109, L_110, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_112;
		L_112 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_107, L_111, NULL);
		__this->___CameraTargetPos_13 = L_112;
		// CameraTargetPos.y = Mathf.Clamp(YDis - angele / 180 * 10f, 6f,7f);//????????????????????????????????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_113 = (&__this->___CameraTargetPos_13);
		float L_114 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		float L_115 = __this->___angele_19;
		float L_116;
		L_116 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(((float)il2cpp_codegen_subtract(L_114, ((float)il2cpp_codegen_multiply(((float)(L_115/(180.0f))), (10.0f))))), (6.0f), (7.0f), NULL);
		L_113->___y_3 = L_116;
		// rotateToDirection = focuscenter - CameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_117 = __this->___focuscenter_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_119;
		L_119 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_117, L_118, NULL);
		__this->___rotateToDirection_23 = L_119;
		// rotateToDirection.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_120 = (&__this->___rotateToDirection_23);
		L_120->___y_3 = (0.0f);
		// rotateToDirection = rotateToDirection.normalized + Vector3.down/2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_121 = (&__this->___rotateToDirection_23);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_122;
		L_122 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_121, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_123;
		L_123 = Vector3_get_down_m19EB5B5B0EDFE9C272BD7BCC6923C4A9D616F771_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_124;
		L_124 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_123, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_125;
		L_125 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_122, L_124, NULL);
		__this->___rotateToDirection_23 = L_125;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//????????????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_126 = ____camera0;
		NullCheck(L_126);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_127;
		L_127 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_126, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_128 = ____camera0;
		NullCheck(L_128);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_129;
		L_129 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_128, NULL);
		NullCheck(L_129);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_130;
		L_130 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_129, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_131 = __this->___CameraTargetPos_13;
		float L_132;
		L_132 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_133;
		L_133 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_134;
		L_134 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_130, L_131, ((float)(L_132/((float)il2cpp_codegen_add((0.200000003f), L_133)))), NULL);
		NullCheck(L_127);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_127, L_134, NULL);
		// ToRotation = Quaternion.LookRotation(rotateToDirection);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_135 = __this->___rotateToDirection_23;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_136;
		L_136 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_135, NULL);
		__this->___ToRotation_16 = L_136;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_137 = ____camera0;
		NullCheck(L_137);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_138;
		L_138 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_137, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_139 = ____camera0;
		NullCheck(L_139);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_140;
		L_140 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_139, NULL);
		NullCheck(L_140);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_141;
		L_141 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_140, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_142 = __this->___ToRotation_16;
		float L_143;
		L_143 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_144;
		L_144 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_145;
		L_145 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_141, L_142, ((float)(L_143/((float)il2cpp_codegen_add((0.200000003f), L_144)))), NULL);
		NullCheck(L_138);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_138, L_145, NULL);
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
// System.Single ChatGptFix::get_TransitionSpeedPara()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float ChatGptFix_get_TransitionSpeedPara_m409A745620888BFEB116DF710455916A9882F9A8 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// get => _transitionSpeedPara;
		float L_0 = __this->____transitionSpeedPara_26;
		return L_0;
	}
}
// System.Void ChatGptFix::set_TransitionSpeedPara(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_set_TransitionSpeedPara_mFC400679B27EB46F538B55D31A3912F8F6358CDB (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => _transitionSpeedPara = Mathf.Clamp(value, 0.2f, 5f);
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (0.200000003f), (5.0f), NULL);
		__this->____transitionSpeedPara_26 = L_1;
		return;
	}
}
// System.Void ChatGptFix::.ctor(System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix__ctor_m71A6397CC055AD4595AF6204E084B90CDC9DE526 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___XZDis0, float ___YDis1, float ___fieldOfView2, const RuntimeMethod* method) 
{
	{
		// float autoChangeAngleLimit = 30f;
		__this->___autoChangeAngleLimit_23 = (30.0f);
		// float autoRotateSpeed = 100;
		__this->___autoRotateSpeed_24 = (100.0f);
		// float _transitionSpeedPara = 10f;
		__this->____transitionSpeedPara_26 = (10.0f);
		// readonly float _lookPointHeight = 2f;
		__this->____lookPointHeight_27 = (2.0f);
		// private float screenDifferForRotate = 150;
		__this->___screenDifferForRotate_30 = (150.0f);
		// public ChatGptFix(float XZDis, float YDis, float fieldOfView)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// _minXZ = XZDis;
		float L_0 = ___XZDis0;
		__this->____minXZ_28 = L_0;
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// this.fieldOfView = fieldOfView;
		float L_3 = ___fieldOfView2;
		__this->___fieldOfView_29 = L_3;
		// }
		return;
	}
}
// System.Single ChatGptFix::get_XZDistance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float ChatGptFix_get_XZDistance_mB4A7F32E31E49E7F23F5088D645A76646325902C (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
// System.Void ChatGptFix::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_set_XZDistance_m6ECC3A6C2DE49FD4578047AC7946584D6DE56A64 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => XZDis = Mathf.Clamp(value, _minXZ , _minXZ + 20f);
		float L_0 = ___value0;
		float L_1 = __this->____minXZ_28;
		float L_2 = __this->____minXZ_28;
		float L_3;
		L_3 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, L_1, ((float)il2cpp_codegen_add(L_2, (20.0f))), NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_3;
		return;
	}
}
// System.Void ChatGptFix::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_Enter_m9BE09641B267D076983225B8BC6053B9F399D8FC (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ChatGptFix_U3CEnterU3Eb__25_0_m13ADE7F9BD5A6AEA1EC212EED8AC53991D5BC74C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ChatGptFix_U3CEnterU3Eb__25_1_mD0AD2D3F2C19FFBC7674083BD3C4C225118CD816_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// CanSetH = true;
		ChatGptFix_set_CanSetH_m55511D4FFCF8219BB2B27872D20A93E46FABAC6F(__this, (bool)1, NULL);
		// _camera.fieldOfView = this.fieldOfView;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		float L_1 = __this->___fieldOfView_29;
		NullCheck(L_0);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_0, L_1, NULL);
		// CameraManager._subCamera.fieldOfView = this.fieldOfView;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_2 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____subCamera_5;
		float L_3 = __this->___fieldOfView_29;
		NullCheck(L_2);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_2, L_3, NULL);
		// LocalUpdate(_camera);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_4 = ____camera0;
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(6 /* System.Void CameraMode::LocalUpdate(UnityEngine.Camera) */, __this, L_4);
		// xzOff = _camera.transform.position - lookPoint;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = ____camera0;
		NullCheck(L_5);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
		L_6 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_5, NULL);
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_7, L_8, NULL);
		__this->___xzOff_18 = L_9;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_10 = (&__this->___xzOff_18);
		L_10->___y_3 = (0.0f);
		// TransitionSpeedPara = 5f;
		ChatGptFix_set_TransitionSpeedPara_mFC400679B27EB46F538B55D31A3912F8F6358CDB(__this, (5.0f), NULL);
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* L_11 = (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03*)il2cpp_codegen_object_new(DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		NullCheck(L_11);
		DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA(L_11, __this, (intptr_t)((void*)ChatGptFix_U3CEnterU3Eb__25_0_m13ADE7F9BD5A6AEA1EC212EED8AC53991D5BC74C_RuntimeMethod_var), NULL);
		DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* L_12 = (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200*)il2cpp_codegen_object_new(DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E(L_12, __this, (intptr_t)((void*)ChatGptFix_U3CEnterU3Eb__25_1_mD0AD2D3F2C19FFBC7674083BD3C4C225118CD816_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* L_13;
		L_13 = DOTween_To_mEF916279231A76EB7217D421308E489B2B19E85D(L_11, L_12, (0.00100000005f), (1.0f), NULL);
		// }
		return;
	}
}
// System.Boolean ChatGptFix::get_CanSetH()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ChatGptFix_get_CanSetH_m1B1804C59790DF4A933DDB76290FB78C66A40869 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// get => _canSetH;
		bool L_0 = __this->____canSetH_36;
		return L_0;
	}
}
// System.Void ChatGptFix::set_CanSetH(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_set_CanSetH_m55511D4FFCF8219BB2B27872D20A93E46FABAC6F (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, bool ___value0, const RuntimeMethod* method) 
{
	{
		// _canSetH = value;
		bool L_0 = ___value0;
		__this->____canSetH_36 = L_0;
		// if (!_canSetH)
		bool L_1 = __this->____canSetH_36;
		if (L_1)
		{
			goto IL_001a;
		}
	}
	{
		// h = 0;
		__this->___h_31 = (0.0f);
	}

IL_001a:
	{
		// }
		return;
	}
}
// System.Void ChatGptFix::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_LocalUpdate_mE7A6FB6838B78A92A5F24F02CA979461CBB13E47 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_1;
	memset((&V_1), 0, sizeof(V_1));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_2 = NULL;
	float V_3 = 0.0f;
	int32_t G_B5_0 = 0;
	float G_B22_0 = 0.0f;
	float G_B22_1 = 0.0f;
	ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* G_B22_2 = NULL;
	float G_B21_0 = 0.0f;
	float G_B21_1 = 0.0f;
	ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* G_B21_2 = NULL;
	float G_B23_0 = 0.0f;
	float G_B23_1 = 0.0f;
	float G_B23_2 = 0.0f;
	ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* G_B23_3 = NULL;
	{
		// if (meCenter != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_001f;
		}
	}
	{
		// mePos = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_2, NULL);
		__this->___mePos_37 = L_3;
	}

IL_001f:
	{
		// _changeSpeed = Time.deltaTime / (TransitionSpeedPara + Time.deltaTime); //????????????????
		float L_4;
		L_4 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_5;
		L_5 = ChatGptFix_get_TransitionSpeedPara_m409A745620888BFEB116DF710455916A9882F9A8_inline(__this, NULL);
		float L_6;
		L_6 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->____changeSpeed_25 = ((float)(L_4/((float)il2cpp_codegen_add(L_5, L_6))));
		// bool hasTargets = targets != null && targets.Count > 0;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_7 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_7)
		{
			goto IL_004f;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_8 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_8);
		int32_t L_9;
		L_9 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_8, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		G_B5_0 = ((((int32_t)L_9) > ((int32_t)0))? 1 : 0);
		goto IL_0050;
	}

IL_004f:
	{
		G_B5_0 = 0;
	}

IL_0050:
	{
		V_0 = (bool)G_B5_0;
		// if (hasTargets)
		bool L_10 = V_0;
		if (!L_10)
		{
			goto IL_00d0;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_11;
		// foreach (var o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_12 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_12);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_13;
		L_13 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_12, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_1 = L_13;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00a5:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_1), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_009a_1;
			}

IL_006d_1:
			{
				// foreach (var o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14;
				L_14 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_1), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_2 = L_14;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_15 = V_2;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_16;
				L_16 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_15, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_16)
				{
					goto IL_009a_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_18 = V_2;
				NullCheck(L_18);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19;
				L_19 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_18, NULL);
				NullCheck(L_19);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
				L_20 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_19, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
				L_21 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_17, L_20, NULL);
				__this->___enemiesCenter_14 = L_21;
			}

IL_009a_1:
			{
				// foreach (var o in targets)
				bool L_22;
				L_22 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_1), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_22)
				{
					goto IL_006d_1;
				}
			}
			{
				goto IL_00b3;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00b3:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_24 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_24);
		int32_t L_25;
		L_25 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_24, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26;
		L_26 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_23, ((float)L_25), NULL);
		__this->___enemiesCenter_14 = L_26;
	}

IL_00d0:
	{
		// enemyScreenPos = camera.WorldToScreenPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_27 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28 = __this->___enemiesCenter_14;
		NullCheck(L_27);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_27, L_28, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_30;
		L_30 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_29, NULL);
		__this->___enemyScreenPos_17 = L_30;
		// meScreenPos = camera.WorldToScreenPoint(mePos);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_31 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_32 = __this->___mePos_37;
		NullCheck(L_31);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_31, L_32, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_34;
		L_34 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_33, NULL);
		__this->___meScreenPos_16 = L_34;
		// if (CanSetH)
		bool L_35;
		L_35 = ChatGptFix_get_CanSetH_m1B1804C59790DF4A933DDB76290FB78C66A40869_inline(__this, NULL);
		if (!L_35)
		{
			goto IL_0116;
		}
	}
	{
		// h = UltimateJoystick.GetHorizontalAxis("RotateCamera");
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_36;
		L_36 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_31 = L_36;
	}

IL_0116:
	{
		// if (h != 0)
		float L_37 = __this->___h_31;
		if ((((float)L_37) == ((float)(0.0f))))
		{
			goto IL_015f;
		}
	}
	{
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_38 = __this->___h_31;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_40;
		L_40 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_38, (1.5f))), L_39, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_41 = __this->___xzOff_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42;
		L_42 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_40, L_41, NULL);
		__this->___xzOff_18 = L_42;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_43 = (&__this->___xzOff_18);
		L_43->___y_3 = (0.0f);
		goto IL_01f1;
	}

IL_015f:
	{
		// if (Vector2.Distance(meScreenPos, enemyScreenPos) > screenDifferForRotate)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_44 = __this->___meScreenPos_16;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_45 = __this->___enemyScreenPos_17;
		float L_46;
		L_46 = Vector2_Distance_m220B2ADBE9F87426BEEE291263560DFE78F835B5_inline(L_44, L_45, NULL);
		float L_47 = __this->___screenDifferForRotate_30;
		if ((!(((float)L_46) > ((float)L_47))))
		{
			goto IL_01f1;
		}
	}
	{
		// float angleToHorizontal = 0;
		V_3 = (0.0f);
		// angleToHorizontal = CheckNeedForAutoRotate();
		float L_48;
		L_48 = ChatGptFix_U3CLocalUpdateU3Eg__CheckNeedForAutoRotateU7C38_0_m3DBBCD619B68FAC4E0E2E4CE474B4D143DD24217(__this, NULL);
		V_3 = L_48;
		// if (angleToHorizontal > autoChangeAngleLimit)
		float L_49 = V_3;
		float L_50 = __this->___autoChangeAngleLimit_23;
		if ((!(((float)L_49) > ((float)L_50))))
		{
			goto IL_01f1;
		}
	}
	{
		// _currentRotateClockWiseDirection = Clock();
		bool L_51;
		L_51 = ChatGptFix_U3CLocalUpdateU3Eg__ClockU7C38_1_m96C230C57C5E989514BCE8FC1D9488D22706E10D(__this, NULL);
		__this->____currentRotateClockWiseDirection_39 = L_51;
		// xzOff = Quaternion.Euler(0f, autoRotateSpeed *
		//                              ((angleToHorizontal - autoChangeAngleLimit)/(90 - autoChangeAngleLimit)) * Time.deltaTime  // ???????????????????"????"?????????????????????????????????????
		//                              * (_currentRotateClockWiseDirection ? -1f : 1f), 0f) * xzOff;
		float L_52 = __this->___autoRotateSpeed_24;
		float L_53 = V_3;
		float L_54 = __this->___autoChangeAngleLimit_23;
		float L_55 = __this->___autoChangeAngleLimit_23;
		float L_56;
		L_56 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		bool L_57 = __this->____currentRotateClockWiseDirection_39;
		G_B21_0 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_52, ((float)(((float)il2cpp_codegen_subtract(L_53, L_54))/((float)il2cpp_codegen_subtract((90.0f), L_55)))))), L_56));
		G_B21_1 = (0.0f);
		G_B21_2 = __this;
		if (L_57)
		{
			G_B22_0 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_52, ((float)(((float)il2cpp_codegen_subtract(L_53, L_54))/((float)il2cpp_codegen_subtract((90.0f), L_55)))))), L_56));
			G_B22_1 = (0.0f);
			G_B22_2 = __this;
			goto IL_01d1;
		}
	}
	{
		G_B23_0 = (1.0f);
		G_B23_1 = G_B21_0;
		G_B23_2 = G_B21_1;
		G_B23_3 = G_B21_2;
		goto IL_01d6;
	}

IL_01d1:
	{
		G_B23_0 = (-1.0f);
		G_B23_1 = G_B22_0;
		G_B23_2 = G_B22_1;
		G_B23_3 = G_B22_2;
	}

IL_01d6:
	{
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_58;
		L_58 = Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline(G_B23_2, ((float)il2cpp_codegen_multiply(G_B23_1, G_B23_0)), (0.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_59 = __this->___xzOff_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_60;
		L_60 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_58, L_59, NULL);
		NullCheck(G_B23_3);
		G_B23_3->___xzOff_18 = L_60;
	}

IL_01f1:
	{
		// ePosX = (float)((decimal)enemyScreenPos.x / Screen.width);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_61 = (&__this->___enemyScreenPos_17);
		float L_62 = L_61->___x_0;
		il2cpp_codegen_runtime_class_init_inline(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_63;
		L_63 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_62, NULL);
		int32_t L_64;
		L_64 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_65;
		L_65 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_64, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_66;
		L_66 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_63, L_65, NULL);
		float L_67;
		L_67 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_66, NULL);
		__this->___ePosX_32 = ((float)L_67);
		// ePosY = (float)((decimal)enemyScreenPos.y / Screen.height);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_68 = (&__this->___enemyScreenPos_17);
		float L_69 = L_68->___y_1;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_70;
		L_70 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_69, NULL);
		int32_t L_71;
		L_71 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_72;
		L_72 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_71, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_73;
		L_73 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_70, L_72, NULL);
		float L_74;
		L_74 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_73, NULL);
		__this->___ePosY_33 = ((float)L_74);
		// mPosX = (float)((decimal)meScreenPos.x / Screen.width);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_75 = (&__this->___meScreenPos_16);
		float L_76 = L_75->___x_0;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_77;
		L_77 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_76, NULL);
		int32_t L_78;
		L_78 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_79;
		L_79 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_78, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_80;
		L_80 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_77, L_79, NULL);
		float L_81;
		L_81 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_80, NULL);
		__this->___mPosX_34 = ((float)L_81);
		// mPosY = (float)((decimal)meScreenPos.y / Screen.height);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_82 = (&__this->___meScreenPos_16);
		float L_83 = L_82->___y_1;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_84;
		L_84 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_83, NULL);
		int32_t L_85;
		L_85 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_86;
		L_86 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_85, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_87;
		L_87 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_84, L_86, NULL);
		float L_88;
		L_88 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_87, NULL);
		__this->___mPosY_35 = ((float)L_88);
		// if (ePosX >= 0.3 && ePosX <= 0.7 &&
		//     mPosX >= 0.3 && mPosX <= 0.7 &&
		//     ePosY >= 0.3 && ePosY <= 0.7 &&
		//     mPosY >= 0.3 && mPosY <= 0.7)
		float L_89 = __this->___ePosX_32;
		if ((!(((double)((double)L_89)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_90 = __this->___ePosX_32;
		if ((!(((double)((double)L_90)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_91 = __this->___mPosX_34;
		if ((!(((double)((double)L_91)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_92 = __this->___mPosX_34;
		if ((!(((double)((double)L_92)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_93 = __this->___ePosY_33;
		if ((!(((double)((double)L_93)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_94 = __this->___ePosY_33;
		if ((!(((double)((double)L_94)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_95 = __this->___mPosY_35;
		if ((!(((double)((double)L_95)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034b;
		}
	}
	{
		float L_96 = __this->___mPosY_35;
		if ((!(((double)((double)L_96)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034b;
		}
	}
	{
		// XZDistance -= _changeSpeed;
		float L_97;
		L_97 = ChatGptFix_get_XZDistance_mB4A7F32E31E49E7F23F5088D645A76646325902C_inline(__this, NULL);
		float L_98 = __this->____changeSpeed_25;
		ChatGptFix_set_XZDistance_m6ECC3A6C2DE49FD4578047AC7946584D6DE56A64(__this, ((float)il2cpp_codegen_subtract(L_97, L_98)), NULL);
		goto IL_03ee;
	}

IL_034b:
	{
		// else if (ePosX <= 0.2 || ePosX >= 0.8 ||
		//          mPosX <= 0.2 || mPosX >= 0.8 ||
		//          ePosY <= 0.2 || ePosY >= 0.8 ||
		//          mPosY <= 0.2 || mPosY >= 0.8)
		float L_99 = __this->___ePosX_32;
		if ((((double)((double)L_99)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03db;
		}
	}
	{
		float L_100 = __this->___ePosX_32;
		if ((((double)((double)L_100)) >= ((double)(0.80000000000000004))))
		{
			goto IL_03db;
		}
	}
	{
		float L_101 = __this->___mPosX_34;
		if ((((double)((double)L_101)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03db;
		}
	}
	{
		float L_102 = __this->___mPosX_34;
		if ((((double)((double)L_102)) >= ((double)(0.80000000000000004))))
		{
			goto IL_03db;
		}
	}
	{
		float L_103 = __this->___ePosY_33;
		if ((((double)((double)L_103)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03db;
		}
	}
	{
		float L_104 = __this->___ePosY_33;
		if ((((double)((double)L_104)) >= ((double)(0.80000000000000004))))
		{
			goto IL_03db;
		}
	}
	{
		float L_105 = __this->___mPosY_35;
		if ((((double)((double)L_105)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03db;
		}
	}
	{
		float L_106 = __this->___mPosY_35;
		if ((!(((double)((double)L_106)) >= ((double)(0.80000000000000004)))))
		{
			goto IL_03ee;
		}
	}

IL_03db:
	{
		// XZDistance += _changeSpeed;
		float L_107;
		L_107 = ChatGptFix_get_XZDistance_mB4A7F32E31E49E7F23F5088D645A76646325902C_inline(__this, NULL);
		float L_108 = __this->____changeSpeed_25;
		ChatGptFix_set_XZDistance_m6ECC3A6C2DE49FD4578047AC7946584D6DE56A64(__this, ((float)il2cpp_codegen_add(L_107, L_108)), NULL);
	}

IL_03ee:
	{
		// if (enemyScreenPos.y >= meScreenPos.y)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_109 = (&__this->___enemyScreenPos_17);
		float L_110 = L_109->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_111 = (&__this->___meScreenPos_16);
		float L_112 = L_111->___y_1;
		if ((!(((float)L_110) >= ((float)L_112))))
		{
			goto IL_0420;
		}
	}
	{
		// frontWPos = mePos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_113 = __this->___mePos_37;
		__this->___frontWPos_20 = L_113;
		// backWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_114 = __this->___enemiesCenter_14;
		__this->___backWPos_21 = L_114;
		goto IL_0438;
	}

IL_0420:
	{
		// frontWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_115 = __this->___enemiesCenter_14;
		__this->___frontWPos_20 = L_115;
		// backWPos = mePos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_116 = __this->___mePos_37;
		__this->___backWPos_21 = L_116;
	}

IL_0438:
	{
		// lookPoint = (backWPos - frontWPos) * 0.5f + frontWPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_117 = __this->___backWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118 = __this->___frontWPos_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_119;
		L_119 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_117, L_118, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120;
		L_120 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_119, (0.5f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_121 = __this->___frontWPos_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_122;
		L_122 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_120, L_121, NULL);
		__this->___lookPoint_19 = L_122;
		// cameraTargetPos = lookPoint + xzOff.normalized * XZDistance;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_123 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_124 = (&__this->___xzOff_18);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_125;
		L_125 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_124, NULL);
		float L_126;
		L_126 = ChatGptFix_get_XZDistance_mB4A7F32E31E49E7F23F5088D645A76646325902C_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_127;
		L_127 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_125, L_126, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_128;
		L_128 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_123, L_127, NULL);
		__this->___cameraTargetPos_13 = L_128;
		// cameraTargetPos.y = YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_129 = (&__this->___cameraTargetPos_13);
		float L_130 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		L_129->___y_3 = L_130;
		// lookPoint.y = _lookPointHeight;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_131 = (&__this->___lookPoint_19);
		float L_132 = __this->____lookPointHeight_27;
		L_131->___y_3 = L_132;
		// if (hasTargets || h != 0)
		bool L_133 = V_0;
		if (L_133)
		{
			goto IL_04bd;
		}
	}
	{
		float L_134 = __this->___h_31;
		if ((((float)L_134) == ((float)(0.0f))))
		{
			goto IL_0538;
		}
	}

IL_04bd:
	{
		// camera.transform.position = Vector3.Lerp(camera.transform.position, cameraTargetPos, _changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_135 = ___camera0;
		NullCheck(L_135);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_136;
		L_136 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_135, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_137 = ___camera0;
		NullCheck(L_137);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_138;
		L_138 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_137, NULL);
		NullCheck(L_138);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_139;
		L_139 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_138, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_140 = __this->___cameraTargetPos_13;
		float L_141 = __this->____changeSpeed_25;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_142;
		L_142 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_139, L_140, L_141, NULL);
		NullCheck(L_136);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_136, L_142, NULL);
		// rotateToDirection = lookPoint - cameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_143 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_144 = __this->___cameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_145;
		L_145 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_143, L_144, NULL);
		__this->___rotateToDirection_15 = L_145;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_146 = (&__this->___rotateToDirection_15);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_147;
		L_147 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_146, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_148;
		L_148 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_147, NULL);
		__this->___ToRotation_22 = L_148;
		// camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, ToRotation, _changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_149 = ___camera0;
		NullCheck(L_149);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_150;
		L_150 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_149, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_151 = ___camera0;
		NullCheck(L_151);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_152;
		L_152 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_151, NULL);
		NullCheck(L_152);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_153;
		L_153 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_152, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_154 = __this->___ToRotation_22;
		float L_155 = __this->____changeSpeed_25;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_156;
		L_156 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_153, L_154, L_155, NULL);
		NullCheck(L_150);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_150, L_156, NULL);
	}

IL_0538:
	{
		// }
		return;
	}
}
// System.Single ChatGptFix::<Enter>b__25_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float ChatGptFix_U3CEnterU3Eb__25_0_m13ADE7F9BD5A6AEA1EC212EED8AC53991D5BC74C (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		float L_0;
		L_0 = ChatGptFix_get_TransitionSpeedPara_m409A745620888BFEB116DF710455916A9882F9A8_inline(__this, NULL);
		return L_0;
	}
}
// System.Void ChatGptFix::<Enter>b__25_1(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix_U3CEnterU3Eb__25_1_mD0AD2D3F2C19FFBC7674083BD3C4C225118CD816 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, float ___x0, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		float L_0 = ___x0;
		ChatGptFix_set_TransitionSpeedPara_mFC400679B27EB46F538B55D31A3912F8F6358CDB(__this, L_0, NULL);
		return;
	}
}
// System.Single ChatGptFix::<LocalUpdate>g__CheckNeedForAutoRotate|38_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float ChatGptFix_U3CLocalUpdateU3Eg__CheckNeedForAutoRotateU7C38_0_m3DBBCD619B68FAC4E0E2E4CE474B4D143DD24217 (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// if (meScreenPos.x < enemyScreenPos.x)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_0 = (&__this->___meScreenPos_16);
		float L_1 = L_0->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_2 = (&__this->___enemyScreenPos_17);
		float L_3 = L_2->___x_0;
		if ((!(((float)L_1) < ((float)L_3))))
		{
			goto IL_003e;
		}
	}
	{
		// return Mathf.Abs(Vector2.Angle(enemyScreenPos - meScreenPos, Vector3.right));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4 = __this->___enemyScreenPos_17;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_5 = __this->___meScreenPos_16;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6;
		L_6 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_4, L_5, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_get_right_m13B7C3EAA64DC921EC23346C56A5A597B5481FF5_inline(NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_8;
		L_8 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_7, NULL);
		float L_9;
		L_9 = Vector2_Angle_m9668B13074D1664DD192669C14B3A8FC01676299_inline(L_6, L_8, NULL);
		float L_10;
		L_10 = fabsf(L_9);
		return L_10;
	}

IL_003e:
	{
		// return Mathf.Abs(Vector2.Angle(enemyScreenPos - meScreenPos, -Vector3.right));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_11 = __this->___enemyScreenPos_17;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_12 = __this->___meScreenPos_16;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_13;
		L_13 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_11, L_12, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Vector3_get_right_m13B7C3EAA64DC921EC23346C56A5A597B5481FF5_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
		L_15 = Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline(L_14, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_16;
		L_16 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_15, NULL);
		float L_17;
		L_17 = Vector2_Angle_m9668B13074D1664DD192669C14B3A8FC01676299_inline(L_13, L_16, NULL);
		float L_18;
		L_18 = fabsf(L_17);
		return L_18;
	}
}
// System.Boolean ChatGptFix::<LocalUpdate>g__Clock|38_1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ChatGptFix_U3CLocalUpdateU3Eg__ClockU7C38_1_m96C230C57C5E989514BCE8FC1D9488D22706E10D (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// if (meScreenPos.x < enemyScreenPos.x)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_0 = (&__this->___meScreenPos_16);
		float L_1 = L_0->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_2 = (&__this->___enemyScreenPos_17);
		float L_3 = L_2->___x_0;
		if ((!(((float)L_1) < ((float)L_3))))
		{
			goto IL_0031;
		}
	}
	{
		// return meScreenPos.y < enemyScreenPos.y;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_4 = (&__this->___meScreenPos_16);
		float L_5 = L_4->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_6 = (&__this->___enemyScreenPos_17);
		float L_7 = L_6->___y_1;
		return (bool)((((float)L_5) < ((float)L_7))? 1 : 0);
	}

IL_0031:
	{
		// return meScreenPos.y > enemyScreenPos.y;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_8 = (&__this->___meScreenPos_16);
		float L_9 = L_8->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_10 = (&__this->___enemyScreenPos_17);
		float L_11 = L_10->___y_1;
		return (bool)((((float)L_9) > ((float)L_11))? 1 : 0);
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
// System.Void ChatGptFix2::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix2__ctor_m4EA4B5F6B599BF2F041B7321E8337517289C642D (ChatGptFix2_tA67A0EB8B87FEFA6B8FB70BF923327A8D5BEC9D3* __this, const RuntimeMethod* method) 
{
	{
		// public float radius = 20; // ?????
		__this->___radius_13 = (20.0f);
		// public float height = 8; // ????
		__this->___height_14 = (8.0f);
		// public float angle = 45; // ????
		__this->___angle_15 = (45.0f);
		// public float rotationSpeed =100 ; // ????
		__this->___rotationSpeed_16 = (100.0f);
		// public float panSpeed = 100; // ????
		__this->___panSpeed_17 = (100.0f);
		// public ChatGptFix2()
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// }
		return;
	}
}
// System.Void ChatGptFix2::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix2_Enter_m0E48B63C64B49839DDD1DFF92B3CFF78AC64C7A6 (ChatGptFix2_tA67A0EB8B87FEFA6B8FB70BF923327A8D5BEC9D3* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) 
{
	{
		// circleCenter = Vector3.zero; // ???????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___circleCenter_19 = L_0;
		// offset = new Vector3(0, height, -radius);
		float L_1 = __this->___height_14;
		float L_2 = __this->___radius_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		memset((&L_3), 0, sizeof(L_3));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_3), (0.0f), L_1, ((-L_2)), /*hidden argument*/NULL);
		__this->___offset_18 = L_3;
		// camera.transform.position = circleCenter + offset;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_4 = ___camera0;
		NullCheck(L_4);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
		L_5 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_4, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = __this->___circleCenter_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = __this->___offset_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_6, L_7, NULL);
		NullCheck(L_5);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_5, L_8, NULL);
		// camera.transform.rotation = Quaternion.Euler(angle, 0, 0);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_9 = ___camera0;
		NullCheck(L_9);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10;
		L_10 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_9, NULL);
		float L_11 = __this->___angle_15;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_12;
		L_12 = Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline(L_11, (0.0f), (0.0f), NULL);
		NullCheck(L_10);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_10, L_12, NULL);
		// }
		return;
	}
}
// System.Void ChatGptFix2::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ChatGptFix2_LocalUpdate_m4CED89B1764C8D3CC83119287F7620B19FEFA341 (ChatGptFix2_tA67A0EB8B87FEFA6B8FB70BF923327A8D5BEC9D3* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_1;
	memset((&V_1), 0, sizeof(V_1));
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_4;
	memset((&V_4), 0, sizeof(V_4));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_5;
	memset((&V_5), 0, sizeof(V_5));
	{
		// if (target != null) // ???????
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___target_2;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_005d;
		}
	}
	{
		// Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___target_2;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_2, NULL);
		float L_4 = L_3.___x_2;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___target_2;
		NullCheck(L_5);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_5, NULL);
		float L_7 = L_6.___z_4;
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&V_0), L_4, (0.0f), L_7, NULL);
		// camera.transform.position = targetPosition + offset;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_8 = ___camera0;
		NullCheck(L_8);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9;
		L_9 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_8, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = __this->___offset_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_10, L_11, NULL);
		NullCheck(L_9);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_9, L_12, NULL);
		// circleCenter = targetPosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		__this->___circleCenter_19 = L_13;
		goto IL_00f9;
	}

IL_005d:
	{
		// if (Input.GetMouseButton(1) || (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Moved))
		bool L_14;
		L_14 = Input_GetMouseButton_mE545CF4B790C6E202808B827E3141BEC3330DB70(1, NULL);
		if (L_14)
		{
			goto IL_0081;
		}
	}
	{
		int32_t L_15;
		L_15 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((!(((uint32_t)L_15) == ((uint32_t)2))))
		{
			goto IL_00f9;
		}
	}
	{
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_16;
		L_16 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(1, NULL);
		V_1 = L_16;
		int32_t L_17;
		L_17 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_1), NULL);
		if ((!(((uint32_t)L_17) == ((uint32_t)1))))
		{
			goto IL_00f9;
		}
	}

IL_0081:
	{
		// float panX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
		float L_18;
		L_18 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7, NULL);
		float L_19 = __this->___panSpeed_17;
		float L_20;
		L_20 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		V_2 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_18, L_19)), L_20));
		// float panZ = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
		float L_21;
		L_21 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0, NULL);
		float L_22 = __this->___panSpeed_17;
		float L_23;
		L_23 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		V_3 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_21, L_22)), L_23));
		// Vector3 direction = new Vector3(panX, 0, panZ);
		float L_24 = V_2;
		float L_25 = V_3;
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&V_4), L_24, (0.0f), L_25, NULL);
		// circleCenter = Vector3.ClampMagnitude(circleCenter + direction, radius);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26 = __this->___circleCenter_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = V_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_26, L_27, NULL);
		float L_29 = __this->___radius_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Vector3_ClampMagnitude_mDEF1E073986286F6EFA1552A5D0E1A0F6CBF4500_inline(L_28, L_29, NULL);
		__this->___circleCenter_19 = L_30;
		// camera.transform.position = circleCenter + offset;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_31 = ___camera0;
		NullCheck(L_31);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_32;
		L_32 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_31, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33 = __this->___circleCenter_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34 = __this->___offset_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
		L_35 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_33, L_34, NULL);
		NullCheck(L_32);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_32, L_35, NULL);
	}

IL_00f9:
	{
		// if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
		bool L_36;
		L_36 = Input_GetMouseButton_mE545CF4B790C6E202808B827E3141BEC3330DB70(0, NULL);
		if (L_36)
		{
			goto IL_0120;
		}
	}
	{
		int32_t L_37;
		L_37 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((!(((uint32_t)L_37) == ((uint32_t)1))))
		{
			goto IL_01a9;
		}
	}
	{
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_38;
		L_38 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_1 = L_38;
		int32_t L_39;
		L_39 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_1), NULL);
		if ((!(((uint32_t)L_39) == ((uint32_t)1))))
		{
			goto IL_01a9;
		}
	}

IL_0120:
	{
		// rotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
		float L_40 = __this->___rotationY_20;
		float L_41;
		L_41 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7, NULL);
		float L_42 = __this->___rotationSpeed_16;
		float L_43;
		L_43 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___rotationY_20 = ((float)il2cpp_codegen_add(L_40, ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_41, L_42)), L_43))));
		// Quaternion rotation = Quaternion.Euler(angle, rotationY, 0);
		float L_44 = __this->___angle_15;
		float L_45 = __this->___rotationY_20;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_46;
		L_46 = Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline(L_44, L_45, (0.0f), NULL);
		V_5 = L_46;
		// offset = rotation * new Vector3(0, height, -radius);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_47 = V_5;
		float L_48 = __this->___height_14;
		float L_49 = __this->___radius_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		memset((&L_50), 0, sizeof(L_50));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_50), (0.0f), L_48, ((-L_49)), /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_51;
		L_51 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_47, L_50, NULL);
		__this->___offset_18 = L_51;
		// camera.transform.position = circleCenter + offset;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_52 = ___camera0;
		NullCheck(L_52);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_53;
		L_53 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_52, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_54 = __this->___circleCenter_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_55 = __this->___offset_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_56;
		L_56 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_54, L_55, NULL);
		NullCheck(L_53);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_53, L_56, NULL);
		// camera.transform.rotation = rotation;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_57 = ___camera0;
		NullCheck(L_57);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_58;
		L_58 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_57, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_59 = V_5;
		NullCheck(L_58);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_58, L_59, NULL);
	}

IL_01a9:
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
// System.Void GodplayerCamera::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GodplayerCamera__ctor_m7E1B52BE9F2AD9BAAC9D73F2B49212B8EFE49C4A (GodplayerCamera_t69B1E62878C9B321870DEB22A717E76037545DB2* __this, float ___distance0, float ___zoom_range1, const RuntimeMethod* method) 
{
	{
		// private float distance_use = 1f;
		__this->___distance_use_13 = (1.0f);
		// public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
		__this->___perspectiveZoomSpeed_18 = (0.5f);
		// public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode
		__this->___orthoZoomSpeed_19 = (0.5f);
		// Vector3 direction = Vector3.forward;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		__this->___direction_20 = L_0;
		// public GodplayerCamera(float distance, float zoom_range)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.distance = distance;
		float L_1 = ___distance0;
		__this->___distance_14 = L_1;
		// this.distance_use = distance;
		float L_2 = ___distance0;
		__this->___distance_use_13 = L_2;
		// this.zoom_range = zoom_range;
		float L_3 = ___zoom_range1;
		__this->___zoom_range_15 = L_3;
		// }
		return;
	}
}
// System.Void GodplayerCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GodplayerCamera_LocalUpdate_m033B4ED9031D3204CE821CD96C6EF4DE02138939 (GodplayerCamera_t69B1E62878C9B321870DEB22A717E76037545DB2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFC6687DC37346CD2569888E29764F727FAF530E0);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_4;
	memset((&V_4), 0, sizeof(V_4));
	float V_5 = 0.0f;
	float V_6 = 0.0f;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_7;
	memset((&V_7), 0, sizeof(V_7));
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_8;
	memset((&V_8), 0, sizeof(V_8));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_9;
	memset((&V_9), 0, sizeof(V_9));
	{
		// if (targets == null)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		// return;
		return;
	}

IL_0009:
	{
		// if (targets.Count == 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_1, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0017;
		}
	}
	{
		// return;
		return;
	}

IL_0017:
	{
		// center = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___center_21 = L_3;
		// foreach (Transform o in this.targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_4 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_4);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_5;
		L_5 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_4, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_5;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0068:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_005d_1;
			}

IL_0030_1:
			{
				// foreach (Transform o in this.targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
				L_6 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_6;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_8;
				L_8 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_7, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_8)
				{
					goto IL_005d_1;
				}
			}
			{
				// center += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = __this->___center_21;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = V_1;
				NullCheck(L_10);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11;
				L_11 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_10, NULL);
				NullCheck(L_11);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
				L_12 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_11, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
				L_13 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_9, L_12, NULL);
				__this->___center_21 = L_13;
			}

IL_005d_1:
			{
				// foreach (Transform o in this.targets)
				bool L_14;
				L_14 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_14)
				{
					goto IL_0030_1;
				}
			}
			{
				goto IL_0076;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0076:
	{
		// center /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = __this->___center_21;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_16 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_16);
		int32_t L_17;
		L_17 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_16, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_15, ((float)L_17), NULL);
		__this->___center_21 = L_18;
		// speed = 1f;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6 = (1.0f);
		// if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor ||
		//     Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
		int32_t L_19;
		L_19 = Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D(NULL);
		if ((((int32_t)L_19) == ((int32_t)7)))
		{
			goto IL_00c0;
		}
	}
	{
		int32_t L_20;
		L_20 = Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D(NULL);
		if (!L_20)
		{
			goto IL_00c0;
		}
	}
	{
		int32_t L_21;
		L_21 = Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D(NULL);
		if ((((int32_t)L_21) == ((int32_t)2)))
		{
			goto IL_00c0;
		}
	}
	{
		int32_t L_22;
		L_22 = Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D(NULL);
		if ((!(((uint32_t)L_22) == ((uint32_t)1))))
		{
			goto IL_01dd;
		}
	}

IL_00c0:
	{
		// if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0)
		float L_23;
		L_23 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteralFC6687DC37346CD2569888E29764F727FAF530E0, NULL);
		if ((!(((float)L_23) < ((float)(0.0f)))))
		{
			goto IL_00f8;
		}
	}
	{
		// if (distance_use > distance - zoom_range)
		float L_24 = __this->___distance_use_13;
		float L_25 = __this->___distance_14;
		float L_26 = __this->___zoom_range_15;
		if ((!(((float)L_24) > ((float)((float)il2cpp_codegen_subtract(L_25, L_26))))))
		{
			goto IL_00f8;
		}
	}
	{
		// distance_use -= 0.1f;
		float L_27 = __this->___distance_use_13;
		__this->___distance_use_13 = ((float)il2cpp_codegen_subtract(L_27, (0.100000001f)));
	}

IL_00f8:
	{
		// if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0)
		float L_28;
		L_28 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteralFC6687DC37346CD2569888E29764F727FAF530E0, NULL);
		if ((!(((float)L_28) > ((float)(0.0f)))))
		{
			goto IL_0130;
		}
	}
	{
		// if (distance_use < distance + zoom_range)
		float L_29 = __this->___distance_use_13;
		float L_30 = __this->___distance_14;
		float L_31 = __this->___zoom_range_15;
		if ((!(((float)L_29) < ((float)((float)il2cpp_codegen_add(L_30, L_31))))))
		{
			goto IL_0130;
		}
	}
	{
		// distance_use += 0.1f;
		float L_32 = __this->___distance_use_13;
		__this->___distance_use_13 = ((float)il2cpp_codegen_add(L_32, (0.100000001f)));
	}

IL_0130:
	{
		// if (System.Math.Abs(UnityEngine.Input.GetAxis("Mouse X")) > 0)
		float L_33;
		L_33 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7, NULL);
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		float L_34;
		L_34 = fabsf(L_33);
		if ((!(((float)L_34) > ((float)(0.0f)))))
		{
			goto IL_017f;
		}
	}
	{
		// x = UnityEngine.Input.GetAxis("Mouse X") * speed * Time.deltaTime;
		float L_35;
		L_35 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7, NULL);
		float L_36 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_37;
		L_37 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___x_16 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_35, L_36)), L_37));
		// direction = GetDirection(direction, x, 0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38 = __this->___direction_20;
		float L_39 = __this->___x_16;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = CameraMode_GetDirection_m09F7279A566D19CC5889EEDD7AD3487C4E842707(L_38, L_39, (0.0f), NULL);
		__this->___direction_20 = L_40;
	}

IL_017f:
	{
		// if (System.Math.Abs(UnityEngine.Input.GetAxis("Mouse Y")) > 0)
		float L_41;
		L_41 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0, NULL);
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		float L_42;
		L_42 = fabsf(L_41);
		if ((!(((float)L_42) > ((float)(0.0f)))))
		{
			goto IL_0339;
		}
	}
	{
		// y = UnityEngine.Input.GetAxis("Mouse Y") * speed * 30 * Time.deltaTime;
		float L_43;
		L_43 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0, NULL);
		float L_44 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_45;
		L_45 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___y_17 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_43, L_44)), (30.0f))), L_45));
		// direction = GetDirection(direction, 0, -y);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46 = __this->___direction_20;
		float L_47 = __this->___y_17;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = CameraMode_GetDirection_m09F7279A566D19CC5889EEDD7AD3487C4E842707(L_46, (0.0f), ((-L_47)), NULL);
		__this->___direction_20 = L_48;
		goto IL_0339;
	}

IL_01dd:
	{
		// else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		int32_t L_49;
		L_49 = Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D(NULL);
		if ((((int32_t)L_49) == ((int32_t)((int32_t)11))))
		{
			goto IL_01f1;
		}
	}
	{
		int32_t L_50;
		L_50 = Application_get_platform_m1AB34E71D9885B120F6021EB2B11DCB28CD6008D(NULL);
		if ((!(((uint32_t)L_50) == ((uint32_t)8))))
		{
			goto IL_0339;
		}
	}

IL_01f1:
	{
		// if (UnityEngine.Input.touchCount == 2)
		int32_t L_51;
		L_51 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((!(((uint32_t)L_51) == ((uint32_t)2))))
		{
			goto IL_02cc;
		}
	}
	{
		// Touch touchZero = UnityEngine.Input.GetTouch(0);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_52;
		L_52 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_2 = L_52;
		// Touch touchOne = UnityEngine.Input.GetTouch(1);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_53;
		L_53 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(1, NULL);
		V_3 = L_53;
		// Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_54;
		L_54 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_2), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_55;
		L_55 = Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299((&V_2), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_56;
		L_56 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_54, L_55, NULL);
		// Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_57;
		L_57 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_3), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_58;
		L_58 = Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299((&V_3), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_59;
		L_59 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_57, L_58, NULL);
		V_4 = L_59;
		// float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_60 = V_4;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_61;
		L_61 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_56, L_60, NULL);
		V_7 = L_61;
		float L_62;
		L_62 = Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline((&V_7), NULL);
		// float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_63;
		L_63 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_2), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_64;
		L_64 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_3), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_65;
		L_65 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_63, L_64, NULL);
		V_7 = L_65;
		float L_66;
		L_66 = Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline((&V_7), NULL);
		V_5 = L_66;
		// float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
		float L_67 = V_5;
		V_6 = ((float)il2cpp_codegen_subtract(L_62, L_67));
		// if (_camera.orthographic)
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_68 = ____camera0;
		NullCheck(L_68);
		bool L_69;
		L_69 = Camera_get_orthographic_m904DEFC76C54DA4E30C20A62A86D5D87B7D4DD8F(L_68, NULL);
		if (!L_69)
		{
			goto IL_029b;
		}
	}
	{
		// _camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_70 = ____camera0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_71 = L_70;
		NullCheck(L_71);
		float L_72;
		L_72 = Camera_get_orthographicSize_m7950C5627086253E02992A43ADFE59039DB473F8(L_71, NULL);
		float L_73 = V_6;
		float L_74 = __this->___orthoZoomSpeed_19;
		NullCheck(L_71);
		Camera_set_orthographicSize_m76DD021032ACB3DDBD052B75EC66DCE3A7295A5C(L_71, ((float)il2cpp_codegen_add(L_72, ((float)il2cpp_codegen_multiply(L_73, L_74)))), NULL);
		// _camera.orthographicSize = Mathf.Max(_camera.orthographicSize, 0.1f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_75 = ____camera0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_76 = ____camera0;
		NullCheck(L_76);
		float L_77;
		L_77 = Camera_get_orthographicSize_m7950C5627086253E02992A43ADFE59039DB473F8(L_76, NULL);
		float L_78;
		L_78 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_77, (0.100000001f), NULL);
		NullCheck(L_75);
		Camera_set_orthographicSize_m76DD021032ACB3DDBD052B75EC66DCE3A7295A5C(L_75, L_78, NULL);
		goto IL_02cc;
	}

IL_029b:
	{
		// _camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_79 = ____camera0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_80 = L_79;
		NullCheck(L_80);
		float L_81;
		L_81 = Camera_get_fieldOfView_m9A93F17BBF89F496AE231C21817AFD1C1E833FBB(L_80, NULL);
		float L_82 = V_6;
		float L_83 = __this->___perspectiveZoomSpeed_18;
		NullCheck(L_80);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_80, ((float)il2cpp_codegen_add(L_81, ((float)il2cpp_codegen_multiply(L_82, L_83)))), NULL);
		// _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 5f, 90.9f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_84 = ____camera0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_85 = ____camera0;
		NullCheck(L_85);
		float L_86;
		L_86 = Camera_get_fieldOfView_m9A93F17BBF89F496AE231C21817AFD1C1E833FBB(L_85, NULL);
		float L_87;
		L_87 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_86, (5.0f), (90.9000015f), NULL);
		NullCheck(L_84);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_84, L_87, NULL);
	}

IL_02cc:
	{
		// if (UnityEngine.Input.touchCount == 1)
		int32_t L_88;
		L_88 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((!(((uint32_t)L_88) == ((uint32_t)1))))
		{
			goto IL_0339;
		}
	}
	{
		// if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_89;
		L_89 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_8 = L_89;
		int32_t L_90;
		L_90 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_8), NULL);
		if ((!(((uint32_t)L_90) == ((uint32_t)1))))
		{
			goto IL_0339;
		}
	}
	{
		// direction = GetDirection(direction, UnityEngine.Input.GetTouch(0).deltaPosition.x * 60f / Screen.width, UnityEngine.Input.GetTouch(0).deltaPosition.y * 50f / Screen.height);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_91 = __this->___direction_20;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_92;
		L_92 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_8 = L_92;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_93;
		L_93 = Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299((&V_8), NULL);
		float L_94 = L_93.___x_0;
		int32_t L_95;
		L_95 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_96;
		L_96 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_8 = L_96;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_97;
		L_97 = Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299((&V_8), NULL);
		float L_98 = L_97.___y_1;
		int32_t L_99;
		L_99 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_100;
		L_100 = CameraMode_GetDirection_m09F7279A566D19CC5889EEDD7AD3487C4E842707(L_91, ((float)(((float)il2cpp_codegen_multiply(L_94, (60.0f)))/((float)L_95))), ((float)(((float)il2cpp_codegen_multiply(L_98, (50.0f)))/((float)L_99))), NULL);
		__this->___direction_20 = L_100;
	}

IL_0339:
	{
		// if (_camera.transform.position != center - direction * distance_use)
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_101 = ____camera0;
		NullCheck(L_101);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_102;
		L_102 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_101, NULL);
		NullCheck(L_102);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_103;
		L_103 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_102, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_104 = __this->___center_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105 = __this->___direction_20;
		float L_106 = __this->___distance_use_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_107;
		L_107 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_105, L_106, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_108;
		L_108 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_104, L_107, NULL);
		bool L_109;
		L_109 = Vector3_op_Inequality_m6A7FB1C9E9DE194708997BFA24C6E238D92D908E_inline(L_103, L_108, NULL);
		if (!L_109)
		{
			goto IL_03d7;
		}
	}
	{
		// Vector3 to = center - direction * distance_use;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_110 = __this->___center_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_111 = __this->___direction_20;
		float L_112 = __this->___distance_use_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_113;
		L_113 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_111, L_112, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_114;
		L_114 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_110, L_113, NULL);
		V_9 = L_114;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, Vector3.Distance(_camera.transform.position, to) * speed * Time.deltaTime);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_115 = ____camera0;
		NullCheck(L_115);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_116;
		L_116 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_115, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_117 = ____camera0;
		NullCheck(L_117);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_118;
		L_118 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_117, NULL);
		NullCheck(L_118);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_119;
		L_119 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_118, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120 = V_9;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_121 = ____camera0;
		NullCheck(L_121);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_122;
		L_122 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_121, NULL);
		NullCheck(L_122);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_123;
		L_123 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_122, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_124 = V_9;
		float L_125;
		L_125 = Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline(L_123, L_124, NULL);
		float L_126 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_127;
		L_127 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_128;
		L_128 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_119, L_120, ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_125, L_126)), L_127)), NULL);
		NullCheck(L_116);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_116, L_128, NULL);
		// _camera.transform.LookAt(center, Vector3.up);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_129 = ____camera0;
		NullCheck(L_129);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_130;
		L_130 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_129, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_131 = __this->___center_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_132;
		L_132 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		NullCheck(L_130);
		Transform_LookAt_mBD38EDB5E915C5DA6C5A79D191DEE2C826A9FC2C(L_130, L_131, L_132, NULL);
	}

IL_03d7:
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
// System.Void GodPlayerCertainY::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GodPlayerCertainY__ctor_mBA1B473EBCD33E019778AB17B00C089F3B714726 (GodPlayerCertainY_t2757B4570A2E99A42AFDDAFADFBC705E58F4F37A* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// public GodPlayerCertainY(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZDis = XZDis;
		float L_0 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_0;
		// this.YDis = YDis;
		float L_1 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_1;
		// }
		return;
	}
}
// System.Void GodPlayerCertainY::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GodPlayerCertainY_LocalUpdate_mF7E0406EE95244EC39CF3628A16511E2D77F2FB3 (GodPlayerCertainY_t2757B4570A2E99A42AFDDAFADFBC705E58F4F37A* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// if (targets == null || targets.Count == 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_0)
		{
			goto IL_0015;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_1, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0016;
		}
	}

IL_0015:
	{
		// return;
		return;
	}

IL_0016:
	{
		// center = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___center_14 = L_3;
		// foreach (Transform o in this.targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_4 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_4);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_5;
		L_5 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_4, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_5;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0067:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_005c_1;
			}

IL_002f_1:
			{
				// foreach (Transform o in this.targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
				L_6 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_6;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_8;
				L_8 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_7, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_8)
				{
					goto IL_005c_1;
				}
			}
			{
				// center += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = __this->___center_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = V_1;
				NullCheck(L_10);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11;
				L_11 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_10, NULL);
				NullCheck(L_11);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
				L_12 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_11, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
				L_13 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_9, L_12, NULL);
				__this->___center_14 = L_13;
			}

IL_005c_1:
			{
				// foreach (Transform o in this.targets)
				bool L_14;
				L_14 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_14)
				{
					goto IL_002f_1;
				}
			}
			{
				goto IL_0075;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0075:
	{
		// center /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = __this->___center_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_16 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_16);
		int32_t L_17;
		L_17 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_16, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_15, ((float)L_17), NULL);
		__this->___center_14 = L_18;
		// Xi = center - Vector3.forward * XZDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = __this->___center_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		float L_21 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_20, L_21, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_19, L_22, NULL);
		__this->___Xi_13 = L_23;
		// Xi.y = YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_24 = (&__this->___Xi_13);
		float L_25 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		L_24->___y_3 = L_25;
		// ToRotation = Quaternion.LookRotation(center - Xi);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26 = __this->___center_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = __this->___Xi_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_26, L_27, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_29;
		L_29 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_28, NULL);
		__this->___ToRotation_15 = L_29;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, Xi, Time.deltaTime / (0.1f + Time.deltaTime));//????????????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_30 = ____camera0;
		NullCheck(L_30);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_31;
		L_31 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_30, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_32 = ____camera0;
		NullCheck(L_32);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_33;
		L_33 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_32, NULL);
		NullCheck(L_33);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_33, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35 = __this->___Xi_13;
		float L_36;
		L_36 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_37;
		L_37 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38;
		L_38 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_34, L_35, ((float)(L_36/((float)il2cpp_codegen_add((0.100000001f), L_37)))), NULL);
		NullCheck(L_31);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_31, L_38, NULL);
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_39 = ____camera0;
		NullCheck(L_39);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_40;
		L_40 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_39, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_41 = ____camera0;
		NullCheck(L_41);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_42;
		L_42 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_41, NULL);
		NullCheck(L_42);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_43;
		L_43 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_42, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_44 = __this->___ToRotation_15;
		float L_45;
		L_45 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_46;
		L_46 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_47;
		L_47 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_43, L_44, ((float)(L_45/((float)il2cpp_codegen_add((2.0f), L_46)))), NULL);
		NullCheck(L_40);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_40, L_47, NULL);
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
// System.Void keepTargetLeftCamera::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void keepTargetLeftCamera_Enter_mDC358E2F872039C1473538AAADD0FD21404A0698 (keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// _camera.DOOrthoSize(4f,3f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* L_1;
		L_1 = ShortcutExtensions_DOOrthoSize_m12DBC3D9BB3AEE9AC4D59C422E2514D74FD27A66(L_0, (4.0f), (3.0f), NULL);
		// }
		return;
	}
}
// System.Void keepTargetLeftCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void keepTargetLeftCamera_LocalUpdate_mB53B2F59B8B2D7BD75F769D23D53059C968B520A (keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// if (this.targets == null || this.targets.Count == 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_0)
		{
			goto IL_0015;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_1, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0016;
		}
	}

IL_0015:
	{
		// return;
		return;
	}

IL_0016:
	{
		// center = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___center_13 = L_3;
		// foreach (Transform o in this.targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_4 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_4);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_5;
		L_5 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_4, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_5;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0067:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_005c_1;
			}

IL_002f_1:
			{
				// foreach (Transform o in this.targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
				L_6 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_6;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_8;
				L_8 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_7, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_8)
				{
					goto IL_005c_1;
				}
			}
			{
				// center += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = __this->___center_13;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = V_1;
				NullCheck(L_10);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11;
				L_11 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_10, NULL);
				NullCheck(L_11);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
				L_12 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_11, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
				L_13 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_9, L_12, NULL);
				__this->___center_13 = L_13;
			}

IL_005c_1:
			{
				// foreach (Transform o in this.targets)
				bool L_14;
				L_14 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_14)
				{
					goto IL_002f_1;
				}
			}
			{
				goto IL_0075;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0075:
	{
		// center /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = __this->___center_13;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_16 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_16);
		int32_t L_17;
		L_17 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_16, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_15, ((float)L_17), NULL);
		__this->___center_13 = L_18;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, center + new Vector3(-7f,1f,7f), 2* Time.deltaTime/(0.01f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_19 = ____camera0;
		NullCheck(L_19);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_20;
		L_20 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_19, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_21 = ____camera0;
		NullCheck(L_21);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_22;
		L_22 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_21, NULL);
		NullCheck(L_22);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_22, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = __this->___center_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		memset((&L_25), 0, sizeof(L_25));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_25), (-7.0f), (1.0f), (7.0f), /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26;
		L_26 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_24, L_25, NULL);
		float L_27;
		L_27 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_28;
		L_28 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_23, L_26, ((float)(((float)il2cpp_codegen_multiply((2.0f), L_27))/((float)il2cpp_codegen_add((0.00999999978f), L_28)))), NULL);
		NullCheck(L_20);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_20, L_29, NULL);
		// torotation = Quaternion.LookRotation(center - Vector3.right * 3 + Vector3.up * 1f - _camera.transform.position, Vector3.up);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = __this->___center_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31;
		L_31 = Vector3_get_right_m13B7C3EAA64DC921EC23346C56A5A597B5481FF5_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_32;
		L_32 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_31, (3.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_30, L_32, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
		L_35 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_34, (1.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_36;
		L_36 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_33, L_35, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_37 = ____camera0;
		NullCheck(L_37);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_38;
		L_38 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_37, NULL);
		NullCheck(L_38);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_38, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_36, L_39, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_41;
		L_41 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_42;
		L_42 = Quaternion_LookRotation_mE6859FEBE85BC0AE72A14159988151FF69BF4401(L_40, L_41, NULL);
		__this->___torotation_14 = L_42;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, torotation, 2* Time.deltaTime / (0.01f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_43 = ____camera0;
		NullCheck(L_43);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_44;
		L_44 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_43, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_45 = ____camera0;
		NullCheck(L_45);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_46;
		L_46 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_45, NULL);
		NullCheck(L_46);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_47;
		L_47 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_46, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_48 = __this->___torotation_14;
		float L_49;
		L_49 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_50;
		L_50 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_51;
		L_51 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_47, L_48, ((float)(((float)il2cpp_codegen_multiply((2.0f), L_49))/((float)il2cpp_codegen_add((0.00999999978f), L_50)))), NULL);
		NullCheck(L_44);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_44, L_51, NULL);
		// }
		return;
	}
}
// System.Void keepTargetLeftCamera::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void keepTargetLeftCamera__ctor_m05CDA1EFB71DFD240F297A24586B20B01DB78BBA (keepTargetLeftCamera_tFB335F8BE1E4EA9C5E837627541851AB3E6A3C95* __this, const RuntimeMethod* method) 
{
	{
		// Vector3 center = new Vector3(0, 0, 0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_0), (0.0f), (0.0f), (0.0f), /*hidden argument*/NULL);
		__this->___center_13 = L_0;
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
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
// System.Void LerpToCertainDistance::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LerpToCertainDistance__ctor_mAB95B31D424196399B9CC64124D6A8B0554663B7 (LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD* __this, float ___distance0, float ___speed1, const RuntimeMethod* method) 
{
	{
		// public LerpToCertainDistance(float distance,float speed)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.distancefromtarget = distance;
		float L_0 = ___distance0;
		__this->___distancefromtarget_13 = L_0;
		// this.speed = speed;
		float L_1 = ___speed1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6 = L_1;
		// }
		return;
	}
}
// System.Void LerpToCertainDistance::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LerpToCertainDistance_LocalUpdate_m3F66AD9DEAC78B865C8BA25DA7AF9AA10E414421 (LerpToCertainDistance_t88045C971A69EF9FE0E9E2D04F65F504BFE99FCD* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_m8EAA91B4CE37CBB6C720FD238E4505097B29FFDA_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* G_B5_0 = NULL;
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* G_B4_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B6_0;
	memset((&G_B6_0), 0, sizeof(G_B6_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* G_B6_1 = NULL;
	{
		// targetcenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___targetcenter_14 = L_0;
		// for (int i = 0; i < targets.Count;i++)
		V_0 = 0;
		goto IL_0035;
	}

IL_000f:
	{
		// targetcenter += targets[i].position;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = __this->___targetcenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4;
		L_4 = List_1_get_Item_m8EAA91B4CE37CBB6C720FD238E4505097B29FFDA(L_2, L_3, List_1_get_Item_m8EAA91B4CE37CBB6C720FD238E4505097B29FFDA_RuntimeMethod_var);
		NullCheck(L_4);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_4, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_1, L_5, NULL);
		__this->___targetcenter_14 = L_6;
		// for (int i = 0; i < targets.Count;i++)
		int32_t L_7 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, 1));
	}

IL_0035:
	{
		// for (int i = 0; i < targets.Count;i++)
		int32_t L_8 = V_0;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_9 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_9);
		int32_t L_10;
		L_10 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_9, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_8) < ((int32_t)L_10)))
		{
			goto IL_000f;
		}
	}
	{
		// targetcenter = targetcenter / targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = __this->___targetcenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_12 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_12);
		int32_t L_13;
		L_13 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_12, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_11, ((float)L_13), NULL);
		__this->___targetcenter_14 = L_14;
		// _camera.transform.position = Vector3.Distance(targetcenter, _camera.transform.position) > distancefromtarget
		//     ? Vector3.Lerp(_camera.transform.position, targetcenter, speed * Time.deltaTime)
		//     : Vector3.Lerp(_camera.transform.position, (_camera.transform.position - targetcenter) + _camera.transform.position, speed * Time.deltaTime);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_15 = ____camera0;
		NullCheck(L_15);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_16;
		L_16 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_15, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = __this->___targetcenter_14;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_18 = ____camera0;
		NullCheck(L_18);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19;
		L_19 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_18, NULL);
		NullCheck(L_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_19, NULL);
		float L_21;
		L_21 = Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline(L_17, L_20, NULL);
		float L_22 = __this->___distancefromtarget_13;
		G_B4_0 = L_16;
		if ((((float)L_21) > ((float)L_22)))
		{
			G_B5_0 = L_16;
			goto IL_00c8;
		}
	}
	{
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_23 = ____camera0;
		NullCheck(L_23);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_24;
		L_24 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_23, NULL);
		NullCheck(L_24);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_24, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_26 = ____camera0;
		NullCheck(L_26);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_27;
		L_27 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_26, NULL);
		NullCheck(L_27);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_27, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29 = __this->___targetcenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_28, L_29, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_31 = ____camera0;
		NullCheck(L_31);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_32;
		L_32 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_31, NULL);
		NullCheck(L_32);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_32, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_30, L_33, NULL);
		float L_35 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_36;
		L_36 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_37;
		L_37 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_25, L_34, ((float)il2cpp_codegen_multiply(L_35, L_36)), NULL);
		G_B6_0 = L_37;
		G_B6_1 = G_B4_0;
		goto IL_00ea;
	}

IL_00c8:
	{
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_38 = ____camera0;
		NullCheck(L_38);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_39;
		L_39 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_38, NULL);
		NullCheck(L_39);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_39, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_41 = __this->___targetcenter_14;
		float L_42 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_43;
		L_43 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44;
		L_44 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_40, L_41, ((float)il2cpp_codegen_multiply(L_42, L_43)), NULL);
		G_B6_0 = L_44;
		G_B6_1 = G_B5_0;
	}

IL_00ea:
	{
		NullCheck(G_B6_1);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(G_B6_1, G_B6_0, NULL);
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
// System.Single MCamera::get_TransitionSpeedPara()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float MCamera_get_TransitionSpeedPara_mFB4C4B859D16E59A9AB98B4D9A30365E9E1B97B6 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// get => _transitionSpeedPara;
		float L_0 = __this->____transitionSpeedPara_26;
		return L_0;
	}
}
// System.Void MCamera::set_TransitionSpeedPara(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_set_TransitionSpeedPara_mABF7D9976C8AE3EB09015BA3397A2C8B955514AA (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => _transitionSpeedPara = Mathf.Clamp(value, 0.2f, 5f);
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (0.200000003f), (5.0f), NULL);
		__this->____transitionSpeedPara_26 = L_1;
		return;
	}
}
// System.Void MCamera::.ctor(System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera__ctor_m0C7A5B62FD9724E9A21B55C82C19887BA48A8623 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___XZDis0, float ___YDis1, float ___fieldOfView2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// float autoChangeAngleLimit = 30f;
		__this->___autoChangeAngleLimit_23 = (30.0f);
		// float autoRotateSpeed = 100;
		__this->___autoRotateSpeed_24 = (100.0f);
		// float _transitionSpeedPara = 10f;
		__this->____transitionSpeedPara_26 = (10.0f);
		// readonly float _lookPointHeight = 2f;
		__this->____lookPointHeight_27 = (2.0f);
		// private float screenDifferForRotate = 150;
		__this->___screenDifferForRotate_30 = (150.0f);
		// public MCamera(float XZDis, float YDis, float fieldOfView)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// _minXZ = XZDis;
		float L_0 = ___XZDis0;
		__this->____minXZ_28 = L_0;
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// this.disToH = (float) ((decimal)this.YDis/ (decimal)this.XZDis);
		float L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		il2cpp_codegen_runtime_class_init_inline(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_4;
		L_4 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_3, NULL);
		float L_5 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_6;
		L_6 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_5, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_7;
		L_7 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_4, L_6, NULL);
		float L_8;
		L_8 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_7, NULL);
		__this->___disToH_31 = ((float)L_8);
		// this.fieldOfView = fieldOfView;
		float L_9 = ___fieldOfView2;
		__this->___fieldOfView_29 = L_9;
		// }
		return;
	}
}
// System.Single MCamera::get_XZDistance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
// System.Void MCamera::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_set_XZDistance_m86188E66D0BD6C6CF43DE81C0118F9B6C37B2DB7 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => XZDis = Mathf.Clamp(value, _minXZ , _minXZ + 10f);
		float L_0 = ___value0;
		float L_1 = __this->____minXZ_28;
		float L_2 = __this->____minXZ_28;
		float L_3;
		L_3 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, L_1, ((float)il2cpp_codegen_add(L_2, (10.0f))), NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_3;
		return;
	}
}
// System.Void MCamera::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_Enter_m72C8BA969426553E08DD0C4F56CE621E9AE6AA65 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MCamera_U3CEnterU3Eb__26_0_mE9E17032F8267383045DC9F016A5ADAFFE645D1F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MCamera_U3CEnterU3Eb__26_1_mD60F8A737B8E0DD31DCE54F573CB8595E3A1FB29_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// CanSetH = true;
		MCamera_set_CanSetH_m346CCA645BA5CD5AB107DBCB5AF9DC89E3292DE4(__this, (bool)1, NULL);
		// _camera.fieldOfView = this.fieldOfView;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		float L_1 = __this->___fieldOfView_29;
		NullCheck(L_0);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_0, L_1, NULL);
		// CameraManager._subCamera.fieldOfView = this.fieldOfView;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_2 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____subCamera_5;
		float L_3 = __this->___fieldOfView_29;
		NullCheck(L_2);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_2, L_3, NULL);
		// LocalUpdate(_camera);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_4 = ____camera0;
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(6 /* System.Void CameraMode::LocalUpdate(UnityEngine.Camera) */, __this, L_4);
		// xzOff = _camera.transform.position - lookPoint;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = ____camera0;
		NullCheck(L_5);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
		L_6 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_5, NULL);
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_6, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_7, L_8, NULL);
		__this->___xzOff_18 = L_9;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_10 = (&__this->___xzOff_18);
		L_10->___y_3 = (0.0f);
		// TransitionSpeedPara = 5f;
		MCamera_set_TransitionSpeedPara_mABF7D9976C8AE3EB09015BA3397A2C8B955514AA(__this, (5.0f), NULL);
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* L_11 = (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03*)il2cpp_codegen_object_new(DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		NullCheck(L_11);
		DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA(L_11, __this, (intptr_t)((void*)MCamera_U3CEnterU3Eb__26_0_mE9E17032F8267383045DC9F016A5ADAFFE645D1F_RuntimeMethod_var), NULL);
		DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* L_12 = (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200*)il2cpp_codegen_object_new(DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E(L_12, __this, (intptr_t)((void*)MCamera_U3CEnterU3Eb__26_1_mD60F8A737B8E0DD31DCE54F573CB8595E3A1FB29_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* L_13;
		L_13 = DOTween_To_mEF916279231A76EB7217D421308E489B2B19E85D(L_11, L_12, (0.00100000005f), (1.0f), NULL);
		// }
		return;
	}
}
// System.Boolean MCamera::get_CanSetH()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool MCamera_get_CanSetH_mBC65ADE59DB394E41A9CA17B9EE12EC94C2FC0A2 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// get => _canSetH;
		bool L_0 = __this->____canSetH_37;
		return L_0;
	}
}
// System.Void MCamera::set_CanSetH(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_set_CanSetH_m346CCA645BA5CD5AB107DBCB5AF9DC89E3292DE4 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, bool ___value0, const RuntimeMethod* method) 
{
	{
		// _canSetH = value;
		bool L_0 = ___value0;
		__this->____canSetH_37 = L_0;
		// if (!_canSetH)
		bool L_1 = __this->____canSetH_37;
		if (L_1)
		{
			goto IL_001a;
		}
	}
	{
		// h = 0;
		__this->___h_32 = (0.0f);
	}

IL_001a:
	{
		// }
		return;
	}
}
// System.Void MCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_LocalUpdate_m1B81A05974CEA7B02DBD10C9C7ABE9610A85E4DD (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* V_2 = NULL;
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_3;
	memset((&V_3), 0, sizeof(V_3));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_4 = NULL;
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_5 = NULL;
	int32_t G_B14_0 = 0;
	{
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ___camera0;
		(&V_0)->___camera_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___camera_0), (void*)L_0);
		(&V_0)->___U3CU3E4__this_1 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___U3CU3E4__this_1), (void*)__this);
		// if (meCenter != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_1, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_2)
		{
			goto IL_0034;
		}
	}
	{
		// mePos = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_3);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_3, NULL);
		__this->___mePos_38 = L_4;
		goto IL_00c1;
	}

IL_0034:
	{
		// if (myTeamTargets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_5 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___myTeamTargets_3;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_5, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_6) <= ((int32_t)0)))
		{
			goto IL_00c1;
		}
	}
	{
		// mePos = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___mePos_38 = L_7;
		// foreach (var o in myTeamTargets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_8 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___myTeamTargets_3;
		NullCheck(L_8);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_9;
		L_9 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_8, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_3 = L_9;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0096:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_3), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_008b_1;
			}

IL_005b_1:
			{
				// foreach (var o in myTeamTargets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10;
				L_10 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_3), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_4 = L_10;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11 = V_4;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_12;
				L_12 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_11, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_12)
				{
					goto IL_008b_1;
				}
			}
			{
				// mePos += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = __this->___mePos_38;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14 = V_4;
				NullCheck(L_14);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_15;
				L_15 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_14, NULL);
				NullCheck(L_15);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
				L_16 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_15, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
				L_17 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_13, L_16, NULL);
				__this->___mePos_38 = L_17;
			}

IL_008b_1:
			{
				// foreach (var o in myTeamTargets)
				bool L_18;
				L_18 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_3), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_18)
				{
					goto IL_005b_1;
				}
			}
			{
				goto IL_00a4;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00a4:
	{
		// mePos /= myTeamTargets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = __this->___mePos_38;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_20 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___myTeamTargets_3;
		NullCheck(L_20);
		int32_t L_21;
		L_21 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_20, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_19, ((float)L_21), NULL);
		__this->___mePos_38 = L_22;
	}

IL_00c1:
	{
		// _changeSpeed = Time.deltaTime / (TransitionSpeedPara + Time.deltaTime); //????????????????
		float L_23;
		L_23 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_24;
		L_24 = MCamera_get_TransitionSpeedPara_mFB4C4B859D16E59A9AB98B4D9A30365E9E1B97B6_inline(__this, NULL);
		float L_25;
		L_25 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->____changeSpeed_25 = ((float)(L_23/((float)il2cpp_codegen_add(L_24, L_25))));
		// bool hasTargets = targets != null && targets.Count > 0;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_26 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_26)
		{
			goto IL_00f1;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_27 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_27);
		int32_t L_28;
		L_28 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_27, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		G_B14_0 = ((((int32_t)L_28) > ((int32_t)0))? 1 : 0);
		goto IL_00f2;
	}

IL_00f1:
	{
		G_B14_0 = 0;
	}

IL_00f2:
	{
		V_1 = (bool)G_B14_0;
		// if (hasTargets)
		bool L_29 = V_1;
		if (!L_29)
		{
			goto IL_0175;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_30;
		// foreach (var o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_31 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_31);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_32;
		L_32 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_31, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_3 = L_32;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_014a:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_3), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_013f_1;
			}

IL_010f_1:
			{
				// foreach (var o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_33;
				L_33 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_3), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_5 = L_33;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_34 = V_5;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_35;
				L_35 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_34, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_35)
				{
					goto IL_013f_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_36 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_37 = V_5;
				NullCheck(L_37);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_38;
				L_38 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_37, NULL);
				NullCheck(L_38);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
				L_39 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_38, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
				L_40 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_36, L_39, NULL);
				__this->___enemiesCenter_14 = L_40;
			}

IL_013f_1:
			{
				// foreach (var o in targets)
				bool L_41;
				L_41 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_3), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_41)
				{
					goto IL_010f_1;
				}
			}
			{
				goto IL_0158;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0158:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_43 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_43);
		int32_t L_44;
		L_44 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_43, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_45;
		L_45 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_42, ((float)L_44), NULL);
		__this->___enemiesCenter_14 = L_45;
	}

IL_0175:
	{
		// enemyScreenPos = camera.WorldToScreenPoint(enemiesCenter);
		U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D L_46 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_47 = L_46.___camera_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48 = __this->___enemiesCenter_14;
		NullCheck(L_47);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_49;
		L_49 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_47, L_48, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_50;
		L_50 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_49, NULL);
		__this->___enemyScreenPos_17 = L_50;
		// meScreenPos = camera.WorldToScreenPoint(mePos);
		U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D L_51 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_52 = L_51.___camera_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_53 = __this->___mePos_38;
		NullCheck(L_52);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_54;
		L_54 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_52, L_53, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_55;
		L_55 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_54, NULL);
		__this->___meScreenPos_16 = L_55;
		// if (CanSetH)
		bool L_56;
		L_56 = MCamera_get_CanSetH_mBC65ADE59DB394E41A9CA17B9EE12EC94C2FC0A2_inline(__this, NULL);
		if (!L_56)
		{
			goto IL_01c5;
		}
	}
	{
		// h = UltimateJoystick.GetHorizontalAxis("RotateCamera");
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_57;
		L_57 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_32 = L_57;
	}

IL_01c5:
	{
		// if (h != 0)
		float L_58 = __this->___h_32;
		if ((((float)L_58) == ((float)(0.0f))))
		{
			goto IL_0209;
		}
	}
	{
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_59 = __this->___h_32;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_60;
		L_60 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_61;
		L_61 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_59, (1.5f))), L_60, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_62 = __this->___xzOff_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_63;
		L_63 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_61, L_62, NULL);
		__this->___xzOff_18 = L_63;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_64 = (&__this->___xzOff_18);
		L_64->___y_3 = (0.0f);
	}

IL_0209:
	{
		// var wholeTargets = new List<Transform>() { };
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_65 = (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*)il2cpp_codegen_object_new(List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		NullCheck(L_65);
		List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268(L_65, List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		V_2 = L_65;
		// wholeTargets.AddRange(myTeamTargets);
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_66 = V_2;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_67 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___myTeamTargets_3;
		NullCheck(L_66);
		List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A(L_66, L_67, List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A_RuntimeMethod_var);
		// wholeTargets.AddRange(targets);
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_68 = V_2;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_69 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_68);
		List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A(L_68, L_69, List_1_AddRange_mE057CF4032DB4BC8DFEFD0F90228EEBBB8A0838A_RuntimeMethod_var);
		// AdjustXZDis(wholeTargets);
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_70 = V_2;
		MCamera_U3CLocalUpdateU3Eg__AdjustXZDisU7C39_0_m2097013F510348BD939DBCDE82665C916935C422(__this, L_70, (&V_0), NULL);
		// YDis = XZDistance * disToH;
		float L_71;
		L_71 = MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74_inline(__this, NULL);
		float L_72 = __this->___disToH_31;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_71, L_72));
		// if (enemyScreenPos.y >= meScreenPos.y)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_73 = (&__this->___enemyScreenPos_17);
		float L_74 = L_73->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_75 = (&__this->___meScreenPos_16);
		float L_76 = L_75->___y_1;
		if ((!(((float)L_74) >= ((float)L_76))))
		{
			goto IL_0275;
		}
	}
	{
		// frontWPos = mePos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_77 = __this->___mePos_38;
		__this->___frontWPos_20 = L_77;
		// backWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78 = __this->___enemiesCenter_14;
		__this->___backWPos_21 = L_78;
		goto IL_028d;
	}

IL_0275:
	{
		// frontWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_79 = __this->___enemiesCenter_14;
		__this->___frontWPos_20 = L_79;
		// backWPos = mePos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_80 = __this->___mePos_38;
		__this->___backWPos_21 = L_80;
	}

IL_028d:
	{
		// lookPoint = (backWPos - frontWPos) * 0.5f + frontWPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_81 = __this->___backWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_82 = __this->___frontWPos_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_83;
		L_83 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_81, L_82, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_84;
		L_84 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_83, (0.5f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85 = __this->___frontWPos_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_86;
		L_86 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_84, L_85, NULL);
		__this->___lookPoint_19 = L_86;
		// cameraTargetPos = lookPoint + xzOff.normalized * XZDistance;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_87 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_88 = (&__this->___xzOff_18);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_89;
		L_89 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_88, NULL);
		float L_90;
		L_90 = MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_91;
		L_91 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_89, L_90, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_92;
		L_92 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_87, L_91, NULL);
		__this->___cameraTargetPos_13 = L_92;
		// cameraTargetPos.y = YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_93 = (&__this->___cameraTargetPos_13);
		float L_94 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		L_93->___y_3 = L_94;
		// lookPoint.y = _lookPointHeight;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_95 = (&__this->___lookPoint_19);
		float L_96 = __this->____lookPointHeight_27;
		L_95->___y_3 = L_96;
		// if (hasTargets || h != 0)
		bool L_97 = V_1;
		if (L_97)
		{
			goto IL_0315;
		}
	}
	{
		float L_98 = __this->___h_32;
		if ((((float)L_98) == ((float)(0.0f))))
		{
			goto IL_03a4;
		}
	}

IL_0315:
	{
		// camera.transform.position = Vector3.Lerp(camera.transform.position, cameraTargetPos, _changeSpeed);
		U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D L_99 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_100 = L_99.___camera_0;
		NullCheck(L_100);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_101;
		L_101 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_100, NULL);
		U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D L_102 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_103 = L_102.___camera_0;
		NullCheck(L_103);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_104;
		L_104 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_103, NULL);
		NullCheck(L_104);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105;
		L_105 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_104, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_106 = __this->___cameraTargetPos_13;
		float L_107 = __this->____changeSpeed_25;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_108;
		L_108 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_105, L_106, L_107, NULL);
		NullCheck(L_101);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_101, L_108, NULL);
		// rotateToDirection = lookPoint - cameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_109 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_110 = __this->___cameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_111;
		L_111 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_109, L_110, NULL);
		__this->___rotateToDirection_15 = L_111;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_112 = (&__this->___rotateToDirection_15);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_113;
		L_113 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_112, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_114;
		L_114 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_113, NULL);
		__this->___ToRotation_22 = L_114;
		// camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, ToRotation, _changeSpeed);
		U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D L_115 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_116 = L_115.___camera_0;
		NullCheck(L_116);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_117;
		L_117 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_116, NULL);
		U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D L_118 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_119 = L_118.___camera_0;
		NullCheck(L_119);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_120;
		L_120 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_119, NULL);
		NullCheck(L_120);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_121;
		L_121 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_120, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_122 = __this->___ToRotation_22;
		float L_123 = __this->____changeSpeed_25;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_124;
		L_124 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_121, L_122, L_123, NULL);
		NullCheck(L_117);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_117, L_124, NULL);
	}

IL_03a4:
	{
		// }
		return;
	}
}
// System.Single MCamera::<Enter>b__26_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float MCamera_U3CEnterU3Eb__26_0_mE9E17032F8267383045DC9F016A5ADAFFE645D1F (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		float L_0;
		L_0 = MCamera_get_TransitionSpeedPara_mFB4C4B859D16E59A9AB98B4D9A30365E9E1B97B6_inline(__this, NULL);
		return L_0;
	}
}
// System.Void MCamera::<Enter>b__26_1(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_U3CEnterU3Eb__26_1_mD60F8A737B8E0DD31DCE54F573CB8595E3A1FB29 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, float ___x0, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		float L_0 = ___x0;
		MCamera_set_TransitionSpeedPara_mABF7D9976C8AE3EB09015BA3397A2C8B955514AA(__this, L_0, NULL);
		return;
	}
}
// System.Void MCamera::<LocalUpdate>g__AdjustXZDis|39_0(System.Collections.Generic.List`1<UnityEngine.Transform>,MCamera/<>c__DisplayClass39_0&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MCamera_U3CLocalUpdateU3Eg__AdjustXZDisU7C39_0_m2097013F510348BD939DBCDE82665C916935C422 (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* ___targets0, U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D* p1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_2;
	memset((&V_2), 0, sizeof(V_2));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_3 = NULL;
	float V_4 = 0.0f;
	float V_5 = 0.0f;
	float V_6 = 0.0f;
	float V_7 = 0.0f;
	bool G_B6_0 = false;
	bool G_B3_0 = false;
	bool G_B4_0 = false;
	bool G_B5_0 = false;
	int32_t G_B7_0 = 0;
	bool G_B7_1 = false;
	bool G_B11_0 = false;
	bool G_B8_0 = false;
	bool G_B9_0 = false;
	bool G_B10_0 = false;
	int32_t G_B12_0 = 0;
	bool G_B12_1 = false;
	{
		// bool shouldZoomOut = false;
		V_0 = (bool)0;
		// bool shouldZoomIn = true;
		V_1 = (bool)1;
		// foreach (var target in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = ___targets0;
		NullCheck(L_0);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_1;
		L_1 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_0, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_2 = L_1;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00df:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_2), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_00d1_1;
			}

IL_0010_1:
			{
				// foreach (var target in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
				L_2 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_2), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_3 = L_2;
				// var screenPos = camera.WorldToScreenPoint(target.position);
				U3CU3Ec__DisplayClass39_0_t733E49FF067F854117BF9DFED9C1AC164658D92D* L_3 = p1;
				Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_4 = L_3->___camera_0;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5 = V_3;
				NullCheck(L_5);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
				L_6 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_5, NULL);
				NullCheck(L_4);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
				L_7 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_4, L_6, NULL);
				// var ePosX = (float)((decimal)screenPos.x / Screen.width);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = L_7;
				float L_9 = L_8.___x_2;
				il2cpp_codegen_runtime_class_init_inline(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
				Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_10;
				L_10 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_9, NULL);
				int32_t L_11;
				L_11 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
				Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_12;
				L_12 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_11, NULL);
				Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_13;
				L_13 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_10, L_12, NULL);
				float L_14;
				L_14 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_13, NULL);
				V_4 = ((float)L_14);
				// var ePosY = (float)((decimal)screenPos.y / Screen.height);
				float L_15 = L_8.___y_3;
				Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_16;
				L_16 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_15, NULL);
				int32_t L_17;
				L_17 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
				Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_18;
				L_18 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_17, NULL);
				Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_19;
				L_19 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_16, L_18, NULL);
				float L_20;
				L_20 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_19, NULL);
				V_5 = ((float)L_20);
				// float edgeForIn = 0.3f;
				V_6 = (0.300000012f);
				// float edgeForOut = 0.15f;
				V_7 = (0.150000006f);
				// shouldZoomIn &= (ePosX >= edgeForIn && ePosX <= (1 - edgeForIn) && ePosY >= edgeForIn && ePosY <= (1 - edgeForIn));
				bool L_21 = V_1;
				float L_22 = V_4;
				float L_23 = V_6;
				G_B3_0 = L_21;
				if ((!(((float)L_22) >= ((float)L_23))))
				{
					G_B6_0 = L_21;
					goto IL_00a4_1;
				}
			}
			{
				float L_24 = V_4;
				float L_25 = V_6;
				G_B4_0 = G_B3_0;
				if ((!(((float)L_24) <= ((float)((float)il2cpp_codegen_subtract((1.0f), L_25))))))
				{
					G_B6_0 = G_B3_0;
					goto IL_00a4_1;
				}
			}
			{
				float L_26 = V_5;
				float L_27 = V_6;
				G_B5_0 = G_B4_0;
				if ((!(((float)L_26) >= ((float)L_27))))
				{
					G_B6_0 = G_B4_0;
					goto IL_00a4_1;
				}
			}
			{
				float L_28 = V_5;
				float L_29 = V_6;
				G_B7_0 = ((((int32_t)((!(((float)L_28) <= ((float)((float)il2cpp_codegen_subtract((1.0f), L_29)))))? 1 : 0)) == ((int32_t)0))? 1 : 0);
				G_B7_1 = G_B5_0;
				goto IL_00a5_1;
			}

IL_00a4_1:
			{
				G_B7_0 = 0;
				G_B7_1 = G_B6_0;
			}

IL_00a5_1:
			{
				V_1 = (bool)((int32_t)((int32_t)G_B7_1&G_B7_0));
				// shouldZoomOut |= (ePosX < edgeForOut || ePosX > (1 - edgeForOut) || ePosY < edgeForOut || ePosY > (1 - edgeForOut));
				bool L_30 = V_0;
				float L_31 = V_4;
				float L_32 = V_7;
				G_B8_0 = L_30;
				if ((((float)L_31) < ((float)L_32)))
				{
					G_B11_0 = L_30;
					goto IL_00ce_1;
				}
			}
			{
				float L_33 = V_4;
				float L_34 = V_7;
				G_B9_0 = G_B8_0;
				if ((((float)L_33) > ((float)((float)il2cpp_codegen_subtract((1.0f), L_34)))))
				{
					G_B11_0 = G_B8_0;
					goto IL_00ce_1;
				}
			}
			{
				float L_35 = V_5;
				float L_36 = V_7;
				G_B10_0 = G_B9_0;
				if ((((float)L_35) < ((float)L_36)))
				{
					G_B11_0 = G_B9_0;
					goto IL_00ce_1;
				}
			}
			{
				float L_37 = V_5;
				float L_38 = V_7;
				G_B12_0 = ((((float)L_37) > ((float)((float)il2cpp_codegen_subtract((1.0f), L_38))))? 1 : 0);
				G_B12_1 = G_B10_0;
				goto IL_00cf_1;
			}

IL_00ce_1:
			{
				G_B12_0 = 1;
				G_B12_1 = G_B11_0;
			}

IL_00cf_1:
			{
				V_0 = (bool)((int32_t)((int32_t)G_B12_1|G_B12_0));
			}

IL_00d1_1:
			{
				// foreach (var target in targets)
				bool L_39;
				L_39 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_2), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_39)
				{
					goto IL_0010_1;
				}
			}
			{
				goto IL_00ed;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00ed:
	{
		// if (shouldZoomIn)
		bool L_40 = V_1;
		if (!L_40)
		{
			goto IL_0104;
		}
	}
	{
		// XZDistance -= _changeSpeed;
		float L_41;
		L_41 = MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74_inline(__this, NULL);
		float L_42 = __this->____changeSpeed_25;
		MCamera_set_XZDistance_m86188E66D0BD6C6CF43DE81C0118F9B6C37B2DB7(__this, ((float)il2cpp_codegen_subtract(L_41, L_42)), NULL);
		return;
	}

IL_0104:
	{
		// else if (shouldZoomOut)
		bool L_43 = V_0;
		if (!L_43)
		{
			goto IL_011a;
		}
	}
	{
		// XZDistance += _changeSpeed;
		float L_44;
		L_44 = MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74_inline(__this, NULL);
		float L_45 = __this->____changeSpeed_25;
		MCamera_set_XZDistance_m86188E66D0BD6C6CF43DE81C0118F9B6C37B2DB7(__this, ((float)il2cpp_codegen_add(L_44, L_45)), NULL);
	}

IL_011a:
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
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void New2021::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2021__ctor_m91E1B9A79DD5E2C5E602CD2292980C1F112F5C6B (New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		__this->___xzOff_20 = L_0;
		// public New2021(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// }
		return;
	}
}
// System.Void New2021::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2021_LocalUpdate_mCD940AB74E2E9D708BBE5A540A7AFE0D013B975F (New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B19_0;
	memset((&G_B19_0), 0, sizeof(G_B19_0));
	New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5* G_B19_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B18_0;
	memset((&G_B18_0), 0, sizeof(G_B18_0));
	New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5* G_B18_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B20_0;
	memset((&G_B20_0), 0, sizeof(G_B20_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B20_1;
	memset((&G_B20_1), 0, sizeof(G_B20_1));
	New2021_t048082C8E81C5D1AD98D10704EF1DC9CBC4F55D5* G_B20_2 = NULL;
	{
		// h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_0;
		L_0 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_1;
		L_1 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_22 = ((float)il2cpp_codegen_add(L_0, L_1));
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_2 = __this->___h_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4;
		L_4 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_2, (1.5f))), L_3, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = __this->___xzOff_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_4, L_5, NULL);
		__this->___xzOff_20 = L_6;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_7 = (&__this->___xzOff_20);
		L_7->___y_3 = (0.0f);
		// if (auto)
		bool L_8 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_8)
		{
			goto IL_029a;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_9 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_9)
		{
			goto IL_029a;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_10 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_10, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_11) <= ((int32_t)0)))
		{
			goto IL_029a;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_12;
		// foreach (var o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_13 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_13);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_14;
		L_14 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_13, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_14;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ca:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_00bf_1;
			}

IL_0092_1:
			{
				// foreach (var o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_15;
				L_15 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_15;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_16 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_17;
				L_17 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_16, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_17)
				{
					goto IL_00bf_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19 = V_1;
				NullCheck(L_19);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_20;
				L_20 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_19, NULL);
				NullCheck(L_20);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
				L_21 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_20, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
				L_22 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_18, L_21, NULL);
				__this->___enemiesCenter_14 = L_22;
			}

IL_00bf_1:
			{
				// foreach (var o in targets)
				bool L_23;
				L_23 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_23)
				{
					goto IL_0092_1;
				}
			}
			{
				goto IL_00d8;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00d8:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_25 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_25);
		int32_t L_26;
		L_26 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_25, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_24, ((float)L_26), NULL);
		__this->___enemiesCenter_14 = L_27;
		// enemiesCenter.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_28 = (&__this->___enemiesCenter_14);
		L_28->___y_3 = (0.0f);
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_29 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = __this->___enemiesCenter_14;
		NullCheck(L_29);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31;
		L_31 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_29, L_30, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_32;
		L_32 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_31, NULL);
		__this->___enemyscreenpos_19 = L_32;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_33 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_34 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_34);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
		L_35 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_34, NULL);
		NullCheck(L_33);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_36;
		L_36 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_33, L_35, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_37;
		L_37 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_36, NULL);
		__this->___mescreenpos_18 = L_37;
		// if (enemyscreenpos.x < 0.08 || enemyscreenpos.x > 0.92 || enemyscreenpos.y < 0.1)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___enemyscreenpos_19);
		float L_39 = L_38->___x_0;
		if ((((double)((double)L_39)) < ((double)(0.080000000000000002))))
		{
			goto IL_017d;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_40 = (&__this->___enemyscreenpos_19);
		float L_41 = L_40->___x_0;
		if ((((double)((double)L_41)) > ((double)(0.92000000000000004))))
		{
			goto IL_017d;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_42 = (&__this->___enemyscreenpos_19);
		float L_43 = L_42->___y_1;
		if ((!(((double)((double)L_43)) < ((double)(0.10000000000000001)))))
		{
			goto IL_01b9;
		}
	}

IL_017d:
	{
		// xzOff = Vector3.RotateTowards(xzOff, meCenter.position - enemiesCenter, 4 * Time.deltaTime, 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44 = __this->___xzOff_20;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_45 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_45);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_45, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_46, L_47, NULL);
		float L_49;
		L_49 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		L_50 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_44, L_48, ((float)il2cpp_codegen_multiply((4.0f), L_49)), (0.0f), NULL);
		__this->___xzOff_20 = L_50;
		goto IL_029a;
	}

IL_01b9:
	{
		// if (Mathf.Abs(mescreenpos.x - enemyscreenpos.x) < (Mathf.Abs(mescreenpos.y - enemyscreenpos.y) + 0.2f) &&
		//     (enemyscreenpos.x > 0.35 && enemyscreenpos.x < 0.65))
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_51 = (&__this->___mescreenpos_18);
		float L_52 = L_51->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_53 = (&__this->___enemyscreenpos_19);
		float L_54 = L_53->___x_0;
		float L_55;
		L_55 = fabsf(((float)il2cpp_codegen_subtract(L_52, L_54)));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_56 = (&__this->___mescreenpos_18);
		float L_57 = L_56->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_58 = (&__this->___enemyscreenpos_19);
		float L_59 = L_58->___y_1;
		float L_60;
		L_60 = fabsf(((float)il2cpp_codegen_subtract(L_57, L_59)));
		if ((!(((float)L_55) < ((float)((float)il2cpp_codegen_add(L_60, (0.200000003f)))))))
		{
			goto IL_029a;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_61 = (&__this->___enemyscreenpos_19);
		float L_62 = L_61->___x_0;
		if ((!(((double)((double)L_62)) > ((double)(0.34999999999999998)))))
		{
			goto IL_029a;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_63 = (&__this->___enemyscreenpos_19);
		float L_64 = L_63->___x_0;
		if ((!(((double)((double)L_64)) < ((double)(0.65000000000000002)))))
		{
			goto IL_029a;
		}
	}
	{
		// xzOff = Vector3.RotateTowards(xzOff,
		//     mescreenpos.x > enemyscreenpos.x ?
		//     GetVerticalDir(meCenter.position - enemiesCenter):
		//     GetVerticalDir(enemiesCenter - meCenter.position)
		//     , Time.deltaTime, 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_65 = __this->___xzOff_20;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_66 = (&__this->___mescreenpos_18);
		float L_67 = L_66->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_68 = (&__this->___enemyscreenpos_19);
		float L_69 = L_68->___x_0;
		G_B18_0 = L_65;
		G_B18_1 = __this;
		if ((((float)L_67) > ((float)L_69)))
		{
			G_B19_0 = L_65;
			G_B19_1 = __this;
			goto IL_026a;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_70 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_71 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_71);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_72;
		L_72 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_71, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_73;
		L_73 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_70, L_72, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_74;
		L_74 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_73, NULL);
		G_B20_0 = L_74;
		G_B20_1 = G_B18_0;
		G_B20_2 = G_B18_1;
		goto IL_0286;
	}

IL_026a:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_75 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_75);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_76;
		L_76 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_75, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_77 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78;
		L_78 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_76, L_77, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_79;
		L_79 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_78, NULL);
		G_B20_0 = L_79;
		G_B20_1 = G_B19_0;
		G_B20_2 = G_B19_1;
	}

IL_0286:
	{
		float L_80;
		L_80 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_81;
		L_81 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(G_B20_1, G_B20_0, L_80, (0.0f), NULL);
		NullCheck(G_B20_2);
		G_B20_2->___xzOff_20 = L_81;
	}

IL_029a:
	{
		// CameraTargetPos = meCenter.position + xzOff.normalized * XZDis;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_82 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_82);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_83;
		L_83 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_82, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_84 = (&__this->___xzOff_20);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85;
		L_85 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_84, NULL);
		float L_86 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_87;
		L_87 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_85, L_86, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_88;
		L_88 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_83, L_87, NULL);
		__this->___CameraTargetPos_13 = L_88;
		// CameraTargetPos += Vector3.up * YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_89 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_90;
		L_90 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		float L_91 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_92;
		L_92 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_90, L_91, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_93;
		L_93 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_89, L_92, NULL);
		__this->___CameraTargetPos_13 = L_93;
		// fixy = Mathf.Clamp(CameraTargetPos.y, YDis, CameraTargetPos.y);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_94 = (&__this->___CameraTargetPos_13);
		float L_95 = L_94->___y_3;
		float L_96 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_97 = (&__this->___CameraTargetPos_13);
		float L_98 = L_97->___y_3;
		float L_99;
		L_99 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_95, L_96, L_98, NULL);
		__this->___fixy_21 = L_99;
		// CameraTargetPos.y = fixy;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_100 = (&__this->___CameraTargetPos_13);
		float L_101 = __this->___fixy_21;
		L_100->___y_3 = L_101;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//????????????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_102 = ____camera0;
		NullCheck(L_102);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_103;
		L_103 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_102, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_104 = ____camera0;
		NullCheck(L_104);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_105;
		L_105 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_104, NULL);
		NullCheck(L_105);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_106;
		L_106 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_105, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_107 = __this->___CameraTargetPos_13;
		float L_108;
		L_108 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_109;
		L_109 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_110;
		L_110 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_106, L_107, ((float)(L_108/((float)il2cpp_codegen_add((0.200000003f), L_109)))), NULL);
		NullCheck(L_103);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_103, L_110, NULL);
		// temp = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_111 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_111);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_112;
		L_112 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_111, NULL);
		__this->___temp_17 = L_112;
		// temp = new Vector3(temp.x, 2, temp.z);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_113 = (&__this->___temp_17);
		float L_114 = L_113->___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_115 = (&__this->___temp_17);
		float L_116 = L_115->___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_117;
		memset((&L_117), 0, sizeof(L_117));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_117), L_114, (2.0f), L_116, /*hidden argument*/NULL);
		__this->___temp_17 = L_117;
		// rotateToDirection = temp - CameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118 = __this->___temp_17;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_119 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120;
		L_120 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_118, L_119, NULL);
		__this->___rotateToDirection_16 = L_120;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_121 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_122;
		L_122 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_121, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_123;
		L_123 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_122, NULL);
		__this->___ToRotation_15 = L_123;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, (Time.deltaTime) / (0.2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_124 = ____camera0;
		NullCheck(L_124);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_125;
		L_125 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_124, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_126 = ____camera0;
		NullCheck(L_126);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_127;
		L_127 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_126, NULL);
		NullCheck(L_127);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_128;
		L_128 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_127, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_129 = __this->___ToRotation_15;
		float L_130;
		L_130 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_131;
		L_131 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_132;
		L_132 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_128, L_129, ((float)(L_130/((float)il2cpp_codegen_add((0.200000003f), L_131)))), NULL);
		NullCheck(L_125);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_125, L_132, NULL);
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
// System.Void New2022::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2022__ctor_m5142693A5D1DA5F6D9FF29973239C37AE926A22E (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = - Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline(L_0, NULL);
		__this->___xzOff_19 = L_1;
		// private float changeSpeed = 0.7f;
		__this->___changeSpeed_25 = (0.699999988f);
		// private float autoRotateDelay = 3f;
		__this->___autoRotateDelay_28 = (3.0f);
		// private float transitionSpeedPara = 10f;
		__this->___transitionSpeedPara_29 = (10.0f);
		// public New2022(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// minXZ = XZDis;
		float L_2 = ___XZDis0;
		__this->___minXZ_26 = L_2;
		// this.XZDis = XZDis;
		float L_3 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_3;
		// this.YDis = YDis;
		float L_4 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_4;
		// }
		return;
	}
}
// System.Single New2022::get_XZDistance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
// System.Void New2022::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2022_set_XZDistance_m5933B8E8F03EFBB9069AF87219780AE58A44F2DB (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// XZDis = Mathf.Clamp(value, minXZ , minXZ + 20f);
		float L_0 = ___value0;
		float L_1 = __this->___minXZ_26;
		float L_2 = __this->___minXZ_26;
		float L_3;
		L_3 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, L_1, ((float)il2cpp_codegen_add(L_2, (20.0f))), NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_3;
		// }
		return;
	}
}
// System.Void New2022::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2022_Enter_m222F2DD1F84CC4F1CBCC116E2513C5C7C9FDDB17 (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&New2022_U3CEnterU3Eb__21_0_m0C421AE011466B048D269224F7612BF40625DE24_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&New2022_U3CEnterU3Eb__21_1_mCF650825B73C01987D7E65A95573F15A7FB91FE4_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// LocalUpdate(_camera);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(6 /* System.Void CameraMode::LocalUpdate(UnityEngine.Camera) */, __this, L_0);
		// xzOff = _camera.transform.position - frontWPos;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_1 = ____camera0;
		NullCheck(L_1);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
		L_2 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_1, NULL);
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_2, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_3, L_4, NULL);
		__this->___xzOff_19 = L_5;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_6 = (&__this->___xzOff_19);
		L_6->___y_3 = (0.0f);
		// transitionSpeedPara = 10f;
		__this->___transitionSpeedPara_29 = (10.0f);
		// DOTween.To(()=> transitionSpeedPara, (x) => transitionSpeedPara = x, 0.2f, 1f);
		DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* L_7 = (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03*)il2cpp_codegen_object_new(DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		NullCheck(L_7);
		DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA(L_7, __this, (intptr_t)((void*)New2022_U3CEnterU3Eb__21_0_m0C421AE011466B048D269224F7612BF40625DE24_RuntimeMethod_var), NULL);
		DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* L_8 = (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200*)il2cpp_codegen_object_new(DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		NullCheck(L_8);
		DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E(L_8, __this, (intptr_t)((void*)New2022_U3CEnterU3Eb__21_1_mCF650825B73C01987D7E65A95573F15A7FB91FE4_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* L_9;
		L_9 = DOTween_To_mEF916279231A76EB7217D421308E489B2B19E85D(L_7, L_8, (0.200000003f), (1.0f), NULL);
		// }
		return;
	}
}
// System.Void New2022::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2022_LocalUpdate_mD8FC7ABA8B7619A8CA6EF38BCDD5EE5EE7045F6A (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B24_0;
	memset((&G_B24_0), 0, sizeof(G_B24_0));
	New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* G_B24_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B23_0;
	memset((&G_B23_0), 0, sizeof(G_B23_0));
	New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* G_B23_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B25_0;
	memset((&G_B25_0), 0, sizeof(G_B25_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B25_1;
	memset((&G_B25_1), 0, sizeof(G_B25_1));
	New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* G_B25_2 = NULL;
	{
		// time_counter += Time.deltaTime;
		float L_0 = __this->___time_counter_27;
		float L_1;
		L_1 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___time_counter_27 = ((float)il2cpp_codegen_add(L_0, L_1));
		// h = UltimateJoystick.GetHorizontalAxis("RotateCamera");
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_2;
		L_2 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_31 = L_2;
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_3 = __this->___h_31;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_5;
		L_5 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_3, (1.5f))), L_4, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_5, L_6, NULL);
		__this->___xzOff_19 = L_7;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_8 = (&__this->___xzOff_19);
		L_8->___y_3 = (0.0f);
		// changeSpeed = Time.deltaTime / (transitionSpeedPara + Time.deltaTime);//????????????????
		float L_9;
		L_9 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_10 = __this->___transitionSpeedPara_29;
		float L_11;
		L_11 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___changeSpeed_25 = ((float)(L_9/((float)il2cpp_codegen_add(L_10, L_11))));
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_12 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_12)
		{
			goto IL_04ad;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_13 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_13);
		int32_t L_14;
		L_14 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_13, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_14) <= ((int32_t)0)))
		{
			goto IL_04ad;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
		L_15 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_15;
		// foreach (var o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_16 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_16);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_17;
		L_17 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_16, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_17;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00de:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_00d3_1;
			}

IL_00a6_1:
			{
				// foreach (var o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_18;
				L_18 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_18;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_20;
				L_20 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_19, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_20)
				{
					goto IL_00d3_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_22 = V_1;
				NullCheck(L_22);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_23;
				L_23 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_22, NULL);
				NullCheck(L_23);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
				L_24 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_23, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
				L_25 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_21, L_24, NULL);
				__this->___enemiesCenter_14 = L_25;
			}

IL_00d3_1:
			{
				// foreach (var o in targets)
				bool L_26;
				L_26 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_26)
				{
					goto IL_00a6_1;
				}
			}
			{
				goto IL_00ec;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00ec:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_28 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_28);
		int32_t L_29;
		L_29 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_28, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_27, ((float)L_29), NULL);
		__this->___enemiesCenter_14 = L_30;
		// enemiesCenter.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_31 = (&__this->___enemiesCenter_14);
		L_31->___y_3 = (0.0f);
		// enemyScreenPos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_32 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33 = __this->___enemiesCenter_14;
		NullCheck(L_32);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_32, L_33, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_35;
		L_35 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_34, NULL);
		__this->___enemyScreenPos_18 = L_35;
		// meScreenPos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_36 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_37 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_37);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38;
		L_38 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_37, NULL);
		NullCheck(L_36);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_36, L_38, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_40;
		L_40 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_39, NULL);
		__this->___meScreenPos_17 = L_40;
		// if (enemyScreenPos.y >= meScreenPos.y)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_41 = (&__this->___enemyScreenPos_18);
		float L_42 = L_41->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_43 = (&__this->___meScreenPos_17);
		float L_44 = L_43->___y_1;
		if ((!(((float)L_42) >= ((float)L_44))))
		{
			goto IL_019b;
		}
	}
	{
		// frontWPos = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_45 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_45);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_45, NULL);
		__this->___frontWPos_21 = L_46;
		// frontScreenPos = meScreenPos;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_47 = __this->___meScreenPos_17;
		__this->___frontScreenPos_23 = L_47;
		// backWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48 = __this->___enemiesCenter_14;
		__this->___backWPos_22 = L_48;
		// backScreenPos = enemyScreenPos;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_49 = __this->___enemyScreenPos_18;
		__this->___backScreenPos_24 = L_49;
		goto IL_01d0;
	}

IL_019b:
	{
		// frontWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50 = __this->___enemiesCenter_14;
		__this->___frontWPos_21 = L_50;
		// frontScreenPos = enemyScreenPos;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_51 = __this->___enemyScreenPos_18;
		__this->___frontScreenPos_23 = L_51;
		// backWPos = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_52 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_52);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_53;
		L_53 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_52, NULL);
		__this->___backWPos_22 = L_53;
		// backScreenPos = meScreenPos;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_54 = __this->___meScreenPos_17;
		__this->___backScreenPos_24 = L_54;
	}

IL_01d0:
	{
		// if (frontScreenPos.x <= 0.2 || frontScreenPos.x >= 0.8 ||
		//     backScreenPos.x <= 0.2 || backScreenPos.x >= 0.8 )
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_55 = (&__this->___frontScreenPos_23);
		float L_56 = L_55->___x_0;
		if ((((double)((double)L_56)) <= ((double)(0.20000000000000001))))
		{
			goto IL_022c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_57 = (&__this->___frontScreenPos_23);
		float L_58 = L_57->___x_0;
		if ((((double)((double)L_58)) >= ((double)(0.80000000000000004))))
		{
			goto IL_022c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_59 = (&__this->___backScreenPos_24);
		float L_60 = L_59->___x_0;
		if ((((double)((double)L_60)) <= ((double)(0.20000000000000001))))
		{
			goto IL_022c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_61 = (&__this->___backScreenPos_24);
		float L_62 = L_61->___x_0;
		if ((!(((double)((double)L_62)) >= ((double)(0.80000000000000004)))))
		{
			goto IL_0241;
		}
	}

IL_022c:
	{
		// XZDistance += changeSpeed;
		float L_63;
		L_63 = New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E_inline(__this, NULL);
		float L_64 = __this->___changeSpeed_25;
		New2022_set_XZDistance_m5933B8E8F03EFBB9069AF87219780AE58A44F2DB(__this, ((float)il2cpp_codegen_add(L_63, L_64)), NULL);
		goto IL_0254;
	}

IL_0241:
	{
		// XZDistance -= changeSpeed;
		float L_65;
		L_65 = New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E_inline(__this, NULL);
		float L_66 = __this->___changeSpeed_25;
		New2022_set_XZDistance_m5933B8E8F03EFBB9069AF87219780AE58A44F2DB(__this, ((float)il2cpp_codegen_subtract(L_65, L_66)), NULL);
	}

IL_0254:
	{
		// rate = Vector2.Angle((backScreenPos - frontScreenPos), Vector2.right) / 180;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_67 = __this->___backScreenPos_24;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_68 = __this->___frontScreenPos_23;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_69;
		L_69 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_67, L_68, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_70;
		L_70 = Vector2_get_right_mCE2D0142663361ED4B48C36873786986D25A6E0A_inline(NULL);
		float L_71;
		L_71 = Vector2_Angle_m9668B13074D1664DD192669C14B3A8FC01676299_inline(L_69, L_70, NULL);
		__this->___rate_32 = ((float)(L_71/(180.0f)));
		// c_offSet = (float)(2 * (Mathf.Pow(rate,2) - rate) + 0.5);
		float L_72 = __this->___rate_32;
		float L_73;
		L_73 = powf(L_72, (2.0f));
		float L_74 = __this->___rate_32;
		__this->___c_offSet_33 = ((float)((double)il2cpp_codegen_add(((double)((float)il2cpp_codegen_multiply((2.0f), ((float)il2cpp_codegen_subtract(L_73, L_74))))), (0.5))));
		// if (auto && time_counter > autoRotateDelay && (rate >= 0.3 && rate <= 0.7))//??????????????????
		bool L_75 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_75)
		{
			goto IL_034d;
		}
	}
	{
		float L_76 = __this->___time_counter_27;
		float L_77 = __this->___autoRotateDelay_28;
		if ((!(((float)L_76) > ((float)L_77))))
		{
			goto IL_034d;
		}
	}
	{
		float L_78 = __this->___rate_32;
		if ((!(((double)((double)L_78)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034d;
		}
	}
	{
		float L_79 = __this->___rate_32;
		if ((!(((double)((double)L_79)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034d;
		}
	}
	{
		// xzOff = Vector3.RotateTowards(xzOff,
		//     frontWPos.x > backWPos.x ?
		//         GetVerticalDir(frontWPos - backWPos) : GetVerticalDir(backWPos - frontWPos), Time.deltaTime, 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_80 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_81 = (&__this->___frontWPos_21);
		float L_82 = L_81->___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_83 = (&__this->___backWPos_22);
		float L_84 = L_83->___x_2;
		G_B23_0 = L_80;
		G_B23_1 = __this;
		if ((((float)L_82) > ((float)L_84)))
		{
			G_B24_0 = L_80;
			G_B24_1 = __this;
			goto IL_0322;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85 = __this->___backWPos_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_86 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_87;
		L_87 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_85, L_86, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_88;
		L_88 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_87, NULL);
		G_B25_0 = L_88;
		G_B25_1 = G_B23_0;
		G_B25_2 = G_B23_1;
		goto IL_0339;
	}

IL_0322:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_89 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_90 = __this->___backWPos_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_91;
		L_91 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_89, L_90, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_92;
		L_92 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_91, NULL);
		G_B25_0 = L_92;
		G_B25_1 = G_B24_0;
		G_B25_2 = G_B24_1;
	}

IL_0339:
	{
		float L_93;
		L_93 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_94;
		L_94 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(G_B25_1, G_B25_0, L_93, (0.0f), NULL);
		NullCheck(G_B25_2);
		G_B25_2->___xzOff_19 = L_94;
	}

IL_034d:
	{
		// cameraTargetPos = (backWPos - frontWPos) * c_offSet + frontWPos + xzOff.normalized * XZDistance;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_95 = __this->___backWPos_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_96 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_97;
		L_97 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_95, L_96, NULL);
		float L_98 = __this->___c_offSet_33;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_99;
		L_99 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_97, L_98, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_100 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_101;
		L_101 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_99, L_100, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_102 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_103;
		L_103 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_102, NULL);
		float L_104;
		L_104 = New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105;
		L_105 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_103, L_104, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_106;
		L_106 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_101, L_105, NULL);
		__this->___cameraTargetPos_13 = L_106;
		// cameraTargetPos += Vector3.up * YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_107 = __this->___cameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_108;
		L_108 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		float L_109 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_110;
		L_110 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_108, L_109, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_111;
		L_111 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_107, L_110, NULL);
		__this->___cameraTargetPos_13 = L_111;
		// fixY = Mathf.Clamp(cameraTargetPos.y, YDis, cameraTargetPos.y);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_112 = (&__this->___cameraTargetPos_13);
		float L_113 = L_112->___y_3;
		float L_114 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_115 = (&__this->___cameraTargetPos_13);
		float L_116 = L_115->___y_3;
		float L_117;
		L_117 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_113, L_114, L_116, NULL);
		__this->___fixY_30 = L_117;
		// cameraTargetPos.y = fixY;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_118 = (&__this->___cameraTargetPos_13);
		float L_119 = __this->___fixY_30;
		L_118->___y_3 = L_119;
		// lookPoint = (backWPos - frontWPos) * (1 - c_offSet) + frontWPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120 = __this->___backWPos_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_121 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_122;
		L_122 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_120, L_121, NULL);
		float L_123 = __this->___c_offSet_33;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_124;
		L_124 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_122, ((float)il2cpp_codegen_subtract((1.0f), L_123)), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_125 = __this->___frontWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_126;
		L_126 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_124, L_125, NULL);
		__this->___lookPoint_20 = L_126;
		// lookPoint.y = 2.3f;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_127 = (&__this->___lookPoint_20);
		L_127->___y_3 = (2.29999995f);
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraTargetPos, changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_128 = ____camera0;
		NullCheck(L_128);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_129;
		L_129 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_128, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_130 = ____camera0;
		NullCheck(L_130);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_131;
		L_131 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_130, NULL);
		NullCheck(L_131);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_132;
		L_132 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_131, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_133 = __this->___cameraTargetPos_13;
		float L_134 = __this->___changeSpeed_25;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_135;
		L_135 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_132, L_133, L_134, NULL);
		NullCheck(L_129);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_129, L_135, NULL);
		// rotateToDirection = lookPoint - cameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_136 = __this->___lookPoint_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_137 = __this->___cameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_138;
		L_138 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_136, L_137, NULL);
		__this->___rotateToDirection_16 = L_138;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_139 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_140;
		L_140 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_139, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_141;
		L_141 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_140, NULL);
		__this->___ToRotation_15 = L_141;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_142 = ____camera0;
		NullCheck(L_142);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_143;
		L_143 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_142, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_144 = ____camera0;
		NullCheck(L_144);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_145;
		L_145 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_144, NULL);
		NullCheck(L_145);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_146;
		L_146 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_145, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_147 = __this->___ToRotation_15;
		float L_148 = __this->___changeSpeed_25;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_149;
		L_149 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_146, L_147, L_148, NULL);
		NullCheck(L_143);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_143, L_149, NULL);
		return;
	}

IL_04ad:
	{
		// cameraTargetPos = meCenter.position + xzOff.normalized * XZDistance;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_150 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_150);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_151;
		L_151 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_150, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_152 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_153;
		L_153 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_152, NULL);
		float L_154;
		L_154 = New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_155;
		L_155 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_153, L_154, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_156;
		L_156 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_151, L_155, NULL);
		__this->___cameraTargetPos_13 = L_156;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraTargetPos, changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_157 = ____camera0;
		NullCheck(L_157);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_158;
		L_158 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_157, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_159 = ____camera0;
		NullCheck(L_159);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_160;
		L_160 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_159, NULL);
		NullCheck(L_160);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_161;
		L_161 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_160, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_162 = __this->___cameraTargetPos_13;
		float L_163 = __this->___changeSpeed_25;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_164;
		L_164 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_161, L_162, L_163, NULL);
		NullCheck(L_158);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_158, L_164, NULL);
		// }
		return;
	}
}
// System.Single New2022::<Enter>b__21_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float New2022_U3CEnterU3Eb__21_0_m0C421AE011466B048D269224F7612BF40625DE24 (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> transitionSpeedPara, (x) => transitionSpeedPara = x, 0.2f, 1f);
		float L_0 = __this->___transitionSpeedPara_29;
		return L_0;
	}
}
// System.Void New2022::<Enter>b__21_1(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2022_U3CEnterU3Eb__21_1_mCF650825B73C01987D7E65A95573F15A7FB91FE4 (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, float ___x0, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> transitionSpeedPara, (x) => transitionSpeedPara = x, 0.2f, 1f);
		float L_0 = ___x0;
		__this->___transitionSpeedPara_29 = L_0;
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
// System.Single New2023::get_TransitionSpeedPara()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float New2023_get_TransitionSpeedPara_mD8F056A7B4BE13EAB7939DD695AC3C9FB023C20D (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) 
{
	{
		// get => _transitionSpeedPara;
		float L_0 = __this->____transitionSpeedPara_24;
		return L_0;
	}
}
// System.Void New2023::set_TransitionSpeedPara(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_set_TransitionSpeedPara_mCF7B4B51B2ECC018DAC036D41F57C01F8F995574 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => _transitionSpeedPara = Mathf.Clamp(value, 0.2f, 5f);
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (0.200000003f), (5.0f), NULL);
		__this->____transitionSpeedPara_24 = L_1;
		return;
	}
}
// System.Void New2023::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023__ctor_m1A3A1B18E2487893683BED1513A0B328793D69E6 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// float _transitionSpeedPara = 10f;
		__this->____transitionSpeedPara_24 = (10.0f);
		// readonly float _lookPointHeight = 2.3f;
		__this->____lookPointHeight_25 = (2.29999995f);
		// public New2023(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// _minXZ = XZDis;
		float L_0 = ___XZDis0;
		__this->____minXZ_26 = L_0;
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// }
		return;
	}
}
// System.Single New2023::get_XZDistance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float New2023_get_XZDistance_mA2235920C05176006556D76DFD5AA4CB4F8A524D (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
// System.Void New2023::set_XZDistance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_set_XZDistance_m1AF28070B513316C2813470EDBE3306CEA9ECF20 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => XZDis = Mathf.Clamp(value, _minXZ , _minXZ + 20f);
		float L_0 = ___value0;
		float L_1 = __this->____minXZ_26;
		float L_2 = __this->____minXZ_26;
		float L_3;
		L_3 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, L_1, ((float)il2cpp_codegen_add(L_2, (20.0f))), NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_3;
		return;
	}
}
// System.Void New2023::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_Enter_m9E6DDAAB19DF8418C9263CB6241D6D30AEA657D7 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&New2023_U3CEnterU3Eb__21_0_mE3340875199190BBAF23F0AC6F440F86703AC54E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&New2023_U3CEnterU3Eb__21_1_mE5C4A357F019129F1A6CFB77C1E342F78C026A43_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// LocalUpdate(_camera);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(6 /* System.Void CameraMode::LocalUpdate(UnityEngine.Camera) */, __this, L_0);
		// xzOff = _camera.transform.position - lookPoint;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_1 = ____camera0;
		NullCheck(L_1);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2;
		L_2 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_1, NULL);
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_2, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_3, L_4, NULL);
		__this->___xzOff_18 = L_5;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_6 = (&__this->___xzOff_18);
		L_6->___y_3 = (0.0f);
		// TransitionSpeedPara =5f;
		New2023_set_TransitionSpeedPara_mCF7B4B51B2ECC018DAC036D41F57C01F8F995574(__this, (5.0f), NULL);
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03* L_7 = (DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03*)il2cpp_codegen_object_new(DOGetter_1_tE8B39477E96408653D0242624F4D7E48ABFD1B03_il2cpp_TypeInfo_var);
		NullCheck(L_7);
		DOGetter_1__ctor_mD5E79861254E8BFB1618B3AB0B9755D18F553CFA(L_7, __this, (intptr_t)((void*)New2023_U3CEnterU3Eb__21_0_mE3340875199190BBAF23F0AC6F440F86703AC54E_RuntimeMethod_var), NULL);
		DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200* L_8 = (DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200*)il2cpp_codegen_object_new(DOSetter_1_t48D41DB8CE0BFC91A1844C4CC49A8A7222A69200_il2cpp_TypeInfo_var);
		NullCheck(L_8);
		DOSetter_1__ctor_mCCAB2BA262A8DC16B8C5A6FD561BADA9160E7D2E(L_8, __this, (intptr_t)((void*)New2023_U3CEnterU3Eb__21_1_mE5C4A357F019129F1A6CFB77C1E342F78C026A43_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* L_9;
		L_9 = DOTween_To_mEF916279231A76EB7217D421308E489B2B19E85D(L_7, L_8, (0.00100000005f), (1.0f), NULL);
		// }
		return;
	}
}
// System.Void New2023::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_LocalUpdate_m3800D4DFCFE98522D76A60C70EC769F221250193 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// h = UltimateJoystick.GetHorizontalAxis("RotateCamera");
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_0;
		L_0 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_27 = L_0;
		// if (h != 0)
		float L_1 = __this->___h_27;
		if ((((float)L_1) == ((float)(0.0f))))
		{
			goto IL_0054;
		}
	}
	{
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_2 = __this->___h_27;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4;
		L_4 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_2, (1.5f))), L_3, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = __this->___xzOff_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_4, L_5, NULL);
		__this->___xzOff_18 = L_6;
		// xzOff.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_7 = (&__this->___xzOff_18);
		L_7->___y_3 = (0.0f);
	}

IL_0054:
	{
		// _changeSpeed = Time.deltaTime / (TransitionSpeedPara + Time.deltaTime); //????????????????
		float L_8;
		L_8 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_9;
		L_9 = New2023_get_TransitionSpeedPara_mD8F056A7B4BE13EAB7939DD695AC3C9FB023C20D_inline(__this, NULL);
		float L_10;
		L_10 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->____changeSpeed_23 = ((float)(L_8/((float)il2cpp_codegen_add(L_9, L_10))));
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_11;
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_12 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_12)
		{
			goto IL_00e1;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_13 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_13);
		int32_t L_14;
		L_14 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_13, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_14) <= ((int32_t)0)))
		{
			goto IL_00e1;
		}
	}
	{
		// foreach (var o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_15 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_15);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_16;
		L_16 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_15, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_16;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00d3:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_00c8_1;
			}

IL_009b_1:
			{
				// foreach (var o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_17;
				L_17 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_17;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_18 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_19;
				L_19 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_18, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_19)
				{
					goto IL_00c8_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_21 = V_1;
				NullCheck(L_21);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_22;
				L_22 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_21, NULL);
				NullCheck(L_22);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
				L_23 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_22, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
				L_24 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_20, L_23, NULL);
				__this->___enemiesCenter_14 = L_24;
			}

IL_00c8_1:
			{
				// foreach (var o in targets)
				bool L_25;
				L_25 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_25)
				{
					goto IL_009b_1;
				}
			}
			{
				goto IL_00f2;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00e1:
	{
		// enemiesCenter = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_26 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_26);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_26, NULL);
		__this->___enemiesCenter_14 = L_27;
	}

IL_00f2:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_29 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_29);
		int32_t L_30;
		L_30 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_29, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31;
		L_31 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_28, ((float)L_30), NULL);
		__this->___enemiesCenter_14 = L_31;
		// enemyScreenPos = _camera.WorldToScreenPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_32 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33 = __this->___enemiesCenter_14;
		NullCheck(L_32);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_32, L_33, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_35;
		L_35 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_34, NULL);
		__this->___enemyScreenPos_17 = L_35;
		// meScreenPos = _camera.WorldToScreenPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_36 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_37 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_37);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38;
		L_38 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_37, NULL);
		NullCheck(L_36);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Camera_WorldToScreenPoint_m26B4C8945C3B5731F1CC5944CFD96BF17126BAA3(L_36, L_38, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_40;
		L_40 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_39, NULL);
		__this->___meScreenPos_16 = L_40;
		// ePosX = (float)((decimal)enemyScreenPos.x / Screen.width);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_41 = (&__this->___enemyScreenPos_17);
		float L_42 = L_41->___x_0;
		il2cpp_codegen_runtime_class_init_inline(Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F_il2cpp_TypeInfo_var);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_43;
		L_43 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_42, NULL);
		int32_t L_44;
		L_44 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_45;
		L_45 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_44, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_46;
		L_46 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_43, L_45, NULL);
		float L_47;
		L_47 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_46, NULL);
		__this->___ePosX_28 = ((float)L_47);
		// ePosY = (float)((decimal)enemyScreenPos.y / Screen.height);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_48 = (&__this->___enemyScreenPos_17);
		float L_49 = L_48->___y_1;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_50;
		L_50 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_49, NULL);
		int32_t L_51;
		L_51 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_52;
		L_52 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_51, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_53;
		L_53 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_50, L_52, NULL);
		float L_54;
		L_54 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_53, NULL);
		__this->___ePosY_29 = ((float)L_54);
		// mPosX = (float)((decimal)meScreenPos.x / Screen.width);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_55 = (&__this->___meScreenPos_16);
		float L_56 = L_55->___x_0;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_57;
		L_57 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_56, NULL);
		int32_t L_58;
		L_58 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_59;
		L_59 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_58, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_60;
		L_60 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_57, L_59, NULL);
		float L_61;
		L_61 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_60, NULL);
		__this->___mPosX_30 = ((float)L_61);
		// mPosY = (float)((decimal)meScreenPos.y / Screen.height);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_62 = (&__this->___meScreenPos_16);
		float L_63 = L_62->___y_1;
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_64;
		L_64 = Decimal_op_Explicit_mDF02276E12CC6D2D0285A8D0843ACA0743F42DEC(L_63, NULL);
		int32_t L_65;
		L_65 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_66;
		L_66 = Decimal_op_Implicit_mE5A73A41E53B29C29A49359A2B5D0615A867B7C7(L_65, NULL);
		Decimal_tDA6C877282B2D789CF97C0949661CC11D643969F L_67;
		L_67 = Decimal_op_Division_mC679B917681D7B7D7791E0017A6A51AA76C1C72A(L_64, L_66, NULL);
		float L_68;
		L_68 = Decimal_op_Explicit_m52A93EB0AC4766C64D68DB6947D9D2770EFE8A93(L_67, NULL);
		__this->___mPosY_31 = ((float)L_68);
		// enemyScreenPos = new Vector2((enemyScreenPos.x /Screen.width),(enemyScreenPos.y /Screen.height));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_69 = (&__this->___enemyScreenPos_17);
		float L_70 = L_69->___x_0;
		int32_t L_71;
		L_71 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_72 = (&__this->___enemyScreenPos_17);
		float L_73 = L_72->___y_1;
		int32_t L_74;
		L_74 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_75;
		memset((&L_75), 0, sizeof(L_75));
		Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&L_75), ((float)(L_70/((float)L_71))), ((float)(L_73/((float)L_74))), /*hidden argument*/NULL);
		__this->___enemyScreenPos_17 = L_75;
		// meScreenPos = new Vector2((meScreenPos.x /Screen.width), (meScreenPos.y /Screen.height));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_76 = (&__this->___meScreenPos_16);
		float L_77 = L_76->___x_0;
		int32_t L_78;
		L_78 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_79 = (&__this->___meScreenPos_16);
		float L_80 = L_79->___y_1;
		int32_t L_81;
		L_81 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_82;
		memset((&L_82), 0, sizeof(L_82));
		Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&L_82), ((float)(L_77/((float)L_78))), ((float)(L_80/((float)L_81))), /*hidden argument*/NULL);
		__this->___meScreenPos_16 = L_82;
		// if (enemyScreenPos.y >= meScreenPos.y)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_83 = (&__this->___enemyScreenPos_17);
		float L_84 = L_83->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_85 = (&__this->___meScreenPos_16);
		float L_86 = L_85->___y_1;
		if ((!(((float)L_84) >= ((float)L_86))))
		{
			goto IL_0283;
		}
	}
	{
		// frontWPos = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_87 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_87);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_88;
		L_88 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_87, NULL);
		__this->___frontWPos_20 = L_88;
		// backWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_89 = __this->___enemiesCenter_14;
		__this->___backWPos_21 = L_89;
		goto IL_02a0;
	}

IL_0283:
	{
		// frontWPos = enemiesCenter;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_90 = __this->___enemiesCenter_14;
		__this->___frontWPos_20 = L_90;
		// backWPos = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_91 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_91);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_92;
		L_92 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_91, NULL);
		__this->___backWPos_21 = L_92;
	}

IL_02a0:
	{
		// if (ePosX >= 0.3 && ePosX <= 0.7 &&
		//     mPosX >= 0.3 && mPosX <= 0.7 &&
		//     ePosY >= 0.3 && ePosY <= 0.7 &&
		//     mPosY >= 0.3 && mPosY <= 0.7)
		float L_93 = __this->___ePosX_28;
		if ((!(((double)((double)L_93)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_94 = __this->___ePosX_28;
		if ((!(((double)((double)L_94)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_95 = __this->___mPosX_30;
		if ((!(((double)((double)L_95)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_96 = __this->___mPosX_30;
		if ((!(((double)((double)L_96)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_97 = __this->___ePosY_29;
		if ((!(((double)((double)L_97)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_98 = __this->___ePosY_29;
		if ((!(((double)((double)L_98)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_99 = __this->___mPosY_31;
		if ((!(((double)((double)L_99)) >= ((double)(0.29999999999999999)))))
		{
			goto IL_034e;
		}
	}
	{
		float L_100 = __this->___mPosY_31;
		if ((!(((double)((double)L_100)) <= ((double)(0.69999999999999996)))))
		{
			goto IL_034e;
		}
	}
	{
		// XZDistance -= _changeSpeed;
		float L_101;
		L_101 = New2023_get_XZDistance_mA2235920C05176006556D76DFD5AA4CB4F8A524D_inline(__this, NULL);
		float L_102 = __this->____changeSpeed_23;
		New2023_set_XZDistance_m1AF28070B513316C2813470EDBE3306CEA9ECF20(__this, ((float)il2cpp_codegen_subtract(L_101, L_102)), NULL);
		goto IL_03f1;
	}

IL_034e:
	{
		// else if (ePosX <= 0.2 || ePosX >= 0.8 ||
		//          mPosX <= 0.2 || mPosX >= 0.8 ||
		//          ePosY <= 0.2 || ePosY >= 0.8 ||
		//          mPosY <= 0.2 || mPosY >= 0.8)
		float L_103 = __this->___ePosX_28;
		if ((((double)((double)L_103)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03de;
		}
	}
	{
		float L_104 = __this->___ePosX_28;
		if ((((double)((double)L_104)) >= ((double)(0.80000000000000004))))
		{
			goto IL_03de;
		}
	}
	{
		float L_105 = __this->___mPosX_30;
		if ((((double)((double)L_105)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03de;
		}
	}
	{
		float L_106 = __this->___mPosX_30;
		if ((((double)((double)L_106)) >= ((double)(0.80000000000000004))))
		{
			goto IL_03de;
		}
	}
	{
		float L_107 = __this->___ePosY_29;
		if ((((double)((double)L_107)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03de;
		}
	}
	{
		float L_108 = __this->___ePosY_29;
		if ((((double)((double)L_108)) >= ((double)(0.80000000000000004))))
		{
			goto IL_03de;
		}
	}
	{
		float L_109 = __this->___mPosY_31;
		if ((((double)((double)L_109)) <= ((double)(0.20000000000000001))))
		{
			goto IL_03de;
		}
	}
	{
		float L_110 = __this->___mPosY_31;
		if ((!(((double)((double)L_110)) >= ((double)(0.80000000000000004)))))
		{
			goto IL_03f1;
		}
	}

IL_03de:
	{
		// XZDistance += _changeSpeed;
		float L_111;
		L_111 = New2023_get_XZDistance_mA2235920C05176006556D76DFD5AA4CB4F8A524D_inline(__this, NULL);
		float L_112 = __this->____changeSpeed_23;
		New2023_set_XZDistance_m1AF28070B513316C2813470EDBE3306CEA9ECF20(__this, ((float)il2cpp_codegen_add(L_111, L_112)), NULL);
	}

IL_03f1:
	{
		// lookPoint = (backWPos - frontWPos) * 0.5f + frontWPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_113 = __this->___backWPos_21;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_114 = __this->___frontWPos_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_115;
		L_115 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_113, L_114, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_116;
		L_116 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_115, (0.5f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_117 = __this->___frontWPos_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118;
		L_118 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_116, L_117, NULL);
		__this->___lookPoint_19 = L_118;
		// cameraTargetPos = lookPoint + xzOff.normalized * XZDistance;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_119 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_120 = (&__this->___xzOff_18);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_121;
		L_121 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_120, NULL);
		float L_122;
		L_122 = New2023_get_XZDistance_mA2235920C05176006556D76DFD5AA4CB4F8A524D_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_123;
		L_123 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_121, L_122, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_124;
		L_124 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_119, L_123, NULL);
		__this->___cameraTargetPos_13 = L_124;
		// cameraTargetPos.y = YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_125 = (&__this->___cameraTargetPos_13);
		float L_126 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		L_125->___y_3 = L_126;
		// lookPoint.y = _lookPointHeight;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_127 = (&__this->___lookPoint_19);
		float L_128 = __this->____lookPointHeight_25;
		L_127->___y_3 = L_128;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraTargetPos, _changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_129 = ____camera0;
		NullCheck(L_129);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_130;
		L_130 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_129, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_131 = ____camera0;
		NullCheck(L_131);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_132;
		L_132 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_131, NULL);
		NullCheck(L_132);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_133;
		L_133 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_132, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_134 = __this->___cameraTargetPos_13;
		float L_135 = __this->____changeSpeed_23;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_136;
		L_136 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_133, L_134, L_135, NULL);
		NullCheck(L_130);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_130, L_136, NULL);
		// rotateToDirection = lookPoint - cameraTargetPos;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_137 = __this->___lookPoint_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_138 = __this->___cameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_139;
		L_139 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_137, L_138, NULL);
		__this->___rotateToDirection_15 = L_139;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_140 = (&__this->___rotateToDirection_15);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_141;
		L_141 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_140, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_142;
		L_142 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_141, NULL);
		__this->___ToRotation_22 = L_142;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, _changeSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_143 = ____camera0;
		NullCheck(L_143);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_144;
		L_144 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_143, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_145 = ____camera0;
		NullCheck(L_145);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_146;
		L_146 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_145, NULL);
		NullCheck(L_146);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_147;
		L_147 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_146, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_148 = __this->___ToRotation_22;
		float L_149 = __this->____changeSpeed_23;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_150;
		L_150 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_147, L_148, L_149, NULL);
		NullCheck(L_144);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_144, L_150, NULL);
		// }
		return;
	}
}
// System.Single New2023::<Enter>b__21_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float New2023_U3CEnterU3Eb__21_0_mE3340875199190BBAF23F0AC6F440F86703AC54E (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		float L_0;
		L_0 = New2023_get_TransitionSpeedPara_mD8F056A7B4BE13EAB7939DD695AC3C9FB023C20D_inline(__this, NULL);
		return L_0;
	}
}
// System.Void New2023::<Enter>b__21_1(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void New2023_U3CEnterU3Eb__21_1_mE5C4A357F019129F1A6CFB77C1E342F78C026A43 (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, float ___x0, const RuntimeMethod* method) 
{
	{
		// DOTween.To(()=> TransitionSpeedPara, (x) => TransitionSpeedPara = x, 0.001f, 1f);
		float L_0 = ___x0;
		New2023_set_TransitionSpeedPara_mCF7B4B51B2ECC018DAC036D41F57C01F8F995574(__this, L_0, NULL);
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
// System.Single OneVOneMode::get_ZoomAcc()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float OneVOneMode_get_ZoomAcc_m16B63CB81DADC371768C54722F124AA88B49A8C2 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) 
{
	{
		// get { return zoomAcc; }
		float L_0 = __this->___zoomAcc_23;
		return L_0;
	}
}
// System.Void OneVOneMode::set_ZoomAcc(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_set_ZoomAcc_m197C408CBFCFF375517137E2313466548467F32C (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// zoomAcc = Mathf.Clamp(value, -1f, 1f);
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (-1.0f), (1.0f), NULL);
		__this->___zoomAcc_23 = L_1;
		// }
		return;
	}
}
// System.Single OneVOneMode::get_XZ_distance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float OneVOneMode_get_XZ_distance_m85DE561AE9FA16B6C72CDA1FFE2F829EA7773A66 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) 
{
	{
		// get { return xzd; }
		float L_0 = __this->___xzd_27;
		return L_0;
	}
}
// System.Void OneVOneMode::set_XZ_distance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_set_XZ_distance_mBC8F29816E166A3E50FA64A563D67BBD9A3221F9 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// xzd = Mathf.Clamp(value, 8.5f, xzMax);
		float L_0 = ___value0;
		float L_1 = __this->___xzMax_21;
		float L_2;
		L_2 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (8.5f), L_1, NULL);
		__this->___xzd_27 = L_2;
		// YDis = this.xzd * heightOfXZRate;
		float L_3 = __this->___xzd_27;
		float L_4 = __this->___heightOfXZRate_26;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_3, L_4));
		// }
		return;
	}
}
// System.Void OneVOneMode::.ctor(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode__ctor_m41397A0452D42F69896C4B8EE7ED6F1D0CBED7AA (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, float ___XZDis0, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = -Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline(L_0, NULL);
		__this->___xzOff_19 = L_1;
		// float startAutoRotateRange = 9; // ?????????????????????????
		__this->___startAutoRotateRange_20 = (9.0f);
		// readonly float xzMax = 16f;// ???????xz??????
		__this->___xzMax_21 = (16.0f);
		// float lookdownDegree = 0.5f; //????????????????1
		__this->___lookdownDegree_22 = (0.5f);
		// float zoomChangeInter = 2f;// zoom in or out ????????????
		__this->___zoomChangeInter_25 = (2.0f);
		// float heightOfXZRate = 0.65f;//?????XZ_distance??????
		__this->___heightOfXZRate_26 = (0.649999976f);
		// bool justEnterdThisMode = true;
		__this->___justEnterdThisMode_28 = (bool)1;
		// public OneVOneMode(float XZDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZ_distance = XZDis;
		float L_2 = ___XZDis0;
		OneVOneMode_set_XZ_distance_mBC8F29816E166A3E50FA64A563D67BBD9A3221F9(__this, L_2, NULL);
		// YDis = this.XZ_distance * heightOfXZRate;
		float L_3;
		L_3 = OneVOneMode_get_XZ_distance_m85DE561AE9FA16B6C72CDA1FFE2F829EA7773A66_inline(__this, NULL);
		float L_4 = __this->___heightOfXZRate_26;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_3, L_4));
		// }
		return;
	}
}
// System.Void OneVOneMode::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_Enter_m9F211BE8DC5C0EBA17B84B7B9EDEB4268BF8B679 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* G_B15_0 = NULL;
	OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* G_B14_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B16_0;
	memset((&G_B16_0), 0, sizeof(G_B16_0));
	OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* G_B16_1 = NULL;
	{
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// if (justEnterdThisMode)
		bool L_2 = __this->___justEnterdThisMode_28;
		if (!L_2)
		{
			goto IL_01d7;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_3)
		{
			goto IL_00af;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_4 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_4);
		int32_t L_5;
		L_5 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_4, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_00af;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_6;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_7 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_7);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_8;
		L_8 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_7, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_8;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0084:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0079_1;
			}

IL_004c_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9;
				L_9 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_9;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_11;
				L_11 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_10, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_11)
				{
					goto IL_0079_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13 = V_1;
				NullCheck(L_13);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14;
				L_14 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_13, NULL);
				NullCheck(L_14);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
				L_15 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_14, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
				L_16 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_12, L_15, NULL);
				__this->___enemiesCenter_14 = L_16;
			}

IL_0079_1:
			{
				// foreach (Transform o in targets)
				bool L_17;
				L_17 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_17)
				{
					goto IL_004c_1;
				}
			}
			{
				goto IL_0092;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0092:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_19 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_19);
		int32_t L_20;
		L_20 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_19, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_18, ((float)L_20), NULL);
		__this->___enemiesCenter_14 = L_21;
	}

IL_00af:
	{
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_22 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = __this->___enemiesCenter_14;
		NullCheck(L_22);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_22, L_23, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_25;
		L_25 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_24, NULL);
		__this->___enemyscreenpos_18 = L_25;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_26 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_27 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_27);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_27, NULL);
		NullCheck(L_26);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_26, L_28, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_30;
		L_30 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_29, NULL);
		__this->___mescreenpos_17 = L_30;
		// temp = Mathf.Abs(mescreenpos.x - enemyscreenpos.x);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_31 = (&__this->___mescreenpos_17);
		float L_32 = L_31->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_33 = (&__this->___enemyscreenpos_18);
		float L_34 = L_33->___x_0;
		float L_35;
		L_35 = fabsf(((float)il2cpp_codegen_subtract(L_32, L_34)));
		__this->___temp_31 = L_35;
		// temp = Mathf.Sqrt(temp);
		float L_36 = __this->___temp_31;
		float L_37;
		L_37 = sqrtf(L_36);
		__this->___temp_31 = L_37;
		// rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___mescreenpos_17);
		float L_39 = L_38->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_40 = (&__this->___enemyscreenpos_18);
		float L_41 = L_40->___x_0;
		G_B14_0 = __this;
		if ((((float)L_39) > ((float)L_41)))
		{
			G_B15_0 = __this;
			goto IL_014c;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_43 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_43);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44;
		L_44 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_43, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_45;
		L_45 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_42, L_44, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_45, NULL);
		G_B16_0 = L_46;
		G_B16_1 = G_B14_0;
		goto IL_0168;
	}

IL_014c:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_47 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_47);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_47, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_49 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		L_50 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_48, L_49, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_51;
		L_51 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_50, NULL);
		G_B16_0 = L_51;
		G_B16_1 = G_B15_0;
	}

IL_0168:
	{
		NullCheck(G_B16_1);
		G_B16_1->___rotateToDirection_16 = G_B16_0;
		// rotateToDirection = rotateToDirection * (1 - temp);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52 = __this->___rotateToDirection_16;
		float L_53 = __this->___temp_31;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_54;
		L_54 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_52, ((float)il2cpp_codegen_subtract((1.0f), L_53)), NULL);
		__this->___rotateToDirection_16 = L_54;
		// rotateToDirection += (meCenter.position - enemiesCenter).normalized * temp;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_55 = __this->___rotateToDirection_16;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_56 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_56);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_57;
		L_57 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_56, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_58 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_59;
		L_59 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_57, L_58, NULL);
		V_2 = L_59;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_60;
		L_60 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_2), NULL);
		float L_61 = __this->___temp_31;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_62;
		L_62 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_60, L_61, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_63;
		L_63 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_55, L_62, NULL);
		__this->___rotateToDirection_16 = L_63;
		// xzOff = rotateToDirection;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_64 = __this->___rotateToDirection_16;
		__this->___xzOff_19 = L_64;
		// justEnterdThisMode = false;
		__this->___justEnterdThisMode_28 = (bool)0;
	}

IL_01d7:
	{
		// }
		return;
	}
}
// System.Void OneVOneMode::Exit(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_Exit_m9F5018119781A761F9FF18FA58C3C3D4374A8DA5 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// justEnterdThisMode = true;
		__this->___justEnterdThisMode_28 = (bool)1;
		// }
		return;
	}
}
// System.Void OneVOneMode::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_LocalUpdate_m5B8CD0B6FE50189E22D99C704C1F5C673FED5620 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* G_B27_0 = NULL;
	OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* G_B26_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B28_0;
	memset((&G_B28_0), 0, sizeof(G_B28_0));
	OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* G_B28_1 = NULL;
	{
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_2)
		{
			goto IL_019d;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_3, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_4) <= ((int32_t)0)))
		{
			goto IL_019d;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_5;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_6 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_6);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_7;
		L_7 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_6, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_7;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_007c:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0071_1;
			}

IL_0044_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8;
				L_8 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_8;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_10;
				L_10 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_9, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_10)
				{
					goto IL_0071_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_12 = V_1;
				NullCheck(L_12);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13;
				L_13 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_12, NULL);
				NullCheck(L_13);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
				L_14 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_13, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
				L_15 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_11, L_14, NULL);
				__this->___enemiesCenter_14 = L_15;
			}

IL_0071_1:
			{
				// foreach (Transform o in targets)
				bool L_16;
				L_16 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_16)
				{
					goto IL_0044_1;
				}
			}
			{
				goto IL_008a;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_008a:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_18 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_18);
		int32_t L_19;
		L_19 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_18, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_17, ((float)L_19), NULL);
		__this->___enemiesCenter_14 = L_20;
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_21 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = __this->___enemiesCenter_14;
		NullCheck(L_21);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_21, L_22, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_24;
		L_24 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_23, NULL);
		__this->___enemyscreenpos_18 = L_24;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_25 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_26 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_26);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_26, NULL);
		NullCheck(L_25);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_25, L_27, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_29;
		L_29 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_28, NULL);
		__this->___mescreenpos_17 = L_29;
		// zoomcounter += Time.deltaTime;
		float L_30 = __this->___zoomcounter_24;
		float L_31;
		L_31 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___zoomcounter_24 = ((float)il2cpp_codegen_add(L_30, L_31));
		// if (mescreenpos.y < 0.2f)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_32 = (&__this->___mescreenpos_17);
		float L_33 = L_32->___y_1;
		if ((!(((float)L_33) < ((float)(0.200000003f)))))
		{
			goto IL_0109;
		}
	}
	{
		// ZoomIn();
		OneVOneMode_U3CLocalUpdateU3Eg__ZoomInU7C31_1_mB67A1D66A9AB976A1AA5249150BA3B5C8F844E0A(__this, NULL);
		goto IL_018a;
	}

IL_0109:
	{
		// if (enemyscreenpos.x < 0.1 || enemyscreenpos.x > 0.9 || enemyscreenpos.y < 0.2 || mescreenpos.x < 0.1 || mescreenpos.x > 0.9)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_34 = (&__this->___enemyscreenpos_18);
		float L_35 = L_34->___x_0;
		if ((((double)((double)L_35)) < ((double)(0.10000000000000001))))
		{
			goto IL_017c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_36 = (&__this->___enemyscreenpos_18);
		float L_37 = L_36->___x_0;
		if ((((double)((double)L_37)) > ((double)(0.90000000000000002))))
		{
			goto IL_017c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___enemyscreenpos_18);
		float L_39 = L_38->___y_1;
		if ((((double)((double)L_39)) < ((double)(0.20000000000000001))))
		{
			goto IL_017c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_40 = (&__this->___mescreenpos_17);
		float L_41 = L_40->___x_0;
		if ((((double)((double)L_41)) < ((double)(0.10000000000000001))))
		{
			goto IL_017c;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_42 = (&__this->___mescreenpos_17);
		float L_43 = L_42->___x_0;
		if ((!(((double)((double)L_43)) > ((double)(0.90000000000000002)))))
		{
			goto IL_0184;
		}
	}

IL_017c:
	{
		// ZoomOut();
		OneVOneMode_U3CLocalUpdateU3Eg__ZoomOutU7C31_0_m9EA9BF9EDC0B1356BF74185472C0B18DC71D8621(__this, NULL);
		goto IL_018a;
	}

IL_0184:
	{
		// ZoomIn();
		OneVOneMode_U3CLocalUpdateU3Eg__ZoomInU7C31_1_mB67A1D66A9AB976A1AA5249150BA3B5C8F844E0A(__this, NULL);
	}

IL_018a:
	{
		// XZ_distance += ZoomAcc;
		float L_44;
		L_44 = OneVOneMode_get_XZ_distance_m85DE561AE9FA16B6C72CDA1FFE2F829EA7773A66_inline(__this, NULL);
		float L_45;
		L_45 = OneVOneMode_get_ZoomAcc_m16B63CB81DADC371768C54722F124AA88B49A8C2_inline(__this, NULL);
		OneVOneMode_set_XZ_distance_mBC8F29816E166A3E50FA64A563D67BBD9A3221F9(__this, ((float)il2cpp_codegen_add(L_44, L_45)), NULL);
	}

IL_019d:
	{
		// if (auto)
		bool L_46 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_46)
		{
			goto IL_029a;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_47 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_47)
		{
			goto IL_029a;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_48 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_48);
		int32_t L_49;
		L_49 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_48, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_49) <= ((int32_t)0)))
		{
			goto IL_029a;
		}
	}
	{
		// temp = Vector3.Distance(meCenter.position, enemiesCenter);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_50 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_50);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_51;
		L_51 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_50, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52 = __this->___enemiesCenter_14;
		float L_53;
		L_53 = Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline(L_51, L_52, NULL);
		__this->___temp_31 = L_53;
		// if (temp < startAutoRotateRange)
		float L_54 = __this->___temp_31;
		float L_55 = __this->___startAutoRotateRange_20;
		if ((!(((float)L_54) < ((float)L_55))))
		{
			goto IL_029a;
		}
	}
	{
		// rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_56 = (&__this->___mescreenpos_17);
		float L_57 = L_56->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_58 = (&__this->___enemyscreenpos_18);
		float L_59 = L_58->___x_0;
		G_B26_0 = __this;
		if ((((float)L_57) > ((float)L_59)))
		{
			G_B27_0 = __this;
			goto IL_0228;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_60 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_61 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_61);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_62;
		L_62 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_61, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_63;
		L_63 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_60, L_62, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_64;
		L_64 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_63, NULL);
		G_B28_0 = L_64;
		G_B28_1 = G_B26_0;
		goto IL_0244;
	}

IL_0228:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_65 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_65);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_66;
		L_66 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_65, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_67 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_68;
		L_68 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_66, L_67, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_69;
		L_69 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_68, NULL);
		G_B28_0 = L_69;
		G_B28_1 = G_B27_0;
	}

IL_0244:
	{
		NullCheck(G_B28_1);
		G_B28_1->___rotateToDirection_16 = G_B28_0;
		// speed = Vector3.Angle(xzOff, rotateToDirection) / 180;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_70 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_71 = __this->___rotateToDirection_16;
		float L_72;
		L_72 = Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline(L_70, L_71, NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6 = ((float)(L_72/(180.0f)));
		// xzOff = Vector3.RotateTowards(xzOff, rotateToDirection, speed * Time.deltaTime / (0.2f + Time.deltaTime), 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_73 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_74 = __this->___rotateToDirection_16;
		float L_75 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_76;
		L_76 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_77;
		L_77 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78;
		L_78 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_73, L_74, ((float)(((float)il2cpp_codegen_multiply(L_75, L_76))/((float)il2cpp_codegen_add((0.200000003f), L_77)))), (0.0f), NULL);
		__this->___xzOff_19 = L_78;
	}

IL_029a:
	{
		// h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_79;
		L_79 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_80;
		L_80 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_29 = ((float)il2cpp_codegen_add(L_79, L_80));
		// xzOff = Quaternion.AngleAxis(h * 2f, Vector3.up) * xzOff;
		float L_81 = __this->___h_29;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_82;
		L_82 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_83;
		L_83 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_81, (2.0f))), L_82, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_84 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85;
		L_85 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_83, L_84, NULL);
		__this->___xzOff_19 = L_85;
		// maxheight = Mathf.Max(meCenter.position.y, enemiesCenter.y);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_86 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_86);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_87;
		L_87 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_86, NULL);
		float L_88 = L_87.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_89 = (&__this->___enemiesCenter_14);
		float L_90 = L_89->___y_3;
		float L_91;
		L_91 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_88, L_90, NULL);
		__this->___maxheight_30 = L_91;
		// temp = Mathf.Max(maxheight, YDis);
		float L_92 = __this->___maxheight_30;
		float L_93 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		float L_94;
		L_94 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_92, L_93, NULL);
		__this->___temp_31 = L_94;
		// SlerpCenter = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_95 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_95);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_96;
		L_96 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_95, NULL);
		__this->___SlerpCenter_33 = L_96;
		// SlerpCenter.y = temp;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_97 = (&__this->___SlerpCenter_33);
		float L_98 = __this->___temp_31;
		L_97->___y_3 = L_98;
		// CameraTargetPos = meCenter.position + xzOff.normalized * XZ_distance;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_99 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_99);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_100;
		L_100 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_99, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_101 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_102;
		L_102 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_101, NULL);
		float L_103;
		L_103 = OneVOneMode_get_XZ_distance_m85DE561AE9FA16B6C72CDA1FFE2F829EA7773A66_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_104;
		L_104 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_102, L_103, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105;
		L_105 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_100, L_104, NULL);
		__this->___CameraTargetPos_13 = L_105;
		// CameraTargetPos.y = temp;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_106 = (&__this->___CameraTargetPos_13);
		float L_107 = __this->___temp_31;
		L_106->___y_3 = L_107;
		// tempV3 = Vector3.Slerp(_camera.transform.position - SlerpCenter, CameraTargetPos - SlerpCenter, Time.deltaTime / (0.1f + Time.deltaTime));//????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_108 = ____camera0;
		NullCheck(L_108);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_109;
		L_109 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_108, NULL);
		NullCheck(L_109);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_110;
		L_110 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_109, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_111 = __this->___SlerpCenter_33;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_112;
		L_112 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_110, L_111, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_113 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_114 = __this->___SlerpCenter_33;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_115;
		L_115 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_113, L_114, NULL);
		float L_116;
		L_116 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_117;
		L_117 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118;
		L_118 = Vector3_Slerp_mBA32C7EAC64C56C7D68480549FA9A892FA5C1728(L_112, L_115, ((float)(L_116/((float)il2cpp_codegen_add((0.100000001f), L_117)))), NULL);
		__this->___tempV3_34 = L_118;
		// _camera.transform.position = tempV3 + SlerpCenter;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_119 = ____camera0;
		NullCheck(L_119);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_120;
		L_120 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_119, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_121 = __this->___tempV3_34;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_122 = __this->___SlerpCenter_33;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_123;
		L_123 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_121, L_122, NULL);
		NullCheck(L_120);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_120, L_123, NULL);
		// rotateToDirection = meCenter.position - _camera.transform.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_124 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_124);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_125;
		L_125 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_124, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_126 = ____camera0;
		NullCheck(L_126);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_127;
		L_127 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_126, NULL);
		NullCheck(L_127);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_128;
		L_128 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_127, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_129;
		L_129 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_125, L_128, NULL);
		__this->___rotateToDirection_16 = L_129;
		// rotateToDirection.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_130 = (&__this->___rotateToDirection_16);
		L_130->___y_3 = (0.0f);
		// rotateToDirection = rotateToDirection.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_131 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_132;
		L_132 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_131, NULL);
		__this->___rotateToDirection_16 = L_132;
		// rotateToDirection.y = -lookdownDegree * 1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_133 = (&__this->___rotateToDirection_16);
		float L_134 = __this->___lookdownDegree_22;
		L_133->___y_3 = ((float)il2cpp_codegen_multiply(((-L_134)), (1.0f)));
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_135 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_136;
		L_136 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_135, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_137;
		L_137 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_136, NULL);
		__this->___ToRotation_15 = L_137;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.1f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_138 = ____camera0;
		NullCheck(L_138);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_139;
		L_139 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_138, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_140 = ____camera0;
		NullCheck(L_140);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_141;
		L_141 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_140, NULL);
		NullCheck(L_141);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_142;
		L_142 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_141, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_143 = __this->___ToRotation_15;
		float L_144;
		L_144 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_145;
		L_145 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_146;
		L_146 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_142, L_143, ((float)(L_144/((float)il2cpp_codegen_add((0.100000001f), L_145)))), NULL);
		NullCheck(L_139);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_139, L_146, NULL);
		// }
		return;
	}
}
// System.Void OneVOneMode::<LocalUpdate>g__ZoomOut|31_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_U3CLocalUpdateU3Eg__ZoomOutU7C31_0_m9EA9BF9EDC0B1356BF74185472C0B18DC71D8621 (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) 
{
	{
		// if (!zoomDirection) //&& zoomcounter > zoomChangeInter) // ????????????zoomout
		bool L_0 = __this->___zoomDirection_32;
		if (L_0)
		{
			goto IL_0025;
		}
	}
	{
		// zoomDirection = true;
		__this->___zoomDirection_32 = (bool)1;
		// ZoomAcc = 0;
		OneVOneMode_set_ZoomAcc_m197C408CBFCFF375517137E2313466548467F32C(__this, (0.0f), NULL);
		// zoomcounter = 0;
		__this->___zoomcounter_24 = (0.0f);
	}

IL_0025:
	{
		// if (zoomDirection)
		bool L_1 = __this->___zoomDirection_32;
		if (!L_1)
		{
			goto IL_003f;
		}
	}
	{
		// ZoomAcc += Time.deltaTime;
		float L_2;
		L_2 = OneVOneMode_get_ZoomAcc_m16B63CB81DADC371768C54722F124AA88B49A8C2_inline(__this, NULL);
		float L_3;
		L_3 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		OneVOneMode_set_ZoomAcc_m197C408CBFCFF375517137E2313466548467F32C(__this, ((float)il2cpp_codegen_add(L_2, L_3)), NULL);
	}

IL_003f:
	{
		// }
		return;
	}
}
// System.Void OneVOneMode::<LocalUpdate>g__ZoomIn|31_1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_U3CLocalUpdateU3Eg__ZoomInU7C31_1_mB67A1D66A9AB976A1AA5249150BA3B5C8F844E0A (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) 
{
	{
		// if (zoomDirection && zoomcounter > zoomChangeInter) // zoomin??????zoomcounter > zoomChangeInter????zoomout?in??????
		bool L_0 = __this->___zoomDirection_32;
		if (!L_0)
		{
			goto IL_0033;
		}
	}
	{
		float L_1 = __this->___zoomcounter_24;
		float L_2 = __this->___zoomChangeInter_25;
		if ((!(((float)L_1) > ((float)L_2))))
		{
			goto IL_0033;
		}
	}
	{
		// zoomDirection = false;
		__this->___zoomDirection_32 = (bool)0;
		// ZoomAcc = 0;
		OneVOneMode_set_ZoomAcc_m197C408CBFCFF375517137E2313466548467F32C(__this, (0.0f), NULL);
		// zoomcounter = 0;
		__this->___zoomcounter_24 = (0.0f);
	}

IL_0033:
	{
		// if (!zoomDirection)
		bool L_3 = __this->___zoomDirection_32;
		if (L_3)
		{
			goto IL_004d;
		}
	}
	{
		// ZoomAcc -= Time.deltaTime;
		float L_4;
		L_4 = OneVOneMode_get_ZoomAcc_m16B63CB81DADC371768C54722F124AA88B49A8C2_inline(__this, NULL);
		float L_5;
		L_5 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		OneVOneMode_set_ZoomAcc_m197C408CBFCFF375517137E2313466548467F32C(__this, ((float)il2cpp_codegen_subtract(L_4, L_5)), NULL);
	}

IL_004d:
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
// System.Single OneVOneModeNew::get_ZoomAcc()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float OneVOneModeNew_get_ZoomAcc_m11AEA3902A80D7C00F4B8A277CD3D59461F12537 (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) 
{
	{
		// get { return zoomAcc; }
		float L_0 = __this->___zoomAcc_22;
		return L_0;
	}
}
// System.Void OneVOneModeNew::set_ZoomAcc(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_set_ZoomAcc_m056925C278FA5621495BEBB29E03D35D235D3A4F (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// zoomAcc = Mathf.Clamp(value, -1f, 1f);// ?????????????zoom???
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (-1.0f), (1.0f), NULL);
		__this->___zoomAcc_22 = L_1;
		// }
		return;
	}
}
// System.Single OneVOneModeNew::get_XZ_distance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float OneVOneModeNew_get_XZ_distance_m94590253CF56035E61E827B63683A58B9867CE56 (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) 
{
	{
		// get { return xzd; }
		float L_0 = __this->___xzd_26;
		return L_0;
	}
}
// System.Void OneVOneModeNew::set_XZ_distance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_set_XZ_distance_m8B3047AAB0CBAD0A685986D15A5105B201EE66CB (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// xzd = Mathf.Clamp(value, 8.5f, xzMax);
		float L_0 = ___value0;
		float L_1 = __this->___xzMax_20;
		float L_2;
		L_2 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (8.5f), L_1, NULL);
		__this->___xzd_26 = L_2;
		// YDis = this.xzd * heightOfXZRate; // ???????????????????????????????
		float L_3 = __this->___xzd_26;
		float L_4 = __this->___heightOfXZRate_25;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_3, L_4));
		// }
		return;
	}
}
// System.Void OneVOneModeNew::.ctor(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew__ctor_m2ACABEC083363C197ED753DEC4B087F0B2BD0B54 (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, float ___XZDis0, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = -Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline(L_0, NULL);
		__this->___xzOff_19 = L_1;
		// readonly float xzMax = 10f;// ???????xz??????
		__this->___xzMax_20 = (10.0f);
		// float lookdownDegree = 0.5f; //????????????????1
		__this->___lookdownDegree_21 = (0.5f);
		// float zoomChangeInter = 0.5f;// zoom in or out ????????????
		__this->___zoomChangeInter_24 = (0.5f);
		// float heightOfXZRate = 0.65f;//?????XZ_distance??????
		__this->___heightOfXZRate_25 = (0.649999976f);
		// bool justEnterdThisMode = true;
		__this->___justEnterdThisMode_27 = (bool)1;
		// public OneVOneModeNew(float XZDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZ_distance = XZDis;
		float L_2 = ___XZDis0;
		OneVOneModeNew_set_XZ_distance_m8B3047AAB0CBAD0A685986D15A5105B201EE66CB(__this, L_2, NULL);
		// YDis = this.XZ_distance * heightOfXZRate;
		float L_3;
		L_3 = OneVOneModeNew_get_XZ_distance_m94590253CF56035E61E827B63683A58B9867CE56_inline(__this, NULL);
		float L_4 = __this->___heightOfXZRate_25;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_3, L_4));
		// }
		return;
	}
}
// System.Void OneVOneModeNew::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_Enter_mBCAE22B07E42E279E547418D80D408B9EAF7686A (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* G_B15_0 = NULL;
	OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* G_B14_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B16_0;
	memset((&G_B16_0), 0, sizeof(G_B16_0));
	OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* G_B16_1 = NULL;
	{
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// if (justEnterdThisMode)
		bool L_2 = __this->___justEnterdThisMode_27;
		if (!L_2)
		{
			goto IL_01d7;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_3)
		{
			goto IL_00af;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_4 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_4);
		int32_t L_5;
		L_5 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_4, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_00af;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_6;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_7 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_7);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_8;
		L_8 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_7, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_8;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0084:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0079_1;
			}

IL_004c_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9;
				L_9 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_9;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_11;
				L_11 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_10, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_11)
				{
					goto IL_0079_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13 = V_1;
				NullCheck(L_13);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14;
				L_14 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_13, NULL);
				NullCheck(L_14);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
				L_15 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_14, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
				L_16 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_12, L_15, NULL);
				__this->___enemiesCenter_14 = L_16;
			}

IL_0079_1:
			{
				// foreach (Transform o in targets)
				bool L_17;
				L_17 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_17)
				{
					goto IL_004c_1;
				}
			}
			{
				goto IL_0092;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0092:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_19 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_19);
		int32_t L_20;
		L_20 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_19, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_18, ((float)L_20), NULL);
		__this->___enemiesCenter_14 = L_21;
	}

IL_00af:
	{
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_22 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = __this->___enemiesCenter_14;
		NullCheck(L_22);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_22, L_23, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_25;
		L_25 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_24, NULL);
		__this->___enemyscreenpos_18 = L_25;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_26 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_27 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_27);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_27, NULL);
		NullCheck(L_26);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_26, L_28, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_30;
		L_30 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_29, NULL);
		__this->___mescreenpos_17 = L_30;
		// temp = Mathf.Abs(mescreenpos.x - enemyscreenpos.x);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_31 = (&__this->___mescreenpos_17);
		float L_32 = L_31->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_33 = (&__this->___enemyscreenpos_18);
		float L_34 = L_33->___x_0;
		float L_35;
		L_35 = fabsf(((float)il2cpp_codegen_subtract(L_32, L_34)));
		__this->___temp_30 = L_35;
		// temp = Mathf.Sqrt(temp);
		float L_36 = __this->___temp_30;
		float L_37;
		L_37 = sqrtf(L_36);
		__this->___temp_30 = L_37;
		// rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___mescreenpos_17);
		float L_39 = L_38->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_40 = (&__this->___enemyscreenpos_18);
		float L_41 = L_40->___x_0;
		G_B14_0 = __this;
		if ((((float)L_39) > ((float)L_41)))
		{
			G_B15_0 = __this;
			goto IL_014c;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_43 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_43);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44;
		L_44 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_43, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_45;
		L_45 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_42, L_44, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_45, NULL);
		G_B16_0 = L_46;
		G_B16_1 = G_B14_0;
		goto IL_0168;
	}

IL_014c:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_47 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_47);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_47, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_49 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		L_50 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_48, L_49, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_51;
		L_51 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_50, NULL);
		G_B16_0 = L_51;
		G_B16_1 = G_B15_0;
	}

IL_0168:
	{
		NullCheck(G_B16_1);
		G_B16_1->___rotateToDirection_16 = G_B16_0;
		// rotateToDirection = rotateToDirection * (1 - temp);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52 = __this->___rotateToDirection_16;
		float L_53 = __this->___temp_30;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_54;
		L_54 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_52, ((float)il2cpp_codegen_subtract((1.0f), L_53)), NULL);
		__this->___rotateToDirection_16 = L_54;
		// rotateToDirection += (meCenter.position - enemiesCenter).normalized * temp;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_55 = __this->___rotateToDirection_16;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_56 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_56);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_57;
		L_57 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_56, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_58 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_59;
		L_59 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_57, L_58, NULL);
		V_2 = L_59;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_60;
		L_60 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_2), NULL);
		float L_61 = __this->___temp_30;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_62;
		L_62 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_60, L_61, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_63;
		L_63 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_55, L_62, NULL);
		__this->___rotateToDirection_16 = L_63;
		// xzOff = rotateToDirection;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_64 = __this->___rotateToDirection_16;
		__this->___xzOff_19 = L_64;
		// justEnterdThisMode = false;
		__this->___justEnterdThisMode_27 = (bool)0;
	}

IL_01d7:
	{
		// this.LocalUpdate(_camera);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_65 = ____camera0;
		VirtualActionInvoker1< Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* >::Invoke(6 /* System.Void CameraMode::LocalUpdate(UnityEngine.Camera) */, __this, L_65);
		// }
		return;
	}
}
// System.Void OneVOneModeNew::Exit(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_Exit_m9BDC3C7FB6F4FFA2105D4E49763D570EE4E27592 (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// justEnterdThisMode = true;
		__this->___justEnterdThisMode_27 = (bool)1;
		// }
		return;
	}
}
// System.Void OneVOneModeNew::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_LocalUpdate_mBC5EC24D1FE2DC8C7725D41BE12695AE86ADE86F (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_2)
		{
			goto IL_0169;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_3, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_4) <= ((int32_t)0)))
		{
			goto IL_0169;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_5;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_6 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_6);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_7;
		L_7 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_6, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_7;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_007c:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0071_1;
			}

IL_0044_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8;
				L_8 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_8;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_10;
				L_10 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_9, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_10)
				{
					goto IL_0071_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_12 = V_1;
				NullCheck(L_12);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13;
				L_13 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_12, NULL);
				NullCheck(L_13);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
				L_14 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_13, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
				L_15 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_11, L_14, NULL);
				__this->___enemiesCenter_14 = L_15;
			}

IL_0071_1:
			{
				// foreach (Transform o in targets)
				bool L_16;
				L_16 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_16)
				{
					goto IL_0044_1;
				}
			}
			{
				goto IL_008a;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_008a:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_18 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_18);
		int32_t L_19;
		L_19 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_18, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_17, ((float)L_19), NULL);
		__this->___enemiesCenter_14 = L_20;
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_21 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = __this->___enemiesCenter_14;
		NullCheck(L_21);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_21, L_22, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_24;
		L_24 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_23, NULL);
		__this->___enemyscreenpos_18 = L_24;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_25 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_26 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_26);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_26, NULL);
		NullCheck(L_25);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_25, L_27, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_29;
		L_29 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_28, NULL);
		__this->___mescreenpos_17 = L_29;
		// zoomcounter += Time.deltaTime;
		float L_30 = __this->___zoomcounter_23;
		float L_31;
		L_31 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		__this->___zoomcounter_23 = ((float)il2cpp_codegen_add(L_30, L_31));
		// if (enemyscreenpos.x < 0.1 || enemyscreenpos.x > 0.9 || enemyscreenpos.y < 0.3 || mescreenpos.y < 0.3)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_32 = (&__this->___enemyscreenpos_18);
		float L_33 = L_32->___x_0;
		if ((((double)((double)L_33)) < ((double)(0.10000000000000001))))
		{
			goto IL_0148;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_34 = (&__this->___enemyscreenpos_18);
		float L_35 = L_34->___x_0;
		if ((((double)((double)L_35)) > ((double)(0.90000000000000002))))
		{
			goto IL_0148;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_36 = (&__this->___enemyscreenpos_18);
		float L_37 = L_36->___y_1;
		if ((((double)((double)L_37)) < ((double)(0.29999999999999999))))
		{
			goto IL_0148;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___mescreenpos_17);
		float L_39 = L_38->___y_1;
		if ((!(((double)((double)L_39)) < ((double)(0.29999999999999999)))))
		{
			goto IL_0150;
		}
	}

IL_0148:
	{
		// ZoomOut();
		OneVOneModeNew_U3CLocalUpdateU3Eg__ZoomOutU7C30_0_m24C5476405E7756753082CBC408EBF90DA323A2D(__this, NULL);
		goto IL_0156;
	}

IL_0150:
	{
		// ZoomIn();
		OneVOneModeNew_U3CLocalUpdateU3Eg__ZoomInU7C30_1_m57A815BD423E9F7D78E04FFF2DC4B968A3D269D2(__this, NULL);
	}

IL_0156:
	{
		// XZ_distance += ZoomAcc;
		float L_40;
		L_40 = OneVOneModeNew_get_XZ_distance_m94590253CF56035E61E827B63683A58B9867CE56_inline(__this, NULL);
		float L_41;
		L_41 = OneVOneModeNew_get_ZoomAcc_m11AEA3902A80D7C00F4B8A277CD3D59461F12537_inline(__this, NULL);
		OneVOneModeNew_set_XZ_distance_m8B3047AAB0CBAD0A685986D15A5105B201EE66CB(__this, ((float)il2cpp_codegen_add(L_40, L_41)), NULL);
	}

IL_0169:
	{
		// h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_42;
		L_42 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_43;
		L_43 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_28 = ((float)il2cpp_codegen_add(L_42, L_43));
		// xzOff = Quaternion.AngleAxis(h * 2f, Vector3.up) * xzOff;
		float L_44 = __this->___h_28;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_45;
		L_45 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_46;
		L_46 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_44, (2.0f))), L_45, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_46, L_47, NULL);
		__this->___xzOff_19 = L_48;
		// maxheight = Mathf.Max(meCenter.position.y, enemiesCenter.y);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_49 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_49);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		L_50 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_49, NULL);
		float L_51 = L_50.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_52 = (&__this->___enemiesCenter_14);
		float L_53 = L_52->___y_3;
		float L_54;
		L_54 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_51, L_53, NULL);
		__this->___maxheight_29 = L_54;
		// temp = Mathf.Max(maxheight, YDis);
		float L_55 = __this->___maxheight_29;
		float L_56 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		float L_57;
		L_57 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_55, L_56, NULL);
		__this->___temp_30 = L_57;
		// SlerpCenter = meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_58 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_58);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_59;
		L_59 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_58, NULL);
		__this->___SlerpCenter_32 = L_59;
		// SlerpCenter.y = temp;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_60 = (&__this->___SlerpCenter_32);
		float L_61 = __this->___temp_30;
		L_60->___y_3 = L_61;
		// CameraTargetPos = meCenter.position + xzOff.normalized * XZ_distance;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_62 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_62);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_63;
		L_63 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_62, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_64 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_65;
		L_65 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_64, NULL);
		float L_66;
		L_66 = OneVOneModeNew_get_XZ_distance_m94590253CF56035E61E827B63683A58B9867CE56_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_67;
		L_67 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_65, L_66, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_68;
		L_68 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_63, L_67, NULL);
		__this->___CameraTargetPos_13 = L_68;
		// CameraTargetPos.y = temp;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_69 = (&__this->___CameraTargetPos_13);
		float L_70 = __this->___temp_30;
		L_69->___y_3 = L_70;
		// tempV3 = Vector3.Slerp(_camera.transform.position - SlerpCenter, CameraTargetPos - SlerpCenter, Time.deltaTime / (0.1f + Time.deltaTime));//????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_71 = ____camera0;
		NullCheck(L_71);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_72;
		L_72 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_71, NULL);
		NullCheck(L_72);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_73;
		L_73 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_72, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_74 = __this->___SlerpCenter_32;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_75;
		L_75 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_73, L_74, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_76 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_77 = __this->___SlerpCenter_32;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78;
		L_78 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_76, L_77, NULL);
		float L_79;
		L_79 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_80;
		L_80 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_81;
		L_81 = Vector3_Slerp_mBA32C7EAC64C56C7D68480549FA9A892FA5C1728(L_75, L_78, ((float)(L_79/((float)il2cpp_codegen_add((0.100000001f), L_80)))), NULL);
		__this->___tempV3_33 = L_81;
		// _camera.transform.position = tempV3 + SlerpCenter;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_82 = ____camera0;
		NullCheck(L_82);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_83;
		L_83 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_82, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_84 = __this->___tempV3_33;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85 = __this->___SlerpCenter_32;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_86;
		L_86 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_84, L_85, NULL);
		NullCheck(L_83);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_83, L_86, NULL);
		// rotateToDirection = meCenter.position - _camera.transform.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_87 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_87);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_88;
		L_88 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_87, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_89 = ____camera0;
		NullCheck(L_89);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_90;
		L_90 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_89, NULL);
		NullCheck(L_90);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_91;
		L_91 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_90, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_92;
		L_92 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_88, L_91, NULL);
		__this->___rotateToDirection_16 = L_92;
		// rotateToDirection.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_93 = (&__this->___rotateToDirection_16);
		L_93->___y_3 = (0.0f);
		// rotateToDirection = rotateToDirection.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_94 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_95;
		L_95 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_94, NULL);
		__this->___rotateToDirection_16 = L_95;
		// rotateToDirection.y = -lookdownDegree * 1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_96 = (&__this->___rotateToDirection_16);
		float L_97 = __this->___lookdownDegree_21;
		L_96->___y_3 = ((float)il2cpp_codegen_multiply(((-L_97)), (1.0f)));
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_98 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_99;
		L_99 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_98, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_100;
		L_100 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_99, NULL);
		__this->___ToRotation_15 = L_100;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation,  Time.deltaTime / (0.05f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_101 = ____camera0;
		NullCheck(L_101);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_102;
		L_102 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_101, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_103 = ____camera0;
		NullCheck(L_103);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_104;
		L_104 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_103, NULL);
		NullCheck(L_104);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_105;
		L_105 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_104, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_106 = __this->___ToRotation_15;
		float L_107;
		L_107 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_108;
		L_108 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_109;
		L_109 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_105, L_106, ((float)(L_107/((float)il2cpp_codegen_add((0.0500000007f), L_108)))), NULL);
		NullCheck(L_102);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_102, L_109, NULL);
		// }
		return;
	}
}
// System.Void OneVOneModeNew::<LocalUpdate>g__ZoomOut|30_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_U3CLocalUpdateU3Eg__ZoomOutU7C30_0_m24C5476405E7756753082CBC408EBF90DA323A2D (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) 
{
	{
		// if (!zoomDirection && zoomcounter > zoomChangeInter)
		bool L_0 = __this->___zoomDirection_31;
		if (L_0)
		{
			goto IL_0033;
		}
	}
	{
		float L_1 = __this->___zoomcounter_23;
		float L_2 = __this->___zoomChangeInter_24;
		if ((!(((float)L_1) > ((float)L_2))))
		{
			goto IL_0033;
		}
	}
	{
		// zoomDirection = true;
		__this->___zoomDirection_31 = (bool)1;
		// ZoomAcc = 0;
		OneVOneModeNew_set_ZoomAcc_m056925C278FA5621495BEBB29E03D35D235D3A4F(__this, (0.0f), NULL);
		// zoomcounter = 0;
		__this->___zoomcounter_23 = (0.0f);
	}

IL_0033:
	{
		// if (zoomDirection)
		bool L_3 = __this->___zoomDirection_31;
		if (!L_3)
		{
			goto IL_00c5;
		}
	}
	{
		// ZoomAcc += Time.deltaTime;
		float L_4;
		L_4 = OneVOneModeNew_get_ZoomAcc_m11AEA3902A80D7C00F4B8A277CD3D59461F12537_inline(__this, NULL);
		float L_5;
		L_5 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		OneVOneModeNew_set_ZoomAcc_m056925C278FA5621495BEBB29E03D35D235D3A4F(__this, ((float)il2cpp_codegen_add(L_4, L_5)), NULL);
		// if (auto)
		bool L_6 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_6)
		{
			goto IL_00c5;
		}
	}
	{
		// rotateToDirection = meCenter.position - enemiesCenter;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_7);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_7, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_8, L_9, NULL);
		__this->___rotateToDirection_16 = L_10;
		// speed = Vector3.Angle(xzOff, rotateToDirection) / 180;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = __this->___rotateToDirection_16;
		float L_13;
		L_13 = Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline(L_11, L_12, NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6 = ((float)(L_13/(180.0f)));
		// xzOff = Vector3.RotateTowards(xzOff, rotateToDirection, speed * Time.deltaTime / (0.1f + Time.deltaTime), 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = __this->___rotateToDirection_16;
		float L_16 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_17;
		L_17 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_18;
		L_18 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
		L_19 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_14, L_15, ((float)(((float)il2cpp_codegen_multiply(L_16, L_17))/((float)il2cpp_codegen_add((0.100000001f), L_18)))), (0.0f), NULL);
		__this->___xzOff_19 = L_19;
	}

IL_00c5:
	{
		// }
		return;
	}
}
// System.Void OneVOneModeNew::<LocalUpdate>g__ZoomIn|30_1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneModeNew_U3CLocalUpdateU3Eg__ZoomInU7C30_1_m57A815BD423E9F7D78E04FFF2DC4B968A3D269D2 (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) 
{
	OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* G_B7_0 = NULL;
	OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* G_B6_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B8_0;
	memset((&G_B8_0), 0, sizeof(G_B8_0));
	OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* G_B8_1 = NULL;
	{
		// if (zoomDirection && zoomcounter > zoomChangeInter)
		bool L_0 = __this->___zoomDirection_31;
		if (!L_0)
		{
			goto IL_0033;
		}
	}
	{
		float L_1 = __this->___zoomcounter_23;
		float L_2 = __this->___zoomChangeInter_24;
		if ((!(((float)L_1) > ((float)L_2))))
		{
			goto IL_0033;
		}
	}
	{
		// zoomDirection = false;
		__this->___zoomDirection_31 = (bool)0;
		// ZoomAcc = 0;
		OneVOneModeNew_set_ZoomAcc_m056925C278FA5621495BEBB29E03D35D235D3A4F(__this, (0.0f), NULL);
		// zoomcounter = 0;
		__this->___zoomcounter_23 = (0.0f);
	}

IL_0033:
	{
		// if (!zoomDirection)
		bool L_3 = __this->___zoomDirection_31;
		if (L_3)
		{
			goto IL_0120;
		}
	}
	{
		// ZoomAcc -= Time.deltaTime;
		float L_4;
		L_4 = OneVOneModeNew_get_ZoomAcc_m11AEA3902A80D7C00F4B8A277CD3D59461F12537_inline(__this, NULL);
		float L_5;
		L_5 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		OneVOneModeNew_set_ZoomAcc_m056925C278FA5621495BEBB29E03D35D235D3A4F(__this, ((float)il2cpp_codegen_subtract(L_4, L_5)), NULL);
		// if (auto)
		bool L_6 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_6)
		{
			goto IL_0120;
		}
	}
	{
		// temp = Vector3.Distance(meCenter.position, enemiesCenter);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_7);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_7, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = __this->___enemiesCenter_14;
		float L_10;
		L_10 = Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline(L_8, L_9, NULL);
		__this->___temp_30 = L_10;
		// rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_11 = (&__this->___mescreenpos_17);
		float L_12 = L_11->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_13 = (&__this->___enemyscreenpos_18);
		float L_14 = L_13->___x_0;
		G_B6_0 = __this;
		if ((((float)L_12) > ((float)L_14)))
		{
			G_B7_0 = __this;
			goto IL_00ae;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_16 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
		L_17 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_16, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_15, L_17, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
		L_19 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_18, NULL);
		G_B8_0 = L_19;
		G_B8_1 = G_B6_0;
		goto IL_00ca;
	}

IL_00ae:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_20 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_20);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_20, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_21, L_22, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_23, NULL);
		G_B8_0 = L_24;
		G_B8_1 = G_B7_0;
	}

IL_00ca:
	{
		NullCheck(G_B8_1);
		G_B8_1->___rotateToDirection_16 = G_B8_0;
		// speed = Vector3.Angle(xzOff, rotateToDirection) / 180;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26 = __this->___rotateToDirection_16;
		float L_27;
		L_27 = Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline(L_25, L_26, NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6 = ((float)(L_27/(180.0f)));
		// xzOff = Vector3.RotateTowards(xzOff, rotateToDirection, speed * Time.deltaTime / (0.1f + Time.deltaTime), 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29 = __this->___rotateToDirection_16;
		float L_30 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_31;
		L_31 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_32;
		L_32 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_28, L_29, ((float)(((float)il2cpp_codegen_multiply(L_30, L_31))/((float)il2cpp_codegen_add((0.100000001f), L_32)))), (0.0f), NULL);
		__this->___xzOff_19 = L_33;
	}

IL_0120:
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
// System.Single OneVOneMode_failed::get_ZoomAcc()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float OneVOneMode_failed_get_ZoomAcc_m0188D1030DC38FCBAACF2CEAE851C047382B2CFC (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, const RuntimeMethod* method) 
{
	{
		// get { return zoomAcc; }
		float L_0 = __this->___zoomAcc_24;
		return L_0;
	}
}
// System.Void OneVOneMode_failed::set_ZoomAcc(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed_set_ZoomAcc_m2E392D5A7F6577A690A1D1C6EA758A4327774E53 (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// zoomAcc = Mathf.Clamp(value, -0.5f, 0.5f);// ?????????????zoom???
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (-0.5f), (0.5f), NULL);
		__this->___zoomAcc_24 = L_1;
		// }
		return;
	}
}
// System.Single OneVOneMode_failed::get_XZ_distance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, const RuntimeMethod* method) 
{
	{
		// get { return xzd; }
		float L_0 = __this->___xzd_26;
		return L_0;
	}
}
// System.Void OneVOneMode_failed::set_XZ_distance(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed_set_XZ_distance_m38063BD537DA620E0FF817F793410D6D1DB397B0 (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// xzd = Mathf.Clamp(value, 8.5f, xzMax);
		float L_0 = ___value0;
		float L_1 = __this->___xzMax_22;
		float L_2;
		L_2 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (8.5f), L_1, NULL);
		__this->___xzd_26 = L_2;
		// YDis = this.xzd * heightOfXZRate; // ???????????????????????????????
		float L_3 = __this->___xzd_26;
		float L_4 = __this->___heightOfXZRate_25;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_3, L_4));
		// }
		return;
	}
}
// System.Void OneVOneMode_failed::.ctor(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed__ctor_mE95A10F593E4A4B4D4B497225E06B98B113669BA (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, float ___XZDis0, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = -Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline(L_0, NULL);
		__this->___xzOff_19 = L_1;
		// float autoRotateXZOffRange = 6f;
		__this->___autoRotateXZOffRange_20 = (6.0f);
		// float autoRotateXZOffRangeMaxSpeed = 0.5f;
		__this->___autoRotateXZOffRangeMaxSpeed_21 = (0.5f);
		// readonly float xzMax = 24f;// ???????xz??????
		__this->___xzMax_22 = (24.0f);
		// float lookdownDegree = 0.5f; //????????????????1
		__this->___lookdownDegree_23 = (0.5f);
		// float heightOfXZRate = 0.7f;//?????XZ_distance??????
		__this->___heightOfXZRate_25 = (0.699999988f);
		// public OneVOneMode_failed(float XZDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZ_distance = XZDis;
		float L_2 = ___XZDis0;
		OneVOneMode_failed_set_XZ_distance_m38063BD537DA620E0FF817F793410D6D1DB397B0(__this, L_2, NULL);
		// YDis = this.XZ_distance * heightOfXZRate;
		float L_3;
		L_3 = OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C_inline(__this, NULL);
		float L_4 = __this->___heightOfXZRate_25;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_multiply(L_3, L_4));
		// }
		return;
	}
}
// System.Void OneVOneMode_failed::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed_Enter_m41790F508ECB936A6EA41DB8E36860E6AF39F033 (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* G_B11_0 = NULL;
	OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* G_B10_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B12_0;
	memset((&G_B12_0), 0, sizeof(G_B12_0));
	OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* G_B12_1 = NULL;
	{
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_2;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_3);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_4;
		L_4 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_3, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_4;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0060:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0055_1;
			}

IL_0028_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
				L_5 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_5;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_7;
				L_7 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_6, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_7)
				{
					goto IL_0055_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9 = V_1;
				NullCheck(L_9);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10;
				L_10 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_9, NULL);
				NullCheck(L_10);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
				L_11 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_10, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
				L_12 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_8, L_11, NULL);
				__this->___enemiesCenter_14 = L_12;
			}

IL_0055_1:
			{
				// foreach (Transform o in targets)
				bool L_13;
				L_13 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_13)
				{
					goto IL_0028_1;
				}
			}
			{
				goto IL_006e;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006e:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_15 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_15);
		int32_t L_16;
		L_16 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_15, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
		L_17 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_14, ((float)L_16), NULL);
		__this->___enemiesCenter_14 = L_17;
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_18 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = __this->___enemiesCenter_14;
		NullCheck(L_18);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_18, L_19, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_21;
		L_21 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_20, NULL);
		__this->___enemyscreenpos_18 = L_21;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_22 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_23 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_23);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_23, NULL);
		NullCheck(L_22);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_22, L_24, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_26;
		L_26 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_25, NULL);
		__this->___mescreenpos_17 = L_26;
		// xzOff = mescreenpos.y < enemyscreenpos.y ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_27 = (&__this->___mescreenpos_17);
		float L_28 = L_27->___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_29 = (&__this->___enemyscreenpos_18);
		float L_30 = L_29->___y_1;
		G_B10_0 = __this;
		if ((((float)L_28) < ((float)L_30)))
		{
			G_B11_0 = __this;
			goto IL_00f5;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_32 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_32);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_32, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_31, L_33, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
		L_35 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_34, NULL);
		G_B12_0 = L_35;
		G_B12_1 = G_B10_0;
		goto IL_0111;
	}

IL_00f5:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_36 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_36);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_37;
		L_37 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_36, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_37, L_38, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_39, NULL);
		G_B12_0 = L_40;
		G_B12_1 = G_B11_0;
	}

IL_0111:
	{
		NullCheck(G_B12_1);
		G_B12_1->___xzOff_19 = G_B12_0;
		// CameraTargetPos = meCenter.position + xzOff.normalized * XZ_distance;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_41 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_41);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42;
		L_42 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_41, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_43 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44;
		L_44 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_43, NULL);
		float L_45;
		L_45 = OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_44, L_45, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47;
		L_47 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_42, L_46, NULL);
		__this->___CameraTargetPos_13 = L_47;
		// }
		return;
	}
}
// System.Void OneVOneMode_failed::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OneVOneMode_failed_LocalUpdate_m9B9B32F1AEE5B496C7F1131297FB26CA7780F918 (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* G_B27_0 = NULL;
	OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* G_B26_0 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 G_B28_0;
	memset((&G_B28_0), 0, sizeof(G_B28_0));
	OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* G_B28_1 = NULL;
	{
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_2;
		L_2 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_3;
		L_3 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_27 = ((float)il2cpp_codegen_add(L_2, L_3));
		// xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
		float L_4 = __this->___h_27;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6;
		L_6 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(((float)il2cpp_codegen_multiply(L_4, (1.5f))), L_5, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8;
		L_8 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_6, L_7, NULL);
		__this->___xzOff_19 = L_8;
		// maxheight = Mathf.Max(meCenter.position.y, enemiesCenter.y);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_9);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_9, NULL);
		float L_11 = L_10.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_12 = (&__this->___enemiesCenter_14);
		float L_13 = L_12->___y_3;
		float L_14;
		L_14 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_11, L_13, NULL);
		__this->___maxheight_28 = L_14;
		// CameraTargetPos = meCenter.position + xzOff.normalized * XZ_distance;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_15 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_15);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
		L_16 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_15, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_17 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
		L_18 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_17, NULL);
		float L_19;
		L_19 = OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C_inline(__this, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_18, L_19, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_16, L_20, NULL);
		__this->___CameraTargetPos_13 = L_21;
		// CameraTargetPos.y = Mathf.Max(maxheight, YDis);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_22 = (&__this->___CameraTargetPos_13);
		float L_23 = __this->___maxheight_28;
		float L_24 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		float L_25;
		L_25 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_23, L_24, NULL);
		L_22->___y_3 = L_25;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.1f + Time.deltaTime));//????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_26 = ____camera0;
		NullCheck(L_26);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_27;
		L_27 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_26, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_28 = ____camera0;
		NullCheck(L_28);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_29;
		L_29 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_28, NULL);
		NullCheck(L_29);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_29, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_31 = __this->___CameraTargetPos_13;
		float L_32;
		L_32 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_33;
		L_33 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_30, L_31, ((float)(L_32/((float)il2cpp_codegen_add((0.100000001f), L_33)))), NULL);
		NullCheck(L_27);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_27, L_34, NULL);
		// rotateToDirection = meCenter.position - _camera.transform.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_35 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_35);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_36;
		L_36 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_35, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_37 = ____camera0;
		NullCheck(L_37);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_38;
		L_38 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_37, NULL);
		NullCheck(L_38);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_38, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_36, L_39, NULL);
		__this->___rotateToDirection_16 = L_40;
		// rotateToDirection.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_41 = (&__this->___rotateToDirection_16);
		L_41->___y_3 = (0.0f);
		// rotateToDirection = rotateToDirection.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_42 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_43;
		L_43 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_42, NULL);
		__this->___rotateToDirection_16 = L_43;
		// rotateToDirection.y = -lookdownDegree * 1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_44 = (&__this->___rotateToDirection_16);
		float L_45 = __this->___lookdownDegree_23;
		L_44->___y_3 = ((float)il2cpp_codegen_multiply(((-L_45)), (1.0f)));
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_46 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47;
		L_47 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_46, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_48;
		L_48 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_47, NULL);
		__this->___ToRotation_15 = L_48;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.1f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_49 = ____camera0;
		NullCheck(L_49);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_50;
		L_50 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_49, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_51 = ____camera0;
		NullCheck(L_51);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_52;
		L_52 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_51, NULL);
		NullCheck(L_52);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_53;
		L_53 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_52, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_54 = __this->___ToRotation_15;
		float L_55;
		L_55 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_56;
		L_56 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_57;
		L_57 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_53, L_54, ((float)(L_55/((float)il2cpp_codegen_add((0.100000001f), L_56)))), NULL);
		NullCheck(L_50);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_50, L_57, NULL);
		// if (auto)
		bool L_58 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_58)
		{
			goto IL_0450;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_59 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_59)
		{
			goto IL_0450;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_60 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_60);
		int32_t L_61;
		L_61 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_60, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_61) <= ((int32_t)0)))
		{
			goto IL_0450;
		}
	}
	{
		// enemiesCenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_62;
		L_62 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiesCenter_14 = L_62;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_63 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_63);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_64;
		L_64 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_63, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_64;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_020b:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0200_1;
			}

IL_01d3_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_65;
				L_65 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_65;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_66 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_67;
				L_67 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_66, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_67)
				{
					goto IL_0200_1;
				}
			}
			{
				// enemiesCenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_68 = __this->___enemiesCenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_69 = V_1;
				NullCheck(L_69);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_70;
				L_70 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_69, NULL);
				NullCheck(L_70);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_71;
				L_71 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_70, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_72;
				L_72 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_68, L_71, NULL);
				__this->___enemiesCenter_14 = L_72;
			}

IL_0200_1:
			{
				// foreach (Transform o in targets)
				bool L_73;
				L_73 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_73)
				{
					goto IL_01d3_1;
				}
			}
			{
				goto IL_0219;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0219:
	{
		// enemiesCenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_74 = __this->___enemiesCenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_75 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_75);
		int32_t L_76;
		L_76 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_75, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_77;
		L_77 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_74, ((float)L_76), NULL);
		__this->___enemiesCenter_14 = L_77;
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_78 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_79 = __this->___enemiesCenter_14;
		NullCheck(L_78);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_80;
		L_80 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_78, L_79, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_81;
		L_81 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_80, NULL);
		__this->___enemyscreenpos_18 = L_81;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_82 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_83 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_83);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_84;
		L_84 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_83, NULL);
		NullCheck(L_82);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85;
		L_85 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_82, L_84, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_86;
		L_86 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_85, NULL);
		__this->___mescreenpos_17 = L_86;
		// if (enemyscreenpos.x < 0.1 || enemyscreenpos.x > 0.9 || enemyscreenpos.y < 0.2 ||
		//     mescreenpos.x < 0.1 || mescreenpos.x > 0.9 || mescreenpos.y < 0.2)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_87 = (&__this->___enemyscreenpos_18);
		float L_88 = L_87->___x_0;
		if ((((double)((double)L_88)) < ((double)(0.10000000000000001))))
		{
			goto IL_02f3;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_89 = (&__this->___enemyscreenpos_18);
		float L_90 = L_89->___x_0;
		if ((((double)((double)L_90)) > ((double)(0.90000000000000002))))
		{
			goto IL_02f3;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_91 = (&__this->___enemyscreenpos_18);
		float L_92 = L_91->___y_1;
		if ((((double)((double)L_92)) < ((double)(0.20000000000000001))))
		{
			goto IL_02f3;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_93 = (&__this->___mescreenpos_17);
		float L_94 = L_93->___x_0;
		if ((((double)((double)L_94)) < ((double)(0.10000000000000001))))
		{
			goto IL_02f3;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_95 = (&__this->___mescreenpos_17);
		float L_96 = L_95->___x_0;
		if ((((double)((double)L_96)) > ((double)(0.90000000000000002))))
		{
			goto IL_02f3;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_97 = (&__this->___mescreenpos_17);
		float L_98 = L_97->___y_1;
		if ((!(((double)((double)L_98)) < ((double)(0.20000000000000001)))))
		{
			goto IL_0321;
		}
	}

IL_02f3:
	{
		// if (!zoomDirection)
		bool L_99 = __this->___zoomDirection_29;
		if (L_99)
		{
			goto IL_030d;
		}
	}
	{
		// zoomDirection = true;
		__this->___zoomDirection_29 = (bool)1;
		// ZoomAcc = 0;
		OneVOneMode_failed_set_ZoomAcc_m2E392D5A7F6577A690A1D1C6EA758A4327774E53(__this, (0.0f), NULL);
	}

IL_030d:
	{
		// ZoomAcc += Time.deltaTime;
		float L_100;
		L_100 = OneVOneMode_failed_get_ZoomAcc_m0188D1030DC38FCBAACF2CEAE851C047382B2CFC_inline(__this, NULL);
		float L_101;
		L_101 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		OneVOneMode_failed_set_ZoomAcc_m2E392D5A7F6577A690A1D1C6EA758A4327774E53(__this, ((float)il2cpp_codegen_add(L_100, L_101)), NULL);
		goto IL_0353;
	}

IL_0321:
	{
		// if (zoomDirection)
		bool L_102 = __this->___zoomDirection_29;
		if (!L_102)
		{
			goto IL_033b;
		}
	}
	{
		// zoomDirection = false;
		__this->___zoomDirection_29 = (bool)0;
		// ZoomAcc = 0;
		OneVOneMode_failed_set_ZoomAcc_m2E392D5A7F6577A690A1D1C6EA758A4327774E53(__this, (0.0f), NULL);
	}

IL_033b:
	{
		// ZoomAcc -= 0.6f * Time.deltaTime;
		float L_103;
		L_103 = OneVOneMode_failed_get_ZoomAcc_m0188D1030DC38FCBAACF2CEAE851C047382B2CFC_inline(__this, NULL);
		float L_104;
		L_104 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		OneVOneMode_failed_set_ZoomAcc_m2E392D5A7F6577A690A1D1C6EA758A4327774E53(__this, ((float)il2cpp_codegen_subtract(L_103, ((float)il2cpp_codegen_multiply((0.600000024f), L_104)))), NULL);
	}

IL_0353:
	{
		// XZ_distance += ZoomAcc;
		float L_105;
		L_105 = OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C_inline(__this, NULL);
		float L_106;
		L_106 = OneVOneMode_failed_get_ZoomAcc_m0188D1030DC38FCBAACF2CEAE851C047382B2CFC_inline(__this, NULL);
		OneVOneMode_failed_set_XZ_distance_m38063BD537DA620E0FF817F793410D6D1DB397B0(__this, ((float)il2cpp_codegen_add(L_105, L_106)), NULL);
		// if (Vector3.Distance(enemiesCenter, meCenter.position) < autoRotateXZOffRange)
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_107 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_108 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_108);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_109;
		L_109 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_108, NULL);
		float L_110;
		L_110 = Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline(L_107, L_109, NULL);
		float L_111 = __this->___autoRotateXZOffRange_20;
		if ((!(((float)L_110) < ((float)L_111))))
		{
			goto IL_0450;
		}
	}
	{
		// rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_112 = (&__this->___mescreenpos_17);
		float L_113 = L_112->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_114 = (&__this->___enemyscreenpos_18);
		float L_115 = L_114->___x_0;
		G_B26_0 = __this;
		if ((((float)L_113) > ((float)L_115)))
		{
			G_B27_0 = __this;
			goto IL_03be;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_116 = __this->___enemiesCenter_14;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_117 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_117);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_118;
		L_118 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_117, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_119;
		L_119 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_116, L_118, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120;
		L_120 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_119, NULL);
		G_B28_0 = L_120;
		G_B28_1 = G_B26_0;
		goto IL_03da;
	}

IL_03be:
	{
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_121 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_121);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_122;
		L_122 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_121, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_123 = __this->___enemiesCenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_124;
		L_124 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_122, L_123, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_125;
		L_125 = CameraMode_GetVerticalDir_m097AE60A77405097E5B1E3295CF2CC62CEC9072F(__this, L_124, NULL);
		G_B28_0 = L_125;
		G_B28_1 = G_B27_0;
	}

IL_03da:
	{
		NullCheck(G_B28_1);
		G_B28_1->___rotateToDirection_16 = G_B28_0;
		// speed = Mathf.Clamp(4f * Mathf.Pow(mescreenpos.x - enemyscreenpos.x, 2f), 0, autoRotateXZOffRangeMaxSpeed);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_126 = (&__this->___mescreenpos_17);
		float L_127 = L_126->___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_128 = (&__this->___enemyscreenpos_18);
		float L_129 = L_128->___x_0;
		float L_130;
		L_130 = powf(((float)il2cpp_codegen_subtract(L_127, L_129)), (2.0f));
		float L_131 = __this->___autoRotateXZOffRangeMaxSpeed_21;
		float L_132;
		L_132 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(((float)il2cpp_codegen_multiply((4.0f), L_130)), (0.0f), L_131, NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6 = L_132;
		// xzOff = Vector3.RotateTowards(xzOff, rotateToDirection, speed * Time.deltaTime / (0.2f + Time.deltaTime), 0.0f);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_133 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_134 = __this->___rotateToDirection_16;
		float L_135 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_136;
		L_136 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_137;
		L_137 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_138;
		L_138 = Vector3_RotateTowards_m3A0BCD584401D5341E1CB544B37764207258B234(L_133, L_134, ((float)(((float)il2cpp_codegen_multiply(L_135, L_136))/((float)il2cpp_codegen_add((0.200000003f), L_137)))), (0.0f), NULL);
		__this->___xzOff_19 = L_138;
	}

IL_0450:
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
// System.Void ScreenSaverC::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScreenSaverC__ctor_m4D3208B9E0F380DC95A22685B5C559A760D8A7D0 (ScreenSaverC_t57D260260EAF244CB16B9345A74371B1CAB86AE7* __this, float ___XZDis0, float ___YDis1, const RuntimeMethod* method) 
{
	{
		// Vector3 xzOff = Vector3.forward;//???focuscenter????????????
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		__this->___xzOff_19 = L_0;
		// float h = 0.3f;//??????
		__this->___h_20 = (0.300000012f);
		// public ScreenSaverC(float XZDis, float YDis)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.XZDis = XZDis;
		float L_1 = ___XZDis0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_1;
		// this.YDis = YDis;
		float L_2 = ___YDis1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_2;
		// }
		return;
	}
}
// System.Void ScreenSaverC::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScreenSaverC_LocalUpdate_mCDEAC4D49AF7D8B3493817877B3A43A504B70B53 (ScreenSaverC_t57D260260EAF244CB16B9345A74371B1CAB86AE7* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_1 = NULL;
	{
		// if (auto)
		bool L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___auto_5;
		if (!L_0)
		{
			goto IL_02fe;
		}
	}
	{
		// if (targets != null && targets.Count > 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_1)
		{
			goto IL_02dd;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_2);
		int32_t L_3;
		L_3 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_2, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if ((((int32_t)L_3) <= ((int32_t)0)))
		{
			goto IL_02dd;
		}
	}
	{
		// enemiescenter = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___enemiescenter_14 = L_4;
		// foreach (Transform o in targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_5 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_5);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_6;
		L_6 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_5, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_0 = L_6;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0078:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_0), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_006d_1;
			}

IL_0040_1:
			{
				// foreach (Transform o in targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7;
				L_7 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_0), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_1 = L_7;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8 = V_1;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_9;
				L_9 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_8, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_9)
				{
					goto IL_006d_1;
				}
			}
			{
				// enemiescenter += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = __this->___enemiescenter_14;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11 = V_1;
				NullCheck(L_11);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_12;
				L_12 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_11, NULL);
				NullCheck(L_12);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
				L_13 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_12, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
				L_14 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_10, L_13, NULL);
				__this->___enemiescenter_14 = L_14;
			}

IL_006d_1:
			{
				// foreach (Transform o in targets)
				bool L_15;
				L_15 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_0), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_15)
				{
					goto IL_0040_1;
				}
			}
			{
				goto IL_0086;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0086:
	{
		// enemiescenter /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = __this->___enemiescenter_14;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_17 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_17);
		int32_t L_18;
		L_18 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_17, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
		L_19 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_16, ((float)L_18), NULL);
		__this->___enemiescenter_14 = L_19;
		// enemiescenter.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_20 = (&__this->___enemiescenter_14);
		L_20->___y_3 = (0.0f);
		// enemyscreenpos = _camera.WorldToViewportPoint(enemiescenter);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_21 = ____camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22 = __this->___enemiescenter_14;
		NullCheck(L_21);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23;
		L_23 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_21, L_22, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_24;
		L_24 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_23, NULL);
		__this->___enemyscreenpos_18 = L_24;
		// mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_25 = ____camera0;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_26 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_26);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_26, NULL);
		NullCheck(L_25);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28;
		L_28 = Camera_WorldToViewportPoint_m285523443225EDA79BBEF9C9EDD76B99CFED054B(L_25, L_27, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_29;
		L_29 = Vector2_op_Implicit_m8F73B300CB4E6F9B4EB5FB6130363D76CEAA230B_inline(L_28, NULL);
		__this->___mescreenpos_17 = L_29;
		// if (enemyscreenpos.x < 0.08 || enemyscreenpos.x > 0.92 || enemyscreenpos.y < 0.2 || enemyscreenpos.y > 0.9 ||
		//     mescreenpos.x < 0.08 || mescreenpos.x > 0.92 || mescreenpos.y < 0.2 || mescreenpos.y > 0.9)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_30 = (&__this->___enemyscreenpos_18);
		float L_31 = L_30->___x_0;
		if ((((double)((double)L_31)) < ((double)(0.080000000000000002))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_32 = (&__this->___enemyscreenpos_18);
		float L_33 = L_32->___x_0;
		if ((((double)((double)L_33)) > ((double)(0.92000000000000004))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_34 = (&__this->___enemyscreenpos_18);
		float L_35 = L_34->___y_1;
		if ((((double)((double)L_35)) < ((double)(0.20000000000000001))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_36 = (&__this->___enemyscreenpos_18);
		float L_37 = L_36->___y_1;
		if ((((double)((double)L_37)) > ((double)(0.90000000000000002))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_38 = (&__this->___mescreenpos_17);
		float L_39 = L_38->___x_0;
		if ((((double)((double)L_39)) < ((double)(0.080000000000000002))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_40 = (&__this->___mescreenpos_17);
		float L_41 = L_40->___x_0;
		if ((((double)((double)L_41)) > ((double)(0.92000000000000004))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_42 = (&__this->___mescreenpos_17);
		float L_43 = L_42->___y_1;
		if ((((double)((double)L_43)) < ((double)(0.20000000000000001))))
		{
			goto IL_01a4;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_44 = (&__this->___mescreenpos_17);
		float L_45 = L_44->___y_1;
		if ((!(((double)((double)L_45)) > ((double)(0.90000000000000002)))))
		{
			goto IL_01e9;
		}
	}

IL_01a4:
	{
		// if (this.XZDis < 15)
		float L_46 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		if ((!(((float)L_46) < ((float)(15.0f)))))
		{
			goto IL_02dd;
		}
	}
	{
		// this.XZDis += Time.deltaTime * 1.5f;
		float L_47 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		float L_48;
		L_48 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = ((float)il2cpp_codegen_add(L_47, ((float)il2cpp_codegen_multiply(L_48, (1.5f)))));
		// this.YDis += Time.deltaTime * 1.5f;
		float L_49 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		float L_50;
		L_50 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_add(L_49, ((float)il2cpp_codegen_multiply(L_50, (1.5f)))));
		goto IL_02dd;
	}

IL_01e9:
	{
		// if (enemyscreenpos.x > 0.45 || enemyscreenpos.x < 0.55 || enemyscreenpos.y > 0.45 || enemyscreenpos.y < 0.55 ||
		//     mescreenpos.x > 0.45 || mescreenpos.x < 0.55 || mescreenpos.y > 0.45 || mescreenpos.y < 0.55)
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_51 = (&__this->___enemyscreenpos_18);
		float L_52 = L_51->___x_0;
		if ((((double)((double)L_52)) > ((double)(0.45000000000000001))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_53 = (&__this->___enemyscreenpos_18);
		float L_54 = L_53->___x_0;
		if ((((double)((double)L_54)) < ((double)(0.55000000000000004))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_55 = (&__this->___enemyscreenpos_18);
		float L_56 = L_55->___y_1;
		if ((((double)((double)L_56)) > ((double)(0.45000000000000001))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_57 = (&__this->___enemyscreenpos_18);
		float L_58 = L_57->___y_1;
		if ((((double)((double)L_58)) < ((double)(0.55000000000000004))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_59 = (&__this->___mescreenpos_17);
		float L_60 = L_59->___x_0;
		if ((((double)((double)L_60)) > ((double)(0.45000000000000001))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_61 = (&__this->___mescreenpos_17);
		float L_62 = L_61->___x_0;
		if ((((double)((double)L_62)) < ((double)(0.55000000000000004))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_63 = (&__this->___mescreenpos_17);
		float L_64 = L_63->___y_1;
		if ((((double)((double)L_64)) > ((double)(0.45000000000000001))))
		{
			goto IL_02a7;
		}
	}
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* L_65 = (&__this->___mescreenpos_17);
		float L_66 = L_65->___y_1;
		if ((!(((double)((double)L_66)) < ((double)(0.55000000000000004)))))
		{
			goto IL_02dd;
		}
	}

IL_02a7:
	{
		// if (this.XZDis > 8.5)
		float L_67 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		if ((!(((double)((double)L_67)) > ((double)(8.5)))))
		{
			goto IL_02dd;
		}
	}
	{
		// this.XZDis -= Time.deltaTime;
		float L_68 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		float L_69;
		L_69 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = ((float)il2cpp_codegen_subtract(L_68, L_69));
		// this.YDis -= Time.deltaTime;
		float L_70 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		float L_71;
		L_71 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = ((float)il2cpp_codegen_subtract(L_70, L_71));
	}

IL_02dd:
	{
		// xzOff = Quaternion.AngleAxis(h, Vector3.up) * xzOff;
		float L_72 = __this->___h_20;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_73;
		L_73 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_74;
		L_74 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(L_72, L_73, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_75 = __this->___xzOff_19;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_76;
		L_76 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_74, L_75, NULL);
		__this->___xzOff_19 = L_76;
	}

IL_02fe:
	{
		// CameraTargetPos = (meCenter.position + enemiescenter)/2 + xzOff.normalized * XZDis;//focuscenter + xzOff.normalized * XZDis;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_77 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_77);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_78;
		L_78 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_77, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_79 = __this->___enemiescenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_80;
		L_80 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_78, L_79, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_81;
		L_81 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_80, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_82 = (&__this->___xzOff_19);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_83;
		L_83 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_82, NULL);
		float L_84 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_85;
		L_85 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_83, L_84, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_86;
		L_86 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_81, L_85, NULL);
		__this->___CameraTargetPos_13 = L_86;
		// CameraTargetPos += Vector3.up * YDis;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_87 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_88;
		L_88 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		float L_89 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_90;
		L_90 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_88, L_89, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_91;
		L_91 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_87, L_90, NULL);
		__this->___CameraTargetPos_13 = L_91;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//????????????????????????
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_92 = ____camera0;
		NullCheck(L_92);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_93;
		L_93 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_92, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_94 = ____camera0;
		NullCheck(L_94);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_95;
		L_95 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_94, NULL);
		NullCheck(L_95);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_96;
		L_96 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_95, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_97 = __this->___CameraTargetPos_13;
		float L_98;
		L_98 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_99;
		L_99 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_100;
		L_100 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_96, L_97, ((float)(L_98/((float)il2cpp_codegen_add((0.200000003f), L_99)))), NULL);
		NullCheck(L_93);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_93, L_100, NULL);
		// rotateToDirection = ((meCenter.position + enemiescenter)/2 + Vector3.up * 2f) - CameraTargetPos;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_101 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_101);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_102;
		L_102 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_101, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_103 = __this->___enemiescenter_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_104;
		L_104 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_102, L_103, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105;
		L_105 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_104, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_106;
		L_106 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_107;
		L_107 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_106, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_108;
		L_108 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_105, L_107, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_109 = __this->___CameraTargetPos_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_110;
		L_110 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_108, L_109, NULL);
		__this->___rotateToDirection_16 = L_110;
		// ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_111 = (&__this->___rotateToDirection_16);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_112;
		L_112 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_111, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_113;
		L_113 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_112, NULL);
		__this->___ToRotation_15 = L_113;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, (Time.deltaTime) / (0.2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_114 = ____camera0;
		NullCheck(L_114);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_115;
		L_115 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_114, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_116 = ____camera0;
		NullCheck(L_116);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_117;
		L_117 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_116, NULL);
		NullCheck(L_117);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_118;
		L_118 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_117, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_119 = __this->___ToRotation_15;
		float L_120;
		L_120 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_121;
		L_121 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_122;
		L_122 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_118, L_119, ((float)(L_120/((float)il2cpp_codegen_add((0.200000003f), L_121)))), NULL);
		NullCheck(L_115);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_115, L_122, NULL);
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
// System.Void StartToEndMode::SetObjPosAndRotAndSpeed(UnityEngine.Vector3,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StartToEndMode_SetObjPosAndRotAndSpeed_mC84496A4A09BCC77C687F3C70E0F5D2D87C50506 (StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___obj_position0, float ___duration1, float ___fieldOfView2, const RuntimeMethod* method) 
{
	{
		// this.obj_position = obj_position;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___obj_position0;
		__this->___obj_position_13 = L_0;
		// this.duration = duration;
		float L_1 = ___duration1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___duration_11 = L_1;
		// this.fieldOfView = fieldOfView;
		float L_2 = ___fieldOfView2;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___fieldOfView_12 = L_2;
		// }
		return;
	}
}
// System.Void StartToEndMode::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StartToEndMode_Enter_m70D754214A4D12378984E8C9860A08F291C3DF68 (StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	{
		// _camera.DOFieldOfView(fieldOfView, duration);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		float L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___fieldOfView_12;
		float L_2 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___duration_11;
		TweenerCore_3_t88CA32E51F4E95E6907CE2C6FD5D8122059AC2C1* L_3;
		L_3 = ShortcutExtensions_DOFieldOfView_m82327EC4821621EBF7957C8DE04B0E7C93778220(L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void StartToEndMode::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StartToEndMode_LocalUpdate_m32AD115AA6F60553B6B0B1FE196DCF7155C19FE1 (StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (target == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___target_2;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		// return;
		return;
	}

IL_000f:
	{
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, obj_position , Time.deltaTime * 5f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_2 = ____camera0;
		NullCheck(L_2);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_3;
		L_3 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_2, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_4 = ____camera0;
		NullCheck(L_4);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
		L_5 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_4, NULL);
		NullCheck(L_5);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_5, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = __this->___obj_position_13;
		float L_8;
		L_8 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		L_9 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_6, L_7, ((float)il2cpp_codegen_multiply(L_8, (5.0f))), NULL);
		NullCheck(L_3);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_3, L_9, NULL);
		// obj_quaternion = Quaternion.LookRotation(target.position -  obj_position , Vector3.up);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___target_2;
		NullCheck(L_10);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_10, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12 = __this->___obj_position_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_11, L_12, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_15;
		L_15 = Quaternion_LookRotation_mE6859FEBE85BC0AE72A14159988151FF69BF4401(L_13, L_14, NULL);
		__this->___obj_quaternion_14 = L_15;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, obj_quaternion, Time.deltaTime * 5f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_16 = ____camera0;
		NullCheck(L_16);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_17;
		L_17 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_16, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_18 = ____camera0;
		NullCheck(L_18);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_19;
		L_19 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_18, NULL);
		NullCheck(L_19);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_20;
		L_20 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_19, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_21 = __this->___obj_quaternion_14;
		float L_22;
		L_22 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_23;
		L_23 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_20, L_21, ((float)il2cpp_codegen_multiply(L_22, (5.0f))), NULL);
		NullCheck(L_17);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_17, L_23, NULL);
		// }
		return;
	}
}
// System.Void StartToEndMode::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StartToEndMode__ctor_m6737649C2652021444D48FCEAAE9DE93C67D8234 (StartToEndMode_t6D7D33795210B777D196577CB15510E18A914338* __this, const RuntimeMethod* method) 
{
	{
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
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
// System.Void TeamEditCamera::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TeamEditCamera__ctor_mB3D6BAFA8F1FC894260017770112A28B7B2D61D3 (TeamEditCamera_tCD912CE0B0950259EDA0CEA3351EF1FEAA085D9F* __this, float ___distance0, float ___height1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Vector3 direction = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___direction_15 = L_0;
		// public TeamEditCamera(float distance, float height)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// targets = new List<Transform>();
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*)il2cpp_codegen_object_new(List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268(L_1, List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4), (void*)L_1);
		// this.distance = distance;
		float L_2 = ___distance0;
		__this->___distance_13 = L_2;
		// this.height = height;
		float L_3 = ___height1;
		__this->___height_14 = L_3;
		// }
		return;
	}
}
// System.Void TeamEditCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TeamEditCamera_LocalUpdate_m2532273A8D03CC85E23FE6C76C6B587EBC41609C (TeamEditCamera_tCD912CE0B0950259EDA0CEA3351EF1FEAA085D9F* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	float V_1 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_4;
	memset((&V_4), 0, sizeof(V_4));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_5 = NULL;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_6;
	memset((&V_6), 0, sizeof(V_6));
	{
		// if (this.targets == null)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		// return;
		return;
	}

IL_0009:
	{
		// if (this.targets.Count == 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_1, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0017;
		}
	}
	{
		// return;
		return;
	}

IL_0017:
	{
		// Vector3 center = new Vector3(0, 0, 0);
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&V_0), (0.0f), (0.0f), (0.0f), NULL);
		// foreach (Transform o in this.targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_3 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_3);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_4;
		L_4 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_3, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_4 = L_4;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_008a:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_4), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_007f_1;
			}

IL_003c_1:
			{
				// foreach (Transform o in this.targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_5;
				L_5 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_4), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_5 = L_5;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6 = V_5;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_7;
				L_7 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_6, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_7)
				{
					goto IL_007f_1;
				}
			}
			{
				// center += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = V_0;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_9 = V_5;
				NullCheck(L_9);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_10;
				L_10 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_9, NULL);
				NullCheck(L_10);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
				L_11 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_10, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
				L_12 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_8, L_11, NULL);
				V_0 = L_12;
				// direction += o.transform.forward;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = __this->___direction_15;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14 = V_5;
				NullCheck(L_14);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_15;
				L_15 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_14, NULL);
				NullCheck(L_15);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16;
				L_16 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_15, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_17;
				L_17 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_13, L_16, NULL);
				__this->___direction_15 = L_17;
			}

IL_007f_1:
			{
				// foreach (Transform o in this.targets)
				bool L_18;
				L_18 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_4), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_18)
				{
					goto IL_003c_1;
				}
			}
			{
				goto IL_0098;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0098:
	{
		// center /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19 = V_0;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_20 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_20);
		int32_t L_21;
		L_21 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_20, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_19, ((float)L_21), NULL);
		V_0 = L_22;
		// float speed = 1f;
		V_1 = (1.0f);
		// if (_camera.transform.position != center - direction * distance + new Vector3(0, height, 0))
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_23 = ____camera0;
		NullCheck(L_23);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_24;
		L_24 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_23, NULL);
		NullCheck(L_24);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_24, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = __this->___direction_15;
		float L_28 = __this->___distance_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_27, L_28, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30;
		L_30 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_26, L_29, NULL);
		float L_31 = __this->___height_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_32;
		memset((&L_32), 0, sizeof(L_32));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_32), (0.0f), L_31, (0.0f), /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_30, L_32, NULL);
		bool L_34;
		L_34 = Vector3_op_Inequality_m6A7FB1C9E9DE194708997BFA24C6E238D92D908E_inline(L_25, L_33, NULL);
		if (!L_34)
		{
			goto IL_0150;
		}
	}
	{
		// Vector3 to = center - direction.normalized * distance + new Vector3(0, height, 0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_36 = (&__this->___direction_15);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_37;
		L_37 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_36, NULL);
		float L_38 = __this->___distance_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39;
		L_39 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_37, L_38, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_35, L_39, NULL);
		float L_41 = __this->___height_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42;
		memset((&L_42), 0, sizeof(L_42));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_42), (0.0f), L_41, (0.0f), /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_43;
		L_43 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_40, L_42, NULL);
		V_6 = L_43;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, speed * Time.deltaTime);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_44 = ____camera0;
		NullCheck(L_44);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_45;
		L_45 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_44, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_46 = ____camera0;
		NullCheck(L_46);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_47;
		L_47 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_46, NULL);
		NullCheck(L_47);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_47, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_49 = V_6;
		float L_50 = V_1;
		float L_51;
		L_51 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52;
		L_52 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_48, L_49, ((float)il2cpp_codegen_multiply(L_50, L_51)), NULL);
		NullCheck(L_45);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_45, L_52, NULL);
	}

IL_0150:
	{
		// Vector3 directionLook = center - _camera.transform.position;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_53 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_54 = ____camera0;
		NullCheck(L_54);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_55;
		L_55 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_54, NULL);
		NullCheck(L_55);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_56;
		L_56 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_55, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_57;
		L_57 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_53, L_56, NULL);
		V_2 = L_57;
		// Quaternion toRotation = Quaternion.FromToRotation(_camera.transform.forward, directionLook);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_58 = ____camera0;
		NullCheck(L_58);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_59;
		L_59 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_58, NULL);
		NullCheck(L_59);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_60;
		L_60 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_59, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_61 = V_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_62;
		L_62 = Quaternion_FromToRotation_m041093DBB23CB3641118310881D6B7746E3B8418(L_60, L_61, NULL);
		V_3 = L_62;
		// _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, toRotation, speed * Time.deltaTime);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_63 = ____camera0;
		NullCheck(L_63);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_64;
		L_64 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_63, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_65 = ____camera0;
		NullCheck(L_65);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_66;
		L_66 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_65, NULL);
		NullCheck(L_66);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_67;
		L_67 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_66, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_68 = V_3;
		float L_69 = V_1;
		float L_70;
		L_70 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_71;
		L_71 = Quaternion_Lerp_m7BE5A2D8FA33A15A5145B2F5261707CA17C3E792(L_67, L_68, ((float)il2cpp_codegen_multiply(L_69, L_70)), NULL);
		NullCheck(L_64);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_64, L_71, NULL);
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
// System.Void TopDownWatchCamera::.ctor(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TopDownWatchCamera__ctor_m5512D0733F363B6A2B64C0BC302B4A6EEF3F9AB8 (TopDownWatchCamera_tB31CB6E39C34F1D87B22F4B4D6E2171F68934989* __this, float ___height0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public float minRotation = -35f;
		__this->___minRotation_14 = (-35.0f);
		// public float maxRotation = 35f;
		__this->___maxRotation_15 = (35.0f);
		// readonly float turnSmoothing = 0.1f;
		__this->___turnSmoothing_16 = (0.100000001f);
		// float smoothX = 0.1f;
		__this->___smoothX_17 = (0.100000001f);
		// float smoothY = 0.1f;
		__this->___smoothY_18 = (0.100000001f);
		// float smoothXVelocity = 0.1f;
		__this->___smoothXVelocity_19 = (0.100000001f);
		// float smoothYVelocity = 0.1f;
		__this->___smoothYVelocity_20 = (0.100000001f);
		// public TopDownWatchCamera(float height)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// targets = new List<Transform>();
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*)il2cpp_codegen_object_new(List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268(L_0, List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4), (void*)L_0);
		// this.height = height;
		float L_1 = ___height0;
		__this->___height_13 = L_1;
		// }
		return;
	}
}
// System.Void TopDownWatchCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TopDownWatchCamera_LocalUpdate_mFD0BE7F02AA0545E241AF70A7B901B98A9034783 (TopDownWatchCamera_tB31CB6E39C34F1D87B22F4B4D6E2171F68934989* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral265E15F1F86F1C766555899D5771CF29055DE75A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8AF7B9D6121033ED1DE80EFA3688A7998521AB1F);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	float V_3 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_4;
	memset((&V_4), 0, sizeof(V_4));
	float V_5 = 0.0f;
	float V_6 = 0.0f;
	{
		// pos = new Vector3(_camera.transform.position.x, height, _camera.transform.position.z);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ____camera0;
		NullCheck(L_0);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1;
		L_1 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_0, NULL);
		NullCheck(L_1);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_1, NULL);
		float L_3 = L_2.___x_2;
		float L_4 = __this->___height_13;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = ____camera0;
		NullCheck(L_5);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
		L_6 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_5, NULL);
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_6, NULL);
		float L_8 = L_7.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9;
		memset((&L_9), 0, sizeof(L_9));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_9), L_3, L_4, L_8, /*hidden argument*/NULL);
		__this->___pos_24 = L_9;
		// Vector3 use_direction = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		V_2 = L_10;
		// float k = 0f;
		V_3 = (0.0f);
		// if (_camera != null)
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_11 = ____camera0;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_12;
		L_12 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_11, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_12)
		{
			goto IL_00f9;
		}
	}
	{
		// screenMovementSpace = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_13 = ____camera0;
		NullCheck(L_13);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_14;
		L_14 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_13, NULL);
		NullCheck(L_14);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15;
		L_15 = Transform_get_eulerAngles_mCAAF48EFCF628F1ED91C2FFE75A4FD19C039DD6A(L_14, NULL);
		float L_16 = L_15.___y_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_17;
		L_17 = Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline((0.0f), L_16, (0.0f), NULL);
		// screenMovementForward = screenMovementSpace * Vector3.forward;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_18 = L_17;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
		L_19 = Vector3_get_forward_mEBAB24D77FC02FC88ED880738C3B1D47C758B3EB_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_20;
		L_20 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_18, L_19, NULL);
		V_0 = L_20;
		// screenMovementRight = screenMovementSpace * Vector3.right;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_get_right_m13B7C3EAA64DC921EC23346C56A5A597B5481FF5_inline(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_18, L_21, NULL);
		V_1 = L_22;
		// h = UnityEngine.Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_23;
		L_23 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral7F8C014BD4810CC276D0F9F81A1E759C7B098B1E, NULL);
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_24;
		L_24 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___h_25 = ((float)il2cpp_codegen_add(L_23, L_24));
		// v = UnityEngine.Input.GetAxis("Vertical") + UltimateJoystick.GetVerticalAxis("RotateCamera");
		float L_25;
		L_25 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral265E15F1F86F1C766555899D5771CF29055DE75A, NULL);
		float L_26;
		L_26 = UltimateJoystick_GetVerticalAxis_mEE877C1F115E2601643900464D8C1093AE878798(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		__this->___v_26 = ((float)il2cpp_codegen_add(L_25, L_26));
		// use_direction = (screenMovementForward * v) + (screenMovementRight * h);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27 = V_0;
		float L_28 = __this->___v_26;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_27, L_28, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = V_1;
		float L_31 = __this->___h_25;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_32;
		L_32 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_30, L_31, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_33;
		L_33 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_29, L_32, NULL);
		V_2 = L_33;
		// k += UltimateJoystick.GetHorizontalAxis("joystick") * speed * Time.deltaTime / (Time.deltaTime + 0.2f);
		float L_34 = V_3;
		float L_35;
		L_35 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral8AF7B9D6121033ED1DE80EFA3688A7998521AB1F, NULL);
		float L_36 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_37;
		L_37 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_38;
		L_38 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		V_3 = ((float)il2cpp_codegen_add(L_34, ((float)(((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_multiply(L_35, L_36)), L_37))/((float)il2cpp_codegen_add(L_38, (0.200000003f)))))));
	}

IL_00f9:
	{
		// Vector3 to = pos + use_direction.normalized * speed;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_39 = __this->___pos_24;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_40;
		L_40 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_2), NULL);
		float L_41 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_42;
		L_42 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_40, L_41, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_43;
		L_43 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_39, L_42, NULL);
		V_4 = L_43;
		// if (_camera.transform.position != to)
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_44 = ____camera0;
		NullCheck(L_44);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_45;
		L_45 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_44, NULL);
		NullCheck(L_45);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_46;
		L_46 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_45, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47 = V_4;
		bool L_48;
		L_48 = Vector3_op_Inequality_m6A7FB1C9E9DE194708997BFA24C6E238D92D908E_inline(L_46, L_47, NULL);
		if (!L_48)
		{
			goto IL_0161;
		}
	}
	{
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, speed * Time.deltaTime / (Time.deltaTime + 0.2f));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_49 = ____camera0;
		NullCheck(L_49);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_50;
		L_50 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_49, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_51 = ____camera0;
		NullCheck(L_51);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_52;
		L_52 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_51, NULL);
		NullCheck(L_52);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_53;
		L_53 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_52, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_54 = V_4;
		float L_55 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_56;
		L_56 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_57;
		L_57 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_58;
		L_58 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_53, L_54, ((float)(((float)il2cpp_codegen_multiply(L_55, L_56))/((float)il2cpp_codegen_add(L_57, (0.200000003f))))), NULL);
		NullCheck(L_50);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_50, L_58, NULL);
	}

IL_0161:
	{
		// float horizontal = Input.GetAxis("Mouse X");
		float L_59;
		L_59 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral88BEE283254D7094E258B3A88730F4CC4F1E4AC7, NULL);
		V_5 = L_59;
		// float vertical = Input.GetAxis("Mouse Y");
		float L_60;
		L_60 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteral16DD21BE77B115D392226EB71A2D3A9FDC29E3F0, NULL);
		V_6 = L_60;
		// RotateCamera(vertical, horizontal, 2f, _camera);
		float L_61 = V_6;
		float L_62 = V_5;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_63 = ____camera0;
		TopDownWatchCamera_RotateCamera_m891C60B0C0936BC0DDCBAADBB44C2513714F54BD(__this, L_61, L_62, (2.0f), L_63, NULL);
		// }
		return;
	}
}
// System.Void TopDownWatchCamera::RotateCamera(System.Single,System.Single,System.Single,UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TopDownWatchCamera_RotateCamera_m891C60B0C0936BC0DDCBAADBB44C2513714F54BD (TopDownWatchCamera_tB31CB6E39C34F1D87B22F4B4D6E2171F68934989* __this, float ___vert0, float ___horz1, float ___camTargetSpeed2, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera3, const RuntimeMethod* method) 
{
	{
		// if (turnSmoothing > 0)
		float L_0 = __this->___turnSmoothing_16;
		if ((!(((float)L_0) > ((float)(0.0f)))))
		{
			goto IL_004b;
		}
	}
	{
		// smoothX = Mathf.SmoothDamp(smoothX, horz, ref smoothXVelocity, turnSmoothing);
		float L_1 = __this->___smoothX_17;
		float L_2 = ___horz1;
		float* L_3 = (&__this->___smoothXVelocity_19);
		float L_4 = __this->___turnSmoothing_16;
		float L_5;
		L_5 = Mathf_SmoothDamp_m4B8C5AACFEBF58E93FF2A33832C27EF1E5AF7AFD_inline(L_1, L_2, L_3, L_4, NULL);
		__this->___smoothX_17 = L_5;
		// smoothY = Mathf.SmoothDamp(smoothY, vert, ref smoothYVelocity, turnSmoothing);
		float L_6 = __this->___smoothY_18;
		float L_7 = ___vert0;
		float* L_8 = (&__this->___smoothYVelocity_20);
		float L_9 = __this->___turnSmoothing_16;
		float L_10;
		L_10 = Mathf_SmoothDamp_m4B8C5AACFEBF58E93FF2A33832C27EF1E5AF7AFD_inline(L_6, L_7, L_8, L_9, NULL);
		__this->___smoothY_18 = L_10;
		goto IL_0059;
	}

IL_004b:
	{
		// smoothX = horz;
		float L_11 = ___horz1;
		__this->___smoothX_17 = L_11;
		// smoothY = vert;
		float L_12 = ___vert0;
		__this->___smoothY_18 = L_12;
	}

IL_0059:
	{
		// tiltRotation -= smoothY * camTargetSpeed;
		float L_13 = __this->___tiltRotation_23;
		float L_14 = __this->___smoothY_18;
		float L_15 = ___camTargetSpeed2;
		__this->___tiltRotation_23 = ((float)il2cpp_codegen_subtract(L_13, ((float)il2cpp_codegen_multiply(L_14, L_15))));
		// tiltRotation = Mathf.Clamp(tiltRotation, minRotation, maxRotation);
		float L_16 = __this->___tiltRotation_23;
		float L_17 = __this->___minRotation_14;
		float L_18 = __this->___maxRotation_15;
		float L_19;
		L_19 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_16, L_17, L_18, NULL);
		__this->___tiltRotation_23 = L_19;
		// lookRotation += smoothX * camTargetSpeed;
		float L_20 = __this->___lookRotation_22;
		float L_21 = __this->___smoothX_17;
		float L_22 = ___camTargetSpeed2;
		__this->___lookRotation_22 = ((float)il2cpp_codegen_add(L_20, ((float)il2cpp_codegen_multiply(L_21, L_22))));
		// _camera.transform.rotation = Quaternion.Euler(tiltRotation, lookRotation, 0);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_23 = ____camera3;
		NullCheck(L_23);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_24;
		L_24 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_23, NULL);
		float L_25 = __this->___tiltRotation_23;
		float L_26 = __this->___lookRotation_22;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_27;
		L_27 = Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline(L_25, L_26, (0.0f), NULL);
		NullCheck(L_24);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_24, L_27, NULL);
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
// System.Single TouchTopDownCamera::get_Height()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float TouchTopDownCamera_get_Height_m6A6A94345B3716F3AA84538F7D4B6F03E4CCD4D2 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, const RuntimeMethod* method) 
{
	{
		// get => height;
		float L_0 = __this->___height_14;
		return L_0;
	}
}
// System.Void TouchTopDownCamera::set_Height(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_set_Height_mE0D463B145814F3ADFCF49B7399251756717CBCA (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, float ___value0, const RuntimeMethod* method) 
{
	{
		// set => height = Mathf.Clamp(value, 10, 20);
		float L_0 = ___value0;
		float L_1;
		L_1 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_0, (10.0f), (20.0f), NULL);
		__this->___height_14 = L_1;
		return;
	}
}
// System.Void TouchTopDownCamera::.ctor(System.Single,System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera__ctor_m937F9275C5485EB3574AABC647DD50ABCC1A430B (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, float ___height0, float ___battlefieldDiameter1, float ___fieldOfView2, const RuntimeMethod* method) 
{
	{
		// float startPosSetDuration = 0.3f;
		__this->___startPosSetDuration_13 = (0.300000012f);
		// private float followTargetSpeed = 10f;
		__this->___followTargetSpeed_22 = (10.0f);
		// float rotationSpeed = 0.5f;
		__this->___rotationSpeed_23 = (0.5f);
		// private float disAwayFromFront = 15.5f;
		__this->___disAwayFromFront_25 = (15.5f);
		// private float zoomScreenDis = 10;
		__this->___zoomScreenDis_26 = (10.0f);
		// private float zoomSpeed = 20;
		__this->___zoomSpeed_27 = (20.0f);
		// public TouchTopDownCamera(float height, float battlefieldDiameter, float fieldOfView)// height == 9
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// this.height = height;
		float L_0 = ___height0;
		__this->___height_14 = L_0;
		// this.battlefieldDiameter = battlefieldDiameter;
		float L_1 = ___battlefieldDiameter1;
		__this->___battlefieldDiameter_15 = L_1;
		// this.fieldOfView = fieldOfView;
		float L_2 = ___fieldOfView2;
		__this->___fieldOfView_28 = L_2;
		// }
		return;
	}
}
// System.Void TouchTopDownCamera::Enter(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_Enter_mB52FF4E44A0B4058D5AD5FB39ABF66F13D0DFE72 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TweenExtensions_Play_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mAE376A6BE21D1F94CE5EAA4DA0C1683A7D6DFDE7_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TweenSettingsExtensions_OnStart_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mCCE914E78193AFF17F77999963371587BAD452E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__0_m3186DC723944338480A2B848E7B36DE2F5ABB70E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__1_m9F7688B45302FA836246EB0B35829E085E4185D3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_0 = (U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass21_0__ctor_m30817E4037C2E0D5D9F8F414B0C960212FB0E61B(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_1 = V_0;
		NullCheck(L_1);
		L_1->___U3CU3E4__this_0 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3E4__this_0), (void*)__this);
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_2 = V_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_3 = ____camera0;
		NullCheck(L_2);
		L_2->____camera_2 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->____camera_2), (void*)L_3);
		// _camera.fieldOfView = this.fieldOfView;
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_4 = V_0;
		NullCheck(L_4);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = L_4->____camera_2;
		float L_6 = __this->___fieldOfView_28;
		NullCheck(L_5);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_5, L_6, NULL);
		// CameraManager._subCamera.fieldOfView = this.fieldOfView;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_7 = ((CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_StaticFields*)il2cpp_codegen_static_fields_for(CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB_il2cpp_TypeInfo_var))->____subCamera_5;
		float L_8 = __this->___fieldOfView_28;
		NullCheck(L_7);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_7, L_8, NULL);
		// Vector3 temp = Vector3.zero;
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_9 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		NullCheck(L_9);
		L_9->___temp_1 = L_10;
		// Height = cameraManager.TopDownModeEndRef.position.y;
		CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* L_11 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___cameraManager_0;
		NullCheck(L_11);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_12;
		L_12 = CameraManager_get_TopDownModeEndRef_mC510D9320204B96C91DBBBEE4EB2835E31B41327_inline(L_11, NULL);
		NullCheck(L_12);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
		L_13 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_12, NULL);
		float L_14 = L_13.___y_3;
		TouchTopDownCamera_set_Height_mE0D463B145814F3ADFCF49B7399251756717CBCA(__this, L_14, NULL);
		// mainSequence = DOTween.Sequence().OnStart(() =>
		// {
		//     canTouch = false;
		//     sameHeightCenter = new Vector3(0,height,0);
		//     temp = _camera.transform.forward;
		//     temp.y = 0;
		// }).Append(_camera.transform.DOMove(cameraManager.TopDownModeEndRef.position, startPosSetDuration)).
		// Join(_camera.transform.DORotateQuaternion(cameraManager.TopDownModeEndRef.rotation, startPosSetDuration)).
		// AppendCallback(() =>
		// {
		//     zoomScreenDis = Screen.width / 7;
		//     canTouch = true;
		// });
		il2cpp_codegen_runtime_class_init_inline(DOTween_t96369E1D40ABE93A56308F57DEA6B04219C66D13_il2cpp_TypeInfo_var);
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_15;
		L_15 = DOTween_Sequence_m57CE12901581E3C5832EAFFB11C1417270E01754(NULL);
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_16 = V_0;
		TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* L_17 = (TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24*)il2cpp_codegen_object_new(TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24_il2cpp_TypeInfo_var);
		NullCheck(L_17);
		TweenCallback__ctor_m68CC9304423CBDE43001F9B1413B5DAAF70DB621(L_17, L_16, (intptr_t)((void*)U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__0_m3186DC723944338480A2B848E7B36DE2F5ABB70E_RuntimeMethod_var), NULL);
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_18;
		L_18 = TweenSettingsExtensions_OnStart_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mCCE914E78193AFF17F77999963371587BAD452E5(L_15, L_17, TweenSettingsExtensions_OnStart_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mCCE914E78193AFF17F77999963371587BAD452E5_RuntimeMethod_var);
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_19 = V_0;
		NullCheck(L_19);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_20 = L_19->____camera_2;
		NullCheck(L_20);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_21;
		L_21 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_20, NULL);
		CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* L_22 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___cameraManager_0;
		NullCheck(L_22);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_23;
		L_23 = CameraManager_get_TopDownModeEndRef_mC510D9320204B96C91DBBBEE4EB2835E31B41327_inline(L_22, NULL);
		NullCheck(L_23);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_23, NULL);
		float L_25 = __this->___startPosSetDuration_13;
		TweenerCore_3_tCD82DFC45FB71C681FA8659EA63A7D7D16BFFE77* L_26;
		L_26 = ShortcutExtensions_DOMove_m32C4BD3E44498A3C651F30108F0D3402416B868B(L_21, L_24, L_25, (bool)0, NULL);
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_27;
		L_27 = TweenSettingsExtensions_Append_mB8CDE24E0410A61DA0D5AD083F8047C18AED3D68(L_18, L_26, NULL);
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_28 = V_0;
		NullCheck(L_28);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_29 = L_28->____camera_2;
		NullCheck(L_29);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_30;
		L_30 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_29, NULL);
		CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* L_31 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___cameraManager_0;
		NullCheck(L_31);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_32;
		L_32 = CameraManager_get_TopDownModeEndRef_mC510D9320204B96C91DBBBEE4EB2835E31B41327_inline(L_31, NULL);
		NullCheck(L_32);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_33;
		L_33 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_32, NULL);
		float L_34 = __this->___startPosSetDuration_13;
		TweenerCore_3_t9A48A35EB4763F174321ED1A1BE49A67BC0A5C6F* L_35;
		L_35 = ShortcutExtensions_DORotateQuaternion_m18A2982A27F3B18F3D738CEFEB15DED04EB6E9AA(L_30, L_33, L_34, NULL);
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_36;
		L_36 = TweenSettingsExtensions_Join_m197C0D892B0D9763AE9E4C09F2A9EBFFC2882EA0(L_27, L_35, NULL);
		U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* L_37 = V_0;
		TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24* L_38 = (TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24*)il2cpp_codegen_object_new(TweenCallback_t7C8B8A38E7B30905FF1B83C943256EF23617BB24_il2cpp_TypeInfo_var);
		NullCheck(L_38);
		TweenCallback__ctor_m68CC9304423CBDE43001F9B1413B5DAAF70DB621(L_38, L_37, (intptr_t)((void*)U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__1_m9F7688B45302FA836246EB0B35829E085E4185D3_RuntimeMethod_var), NULL);
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_39;
		L_39 = TweenSettingsExtensions_AppendCallback_m0AF8553D233D9803D3C45C2AC976D363EF42EB91(L_36, L_38, NULL);
		__this->___mainSequence_19 = L_39;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mainSequence_19), (void*)L_39);
		// mainSequence.Play();
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_40 = __this->___mainSequence_19;
		Sequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C* L_41;
		L_41 = TweenExtensions_Play_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mAE376A6BE21D1F94CE5EAA4DA0C1683A7D6DFDE7(L_40, TweenExtensions_Play_TisSequence_tEADBE56D6ED2E9EE8FB2E5459C3E57131EC0545C_mAE376A6BE21D1F94CE5EAA4DA0C1683A7D6DFDE7_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void TouchTopDownCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_LocalUpdate_mAEE596A6C319E0D4EBE4638AE3689BD3F0EC5F24 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFC6687DC37346CD2569888E29764F727FAF530E0);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass24_0_tF67F393E60EBEB75B219167CBCB8A8DC6B7F9C3B V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_3;
	memset((&V_3), 0, sizeof(V_3));
	float V_4 = 0.0f;
	float V_5 = 0.0f;
	Touch_t03E51455ED508492B3F278903A0114FA0E87B417 V_6;
	memset((&V_6), 0, sizeof(V_6));
	float V_7 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_8;
	memset((&V_8), 0, sizeof(V_8));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_9;
	memset((&V_9), 0, sizeof(V_9));
	{
		// if (!canTouch)
		bool L_0 = __this->___canTouch_20;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		// return;
		return;
	}

IL_0009:
	{
		// var RotateCameraH = UltimateJoystick.GetHorizontalAxis("RotateCamera");
		il2cpp_codegen_runtime_class_init_inline(UltimateJoystick_tDA9ECC0340DC18C29FD9F8963A69139832D8A1D2_il2cpp_TypeInfo_var);
		float L_1;
		L_1 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		(&V_0)->___RotateCameraH_0 = L_1;
		// var RotateCameraV = UltimateJoystick.GetHorizontalAxis("RotateCamera");
		float L_2;
		L_2 = UltimateJoystick_GetHorizontalAxis_m4E164B22D5F68CB8EB8CA6412ADC355CB3FE727A(_stringLiteral2259984EF8B65D89F2777F5C61B9B2172430599B, NULL);
		(&V_0)->___RotateCameraV_1 = L_2;
		// if (Input.touchCount >= 2)
		int32_t L_3;
		L_3 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((((int32_t)L_3) < ((int32_t)2)))
		{
			goto IL_01e2;
		}
	}
	{
		// Touch t1 = Input.GetTouch (0);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_4;
		L_4 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_2 = L_4;
		// Touch t2 = Input.GetTouch (1);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_5;
		L_5 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(1, NULL);
		V_3 = L_5;
		// if (t2.phase == TouchPhase.Began)
		int32_t L_6;
		L_6 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_3), NULL);
		if (L_6)
		{
			goto IL_009a;
		}
	}
	{
		// backDist = Vector2.Distance (t1.position, t2.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_7;
		L_7 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_2), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_8;
		L_8 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_3), NULL);
		float L_9;
		L_9 = Vector2_Distance_m220B2ADBE9F87426BEEE291263560DFE78F835B5_inline(L_7, L_8, NULL);
		__this->___backDist_30 = L_9;
		// firstPoint = t2.position;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_10;
		L_10 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_3), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11;
		L_11 = Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline(L_10, NULL);
		__this->___firstPoint_16 = L_11;
		// startFromPointWhenDrag = camera.transform.position;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_12 = ___camera0;
		NullCheck(L_12);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_13;
		L_13 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_12, NULL);
		NullCheck(L_13);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_13, NULL);
		__this->___startFromPointWhenDrag_18 = L_14;
		// startCameraHeight = height;
		float L_15 = __this->___height_14;
		__this->___startCameraHeight_31 = L_15;
		goto IL_03b5;
	}

IL_009a:
	{
		// else if ((t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved) && (t1.phase != TouchPhase.Ended && t2.phase != TouchPhase.Ended))
		int32_t L_16;
		L_16 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_2), NULL);
		if ((((int32_t)L_16) == ((int32_t)1)))
		{
			goto IL_00b1;
		}
	}
	{
		int32_t L_17;
		L_17 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_3), NULL);
		if ((!(((uint32_t)L_17) == ((uint32_t)1))))
		{
			goto IL_01c3;
		}
	}

IL_00b1:
	{
		int32_t L_18;
		L_18 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_2), NULL);
		if ((((int32_t)L_18) == ((int32_t)3)))
		{
			goto IL_01c3;
		}
	}
	{
		int32_t L_19;
		L_19 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_3), NULL);
		if ((((int32_t)L_19) == ((int32_t)3)))
		{
			goto IL_01c3;
		}
	}
	{
		// var afterDist = Vector2.Distance (t1.position, t2.position);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_20;
		L_20 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_2), NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_21;
		L_21 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_3), NULL);
		float L_22;
		L_22 = Vector2_Distance_m220B2ADBE9F87426BEEE291263560DFE78F835B5_inline(L_20, L_21, NULL);
		V_4 = L_22;
		// if (Mathf.Abs(afterDist - backDist) >  zoomScreenDis)
		float L_23 = V_4;
		float L_24 = __this->___backDist_30;
		float L_25;
		L_25 = fabsf(((float)il2cpp_codegen_subtract(L_23, L_24)));
		float L_26 = __this->___zoomScreenDis_26;
		if ((!(((float)L_25) > ((float)L_26))))
		{
			goto IL_0182;
		}
	}
	{
		// if (afterDist > backDist)
		float L_27 = V_4;
		float L_28 = __this->___backDist_30;
		if ((!(((float)L_27) > ((float)L_28))))
		{
			goto IL_0118;
		}
	}
	{
		// deltaHeight = - (afterDist - backDist - zoomScreenDis);
		float L_29 = V_4;
		float L_30 = __this->___backDist_30;
		float L_31 = __this->___zoomScreenDis_26;
		V_5 = ((-((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_subtract(L_29, L_30)), L_31))));
		goto IL_012a;
	}

IL_0118:
	{
		// deltaHeight = backDist - afterDist - zoomScreenDis;
		float L_32 = __this->___backDist_30;
		float L_33 = V_4;
		float L_34 = __this->___zoomScreenDis_26;
		V_5 = ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_subtract(L_32, L_33)), L_34));
	}

IL_012a:
	{
		// Height = startCameraHeight + (deltaHeight / Screen.height) * zoomSpeed;
		float L_35 = __this->___startCameraHeight_31;
		float L_36 = V_5;
		int32_t L_37;
		L_37 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		float L_38 = __this->___zoomSpeed_27;
		TouchTopDownCamera_set_Height_mE0D463B145814F3ADFCF49B7399251756717CBCA(__this, ((float)il2cpp_codegen_add(L_35, ((float)il2cpp_codegen_multiply(((float)(L_36/((float)L_37))), L_38)))), NULL);
		// camera.transform.position = new Vector3(camera.transform.position.x,Height, camera.transform.position.z);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_39 = ___camera0;
		NullCheck(L_39);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_40;
		L_40 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_39, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_41 = ___camera0;
		NullCheck(L_41);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_42;
		L_42 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_41, NULL);
		NullCheck(L_42);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_43;
		L_43 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_42, NULL);
		float L_44 = L_43.___x_2;
		float L_45;
		L_45 = TouchTopDownCamera_get_Height_m6A6A94345B3716F3AA84538F7D4B6F03E4CCD4D2_inline(__this, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_46 = ___camera0;
		NullCheck(L_46);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_47;
		L_47 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_46, NULL);
		NullCheck(L_47);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48;
		L_48 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_47, NULL);
		float L_49 = L_48.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		memset((&L_50), 0, sizeof(L_50));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_50), L_44, L_45, L_49, /*hidden argument*/NULL);
		NullCheck(L_40);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_40, L_50, NULL);
		goto IL_03b5;
	}

IL_0182:
	{
		// secondPoint = t2.position;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_51;
		L_51 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_3), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_52;
		L_52 = Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline(L_51, NULL);
		__this->___secondPoint_17 = L_52;
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_53 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_54;
		L_54 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_53, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_54)
		{
			goto IL_03b5;
		}
	}
	{
		// CameraDrag(camera, startFromPointWhenDrag, firstPoint, secondPoint);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_55 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_56 = __this->___startFromPointWhenDrag_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_57 = __this->___firstPoint_16;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_58 = __this->___secondPoint_17;
		TouchTopDownCamera_CameraDrag_mAA66A76D28AD22E4AC983C8FD04698415DFB32CE(__this, L_55, L_56, L_57, L_58, NULL);
		goto IL_03b5;
	}

IL_01c3:
	{
		// else if (t1.phase == TouchPhase.Ended || t2.phase == TouchPhase.Ended)
		int32_t L_59;
		L_59 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_2), NULL);
		if ((((int32_t)L_59) == ((int32_t)3)))
		{
			goto IL_03b5;
		}
	}
	{
		int32_t L_60;
		L_60 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_3), NULL);
		if ((!(((uint32_t)L_60) == ((uint32_t)3))))
		{
			goto IL_03b5;
		}
	}
	{
		goto IL_03b5;
	}

IL_01e2:
	{
		// else if (Input.touchCount >= 1)
		int32_t L_61;
		L_61 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((((int32_t)L_61) < ((int32_t)1)))
		{
			goto IL_0280;
		}
	}
	{
		// Touch t1 = Input.GetTouch (0);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_62;
		L_62 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		V_6 = L_62;
		// if (t1.phase == TouchPhase.Began) // ????
		int32_t L_63;
		L_63 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_6), NULL);
		if (L_63)
		{
			goto IL_021c;
		}
	}
	{
		// firstPoint = t1.position;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_64;
		L_64 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_6), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_65;
		L_65 = Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline(L_64, NULL);
		__this->___firstPoint_16 = L_65;
		// isRotating = true;
		__this->___isRotating_24 = (bool)1;
		goto IL_03b5;
	}

IL_021c:
	{
		// else if (t1.phase == TouchPhase.Moved && isRotating) // ????
		int32_t L_66;
		L_66 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_6), NULL);
		if ((!(((uint32_t)L_66) == ((uint32_t)1))))
		{
			goto IL_0267;
		}
	}
	{
		bool L_67 = __this->___isRotating_24;
		if (!L_67)
		{
			goto IL_0267;
		}
	}
	{
		// if (OnPad())
		bool L_68;
		L_68 = TouchTopDownCamera_U3CLocalUpdateU3Eg__OnPadU7C24_0_m647D9C5EC4AC9A7477BCD57EAEDAD9E6E6DAE73B((&V_0), NULL);
		if (!L_68)
		{
			goto IL_0250;
		}
	}
	{
		// CameraRotate(camera, firstPoint, t1.position);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_69 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_70 = __this->___firstPoint_16;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_71;
		L_71 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_6), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_72;
		L_72 = Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline(L_71, NULL);
		TouchTopDownCamera_CameraRotate_m30F7A499D44B8F2EA6B024D9B4013B590F20EF35(__this, L_69, L_70, L_72, NULL);
	}

IL_0250:
	{
		// firstPoint = t1.position;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_73;
		L_73 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A((&V_6), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_74;
		L_74 = Vector2_op_Implicit_mCD214B04BC52AED3C89C3BEF664B6247E5F8954A_inline(L_73, NULL);
		__this->___firstPoint_16 = L_74;
		goto IL_03b5;
	}

IL_0267:
	{
		// else if (t1.phase == TouchPhase.Ended) // ????
		int32_t L_75;
		L_75 = Touch_get_phase_mB82409FB2BE1C32ABDBA6A72E52A099D28AB70B0((&V_6), NULL);
		if ((!(((uint32_t)L_75) == ((uint32_t)3))))
		{
			goto IL_03b5;
		}
	}
	{
		// isRotating = false;
		__this->___isRotating_24 = (bool)0;
		goto IL_03b5;
	}

IL_0280:
	{
		// float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
		float L_76;
		L_76 = Input_GetAxis_m1F49B26F24032F45FB4583C95FB24E6771A161D4(_stringLiteralFC6687DC37346CD2569888E29764F727FAF530E0, NULL);
		V_7 = L_76;
		// if (scrollWheelValue != 0)
		float L_77 = V_7;
		if ((((float)L_77) == ((float)(0.0f))))
		{
			goto IL_02df;
		}
	}
	{
		// Height += scrollWheelValue;
		float L_78;
		L_78 = TouchTopDownCamera_get_Height_m6A6A94345B3716F3AA84538F7D4B6F03E4CCD4D2_inline(__this, NULL);
		float L_79 = V_7;
		TouchTopDownCamera_set_Height_mE0D463B145814F3ADFCF49B7399251756717CBCA(__this, ((float)il2cpp_codegen_add(L_78, L_79)), NULL);
		// camera.transform.position = new Vector3(camera.transform.position.x,Height, camera.transform.position.z);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_80 = ___camera0;
		NullCheck(L_80);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_81;
		L_81 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_80, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_82 = ___camera0;
		NullCheck(L_82);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_83;
		L_83 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_82, NULL);
		NullCheck(L_83);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_84;
		L_84 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_83, NULL);
		float L_85 = L_84.___x_2;
		float L_86;
		L_86 = TouchTopDownCamera_get_Height_m6A6A94345B3716F3AA84538F7D4B6F03E4CCD4D2_inline(__this, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_87 = ___camera0;
		NullCheck(L_87);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_88;
		L_88 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_87, NULL);
		NullCheck(L_88);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_89;
		L_89 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_88, NULL);
		float L_90 = L_89.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_91;
		memset((&L_91), 0, sizeof(L_91));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_91), L_85, L_86, L_90, /*hidden argument*/NULL);
		NullCheck(L_81);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_81, L_91, NULL);
		goto IL_03b5;
	}

IL_02df:
	{
		// else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
		bool L_92;
		L_92 = Input_GetMouseButtonDown_m33522C56A54C402FE6DED802DD7E53435C27A5DE(0, NULL);
		if (L_92)
		{
			goto IL_02ef;
		}
	}
	{
		bool L_93;
		L_93 = Input_GetMouseButton_mE545CF4B790C6E202808B827E3141BEC3330DB70(0, NULL);
		if (!L_93)
		{
			goto IL_0352;
		}
	}

IL_02ef:
	{
		// if (Input.GetMouseButtonDown(0))
		bool L_94;
		L_94 = Input_GetMouseButtonDown_m33522C56A54C402FE6DED802DD7E53435C27A5DE(0, NULL);
		if (!L_94)
		{
			goto IL_0313;
		}
	}
	{
		// firstPoint = Input.mousePosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_95;
		L_95 = Input_get_mousePosition_m2414B43222ED0C5FAB960D393964189AFD21EEAD(NULL);
		__this->___firstPoint_16 = L_95;
		// startFromPointWhenDrag = camera.transform.position;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_96 = ___camera0;
		NullCheck(L_96);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_97;
		L_97 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_96, NULL);
		NullCheck(L_97);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_98;
		L_98 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_97, NULL);
		__this->___startFromPointWhenDrag_18 = L_98;
	}

IL_0313:
	{
		// if (Input.GetMouseButton(0))
		bool L_99;
		L_99 = Input_GetMouseButton_mE545CF4B790C6E202808B827E3141BEC3330DB70(0, NULL);
		if (!L_99)
		{
			goto IL_03b5;
		}
	}
	{
		// secondPoint = Input.mousePosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_100;
		L_100 = Input_get_mousePosition_m2414B43222ED0C5FAB960D393964189AFD21EEAD(NULL);
		__this->___secondPoint_17 = L_100;
		// if (meCenter == null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_101 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_102;
		L_102 = Object_op_Equality_mD3DB0D72CE0250C84033DC2A90AEF9D59896E536(L_101, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_102)
		{
			goto IL_03b5;
		}
	}
	{
		// CameraDrag(camera, startFromPointWhenDrag, firstPoint, secondPoint);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_103 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_104 = __this->___startFromPointWhenDrag_18;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_105 = __this->___firstPoint_16;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_106 = __this->___secondPoint_17;
		TouchTopDownCamera_CameraDrag_mAA66A76D28AD22E4AC983C8FD04698415DFB32CE(__this, L_103, L_104, L_105, L_106, NULL);
		goto IL_03b5;
	}

IL_0352:
	{
		// if (Input.GetMouseButtonDown(1))
		bool L_107;
		L_107 = Input_GetMouseButtonDown_m33522C56A54C402FE6DED802DD7E53435C27A5DE(1, NULL);
		if (!L_107)
		{
			goto IL_036e;
		}
	}
	{
		// firstPoint = Input.mousePosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_108;
		L_108 = Input_get_mousePosition_m2414B43222ED0C5FAB960D393964189AFD21EEAD(NULL);
		__this->___firstPoint_16 = L_108;
		// isRotating = true;
		__this->___isRotating_24 = (bool)1;
		goto IL_03b5;
	}

IL_036e:
	{
		// else if (Input.GetMouseButton(1) && isRotating) // ????
		bool L_109;
		L_109 = Input_GetMouseButton_mE545CF4B790C6E202808B827E3141BEC3330DB70(1, NULL);
		if (!L_109)
		{
			goto IL_03a6;
		}
	}
	{
		bool L_110 = __this->___isRotating_24;
		if (!L_110)
		{
			goto IL_03a6;
		}
	}
	{
		// if (OnPad())
		bool L_111;
		L_111 = TouchTopDownCamera_U3CLocalUpdateU3Eg__OnPadU7C24_0_m647D9C5EC4AC9A7477BCD57EAEDAD9E6E6DAE73B((&V_0), NULL);
		if (!L_111)
		{
			goto IL_0399;
		}
	}
	{
		// CameraRotate(camera, firstPoint, Input.mousePosition);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_112 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_113 = __this->___firstPoint_16;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_114;
		L_114 = Input_get_mousePosition_m2414B43222ED0C5FAB960D393964189AFD21EEAD(NULL);
		TouchTopDownCamera_CameraRotate_m30F7A499D44B8F2EA6B024D9B4013B590F20EF35(__this, L_112, L_113, L_114, NULL);
	}

IL_0399:
	{
		// firstPoint = Input.mousePosition;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_115;
		L_115 = Input_get_mousePosition_m2414B43222ED0C5FAB960D393964189AFD21EEAD(NULL);
		__this->___firstPoint_16 = L_115;
		goto IL_03b5;
	}

IL_03a6:
	{
		// else if (Input.GetMouseButtonUp(1))
		bool L_116;
		L_116 = Input_GetMouseButtonUp_m69FCCF4E6D2F0E4E9B310D1ED2AD5A6927A8C081(1, NULL);
		if (!L_116)
		{
			goto IL_03b5;
		}
	}
	{
		// isRotating = false;
		__this->___isRotating_24 = (bool)0;
	}

IL_03b5:
	{
		// if (meCenter != null)
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_117 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_118;
		L_118 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_117, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_118)
		{
			goto IL_0422;
		}
	}
	{
		// var mePosOnGround= meCenter.position;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_119 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___meCenter_1;
		NullCheck(L_119);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_120;
		L_120 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_119, NULL);
		V_8 = L_120;
		// mePosOnGround.y = groundHeight;
		float L_121 = __this->___groundHeight_21;
		(&V_8)->___y_3 = L_121;
		// camera.transform.position = Vector3.Lerp(camera.transform.position, mePosOnGround + (camera.transform.position - GetCenterScreenGroundPoint(camera)), Time.deltaTime * followTargetSpeed);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_122 = ___camera0;
		NullCheck(L_122);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_123;
		L_123 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_122, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_124 = ___camera0;
		NullCheck(L_124);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_125;
		L_125 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_124, NULL);
		NullCheck(L_125);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_126;
		L_126 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_125, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_127 = V_8;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_128 = ___camera0;
		NullCheck(L_128);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_129;
		L_129 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_128, NULL);
		NullCheck(L_129);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_130;
		L_130 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_129, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_131 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_132;
		L_132 = TouchTopDownCamera_GetCenterScreenGroundPoint_mAF6D401F535BF52FA7B7947C80FDD6E2DB5CA407(__this, L_131, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_133;
		L_133 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_130, L_132, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_134;
		L_134 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_127, L_133, NULL);
		float L_135;
		L_135 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_136 = __this->___followTargetSpeed_22;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_137;
		L_137 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_126, L_134, ((float)il2cpp_codegen_multiply(L_135, L_136)), NULL);
		NullCheck(L_123);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_123, L_137, NULL);
	}

IL_0422:
	{
		// var cameraFront = camera.transform.forward;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_138 = ___camera0;
		NullCheck(L_138);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_139;
		L_139 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_138, NULL);
		NullCheck(L_139);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_140;
		L_140 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_139, NULL);
		V_1 = L_140;
		// cameraFront.y = 0;
		(&V_1)->___y_3 = (0.0f);
		// cameraFront = cameraFront.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_141;
		L_141 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_1), NULL);
		V_1 = L_141;
		// sameHeightCenter = new Vector3(0, camera.transform.position.y, 0) - cameraFront * disAwayFromFront;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_142 = ___camera0;
		NullCheck(L_142);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_143;
		L_143 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_142, NULL);
		NullCheck(L_143);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_144;
		L_144 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_143, NULL);
		float L_145 = L_144.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_146;
		memset((&L_146), 0, sizeof(L_146));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_146), (0.0f), L_145, (0.0f), /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_147 = V_1;
		float L_148 = __this->___disAwayFromFront_25;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_149;
		L_149 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_147, L_148, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_150;
		L_150 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_146, L_149, NULL);
		__this->___sameHeightCenter_29 = L_150;
		// if (Vector3.Distance(camera.transform.position, sameHeightCenter) > battlefieldDiameter / 2)
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_151 = ___camera0;
		NullCheck(L_151);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_152;
		L_152 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_151, NULL);
		NullCheck(L_152);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_153;
		L_153 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_152, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_154 = __this->___sameHeightCenter_29;
		float L_155;
		L_155 = Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline(L_153, L_154, NULL);
		float L_156 = __this->___battlefieldDiameter_15;
		if ((!(((float)L_155) > ((float)((float)(L_156/(2.0f)))))))
		{
			goto IL_04e6;
		}
	}
	{
		// camera.transform.position = sameHeightCenter +
		//                             (camera.transform.position - sameHeightCenter).normalized *
		//                             battlefieldDiameter / 2;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_157 = ___camera0;
		NullCheck(L_157);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_158;
		L_158 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_157, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_159 = __this->___sameHeightCenter_29;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_160 = ___camera0;
		NullCheck(L_160);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_161;
		L_161 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_160, NULL);
		NullCheck(L_161);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_162;
		L_162 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_161, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_163 = __this->___sameHeightCenter_29;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_164;
		L_164 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_162, L_163, NULL);
		V_9 = L_164;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_165;
		L_165 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_9), NULL);
		float L_166 = __this->___battlefieldDiameter_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_167;
		L_167 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_165, L_166, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_168;
		L_168 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_167, (2.0f), NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_169;
		L_169 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_159, L_168, NULL);
		NullCheck(L_158);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_158, L_169, NULL);
	}

IL_04e6:
	{
		// }
		return;
	}
}
// System.Void TouchTopDownCamera::CameraDrag(UnityEngine.Camera,UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_CameraDrag_mAA66A76D28AD22E4AC983C8FD04698415DFB32CE (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___startPoint1, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____firstPoint2, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____secondPoint3, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_3;
	memset((&V_3), 0, sizeof(V_3));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_4;
	memset((&V_4), 0, sizeof(V_4));
	{
		// var transform = camera.transform;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ___camera0;
		NullCheck(L_0);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1;
		L_1 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_0, NULL);
		// var Right = transform.right;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_2 = L_1;
		NullCheck(L_2);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Transform_get_right_mC6DC057C23313802E2186A9E0DB760D795A758A4(L_2, NULL);
		V_0 = L_3;
		// var Front = transform.forward;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4 = L_2;
		NullCheck(L_4);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_4, NULL);
		V_1 = L_5;
		// Front.y = 0;
		(&V_1)->___y_3 = (0.0f);
		// Front = Front.normalized;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_1), NULL);
		V_1 = L_6;
		// var rightDirectionMove = battlefieldDiameter * (-(_secondPoint.x - _firstPoint.x) / Screen.width)  * Right;
		float L_7 = __this->___battlefieldDiameter_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ____secondPoint3;
		float L_9 = L_8.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ____firstPoint2;
		float L_11 = L_10.___x_2;
		int32_t L_12;
		L_12 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
		L_14 = Vector3_op_Multiply_m29F4414A9D30B7C0CD8455C4B2F049E8CCF66745_inline(((float)il2cpp_codegen_multiply(L_7, ((float)(((-((float)il2cpp_codegen_subtract(L_9, L_11))))/((float)L_12))))), L_13, NULL);
		V_2 = L_14;
		// var forwardDirectionMove = battlefieldDiameter * (-(_secondPoint.y - _firstPoint.y) / Screen.height)  * Front;
		float L_15 = __this->___battlefieldDiameter_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_16 = ____secondPoint3;
		float L_17 = L_16.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18 = ____firstPoint2;
		float L_19 = L_18.___y_3;
		int32_t L_20;
		L_20 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_op_Multiply_m29F4414A9D30B7C0CD8455C4B2F049E8CCF66745_inline(((float)il2cpp_codegen_multiply(L_15, ((float)(((-((float)il2cpp_codegen_subtract(L_17, L_19))))/((float)L_20))))), L_21, NULL);
		V_3 = L_22;
		// var position = startPoint + rightDirectionMove + forwardDirectionMove;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = ___startPoint1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = V_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_25;
		L_25 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_23, L_24, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26 = V_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_27;
		L_27 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_25, L_26, NULL);
		V_4 = L_27;
		// transform.position = position;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28 = V_4;
		NullCheck(L_4);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_4, L_28, NULL);
		// }
		return;
	}
}
// System.Void TouchTopDownCamera::CameraRotate(UnityEngine.Camera,UnityEngine.Vector3,UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TouchTopDownCamera_CameraRotate_m30F7A499D44B8F2EA6B024D9B4013B590F20EF35 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____firstPoint1, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ____secondPoint2, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// float deltaX = _secondPoint.x - _firstPoint.x;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ____secondPoint2;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ____firstPoint1;
		float L_3 = L_2.___x_2;
		// float rotationAngle = deltaX * rotationSpeed;
		float L_4 = __this->___rotationSpeed_23;
		V_0 = ((float)il2cpp_codegen_multiply(((float)il2cpp_codegen_subtract(L_1, L_3)), L_4));
		// Vector3 pivotPoint = GetCenterScreenGroundPoint(camera);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = ___camera0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = TouchTopDownCamera_GetCenterScreenGroundPoint_mAF6D401F535BF52FA7B7947C80FDD6E2DB5CA407(__this, L_5, NULL);
		V_1 = L_6;
		// camera.transform.RotateAround(pivotPoint, Vector3.up, rotationAngle);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_7 = ___camera0;
		NullCheck(L_7);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8;
		L_8 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_7, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = V_1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10;
		L_10 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		float L_11 = V_0;
		NullCheck(L_8);
		Transform_RotateAround_m489C5BE8B8B15D0A5F4863DE6D23FF2CC8FA76C6(L_8, L_9, L_10, L_11, NULL);
		// }
		return;
	}
}
// UnityEngine.Vector3 TouchTopDownCamera::GetCenterScreenGroundPoint(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 TouchTopDownCamera_GetCenterScreenGroundPoint_mAF6D401F535BF52FA7B7947C80FDD6E2DB5CA407 (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___camera0, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	float V_4 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_5;
	memset((&V_5), 0, sizeof(V_5));
	{
		// Vector3 cameraPosition = camera.transform.position;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_0 = ___camera0;
		NullCheck(L_0);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_1;
		L_1 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_0, NULL);
		NullCheck(L_1);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2;
		L_2 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_1, NULL);
		// Vector3 cameraForward = camera.transform.forward;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_3 = ___camera0;
		NullCheck(L_3);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_4;
		L_4 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_3, NULL);
		NullCheck(L_4);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5;
		L_5 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_4, NULL);
		V_0 = L_5;
		// float heightDifference = cameraPosition.y - groundHeight;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = L_2;
		float L_7 = L_6.___y_3;
		float L_8 = __this->___groundHeight_21;
		// Vector3 cameraForwardXZ = new Vector3(cameraForward.x, 0, cameraForward.z);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_9 = V_0;
		float L_10 = L_9.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_11 = V_0;
		float L_12 = L_11.___z_4;
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&V_1), L_10, (0.0f), L_12, NULL);
		// float theta = Vector3.Angle(cameraForward, cameraForwardXZ);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14 = V_1;
		float L_15;
		L_15 = Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline(L_13, L_14, NULL);
		V_2 = L_15;
		// float thetaRad = Mathf.Deg2Rad * theta;
		float L_16 = V_2;
		V_3 = ((float)il2cpp_codegen_multiply((0.0174532924f), L_16));
		// float distanceToGround = heightDifference / Mathf.Tan(thetaRad);
		float L_17 = V_3;
		float L_18;
		L_18 = tanf(L_17);
		V_4 = ((float)(((float)il2cpp_codegen_subtract(L_7, L_8))/L_18));
		// Vector3 groundPoint = cameraPosition + cameraForwardXZ.normalized * distanceToGround;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
		L_19 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline((&V_1), NULL);
		float L_20 = V_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21;
		L_21 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_19, L_20, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		L_22 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_6, L_21, NULL);
		V_5 = L_22;
		// groundPoint.y = groundHeight;
		float L_23 = __this->___groundHeight_21;
		(&V_5)->___y_3 = L_23;
		// return groundPoint;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = V_5;
		return L_24;
	}
}
// System.Boolean TouchTopDownCamera::<LocalUpdate>g__OnPad|24_0(TouchTopDownCamera/<>c__DisplayClass24_0&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TouchTopDownCamera_U3CLocalUpdateU3Eg__OnPadU7C24_0_m647D9C5EC4AC9A7477BCD57EAEDAD9E6E6DAE73B (U3CU3Ec__DisplayClass24_0_tF67F393E60EBEB75B219167CBCB8A8DC6B7F9C3B* p0, const RuntimeMethod* method) 
{
	{
		// return RotateCameraH != 0 || RotateCameraV != 0;
		U3CU3Ec__DisplayClass24_0_tF67F393E60EBEB75B219167CBCB8A8DC6B7F9C3B* L_0 = p0;
		float L_1 = L_0->___RotateCameraH_0;
		if ((!(((float)L_1) == ((float)(0.0f)))))
		{
			goto IL_001e;
		}
	}
	{
		U3CU3Ec__DisplayClass24_0_tF67F393E60EBEB75B219167CBCB8A8DC6B7F9C3B* L_2 = p0;
		float L_3 = L_2->___RotateCameraV_1;
		return (bool)((((int32_t)((((float)L_3) == ((float)(0.0f)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_001e:
	{
		return (bool)1;
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
// System.Void TouchTopDownCamera/<>c__DisplayClass21_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0__ctor_m30817E4037C2E0D5D9F8F414B0C960212FB0E61B (U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void TouchTopDownCamera/<>c__DisplayClass21_0::<Enter>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__0_m3186DC723944338480A2B848E7B36DE2F5ABB70E (U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* __this, const RuntimeMethod* method) 
{
	{
		// canTouch = false;
		TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* L_0 = __this->___U3CU3E4__this_0;
		NullCheck(L_0);
		L_0->___canTouch_20 = (bool)0;
		// sameHeightCenter = new Vector3(0,height,0);
		TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* L_1 = __this->___U3CU3E4__this_0;
		TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* L_2 = __this->___U3CU3E4__this_0;
		NullCheck(L_2);
		float L_3 = L_2->___height_14;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		memset((&L_4), 0, sizeof(L_4));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_4), (0.0f), L_3, (0.0f), /*hidden argument*/NULL);
		NullCheck(L_1);
		L_1->___sameHeightCenter_29 = L_4;
		// temp = _camera.transform.forward;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_5 = __this->____camera_2;
		NullCheck(L_5);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_6;
		L_6 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_5, NULL);
		NullCheck(L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_6, NULL);
		__this->___temp_1 = L_7;
		// temp.y = 0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_8 = (&__this->___temp_1);
		L_8->___y_3 = (0.0f);
		// }).Append(_camera.transform.DOMove(cameraManager.TopDownModeEndRef.position, startPosSetDuration)).
		return;
	}
}
// System.Void TouchTopDownCamera/<>c__DisplayClass21_0::<Enter>b__1()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0_U3CEnterU3Eb__1_m9F7688B45302FA836246EB0B35829E085E4185D3 (U3CU3Ec__DisplayClass21_0_tDBC7E1AFB612EC7A780C66D8EF6E721316D2BD66* __this, const RuntimeMethod* method) 
{
	{
		// zoomScreenDis = Screen.width / 7;
		TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* L_0 = __this->___U3CU3E4__this_0;
		int32_t L_1;
		L_1 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		NullCheck(L_0);
		L_0->___zoomScreenDis_26 = ((float)((int32_t)(L_1/7)));
		// canTouch = true;
		TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* L_2 = __this->___U3CU3E4__this_0;
		NullCheck(L_2);
		L_2->___canTouch_20 = (bool)1;
		// });
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
// System.Void WatchOverCamera::.ctor(System.Single,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WatchOverCamera__ctor_mE0D279ADD573E59356B82DF63111893484DC046C (WatchOverCamera_t0B35C99643DDFF8D8D63153C55E4862BAEC5C7F6* __this, float ___distance0, float ___height1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Vector3 direction = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0;
		L_0 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___direction_13 = L_0;
		// public WatchOverCamera(float distance, float height)
		CameraMode__ctor_m429E34EF0CAD251390E5B044323896C3DC6541B8(__this, NULL);
		// targets = new List<Transform>();
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = (List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D*)il2cpp_codegen_object_new(List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268(L_1, List_1__ctor_mDC3E95DC5C927A867B9B42EDE1945F909B894268_RuntimeMethod_var);
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4), (void*)L_1);
		// XZDis = distance;
		float L_2 = ___distance0;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7 = L_2;
		// YDis = height;
		float L_3 = ___height1;
		((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8 = L_3;
		// }
		return;
	}
}
// System.Void WatchOverCamera::LocalUpdate(UnityEngine.Camera)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WatchOverCamera_LocalUpdate_mC6C82EA890FD9CF994E6E8CE397D9CF572ABD921 (WatchOverCamera_t0B35C99643DDFF8D8D63153C55E4862BAEC5C7F6* __this, Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ____camera0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D V_1;
	memset((&V_1), 0, sizeof(V_1));
	Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* V_2 = NULL;
	{
		// if (targets == null || targets.Count == 0)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		if (!L_0)
		{
			goto IL_0015;
		}
	}
	{
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_1 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_1, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_0016;
		}
	}

IL_0015:
	{
		// return;
		return;
	}

IL_0016:
	{
		// center = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		L_3 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___center_15 = L_3;
		// direction = Vector3.zero;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		__this->___direction_13 = L_4;
		// foreach (Transform o in this.targets)
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_5 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_5);
		Enumerator_t519AE1DAA64E517296768BEA2E732ED47F76A91D L_6;
		L_6 = List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5(L_5, List_1_GetEnumerator_m01FCD3FC513065087F7E312BC9DE2D1C3FF655E5_RuntimeMethod_var);
		V_1 = L_6;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_008e:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5((&V_1), Enumerator_Dispose_m9BF6C1C74CD711998DC8FAE5D6B8083586F5CFB5_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0083_1;
			}

IL_003a_1:
			{
				// foreach (Transform o in this.targets)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_7;
				L_7 = Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_inline((&V_1), Enumerator_get_Current_mCBBD283BB42C56D73B7C4194020EC95292B36129_RuntimeMethod_var);
				V_2 = L_7;
				// if (o != null)
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_8 = V_2;
				il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
				bool L_9;
				L_9 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_8, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
				if (!L_9)
				{
					goto IL_0083_1;
				}
			}
			{
				// center += o.transform.position;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = __this->___center_15;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11 = V_2;
				NullCheck(L_11);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_12;
				L_12 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_11, NULL);
				NullCheck(L_12);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13;
				L_13 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_12, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_14;
				L_14 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_10, L_13, NULL);
				__this->___center_15 = L_14;
				// direction += o.transform.forward;
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_15 = __this->___direction_13;
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_16 = V_2;
				NullCheck(L_16);
				Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_17;
				L_17 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_16, NULL);
				NullCheck(L_17);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_18;
				L_18 = Transform_get_forward_mFCFACF7165FDAB21E80E384C494DF278386CEE2F(L_17, NULL);
				Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_19;
				L_19 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_15, L_18, NULL);
				__this->___direction_13 = L_19;
			}

IL_0083_1:
			{
				// foreach (Transform o in this.targets)
				bool L_20;
				L_20 = Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87((&V_1), Enumerator_MoveNext_mBAA697FE341E389C86536D9444A3E4AC02109E87_RuntimeMethod_var);
				if (L_20)
				{
					goto IL_003a_1;
				}
			}
			{
				goto IL_009c;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_009c:
	{
		// center /= targets.Count;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_21 = __this->___center_15;
		List_1_t991BBC5A1D51F59A450367DF944DAA207F22D06D* L_22 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___targets_4;
		NullCheck(L_22);
		int32_t L_23;
		L_23 = List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_inline(L_22, List_1_get_Count_mB5E64608D47703A98476E026480AE38671047C87_RuntimeMethod_var);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24;
		L_24 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_21, ((float)L_23), NULL);
		__this->___center_15 = L_24;
		// direction =  Quaternion.AngleAxis(XZrosOffset, Vector3.up) * direction;
		float L_25 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZrosOffset_9;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_26;
		L_26 = Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_27;
		L_27 = Quaternion_AngleAxis_m01A869DC10F976FAF493B66F15D6D6977BB61DA8(L_25, L_26, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_28 = __this->___direction_13;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_29;
		L_29 = Quaternion_op_Multiply_mF1348668A6CCD46FBFF98D39182F89358ED74AC0(L_27, L_28, NULL);
		__this->___direction_13 = L_29;
		// Vector3 to = center + direction.normalized * XZDis + new Vector3(0, YDis, 0);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_30 = __this->___center_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* L_31 = (&__this->___direction_13);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_32;
		L_32 = Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline(L_31, NULL);
		float L_33 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_34;
		L_34 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_32, L_33, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_35;
		L_35 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_30, L_34, NULL);
		float L_36 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___YDis_8;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_37;
		memset((&L_37), 0, sizeof(L_37));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_37), (0.0f), L_36, (0.0f), /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_38;
		L_38 = Vector3_op_Addition_m087D6F0EC60843D455F9F83D25FE42B2433AAD1D_inline(L_35, L_37, NULL);
		V_0 = L_38;
		// _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, speed * Time.deltaTime);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_39 = ____camera0;
		NullCheck(L_39);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_40;
		L_40 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_39, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_41 = ____camera0;
		NullCheck(L_41);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_42;
		L_42 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_41, NULL);
		NullCheck(L_42);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_43;
		L_43 = Transform_get_position_m69CD5FA214FDAE7BB701552943674846C220FDE1(L_42, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_44 = V_0;
		float L_45 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___speed_6;
		float L_46;
		L_46 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_47;
		L_47 = Vector3_Lerp_m57EE8D709A93B2B0FF8D499FA2947B1D61CB1FD6_inline(L_43, L_44, ((float)il2cpp_codegen_multiply(L_45, L_46)), NULL);
		NullCheck(L_40);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_40, L_47, NULL);
		// ToRotation = Quaternion.LookRotation(center - to);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_48 = __this->___center_15;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_49 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_50;
		L_50 = Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline(L_48, L_49, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_51;
		L_51 = Quaternion_LookRotation_m8C0F294E5143F93D378E020EAD9DA2288A5907A3(L_50, NULL);
		__this->___ToRotation_14 = L_51;
		// _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.2f + Time.deltaTime));
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_52 = ____camera0;
		NullCheck(L_52);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_53;
		L_53 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_52, NULL);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_54 = ____camera0;
		NullCheck(L_54);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_55;
		L_55 = Component_get_transform_m2919A1D81931E6932C7F06D4C2F0AB8DDA9A5371(L_54, NULL);
		NullCheck(L_55);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_56;
		L_56 = Transform_get_rotation_m32AF40CA0D50C797DA639A696F8EAEC7524C179C(L_55, NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_57 = __this->___ToRotation_14;
		float L_58;
		L_58 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		float L_59;
		L_59 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_60;
		L_60 = Quaternion_Slerp_m5FDA8C178E7EB209B43845F73263AFE9C02F3949(L_56, L_57, ((float)(L_58/((float)il2cpp_codegen_add((0.200000003f), L_59)))), NULL);
		NullCheck(L_53);
		Transform_set_rotation_m61340DE74726CF0F9946743A727C4D444397331D(L_53, L_60, NULL);
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
// System.Void PinchZoom::LocalUpdate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PinchZoom_LocalUpdate_mA32783EE7D21A545383AEAC49CC920A4C09AEBF8 (PinchZoom_t94309269E7A55D4CA4DDD01EB4CB93A0B47CFEDD* __this, const RuntimeMethod* method) 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// if (Input.touchCount == 2)
		int32_t L_0;
		L_0 = Input_get_touchCount_m7B8EAAB3449A6DC2D90AF3BA36AF226D97C020CF(NULL);
		if ((!(((uint32_t)L_0) == ((uint32_t)2))))
		{
			goto IL_01c0;
		}
	}
	{
		// _touchZero = UnityEngine.Input.GetTouch(0);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_1;
		L_1 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(0, NULL);
		__this->____touchZero_3 = L_1;
		// _touchOne = UnityEngine.Input.GetTouch(1);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417 L_2;
		L_2 = Input_GetTouch_m37572A728DAE284D3ED1272690E635A61D167AD4(1, NULL);
		__this->____touchOne_4 = L_2;
		// _touchZeroScreenPosX = _touchZero.position.x / Screen.width;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_3 = (&__this->____touchZero_3);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4;
		L_4 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(L_3, NULL);
		float L_5 = L_4.___x_0;
		int32_t L_6;
		L_6 = Screen_get_width_mCA5D955A53CF6D29C8C7118D517D0FC84AE8056C(NULL);
		__this->____touchZeroScreenPosX_5 = ((float)(L_5/((float)L_6)));
		// _touchZeroScreenPosY = _touchZero.position.y / Screen.height;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_7 = (&__this->____touchZero_3);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_8;
		L_8 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(L_7, NULL);
		float L_9 = L_8.___y_1;
		int32_t L_10;
		L_10 = Screen_get_height_m624DD2D53F34087064E3B9D09AC2207DB4E86CA8(NULL);
		__this->____touchZeroScreenPosY_6 = ((float)(L_9/((float)L_10)));
		// if (_touchZeroScreenPosX < 0.1f || _touchZeroScreenPosX > 0.5f || _touchZeroScreenPosY < 0.1f || _touchZeroScreenPosY > 0.8f)
		float L_11 = __this->____touchZeroScreenPosX_5;
		if ((((float)L_11) < ((float)(0.100000001f))))
		{
			goto IL_0091;
		}
	}
	{
		float L_12 = __this->____touchZeroScreenPosX_5;
		if ((((float)L_12) > ((float)(0.5f))))
		{
			goto IL_0091;
		}
	}
	{
		float L_13 = __this->____touchZeroScreenPosY_6;
		if ((((float)L_13) < ((float)(0.100000001f))))
		{
			goto IL_0091;
		}
	}
	{
		float L_14 = __this->____touchZeroScreenPosY_6;
		if ((!(((float)L_14) > ((float)(0.800000012f)))))
		{
			goto IL_0092;
		}
	}

IL_0091:
	{
		// return;// ??????????????????????????zoom???
		return;
	}

IL_0092:
	{
		// _touchZeroPrevPos = _touchZero.position - _touchZero.deltaPosition;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_15 = (&__this->____touchZero_3);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_16;
		L_16 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(L_15, NULL);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_17 = (&__this->____touchZero_3);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_18;
		L_18 = Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299(L_17, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_19;
		L_19 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_16, L_18, NULL);
		__this->____touchZeroPrevPos_7 = L_19;
		// _touchOnePrevPos = _touchOne.position - _touchOne.deltaPosition;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_20 = (&__this->____touchOne_4);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_21;
		L_21 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(L_20, NULL);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_22 = (&__this->____touchOne_4);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_23;
		L_23 = Touch_get_deltaPosition_m2D51F960B74C94821ED0F6A09E44C80FD796D299(L_22, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_24;
		L_24 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_21, L_23, NULL);
		__this->____touchOnePrevPos_8 = L_24;
		// _prevTouchDeltaMag = (_touchZeroPrevPos - _touchOnePrevPos).magnitude;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_25 = __this->____touchZeroPrevPos_7;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_26 = __this->____touchOnePrevPos_8;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_27;
		L_27 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_25, L_26, NULL);
		V_0 = L_27;
		float L_28;
		L_28 = Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline((&V_0), NULL);
		__this->____prevTouchDeltaMag_9 = L_28;
		// _touchDeltaMag = (_touchZero.position - _touchOne.position).magnitude;
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_29 = (&__this->____touchZero_3);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_30;
		L_30 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(L_29, NULL);
		Touch_t03E51455ED508492B3F278903A0114FA0E87B417* L_31 = (&__this->____touchOne_4);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_32;
		L_32 = Touch_get_position_m41B9EB0F3F3E1BE98CEB388253A9E31979CB964A(L_31, NULL);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_33;
		L_33 = Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline(L_30, L_32, NULL);
		V_0 = L_33;
		float L_34;
		L_34 = Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline((&V_0), NULL);
		__this->____touchDeltaMag_10 = L_34;
		// _deltaMagnitudeDiff = _prevTouchDeltaMag - _touchDeltaMag;
		float L_35 = __this->____prevTouchDeltaMag_9;
		float L_36 = __this->____touchDeltaMag_10;
		__this->____deltaMagnitudeDiff_11 = ((float)il2cpp_codegen_subtract(L_35, L_36));
		// if (camera.orthographic)
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_37 = __this->___camera_0;
		NullCheck(L_37);
		bool L_38;
		L_38 = Camera_get_orthographic_m904DEFC76C54DA4E30C20A62A86D5D87B7D4DD8F(L_37, NULL);
		if (!L_38)
		{
			goto IL_017c;
		}
	}
	{
		// camera.orthographicSize += _deltaMagnitudeDiff * _orthoZoomSpeed;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_39 = __this->___camera_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_40 = L_39;
		NullCheck(L_40);
		float L_41;
		L_41 = Camera_get_orthographicSize_m7950C5627086253E02992A43ADFE59039DB473F8(L_40, NULL);
		float L_42 = __this->____deltaMagnitudeDiff_11;
		float L_43 = __this->____orthoZoomSpeed_2;
		NullCheck(L_40);
		Camera_set_orthographicSize_m76DD021032ACB3DDBD052B75EC66DCE3A7295A5C(L_40, ((float)il2cpp_codegen_add(L_41, ((float)il2cpp_codegen_multiply(L_42, L_43)))), NULL);
		// camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_44 = __this->___camera_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_45 = __this->___camera_0;
		NullCheck(L_45);
		float L_46;
		L_46 = Camera_get_orthographicSize_m7950C5627086253E02992A43ADFE59039DB473F8(L_45, NULL);
		float L_47;
		L_47 = Mathf_Max_mA9DCA91E87D6D27034F56ABA52606A9090406016_inline(L_46, (0.100000001f), NULL);
		NullCheck(L_44);
		Camera_set_orthographicSize_m76DD021032ACB3DDBD052B75EC66DCE3A7295A5C(L_44, L_47, NULL);
		return;
	}

IL_017c:
	{
		// camera.fieldOfView += _deltaMagnitudeDiff * _perspectiveZoomSpeed;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_48 = __this->___camera_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_49 = L_48;
		NullCheck(L_49);
		float L_50;
		L_50 = Camera_get_fieldOfView_m9A93F17BBF89F496AE231C21817AFD1C1E833FBB(L_49, NULL);
		float L_51 = __this->____deltaMagnitudeDiff_11;
		float L_52 = __this->____perspectiveZoomSpeed_1;
		NullCheck(L_49);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_49, ((float)il2cpp_codegen_add(L_50, ((float)il2cpp_codegen_multiply(L_51, L_52)))), NULL);
		// camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 15f, 25f);
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_53 = __this->___camera_0;
		Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* L_54 = __this->___camera_0;
		NullCheck(L_54);
		float L_55;
		L_55 = Camera_get_fieldOfView_m9A93F17BBF89F496AE231C21817AFD1C1E833FBB(L_54, NULL);
		float L_56;
		L_56 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(L_55, (15.0f), (25.0f), NULL);
		NullCheck(L_53);
		Camera_set_fieldOfView_m5AA9EED4D1603A1DEDBF883D9C42814B2BDEB777(L_53, L_56, NULL);
	}

IL_01c0:
	{
		// }
		return;
	}
}
// System.Void PinchZoom::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PinchZoom__ctor_mAAE60A9DD0F80622D8E447F7103BCB115B083AE7 (PinchZoom_t94309269E7A55D4CA4DDD01EB4CB93A0B47CFEDD* __this, const RuntimeMethod* method) 
{
	{
		// readonly float _perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
		__this->____perspectiveZoomSpeed_1 = (0.5f);
		// readonly float _orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
		__this->____orthoZoomSpeed_2 = (0.5f);
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
// System.Void C2TDemo::Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void C2TDemo_Start_mD765B45A4392D164562CCFFCCBB1CEBE44C809F6 (C2TDemo_tE3F1FE59F2C6D939EA56302F04A6CAC556272F57* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		s_Il2CppMethodInitialized = true;
	}
	{
		// input.text = "";
		InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* L_0 = __this->___input_5;
		NullCheck(L_0);
		InputField_set_text_m28B1C806BBCAC44F3ACCDC3B550509CA0C7D257F(L_0, _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709, NULL);
		// output.text = "";
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_1 = __this->___output_6;
		NullCheck(L_1);
		VirtualActionInvoker1< String_t* >::Invoke(75 /* System.Void UnityEngine.UI.Text::set_text(System.String) */, L_1, _stringLiteralDA39A3EE5E6B4B0D3255BFEF95601890AFD80709);
		// if(csv != null)
		TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* L_2 = __this->___csv_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Object_op_Inequality_m4D656395C27694A7F33F5AA8DE80A7AAF9E20BA7(L_2, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_3)
		{
			goto IL_0044;
		}
	}
	{
		// input.text = csv.text;
		InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* L_4 = __this->___input_5;
		TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* L_5 = __this->___csv_4;
		NullCheck(L_5);
		String_t* L_6;
		L_6 = TextAsset_get_text_m36846042E3CF3D9DD337BF3F8B2B1902D10C8FD9(L_5, NULL);
		NullCheck(L_4);
		InputField_set_text_m28B1C806BBCAC44F3ACCDC3B550509CA0C7D257F(L_4, L_6, NULL);
	}

IL_0044:
	{
		// }
		return;
	}
}
// System.Void C2TDemo::Generate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void C2TDemo_Generate_mB5F9343360AA61CFD336A32CC7456146EBAF1333 (C2TDemo_tE3F1FE59F2C6D939EA56302F04A6CAC556272F57* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF616212F742C2A1A279331136CF869CE0847A0C0);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	{
		// if(string.IsNullOrEmpty(input.text))
		InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* L_0 = __this->___input_5;
		NullCheck(L_0);
		String_t* L_1;
		L_1 = InputField_get_text_m6E0796350FF559505E4DF17311803962699D6704_inline(L_0, NULL);
		bool L_2;
		L_2 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_1, NULL);
		if (!L_2)
		{
			goto IL_0013;
		}
	}
	{
		// return;
		return;
	}

IL_0013:
	{
		// string code = TableCodeGen.Generate(input.text, "SampleTable");
		InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* L_3 = __this->___input_5;
		NullCheck(L_3);
		String_t* L_4;
		L_4 = InputField_get_text_m6E0796350FF559505E4DF17311803962699D6704_inline(L_3, NULL);
		String_t* L_5;
		L_5 = TableCodeGen_Generate_m1F932F34B8A82A84D17E39F9F49BBFFA3B38928D(L_4, _stringLiteralF616212F742C2A1A279331136CF869CE0847A0C0, NULL);
		V_0 = L_5;
		// output.text = code;
		Text_tD60B2346DAA6666BF0D822FF607F0B220C2B9E62* L_6 = __this->___output_6;
		String_t* L_7 = V_0;
		NullCheck(L_6);
		VirtualActionInvoker1< String_t* >::Invoke(75 /* System.Void UnityEngine.UI.Text::set_text(System.String) */, L_6, L_7);
		// }
		return;
	}
}
// System.Void C2TDemo::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void C2TDemo__ctor_m28037AFFB51F5996023B3C4C83DBCD19EB11527F (C2TDemo_tE3F1FE59F2C6D939EA56302F04A6CAC556272F57* __this, const RuntimeMethod* method) 
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
// System.Boolean SampleTable::IsLoaded()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SampleTable_IsLoaded_mFDE7DA430AF6CB81E3ABCBBB2744C982B79A37F6 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, const RuntimeMethod* method) 
{
	{
		// return isLoaded;
		bool L_0 = __this->___isLoaded_1;
		return L_0;
	}
}
// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::GetRowList()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* SampleTable_GetRowList_mEDD3F9C1186490A1384727D0A11EE31BE577F71F (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, const RuntimeMethod* method) 
{
	{
		// return rowList;
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_0 = __this->___rowList_0;
		return L_0;
	}
}
// System.Void SampleTable::Load(UnityEngine.TextAsset)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleTable_Load_mE1862A35E65C9C8AF70FB98F71788EEA0F5142C5 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* ___csv0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m9761F0D2ADF7CB1D17354DDC09E8F08DB70897EF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Clear_mAF59287F15E95C0F18D3E325B64FCAC82A7610A9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* V_0 = NULL;
	int32_t V_1 = 0;
	Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* V_2 = NULL;
	{
		// rowList.Clear();
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_0 = __this->___rowList_0;
		NullCheck(L_0);
		List_1_Clear_mAF59287F15E95C0F18D3E325B64FCAC82A7610A9_inline(L_0, List_1_Clear_mAF59287F15E95C0F18D3E325B64FCAC82A7610A9_RuntimeMethod_var);
		// string[][] grid = CsvParser2.Parse(csv.text);
		TextAsset_t2C64E93DA366D9DE5A8209E1802FA4884AC1BD69* L_1 = ___csv0;
		NullCheck(L_1);
		String_t* L_2;
		L_2 = TextAsset_get_text_m36846042E3CF3D9DD337BF3F8B2B1902D10C8FD9(L_1, NULL);
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_3;
		L_3 = CsvParser2_Parse_mD97CB56798836B1C073FCEDE3A2371BD0D870617(L_2, NULL);
		V_0 = L_3;
		// for(int i = 1 ; i < grid.Length ; i++)
		V_1 = 1;
		goto IL_0068;
	}

IL_001b:
	{
		// Row row = new Row();
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_4 = (Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC*)il2cpp_codegen_object_new(Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		Row__ctor_m2D008B8DB9286F8856252DE30136CFC350484D59(L_4, NULL);
		V_2 = L_4;
		// row.Year = grid[i][0];
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_5 = V_2;
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_6 = V_0;
		int32_t L_7 = V_1;
		NullCheck(L_6);
		int32_t L_8 = L_7;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_9 = (L_6)->GetAt(static_cast<il2cpp_array_size_t>(L_8));
		NullCheck(L_9);
		int32_t L_10 = 0;
		String_t* L_11 = (L_9)->GetAt(static_cast<il2cpp_array_size_t>(L_10));
		NullCheck(L_5);
		L_5->___Year_0 = L_11;
		Il2CppCodeGenWriteBarrier((void**)(&L_5->___Year_0), (void*)L_11);
		// row.Make = grid[i][1];
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_12 = V_2;
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_13 = V_0;
		int32_t L_14 = V_1;
		NullCheck(L_13);
		int32_t L_15 = L_14;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = (L_13)->GetAt(static_cast<il2cpp_array_size_t>(L_15));
		NullCheck(L_16);
		int32_t L_17 = 1;
		String_t* L_18 = (L_16)->GetAt(static_cast<il2cpp_array_size_t>(L_17));
		NullCheck(L_12);
		L_12->___Make_1 = L_18;
		Il2CppCodeGenWriteBarrier((void**)(&L_12->___Make_1), (void*)L_18);
		// row.Model = grid[i][2];
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_19 = V_2;
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_20 = V_0;
		int32_t L_21 = V_1;
		NullCheck(L_20);
		int32_t L_22 = L_21;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_23 = (L_20)->GetAt(static_cast<il2cpp_array_size_t>(L_22));
		NullCheck(L_23);
		int32_t L_24 = 2;
		String_t* L_25 = (L_23)->GetAt(static_cast<il2cpp_array_size_t>(L_24));
		NullCheck(L_19);
		L_19->___Model_2 = L_25;
		Il2CppCodeGenWriteBarrier((void**)(&L_19->___Model_2), (void*)L_25);
		// row.Description = grid[i][3];
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_26 = V_2;
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_27 = V_0;
		int32_t L_28 = V_1;
		NullCheck(L_27);
		int32_t L_29 = L_28;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_30 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_29));
		NullCheck(L_30);
		int32_t L_31 = 3;
		String_t* L_32 = (L_30)->GetAt(static_cast<il2cpp_array_size_t>(L_31));
		NullCheck(L_26);
		L_26->___Description_3 = L_32;
		Il2CppCodeGenWriteBarrier((void**)(&L_26->___Description_3), (void*)L_32);
		// row.Price = grid[i][4];
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_33 = V_2;
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_34 = V_0;
		int32_t L_35 = V_1;
		NullCheck(L_34);
		int32_t L_36 = L_35;
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_37 = (L_34)->GetAt(static_cast<il2cpp_array_size_t>(L_36));
		NullCheck(L_37);
		int32_t L_38 = 4;
		String_t* L_39 = (L_37)->GetAt(static_cast<il2cpp_array_size_t>(L_38));
		NullCheck(L_33);
		L_33->___Price_4 = L_39;
		Il2CppCodeGenWriteBarrier((void**)(&L_33->___Price_4), (void*)L_39);
		// rowList.Add(row);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_40 = __this->___rowList_0;
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_41 = V_2;
		NullCheck(L_40);
		List_1_Add_m9761F0D2ADF7CB1D17354DDC09E8F08DB70897EF_inline(L_40, L_41, List_1_Add_m9761F0D2ADF7CB1D17354DDC09E8F08DB70897EF_RuntimeMethod_var);
		// for(int i = 1 ; i < grid.Length ; i++)
		int32_t L_42 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_42, 1));
	}

IL_0068:
	{
		// for(int i = 1 ; i < grid.Length ; i++)
		int32_t L_43 = V_1;
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_44 = V_0;
		NullCheck(L_44);
		if ((((int32_t)L_43) < ((int32_t)((int32_t)(((RuntimeArray*)L_44)->max_length)))))
		{
			goto IL_001b;
		}
	}
	{
		// isLoaded = true;
		__this->___isLoaded_1 = (bool)1;
		// }
		return;
	}
}
// System.Int32 SampleTable::NumRows()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SampleTable_NumRows_m58F597740AAB8FCBF46DE36659BC58D93FF3523E (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return rowList.Count;
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_0 = __this->___rowList_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_inline(L_0, List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_RuntimeMethod_var);
		return L_1;
	}
}
// SampleTable/Row SampleTable::GetAt(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* SampleTable_GetAt_m99FA2B56BB9ECCCE2FD068A81A6C3CBEEAD39DAD (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, int32_t ___i0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_m82D8E1795C4DF42DA74D17354A985E517168F936_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if(rowList.Count <= i)
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_0 = __this->___rowList_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_inline(L_0, List_1_get_Count_mCF211465ECD4F2DC650030919B5C74EDFB9B1D67_RuntimeMethod_var);
		int32_t L_2 = ___i0;
		if ((((int32_t)L_1) > ((int32_t)L_2)))
		{
			goto IL_0010;
		}
	}
	{
		// return null;
		return (Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC*)NULL;
	}

IL_0010:
	{
		// return rowList[i];
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		int32_t L_4 = ___i0;
		NullCheck(L_3);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_5;
		L_5 = List_1_get_Item_m82D8E1795C4DF42DA74D17354A985E517168F936(L_3, L_4, List_1_get_Item_m82D8E1795C4DF42DA74D17354A985E517168F936_RuntimeMethod_var);
		return L_5;
	}
}
// SampleTable/Row SampleTable::Find_Year(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* SampleTable_Find_Year_m4AE1FEF99C807CB2FE6D0020BEEC0ECD8222FD0F (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass8_0_U3CFind_YearU3Eb__0_m015A2A5E12E7BDE55B522CC486803B07E9B4F795_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* L_0 = (U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass8_0__ctor_m05D46DB0D640A36C7E789205C80DFDA027987C55(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.Find(x => x.Year == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass8_0_U3CFind_YearU3Eb__0_m015A2A5E12E7BDE55B522CC486803B07E9B4F795_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_6;
		L_6 = List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4(L_3, L_5, List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		return L_6;
	}
}
// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::FindAll_Year(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* SampleTable_FindAll_Year_mAF6AC0EC023D5D6A6716FC097F2EFC8DEC0BF426 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass9_0_U3CFindAll_YearU3Eb__0_m3C1B30422DB8F2F130ED68EE884F821AEFF219BC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* L_0 = (U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass9_0__ctor_mD75892DF63C0FC2BD408AEA656B48DDB7BD1AA20(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.FindAll(x => x.Year == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass9_0_U3CFindAll_YearU3Eb__0_m3C1B30422DB8F2F130ED68EE884F821AEFF219BC_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_6;
		L_6 = List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C(L_3, L_5, List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		return L_6;
	}
}
// SampleTable/Row SampleTable::Find_Make(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* SampleTable_Find_Make_m7721C4C45917286297FF746FFE812FCF75423EEA (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass10_0_U3CFind_MakeU3Eb__0_m9690D15F7018B534711F77ACE95ED4A3C9EAEA53_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* L_0 = (U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass10_0__ctor_m6367426A77E7F16A373755C9B26D381B494A3C95(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.Find(x => x.Make == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass10_0_U3CFind_MakeU3Eb__0_m9690D15F7018B534711F77ACE95ED4A3C9EAEA53_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_6;
		L_6 = List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4(L_3, L_5, List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		return L_6;
	}
}
// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::FindAll_Make(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* SampleTable_FindAll_Make_m34546E31AE63C4E4FBDD9053862E07E2E4ADB213 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass11_0_U3CFindAll_MakeU3Eb__0_m176F964725AF0CAFDAB226E789B460DF024ABAA9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* L_0 = (U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass11_0__ctor_m9E53FC74DCBB7CFE404ABBB2195902FE0C1D7601(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.FindAll(x => x.Make == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass11_0_U3CFindAll_MakeU3Eb__0_m176F964725AF0CAFDAB226E789B460DF024ABAA9_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_6;
		L_6 = List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C(L_3, L_5, List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		return L_6;
	}
}
// SampleTable/Row SampleTable::Find_Model(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* SampleTable_Find_Model_m56E580CD55428BB9CEC4FA3E98F57F9716046584 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass12_0_U3CFind_ModelU3Eb__0_m8BAD4E720795A1A54FB128AECF0C50B86E6C2FF5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* L_0 = (U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass12_0__ctor_m7F5C378544EFF8780DC07E78158AD5F5F08B71EC(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.Find(x => x.Model == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass12_0_U3CFind_ModelU3Eb__0_m8BAD4E720795A1A54FB128AECF0C50B86E6C2FF5_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_6;
		L_6 = List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4(L_3, L_5, List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		return L_6;
	}
}
// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::FindAll_Model(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* SampleTable_FindAll_Model_m7C6B85DC06A7250BBE5860E0334ABEBC2CE2726B (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass13_0_U3CFindAll_ModelU3Eb__0_mD2258DCB18D2FEC0702A063156864A12E36208CF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* L_0 = (U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass13_0__ctor_m21277AF189A8868A3EC82B351B6C0CA613D051B3(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.FindAll(x => x.Model == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass13_0_U3CFindAll_ModelU3Eb__0_mD2258DCB18D2FEC0702A063156864A12E36208CF_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_6;
		L_6 = List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C(L_3, L_5, List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		return L_6;
	}
}
// SampleTable/Row SampleTable::Find_Description(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* SampleTable_Find_Description_m2A5B332642106FC4CDF323A1CB57B9CE3DF4CC77 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass14_0_U3CFind_DescriptionU3Eb__0_mB6C5A693A3EC33C1F95D6E524CCDE48DD46AE39E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* L_0 = (U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass14_0__ctor_m8E169C1967B6403C0B81E90478611CC21A976665(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.Find(x => x.Description == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass14_0_U3CFind_DescriptionU3Eb__0_mB6C5A693A3EC33C1F95D6E524CCDE48DD46AE39E_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_6;
		L_6 = List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4(L_3, L_5, List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		return L_6;
	}
}
// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::FindAll_Description(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* SampleTable_FindAll_Description_m86BBB526C3BF3E1C245FC63504305EEE89D5AB7B (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass15_0_U3CFindAll_DescriptionU3Eb__0_mD4398A3E548390DDED491E828A9E706FD17D6019_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* L_0 = (U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass15_0__ctor_m86FB91648371CB01C88E55065B451D90C7FDE67E(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.FindAll(x => x.Description == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass15_0_U3CFindAll_DescriptionU3Eb__0_mD4398A3E548390DDED491E828A9E706FD17D6019_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_6;
		L_6 = List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C(L_3, L_5, List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		return L_6;
	}
}
// SampleTable/Row SampleTable::Find_Price(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* SampleTable_Find_Price_mD3B37B0851722F97566BCEBE9902E842A1BADDFD (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass16_0_U3CFind_PriceU3Eb__0_mC46DC08CEE1619582B2E05F661CAEB2BA1845131_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* L_0 = (U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass16_0__ctor_m561B4EA43806B0351A0D59EEEE5B775D105088BD(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.Find(x => x.Price == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass16_0_U3CFind_PriceU3Eb__0_mC46DC08CEE1619582B2E05F661CAEB2BA1845131_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_6;
		L_6 = List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4(L_3, L_5, List_1_Find_mAF67B4B8B75EBDEDE6AFE926EB7DE0E8A3417DB4_RuntimeMethod_var);
		return L_6;
	}
}
// System.Collections.Generic.List`1<SampleTable/Row> SampleTable::FindAll_Price(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* SampleTable_FindAll_Price_m96086C791CFF866902C668DBFABE54B67C0DDA31 (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, String_t* ___find0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass17_0_U3CFindAll_PriceU3Eb__0_m14CBD99784ED2E431E7D88416BBA6186F4E65BB1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* L_0 = (U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass17_0__ctor_mC61D7D40BFF5A0ACFF9FDF1C0868B232A2336627(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* L_1 = V_0;
		String_t* L_2 = ___find0;
		NullCheck(L_1);
		L_1->___find_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___find_0), (void*)L_2);
		// return rowList.FindAll(x => x.Price == find);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_3 = __this->___rowList_0;
		U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* L_4 = V_0;
		Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70* L_5 = (Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70*)il2cpp_codegen_object_new(Predicate_1_tC1E40363ED29E5EC66B2494FBAACF4DC39BCDE70_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		Predicate_1__ctor_mAC05AE1EAF2CD5F6CDA902AC3D7312C27B54075E(L_5, L_4, (intptr_t)((void*)U3CU3Ec__DisplayClass17_0_U3CFindAll_PriceU3Eb__0_m14CBD99784ED2E431E7D88416BBA6186F4E65BB1_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_6;
		L_6 = List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C(L_3, L_5, List_1_FindAll_mA4C22FC4CF50FD9B441A2E7B3915B3DFDD9D1C9C_RuntimeMethod_var);
		return L_6;
	}
}
// System.Void SampleTable::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleTable__ctor_m3160D32BF7A57EB8FF5ED56A676DAB2DF8EDFD3F (SampleTable_t8B014DDEA85B2DF520F21A3402EF29788ABAB321* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mA9A28D7BDA09426757EEB0C6020D5BE0CC7A9584_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// List<Row> rowList = new List<Row>();
		List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED* L_0 = (List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED*)il2cpp_codegen_object_new(List_1_tF41D76FB5B9B1885DB345A4FDACC0F20E0836AED_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_mA9A28D7BDA09426757EEB0C6020D5BE0CC7A9584(L_0, List_1__ctor_mA9A28D7BDA09426757EEB0C6020D5BE0CC7A9584_RuntimeMethod_var);
		__this->___rowList_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___rowList_0), (void*)L_0);
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
// System.Void SampleTable/Row::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Row__ctor_m2D008B8DB9286F8856252DE30136CFC350484D59 (Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* __this, const RuntimeMethod* method) 
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
// System.Void SampleTable/<>c__DisplayClass8_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass8_0__ctor_m05D46DB0D640A36C7E789205C80DFDA027987C55 (U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass8_0::<Find_Year>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass8_0_U3CFind_YearU3Eb__0_m015A2A5E12E7BDE55B522CC486803B07E9B4F795 (U3CU3Ec__DisplayClass8_0_tB1D7D70FC96C8E1FA30A3ECCB82AD117F371B565* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.Find(x => x.Year == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Year_0;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass9_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass9_0__ctor_mD75892DF63C0FC2BD408AEA656B48DDB7BD1AA20 (U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass9_0::<FindAll_Year>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass9_0_U3CFindAll_YearU3Eb__0_m3C1B30422DB8F2F130ED68EE884F821AEFF219BC (U3CU3Ec__DisplayClass9_0_t1DBFDAACEDBEDA6AB19BC8AECAC1FFA31E59553D* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.FindAll(x => x.Year == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Year_0;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass10_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass10_0__ctor_m6367426A77E7F16A373755C9B26D381B494A3C95 (U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass10_0::<Find_Make>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass10_0_U3CFind_MakeU3Eb__0_m9690D15F7018B534711F77ACE95ED4A3C9EAEA53 (U3CU3Ec__DisplayClass10_0_t10E7777CF9BBC1AA68BE3D5A557C12AC38740790* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.Find(x => x.Make == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Make_1;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass11_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass11_0__ctor_m9E53FC74DCBB7CFE404ABBB2195902FE0C1D7601 (U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass11_0::<FindAll_Make>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass11_0_U3CFindAll_MakeU3Eb__0_m176F964725AF0CAFDAB226E789B460DF024ABAA9 (U3CU3Ec__DisplayClass11_0_t5B5791979F2BC8C40D4D007D40C1E420549C4DF9* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.FindAll(x => x.Make == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Make_1;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass12_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass12_0__ctor_m7F5C378544EFF8780DC07E78158AD5F5F08B71EC (U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass12_0::<Find_Model>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass12_0_U3CFind_ModelU3Eb__0_m8BAD4E720795A1A54FB128AECF0C50B86E6C2FF5 (U3CU3Ec__DisplayClass12_0_t5FD55F99EFB426B037EC16CCCAEBD4ECB67906C8* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.Find(x => x.Model == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Model_2;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass13_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass13_0__ctor_m21277AF189A8868A3EC82B351B6C0CA613D051B3 (U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass13_0::<FindAll_Model>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass13_0_U3CFindAll_ModelU3Eb__0_mD2258DCB18D2FEC0702A063156864A12E36208CF (U3CU3Ec__DisplayClass13_0_t5EAFF43A5A5FEF090FA59D9ABD16ED0758032778* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.FindAll(x => x.Model == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Model_2;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass14_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass14_0__ctor_m8E169C1967B6403C0B81E90478611CC21A976665 (U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass14_0::<Find_Description>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass14_0_U3CFind_DescriptionU3Eb__0_mB6C5A693A3EC33C1F95D6E524CCDE48DD46AE39E (U3CU3Ec__DisplayClass14_0_tB9BE80D284BD961C9F8E064BD0F095C794490517* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.Find(x => x.Description == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Description_3;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass15_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass15_0__ctor_m86FB91648371CB01C88E55065B451D90C7FDE67E (U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass15_0::<FindAll_Description>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass15_0_U3CFindAll_DescriptionU3Eb__0_mD4398A3E548390DDED491E828A9E706FD17D6019 (U3CU3Ec__DisplayClass15_0_t34060947044A8AB048B81E5E2B20192799BC2869* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.FindAll(x => x.Description == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Description_3;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass16_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass16_0__ctor_m561B4EA43806B0351A0D59EEEE5B775D105088BD (U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass16_0::<Find_Price>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass16_0_U3CFind_PriceU3Eb__0_mC46DC08CEE1619582B2E05F661CAEB2BA1845131 (U3CU3Ec__DisplayClass16_0_t0B2FBD138F2849C31AF530D5F97150BD9A901292* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.Find(x => x.Price == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Price_4;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.Void SampleTable/<>c__DisplayClass17_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass17_0__ctor_mC61D7D40BFF5A0ACFF9FDF1C0868B232A2336627 (U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean SampleTable/<>c__DisplayClass17_0::<FindAll_Price>b__0(SampleTable/Row)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass17_0_U3CFindAll_PriceU3Eb__0_m14CBD99784ED2E431E7D88416BBA6186F4E65BB1 (U3CU3Ec__DisplayClass17_0_t77D60103E5B03D64B33B023B0EAB7FDF0007768D* __this, Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* ___x0, const RuntimeMethod* method) 
{
	{
		// return rowList.FindAll(x => x.Price == find);
		Row_t9A2423B442F337F2D9F219226E94AB85BB680EAC* L_0 = ___x0;
		NullCheck(L_0);
		String_t* L_1 = L_0->___Price_4;
		String_t* L_2 = __this->___find_0;
		bool L_3;
		L_3 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, L_2, NULL);
		return L_3;
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
// System.String[][] CsvParser::Parse(System.IO.TextReader)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* CsvParser_Parse_m786BE4DAC73F7BF7DF882A8E4BE04787C38704F2 (CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* __this, TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7* ___reader0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_ToArray_m0FF88E5645F74AB2208E8BA2A85973B21E5FADA0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* V_0 = NULL;
	ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* V_1 = NULL;
	String_t* V_2 = NULL;
	String_t* V_3 = NULL;
	int32_t V_4 = 0;
	Il2CppChar V_5 = 0x0;
	{
		// var context = new ParserContext();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = (ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9*)il2cpp_codegen_object_new(ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		ParserContext__ctor_m5C1CC4A3CC2996F41AE08533A717CB09B41434F5(L_0, NULL);
		V_0 = L_0;
		// ParserState currentState = ParserState.LineStartState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* L_1 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___LineStartState_0;
		V_1 = L_1;
		goto IL_0061;
	}

IL_000e:
	{
		// foreach (char ch in next)
		String_t* L_2 = V_2;
		V_3 = L_2;
		V_4 = 0;
		goto IL_004f;
	}

IL_0015:
	{
		// foreach (char ch in next)
		String_t* L_3 = V_3;
		int32_t L_4 = V_4;
		NullCheck(L_3);
		Il2CppChar L_5;
		L_5 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_3, L_4, NULL);
		V_5 = L_5;
		Il2CppChar L_6 = V_5;
		if ((((int32_t)L_6) == ((int32_t)((int32_t)34))))
		{
			goto IL_0035;
		}
	}
	{
		Il2CppChar L_7 = V_5;
		if ((!(((uint32_t)L_7) == ((uint32_t)((int32_t)44)))))
		{
			goto IL_003f;
		}
	}
	{
		// currentState = currentState.Comma(context);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_8 = V_1;
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_9 = V_0;
		NullCheck(L_8);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_10;
		L_10 = VirtualFuncInvoker1< ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3*, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* >::Invoke(5 /* CsvParser/ParserState CsvParser/ParserState::Comma(CsvParser/ParserContext) */, L_8, L_9);
		V_1 = L_10;
		// break;
		goto IL_0049;
	}

IL_0035:
	{
		// currentState = currentState.Quote(context);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_11 = V_1;
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_12 = V_0;
		NullCheck(L_11);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_13;
		L_13 = VirtualFuncInvoker1< ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3*, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* >::Invoke(6 /* CsvParser/ParserState CsvParser/ParserState::Quote(CsvParser/ParserContext) */, L_11, L_12);
		V_1 = L_13;
		// break;
		goto IL_0049;
	}

IL_003f:
	{
		// currentState = currentState.AnyChar(ch, context);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_14 = V_1;
		Il2CppChar L_15 = V_5;
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_16 = V_0;
		NullCheck(L_14);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_17;
		L_17 = VirtualFuncInvoker2< ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3*, Il2CppChar, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* >::Invoke(4 /* CsvParser/ParserState CsvParser/ParserState::AnyChar(System.Char,CsvParser/ParserContext) */, L_14, L_15, L_16);
		V_1 = L_17;
	}

IL_0049:
	{
		int32_t L_18 = V_4;
		V_4 = ((int32_t)il2cpp_codegen_add(L_18, 1));
	}

IL_004f:
	{
		// foreach (char ch in next)
		int32_t L_19 = V_4;
		String_t* L_20 = V_3;
		NullCheck(L_20);
		int32_t L_21;
		L_21 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_20, NULL);
		if ((((int32_t)L_19) < ((int32_t)L_21)))
		{
			goto IL_0015;
		}
	}
	{
		// currentState = currentState.EndOfLine(context);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_22 = V_1;
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_23 = V_0;
		NullCheck(L_22);
		ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* L_24;
		L_24 = VirtualFuncInvoker1< ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3*, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* >::Invoke(7 /* CsvParser/ParserState CsvParser/ParserState::EndOfLine(CsvParser/ParserContext) */, L_22, L_23);
		V_1 = L_24;
	}

IL_0061:
	{
		// while ((next = reader.ReadLine()) != null)
		TextReader_tB8D43017CB6BE1633E5A86D64E7757366507C1F7* L_25 = ___reader0;
		NullCheck(L_25);
		String_t* L_26;
		L_26 = VirtualFuncInvoker0< String_t* >::Invoke(14 /* System.String System.IO.TextReader::ReadLine() */, L_25);
		String_t* L_27 = L_26;
		V_2 = L_27;
		if (L_27)
		{
			goto IL_000e;
		}
	}
	{
		// List<string[]> allLines = context.GetAllLines();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_28 = V_0;
		NullCheck(L_28);
		List_1_t77EDD3ECA98BCC1B49E3106C8CB923CA87D088ED* L_29;
		L_29 = ParserContext_GetAllLines_m9BAC583BCE4D3F83A00586053638EE19CFA91877(L_28, NULL);
		// return allLines.ToArray();
		NullCheck(L_29);
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_30;
		L_30 = List_1_ToArray_m0FF88E5645F74AB2208E8BA2A85973B21E5FADA0(L_29, List_1_ToArray_m0FF88E5645F74AB2208E8BA2A85973B21E5FADA0_RuntimeMethod_var);
		return L_30;
	}
}
// System.String[][] CsvParser::Parse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* CsvParser_Parse_mC150952AA195588AE27C82C0F7AB88A016D1855E (String_t* ___input0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* V_0 = NULL;
	StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* V_1 = NULL;
	StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* V_2 = NULL;
	{
		// CsvParser parser = new CsvParser();
		CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* L_0 = (CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607*)il2cpp_codegen_object_new(CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		CsvParser__ctor_mD72DA8A14830DDE8F2E56A277475FEAD5FFBFC71(L_0, NULL);
		V_0 = L_0;
		// using (StringReader reader = new StringReader(input))
		String_t* L_1 = ___input0;
		StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_2 = (StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8*)il2cpp_codegen_object_new(StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		StringReader__ctor_m72556EC1062F49E05CF41B0825AC7FA2DB2A81C0(L_2, L_1, NULL);
		V_1 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0017:
			{// begin finally (depth: 1)
				{
					StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_3 = V_1;
					if (!L_3)
					{
						goto IL_0020;
					}
				}
				{
					StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_4 = V_1;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_0020:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			// return parser.Parse(reader);
			CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* L_5 = V_0;
			StringReader_t1A336148FF22A9584E759A9D720CC96C23E35DD8* L_6 = V_1;
			NullCheck(L_5);
			StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_7;
			L_7 = CsvParser_Parse_m786BE4DAC73F7BF7DF882A8E4BE04787C38704F2(L_5, L_6, NULL);
			V_2 = L_7;
			goto IL_0021;
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0021:
	{
		// }
		StringU5BU5DU5BU5D_t8BCC500C5CC1686D9BADCBAA811074FE00F83ACF* L_8 = V_2;
		return L_8;
	}
}
// System.Void CsvParser::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CsvParser__ctor_mD72DA8A14830DDE8F2E56A277475FEAD5FFBFC71 (CsvParser_t5469801FAAD46ACD84FA0980DE54038C01BF9607* __this, const RuntimeMethod* method) 
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
// System.Void CsvParser/ParserState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserState__ctor_m1C3840E87C5C72B85E675F2E22026412DB87C705 (ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void CsvParser/ParserState::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParserState__cctor_m21F078E5328F856CB49BB675095FCD1165442DA4 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static readonly LineStartState LineStartState = new LineStartState();
		LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* L_0 = (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB*)il2cpp_codegen_object_new(LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		LineStartState__ctor_m3872C17D29CC13EBA595997F0B13AE5ECB486566(L_0, NULL);
		((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___LineStartState_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___LineStartState_0), (void*)L_0);
		// public static readonly ValueStartState ValueStartState = new ValueStartState();
		ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* L_1 = (ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B*)il2cpp_codegen_object_new(ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		ValueStartState__ctor_m9377B0723C0983042911EEE9864E494594C6EDA4(L_1, NULL);
		((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueStartState_1 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueStartState_1), (void*)L_1);
		// public static readonly ValueState ValueState = new ValueState();
		ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* L_2 = (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44*)il2cpp_codegen_object_new(ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		ValueState__ctor_mB055972E5EB17FC0809F30AE4ACF7AC1F868EE59(L_2, NULL);
		((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueState_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueState_2), (void*)L_2);
		// public static readonly QuotedValueState QuotedValueState = new QuotedValueState();
		QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA* L_3 = (QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA*)il2cpp_codegen_object_new(QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		QuotedValueState__ctor_mF9D1202E965D9C87E7D2F1DF19A30F4FCD913C6A(L_3, NULL);
		((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___QuotedValueState_3 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___QuotedValueState_3), (void*)L_3);
		// public static readonly QuoteState QuoteState = new QuoteState();
		QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E* L_4 = (QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E*)il2cpp_codegen_object_new(QuoteState_tE6D980AB13383043AC57F042EABE052BEEA3DF7E_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		QuoteState__ctor_m1D17BE9C37042852DB1C63C5E1DB4EE125B9C1C8(L_4, NULL);
		((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___QuoteState_4 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___QuoteState_4), (void*)L_4);
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
// CsvParser/ParserState CsvParser/LineStartState::AnyChar(System.Char,CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* LineStartState_AnyChar_m0BB5476C53A55052308A6EA829618AE1969F0B92 (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* __this, Il2CppChar ___ch0, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddChar(ch);
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context1;
		Il2CppChar L_1 = ___ch0;
		NullCheck(L_0);
		ParserContext_AddChar_mE8B2A52474CF912A2B135402C52432B47CF68039(L_0, L_1, NULL);
		// return ValueState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* L_2 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueState_2;
		return L_2;
	}
}
// CsvParser/ParserState CsvParser/LineStartState::Comma(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* LineStartState_Comma_m741BBC3478047AD517F5418761E5272EE5D0FA05 (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddValue();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context0;
		NullCheck(L_0);
		ParserContext_AddValue_m971336036E0386C8DC559534A9AEFA04DCFEB3F4(L_0, NULL);
		// return ValueStartState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* L_1 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueStartState_1;
		return L_1;
	}
}
// CsvParser/ParserState CsvParser/LineStartState::Quote(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* LineStartState_Quote_mA2FA7723B498D992DE2D553905ACFB61524AA7EA (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return QuotedValueState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		QuotedValueState_t83AF1C6A3F4CC1885073F0211C8D9877ED1017DA* L_0 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___QuotedValueState_3;
		return L_0;
	}
}
// CsvParser/ParserState CsvParser/LineStartState::EndOfLine(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* LineStartState_EndOfLine_m9F26BEB04D4F4830C6675CFD3162FF6C8EF1105F (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddLine();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context0;
		NullCheck(L_0);
		ParserContext_AddLine_mABF6B3D83F1F738C84CEBED3F90753244F060ED9(L_0, NULL);
		// return LineStartState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* L_1 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___LineStartState_0;
		return L_1;
	}
}
// System.Void CsvParser/LineStartState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LineStartState__ctor_m3872C17D29CC13EBA595997F0B13AE5ECB486566 (LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ParserState__ctor_m1C3840E87C5C72B85E675F2E22026412DB87C705(__this, NULL);
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
// CsvParser/ParserState CsvParser/ValueStartState::EndOfLine(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* ValueStartState_EndOfLine_m9CEC7BD940ED8FA37DEA444D6E554C8C0E60A2A4 (ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddValue();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context0;
		NullCheck(L_0);
		ParserContext_AddValue_m971336036E0386C8DC559534A9AEFA04DCFEB3F4(L_0, NULL);
		// context.AddLine();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_1 = ___context0;
		NullCheck(L_1);
		ParserContext_AddLine_mABF6B3D83F1F738C84CEBED3F90753244F060ED9(L_1, NULL);
		// return LineStartState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* L_2 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___LineStartState_0;
		return L_2;
	}
}
// System.Void CsvParser/ValueStartState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ValueStartState__ctor_m9377B0723C0983042911EEE9864E494594C6EDA4 (ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* __this, const RuntimeMethod* method) 
{
	{
		LineStartState__ctor_m3872C17D29CC13EBA595997F0B13AE5ECB486566(__this, NULL);
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
// CsvParser/ParserState CsvParser/ValueState::AnyChar(System.Char,CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* ValueState_AnyChar_m8044D86B3BE99825A45DC36C32345271528D095A (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* __this, Il2CppChar ___ch0, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddChar(ch);
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context1;
		Il2CppChar L_1 = ___ch0;
		NullCheck(L_0);
		ParserContext_AddChar_mE8B2A52474CF912A2B135402C52432B47CF68039(L_0, L_1, NULL);
		// return ValueState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* L_2 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueState_2;
		return L_2;
	}
}
// CsvParser/ParserState CsvParser/ValueState::Comma(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* ValueState_Comma_m4C216581F16AEFE6CF4744BBE42027BE919CD7B1 (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddValue();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context0;
		NullCheck(L_0);
		ParserContext_AddValue_m971336036E0386C8DC559534A9AEFA04DCFEB3F4(L_0, NULL);
		// return ValueStartState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ValueStartState_t6EFFBB284055613C98DB7DD6C692544300898C7B* L_1 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueStartState_1;
		return L_1;
	}
}
// CsvParser/ParserState CsvParser/ValueState::Quote(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* ValueState_Quote_m0D200F256C1EF9FD16505D03573CA47E1ACBC41B (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddChar(QuoteCharacter);
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context0;
		NullCheck(L_0);
		ParserContext_AddChar_mE8B2A52474CF912A2B135402C52432B47CF68039(L_0, ((int32_t)34), NULL);
		// return ValueState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* L_1 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___ValueState_2;
		return L_1;
	}
}
// CsvParser/ParserState CsvParser/ValueState::EndOfLine(CsvParser/ParserContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3* ValueState_EndOfLine_m5C90E9BF3314EAA8E00EA7DD35FC754CA8DCCFFB (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* __this, ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* ___context0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// context.AddValue();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_0 = ___context0;
		NullCheck(L_0);
		ParserContext_AddValue_m971336036E0386C8DC559534A9AEFA04DCFEB3F4(L_0, NULL);
		// context.AddLine();
		ParserContext_tAFCCFAF9919FBACC1950C71B8975EA52068C9FA9* L_1 = ___context0;
		NullCheck(L_1);
		ParserContext_AddLine_mABF6B3D83F1F738C84CEBED3F90753244F060ED9(L_1, NULL);
		// return LineStartState;
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		LineStartState_t7024D421CA30354751D391D46E116F20211B0CEB* L_2 = ((ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_StaticFields*)il2cpp_codegen_static_fields_for(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var))->___LineStartState_0;
		return L_2;
	}
}
// System.Void CsvParser/ValueState::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ValueState__ctor_mB055972E5EB17FC0809F30AE4ACF7AC1F868EE59 (ValueState_t77A4329D9867D4DB3CC46B78F304A0C62C803B44* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(ParserState_t5BE0B5F19B1D7F6D171AE54D88262484381CD8D3_il2cpp_TypeInfo_var);
		ParserState__ctor_m1C3840E87C5C72B85E675F2E22026412DB87C705(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void CameraMode_SetMeCenter_m7EF634EA83FBD929B8E52E998076BCE50F5AB33D_inline (CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97* __this, Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* ___meCenter0, const RuntimeMethod* method) 
{
	{
		// this.meCenter = meCenter;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = ___meCenter0;
		__this->___meCenter_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___meCenter_1), (void*)L_0);
		// }
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline (const RuntimeMethod* method) 
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
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___zeroVector_5;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_up_mAB5269BFCBCB1BD241450C9BF2F156303D30E0C3_inline (const RuntimeMethod* method) 
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
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___upVector_7;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_left_mA75C525C1E78B5BB99E9B7A63EF68C731043FE18_inline (const RuntimeMethod* method) 
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
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___leftVector_9;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_op_Multiply_m5AC8B39C55015059BDD09122E04E47D4BFAB2276_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___lhs0, Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___rhs1, const RuntimeMethod* method) 
{
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_0 = ___lhs0;
		float L_1 = L_0.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_2 = ___rhs1;
		float L_3 = L_2.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_4 = ___lhs0;
		float L_5 = L_4.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6 = ___rhs1;
		float L_7 = L_6.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_8 = ___lhs0;
		float L_9 = L_8.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_10 = ___rhs1;
		float L_11 = L_10.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_12 = ___lhs0;
		float L_13 = L_12.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_14 = ___rhs1;
		float L_15 = L_14.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_16 = ___lhs0;
		float L_17 = L_16.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_18 = ___rhs1;
		float L_19 = L_18.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_20 = ___lhs0;
		float L_21 = L_20.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_22 = ___rhs1;
		float L_23 = L_22.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_24 = ___lhs0;
		float L_25 = L_24.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_26 = ___rhs1;
		float L_27 = L_26.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_28 = ___lhs0;
		float L_29 = L_28.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_30 = ___rhs1;
		float L_31 = L_30.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_32 = ___lhs0;
		float L_33 = L_32.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_34 = ___rhs1;
		float L_35 = L_34.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_36 = ___lhs0;
		float L_37 = L_36.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_38 = ___rhs1;
		float L_39 = L_38.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_40 = ___lhs0;
		float L_41 = L_40.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_42 = ___rhs1;
		float L_43 = L_42.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_44 = ___lhs0;
		float L_45 = L_44.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_46 = ___rhs1;
		float L_47 = L_46.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_48 = ___lhs0;
		float L_49 = L_48.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_50 = ___rhs1;
		float L_51 = L_50.___w_3;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_52 = ___lhs0;
		float L_53 = L_52.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_54 = ___rhs1;
		float L_55 = L_54.___x_0;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_56 = ___lhs0;
		float L_57 = L_56.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_58 = ___rhs1;
		float L_59 = L_58.___y_1;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_60 = ___lhs0;
		float L_61 = L_60.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_62 = ___rhs1;
		float L_63 = L_62.___z_2;
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_64;
		memset((&L_64), 0, sizeof(L_64));
		Quaternion__ctor_m868FD60AA65DD5A8AC0C5DEB0608381A8D85FCD8_inline((&L_64), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_multiply(L_9, L_11)))), ((float)il2cpp_codegen_multiply(L_13, L_15)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_17, L_19)), ((float)il2cpp_codegen_multiply(L_21, L_23)))), ((float)il2cpp_codegen_multiply(L_25, L_27)))), ((float)il2cpp_codegen_multiply(L_29, L_31)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_33, L_35)), ((float)il2cpp_codegen_multiply(L_37, L_39)))), ((float)il2cpp_codegen_multiply(L_41, L_43)))), ((float)il2cpp_codegen_multiply(L_45, L_47)))), ((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_subtract(((float)il2cpp_codegen_multiply(L_49, L_51)), ((float)il2cpp_codegen_multiply(L_53, L_55)))), ((float)il2cpp_codegen_multiply(L_57, L_59)))), ((float)il2cpp_codegen_multiply(L_61, L_63)))), /*hidden argument*/NULL);
		V_0 = L_64;
		goto IL_00e5;
	}

IL_00e5:
	{
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_65 = V_0;
		return L_65;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_normalized_m736BBF65D5CDA7A18414370D15B4DFCC1E466F07_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = (*(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2*)__this);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1;
		L_1 = Vector3_Normalize_m6120F119433C5B60BBB28731D3D4A0DA50A84DDD_inline(L_0, NULL);
		V_0 = L_1;
		goto IL_000f;
	}

IL_000f:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = V_0;
		return L_2;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Subtraction_m1690F44F6DC92B770A940B6CF8AE0535625A9824_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) 
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
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_12), ((float)il2cpp_codegen_subtract(L_1, L_3)), ((float)il2cpp_codegen_subtract(L_5, L_7)), ((float)il2cpp_codegen_subtract(L_9, L_11)), /*hidden argument*/NULL);
		V_0 = L_12;
		goto IL_0030;
	}

IL_0030:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = V_0;
		return L_13;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_down_m19EB5B5B0EDFE9C272BD7BCC6923C4A9D616F771_inline (const RuntimeMethod* method) 
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
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___downVector_8;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, float ___d1, const RuntimeMethod* method) 
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
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_9), ((float)(L_1/L_2)), ((float)(L_4/L_5)), ((float)(L_7/L_8)), /*hidden argument*/NULL);
		V_0 = L_9;
		goto IL_0021;
	}

IL_0021:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = V_0;
		return L_10;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline (float ___value0, float ___min1, float ___max2, const RuntimeMethod* method) 
{
	bool V_0 = false;
	bool V_1 = false;
	float V_2 = 0.0f;
	{
		float L_0 = ___value0;
		float L_1 = ___min1;
		V_0 = (bool)((((float)L_0) < ((float)L_1))? 1 : 0);
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_000e;
		}
	}
	{
		float L_3 = ___min1;
		___value0 = L_3;
		goto IL_0019;
	}

IL_000e:
	{
		float L_4 = ___value0;
		float L_5 = ___max2;
		V_1 = (bool)((((float)L_4) > ((float)L_5))? 1 : 0);
		bool L_6 = V_1;
		if (!L_6)
		{
			goto IL_0019;
		}
	}
	{
		float L_7 = ___max2;
		___value0 = L_7;
	}

IL_0019:
	{
		float L_8 = ___value0;
		V_2 = L_8;
		goto IL_001d;
	}

IL_001d:
	{
		float L_9 = V_2;
		return L_9;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Angle_m1B9CC61B142C3A0E7EEB0559983CC391D1582F56_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___from0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___to1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	bool V_2 = false;
	float V_3 = 0.0f;
	{
		float L_0;
		L_0 = Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline((&___from0), NULL);
		float L_1;
		L_1 = Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline((&___to1), NULL);
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_2;
		L_2 = sqrt(((double)((float)il2cpp_codegen_multiply(L_0, L_1))));
		V_0 = ((float)L_2);
		float L_3 = V_0;
		V_2 = (bool)((((float)L_3) < ((float)(1.0E-15f)))? 1 : 0);
		bool L_4 = V_2;
		if (!L_4)
		{
			goto IL_002c;
		}
	}
	{
		V_3 = (0.0f);
		goto IL_0056;
	}

IL_002c:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_5 = ___from0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___to1;
		float L_7;
		L_7 = Vector3_Dot_m4688A1A524306675DBDB1E6D483F35E85E3CE6D8_inline(L_5, L_6, NULL);
		float L_8 = V_0;
		float L_9;
		L_9 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(((float)(L_7/L_8)), (-1.0f), (1.0f), NULL);
		V_1 = L_9;
		float L_10 = V_1;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_11;
		L_11 = acos(((double)L_10));
		V_3 = ((float)il2cpp_codegen_multiply(((float)L_11), (57.2957802f)));
		goto IL_0056;
	}

IL_0056:
	{
		float L_12 = V_3;
		return L_12;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_Multiply_m29F4414A9D30B7C0CD8455C4B2F049E8CCF66745_inline (float ___d0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a1, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___a1;
		float L_1 = L_0.___x_2;
		float L_2 = ___d0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3 = ___a1;
		float L_4 = L_3.___y_3;
		float L_5 = ___d0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___a1;
		float L_7 = L_6.___z_4;
		float L_8 = ___d0;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float ChatGptFix_get_TransitionSpeedPara_m409A745620888BFEB116DF710455916A9882F9A8_inline (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// get => _transitionSpeedPara;
		float L_0 = __this->____transitionSpeedPara_26;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool ChatGptFix_get_CanSetH_m1B1804C59790DF4A933DDB76290FB78C66A40869_inline (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// get => _canSetH;
		bool L_0 = __this->____canSetH_36;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_Distance_m220B2ADBE9F87426BEEE291263560DFE78F835B5_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___a0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___b1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = ___a0;
		float L_1 = L_0.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_2 = ___b1;
		float L_3 = L_2.___x_0;
		V_0 = ((float)il2cpp_codegen_subtract(L_1, L_3));
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4 = ___a0;
		float L_5 = L_4.___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6 = ___b1;
		float L_7 = L_6.___y_1;
		V_1 = ((float)il2cpp_codegen_subtract(L_5, L_7));
		float L_8 = V_0;
		float L_9 = V_0;
		float L_10 = V_1;
		float L_11 = V_1;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_12;
		L_12 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_8, L_9)), ((float)il2cpp_codegen_multiply(L_10, L_11))))));
		V_2 = ((float)L_12);
		goto IL_002e;
	}

IL_002e:
	{
		float L_13 = V_2;
		return L_13;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 Quaternion_Euler_mD4601D966F1F58F3FCA01B3FC19A12D0AD0396DD_inline (float ___x0, float ___y1, float ___z2, const RuntimeMethod* method) 
{
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		float L_0 = ___x0;
		float L_1 = ___y1;
		float L_2 = ___z2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_3;
		memset((&L_3), 0, sizeof(L_3));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_3), L_0, L_1, L_2, /*hidden argument*/NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4;
		L_4 = Vector3_op_Multiply_m516FE285F5342F922C6EB3FCB33197E9017FF484_inline(L_3, (0.0174532924f), NULL);
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_5;
		L_5 = Quaternion_Internal_FromEulerRad_m2842B9FFB31CDC0F80B7C2172E22831D11D91E93(L_4, NULL);
		V_0 = L_5;
		goto IL_001b;
	}

IL_001b:
	{
		Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 L_6 = V_0;
		return L_6;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float ChatGptFix_get_XZDistance_mB4A7F32E31E49E7F23F5088D645A76646325902C_inline (ChatGptFix_t619C8D9608A0E59C1295521EEA2EBCF3996837AD* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_op_Subtraction_m664419831773D5BBF06D9DE4E515F6409B2F92B8_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___a0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___b1, const RuntimeMethod* method) 
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = ___a0;
		float L_1 = L_0.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_2 = ___b1;
		float L_3 = L_2.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4 = ___a0;
		float L_5 = L_4.___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6 = ___b1;
		float L_7 = L_6.___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_8;
		memset((&L_8), 0, sizeof(L_8));
		Vector2__ctor_m9525B79969AFFE3254B303A40997A56DEEB6F548_inline((&L_8), ((float)il2cpp_codegen_subtract(L_1, L_3)), ((float)il2cpp_codegen_subtract(L_5, L_7)), /*hidden argument*/NULL);
		V_0 = L_8;
		goto IL_0023;
	}

IL_0023:
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_9 = V_0;
		return L_9;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_right_m13B7C3EAA64DC921EC23346C56A5A597B5481FF5_inline (const RuntimeMethod* method) 
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
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___rightVector_10;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_Angle_m9668B13074D1664DD192669C14B3A8FC01676299_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___from0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___to1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	bool V_2 = false;
	float V_3 = 0.0f;
	{
		float L_0;
		L_0 = Vector2_get_sqrMagnitude_mA16336720C14EEF8BA9B55AE33B98C9EE2082BDC_inline((&___from0), NULL);
		float L_1;
		L_1 = Vector2_get_sqrMagnitude_mA16336720C14EEF8BA9B55AE33B98C9EE2082BDC_inline((&___to1), NULL);
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_2;
		L_2 = sqrt(((double)((float)il2cpp_codegen_multiply(L_0, L_1))));
		V_0 = ((float)L_2);
		float L_3 = V_0;
		V_2 = (bool)((((float)L_3) < ((float)(1.0E-15f)))? 1 : 0);
		bool L_4 = V_2;
		if (!L_4)
		{
			goto IL_002c;
		}
	}
	{
		V_3 = (0.0f);
		goto IL_0056;
	}

IL_002c:
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_5 = ___from0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6 = ___to1;
		float L_7;
		L_7 = Vector2_Dot_mBF0FA0B529C821F4733DDC3AD366B07CD27625F4_inline(L_5, L_6, NULL);
		float L_8 = V_0;
		float L_9;
		L_9 = Mathf_Clamp_m154E404AF275A3B2EC99ECAA3879B4CB9F0606DC_inline(((float)(L_7/L_8)), (-1.0f), (1.0f), NULL);
		V_1 = L_9;
		float L_10 = V_1;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_11;
		L_11 = acos(((double)L_10));
		V_3 = ((float)il2cpp_codegen_multiply(((float)L_11), (57.2957802f)));
		goto IL_0056;
	}

IL_0056:
	{
		float L_12 = V_3;
		return L_12;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_op_UnaryNegation_m3AC523A7BED6E843165BDF598690F0560D8CAA63_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, const RuntimeMethod* method) 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___a0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___a0;
		float L_3 = L_2.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___a0;
		float L_5 = L_4.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		memset((&L_6), 0, sizeof(L_6));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_6), ((-L_1)), ((-L_3)), ((-L_5)), /*hidden argument*/NULL);
		V_0 = L_6;
		goto IL_001e;
	}

IL_001e:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = V_0;
		return L_7;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_ClampMagnitude_mDEF1E073986286F6EFA1552A5D0E1A0F6CBF4500_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___vector0, float ___maxLength1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	bool V_1 = false;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	float V_4 = 0.0f;
	float V_5 = 0.0f;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_6;
	memset((&V_6), 0, sizeof(V_6));
	{
		float L_0;
		L_0 = Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline((&___vector0), NULL);
		V_0 = L_0;
		float L_1 = V_0;
		float L_2 = ___maxLength1;
		float L_3 = ___maxLength1;
		V_1 = (bool)((((float)L_1) > ((float)((float)il2cpp_codegen_multiply(L_2, L_3))))? 1 : 0);
		bool L_4 = V_1;
		if (!L_4)
		{
			goto IL_004e;
		}
	}
	{
		float L_5 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_6;
		L_6 = sqrt(((double)L_5));
		V_2 = ((float)L_6);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7 = ___vector0;
		float L_8 = L_7.___x_2;
		float L_9 = V_2;
		V_3 = ((float)(L_8/L_9));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___vector0;
		float L_11 = L_10.___y_3;
		float L_12 = V_2;
		V_4 = ((float)(L_11/L_12));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_13 = ___vector0;
		float L_14 = L_13.___z_4;
		float L_15 = V_2;
		V_5 = ((float)(L_14/L_15));
		float L_16 = V_3;
		float L_17 = ___maxLength1;
		float L_18 = V_4;
		float L_19 = ___maxLength1;
		float L_20 = V_5;
		float L_21 = ___maxLength1;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_22;
		memset((&L_22), 0, sizeof(L_22));
		Vector3__ctor_m376936E6B999EF1ECBE57D990A386303E2283DE0_inline((&L_22), ((float)il2cpp_codegen_multiply(L_16, L_17)), ((float)il2cpp_codegen_multiply(L_18, L_19)), ((float)il2cpp_codegen_multiply(L_20, L_21)), /*hidden argument*/NULL);
		V_6 = L_22;
		goto IL_0053;
	}

IL_004e:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_23 = ___vector0;
		V_6 = L_23;
		goto IL_0053;
	}

IL_0053:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_24 = V_6;
		return L_24;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		float L_0 = __this->___x_0;
		float L_1 = __this->___x_0;
		float L_2 = __this->___y_1;
		float L_3 = __this->___y_1;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_4;
		L_4 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_0, L_1)), ((float)il2cpp_codegen_multiply(L_2, L_3))))));
		V_0 = ((float)L_4);
		goto IL_0026;
	}

IL_0026:
	{
		float L_5 = V_0;
		return L_5;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Vector3_op_Inequality_m6A7FB1C9E9DE194708997BFA24C6E238D92D908E_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lhs0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rhs1, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___lhs0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = ___rhs1;
		bool L_2;
		L_2 = Vector3_op_Equality_m15951D1B53E3BE36C9D265E229090020FBD72EBB_inline(L_0, L_1, NULL);
		V_0 = (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
		goto IL_000e;
	}

IL_000e:
	{
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Distance_m99C722723EDD875852EF854AD7B7C4F8AC4F84AB_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___a0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___b1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___a0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___b1;
		float L_3 = L_2.___x_2;
		V_0 = ((float)il2cpp_codegen_subtract(L_1, L_3));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___a0;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___b1;
		float L_7 = L_6.___y_3;
		V_1 = ((float)il2cpp_codegen_subtract(L_5, L_7));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___a0;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___b1;
		float L_11 = L_10.___z_4;
		V_2 = ((float)il2cpp_codegen_subtract(L_9, L_11));
		float L_12 = V_0;
		float L_13 = V_0;
		float L_14 = V_1;
		float L_15 = V_1;
		float L_16 = V_2;
		float L_17 = V_2;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_18;
		L_18 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_12, L_13)), ((float)il2cpp_codegen_multiply(L_14, L_15)))), ((float)il2cpp_codegen_multiply(L_16, L_17))))));
		V_3 = ((float)L_18);
		goto IL_0040;
	}

IL_0040:
	{
		float L_19 = V_3;
		return L_19;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float MCamera_get_TransitionSpeedPara_mFB4C4B859D16E59A9AB98B4D9A30365E9E1B97B6_inline (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// get => _transitionSpeedPara;
		float L_0 = __this->____transitionSpeedPara_26;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool MCamera_get_CanSetH_mBC65ADE59DB394E41A9CA17B9EE12EC94C2FC0A2_inline (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// get => _canSetH;
		bool L_0 = __this->____canSetH_37;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float MCamera_get_XZDistance_m012DA0EABACAB1FD41CD009E66281C698D544C74_inline (MCamera_t58CE459E81C8135BF56EC25A4CDE70447F4288BF* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float New2022_get_XZDistance_m04F36A8776A0F3B5B16D767F5EC587C20619251E_inline (New2022_t33F8B4630B0908D2341BCA4D68B961940CBF89D8* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 Vector2_get_right_mCE2D0142663361ED4B48C36873786986D25A6E0A_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = ((Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_StaticFields*)il2cpp_codegen_static_fields_for(Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_il2cpp_TypeInfo_var))->___rightVector_7;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float New2023_get_TransitionSpeedPara_mD8F056A7B4BE13EAB7939DD695AC3C9FB023C20D_inline (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) 
{
	{
		// get => _transitionSpeedPara;
		float L_0 = __this->____transitionSpeedPara_24;
		return L_0;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float New2023_get_XZDistance_mA2235920C05176006556D76DFD5AA4CB4F8A524D_inline (New2023_t01D3CB15E82FA0C631AB83CFB64D340B95DC1C31* __this, const RuntimeMethod* method) 
{
	{
		// get => XZDis;
		float L_0 = ((CameraMode_tD52ED8FC130C0DC42C108539AC233B0275C61D97*)__this)->___XZDis_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_get_XZ_distance_m85DE561AE9FA16B6C72CDA1FFE2F829EA7773A66_inline (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) 
{
	{
		// get { return xzd; }
		float L_0 = __this->___xzd_27;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_get_ZoomAcc_m16B63CB81DADC371768C54722F124AA88B49A8C2_inline (OneVOneMode_t75D10A3B199402FC33A26A5DB0F98D5E0196C822* __this, const RuntimeMethod* method) 
{
	{
		// get { return zoomAcc; }
		float L_0 = __this->___zoomAcc_23;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneModeNew_get_XZ_distance_m94590253CF56035E61E827B63683A58B9867CE56_inline (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) 
{
	{
		// get { return xzd; }
		float L_0 = __this->___xzd_26;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneModeNew_get_ZoomAcc_m11AEA3902A80D7C00F4B8A277CD3D59461F12537_inline (OneVOneModeNew_t344233C4AB09067928436DFC51E98D0D759E04EB* __this, const RuntimeMethod* method) 
{
	{
		// get { return zoomAcc; }
		float L_0 = __this->___zoomAcc_22;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_failed_get_XZ_distance_m0D721DCDD0DB447B6EF3C0E12B4E7B43B2E7771C_inline (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, const RuntimeMethod* method) 
{
	{
		// get { return xzd; }
		float L_0 = __this->___xzd_26;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float OneVOneMode_failed_get_ZoomAcc_m0188D1030DC38FCBAACF2CEAE851C047382B2CFC_inline (OneVOneMode_failed_t39CD229D49BB22CE07D5936B452651B36E26F14F* __this, const RuntimeMethod* method) 
{
	{
		// get { return zoomAcc; }
		float L_0 = __this->___zoomAcc_24;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_SmoothDamp_m4B8C5AACFEBF58E93FF2A33832C27EF1E5AF7AFD_inline (float ___current0, float ___target1, float* ___currentVelocity2, float ___smoothTime3, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	{
		float L_0;
		L_0 = Time_get_deltaTime_m7AB6BFA101D83E1D8F2EF3D5A128AEE9DDBF1A6D(NULL);
		V_0 = L_0;
		V_1 = (std::numeric_limits<float>::infinity());
		float L_1 = ___current0;
		float L_2 = ___target1;
		float* L_3 = ___currentVelocity2;
		float L_4 = ___smoothTime3;
		float L_5 = V_1;
		float L_6 = V_0;
		float L_7;
		L_7 = Mathf_SmoothDamp_m00E482452BCED3FE0F16B4033B2B5323C7E30829(L_1, L_2, L_3, L_4, L_5, L_6, NULL);
		V_2 = L_7;
		goto IL_001b;
	}

IL_001b:
	{
		float L_8 = V_2;
		return L_8;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* CameraManager_get_TopDownModeEndRef_mC510D9320204B96C91DBBBEE4EB2835E31B41327_inline (CameraManager_t27CFDF23ED636E9025EFEA9A5E8B0004355206BB* __this, const RuntimeMethod* method) 
{
	{
		// public Transform TopDownModeEndRef => topDownModeEndRef;
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_0 = __this->___topDownModeEndRef_9;
		return L_0;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float TouchTopDownCamera_get_Height_m6A6A94345B3716F3AA84538F7D4B6F03E4CCD4D2_inline (TouchTopDownCamera_tA67DE083FF1AF6AFCC735BDE29534B5637C1F7D2* __this, const RuntimeMethod* method) 
{
	{
		// get => height;
		float L_0 = __this->___height_14;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* InputField_get_text_m6E0796350FF559505E4DF17311803962699D6704_inline (InputField_tABEA115F23FBD374EBE80D4FAC1D15BD6E37A140* __this, const RuntimeMethod* method) 
{
	{
		// return m_Text;
		String_t* L_0 = __this->___m_Text_41;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____stringLength_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t L_0 = (int32_t)__this->____version_3;
		__this->____version_3 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		if (!true)
		{
			goto IL_0035;
		}
	}
	{
		int32_t L_1 = (int32_t)__this->____size_2;
		V_0 = L_1;
		__this->____size_2 = 0;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) <= ((int32_t)0)))
		{
			goto IL_003c;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->____items_1;
		int32_t L_4 = V_0;
		Array_Clear_m48B57EC27CADC3463CA98A33373D557DA587FF1B((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_0035:
	{
		__this->____size_2 = 0;
	}

IL_003c:
	{
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = (int32_t)__this->____size_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method) 
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = (int32_t)__this->____version_3;
		__this->____version_3 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->____items_1;
		V_0 = L_1;
		int32_t L_2 = (int32_t)__this->____size_2;
		V_1 = L_2;
		int32_t L_3 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size_2 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = V_0;
		int32_t L_7 = V_1;
		RuntimeObject* L_8 = ___item0;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (RuntimeObject*)L_8);
		return;
	}

IL_0034:
	{
		RuntimeObject* L_9 = ___item0;
		((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)))(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* KeyValuePair_2_get_Value_m415A21240AEF58C2E0A2FBA97E2BB75637781DB5_gshared_inline (KeyValuePair_2_tF70DDE0C5A349727371FB070D433FA147032A13B* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___value_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->____current_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Quaternion__ctor_m868FD60AA65DD5A8AC0C5DEB0608381A8D85FCD8_inline (Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974* __this, float ___x0, float ___y1, float ___z2, float ___w3, const RuntimeMethod* method) 
{
	{
		float L_0 = ___x0;
		__this->___x_0 = L_0;
		float L_1 = ___y1;
		__this->___y_1 = L_1;
		float L_2 = ___z2;
		__this->___z_2 = L_2;
		float L_3 = ___w3;
		__this->___w_3 = L_3;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_Normalize_m6120F119433C5B60BBB28731D3D4A0DA50A84DDD_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___value0, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	bool V_1 = false;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___value0;
		float L_1;
		L_1 = Vector3_Magnitude_m6AD0BEBF88AAF98188A851E62D7A32CB5B7830EF_inline(L_0, NULL);
		V_0 = L_1;
		float L_2 = V_0;
		V_1 = (bool)((((float)L_2) > ((float)(9.99999975E-06f)))? 1 : 0);
		bool L_3 = V_1;
		if (!L_3)
		{
			goto IL_001e;
		}
	}
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___value0;
		float L_5 = V_0;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6;
		L_6 = Vector3_op_Division_mD7200D6D432BAFC4135C5B17A0B0A812203B0270_inline(L_4, L_5, NULL);
		V_2 = L_6;
		goto IL_0026;
	}

IL_001e:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_7;
		L_7 = Vector3_get_zero_m9D7F7B580B5A276411267E96AA3425736D9BDC83_inline(NULL);
		V_2 = L_7;
		goto IL_0026;
	}

IL_0026:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = V_2;
		return L_8;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_get_sqrMagnitude_m43C27DEC47C4811FB30AB474FF2131A963B66FC8_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* __this, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0 = __this->___x_2;
		float L_1 = __this->___x_2;
		float L_2 = __this->___y_3;
		float L_3 = __this->___y_3;
		float L_4 = __this->___z_4;
		float L_5 = __this->___z_4;
		V_0 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_0, L_1)), ((float)il2cpp_codegen_multiply(L_2, L_3)))), ((float)il2cpp_codegen_multiply(L_4, L_5))));
		goto IL_002d;
	}

IL_002d:
	{
		float L_6 = V_0;
		return L_6;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Dot_m4688A1A524306675DBDB1E6D483F35E85E3CE6D8_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lhs0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rhs1, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___lhs0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___rhs1;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___lhs0;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___rhs1;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___lhs0;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___rhs1;
		float L_11 = L_10.___z_4;
		V_0 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_multiply(L_9, L_11))));
		goto IL_002d;
	}

IL_002d:
	{
		float L_12 = V_0;
		return L_12;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_get_sqrMagnitude_mA16336720C14EEF8BA9B55AE33B98C9EE2082BDC_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0 = __this->___x_0;
		float L_1 = __this->___x_0;
		float L_2 = __this->___y_1;
		float L_3 = __this->___y_1;
		V_0 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_0, L_1)), ((float)il2cpp_codegen_multiply(L_2, L_3))));
		goto IL_001f;
	}

IL_001f:
	{
		float L_4 = V_0;
		return L_4;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_Dot_mBF0FA0B529C821F4733DDC3AD366B07CD27625F4_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___lhs0, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rhs1, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = ___lhs0;
		float L_1 = L_0.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_2 = ___rhs1;
		float L_3 = L_2.___x_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4 = ___lhs0;
		float L_5 = L_4.___y_1;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_6 = ___rhs1;
		float L_7 = L_6.___y_1;
		V_0 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7))));
		goto IL_001f;
	}

IL_001f:
	{
		float L_8 = V_0;
		return L_8;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Vector3_op_Equality_m15951D1B53E3BE36C9D265E229090020FBD72EBB_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___lhs0, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rhs1, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	float V_1 = 0.0f;
	float V_2 = 0.0f;
	float V_3 = 0.0f;
	bool V_4 = false;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___lhs0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___rhs1;
		float L_3 = L_2.___x_2;
		V_0 = ((float)il2cpp_codegen_subtract(L_1, L_3));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___lhs0;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___rhs1;
		float L_7 = L_6.___y_3;
		V_1 = ((float)il2cpp_codegen_subtract(L_5, L_7));
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___lhs0;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___rhs1;
		float L_11 = L_10.___z_4;
		V_2 = ((float)il2cpp_codegen_subtract(L_9, L_11));
		float L_12 = V_0;
		float L_13 = V_0;
		float L_14 = V_1;
		float L_15 = V_1;
		float L_16 = V_2;
		float L_17 = V_2;
		V_3 = ((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_12, L_13)), ((float)il2cpp_codegen_multiply(L_14, L_15)))), ((float)il2cpp_codegen_multiply(L_16, L_17))));
		float L_18 = V_3;
		V_4 = (bool)((((float)L_18) < ((float)(9.99999944E-11f)))? 1 : 0);
		goto IL_0043;
	}

IL_0043:
	{
		bool L_19 = V_4;
		return L_19;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector3_Magnitude_m6AD0BEBF88AAF98188A851E62D7A32CB5B7830EF_inline (Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___vector0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ___vector0;
		float L_1 = L_0.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_2 = ___vector0;
		float L_3 = L_2.___x_2;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_4 = ___vector0;
		float L_5 = L_4.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_6 = ___vector0;
		float L_7 = L_6.___y_3;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_8 = ___vector0;
		float L_9 = L_8.___z_4;
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_10 = ___vector0;
		float L_11 = L_10.___z_4;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_12;
		L_12 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_1, L_3)), ((float)il2cpp_codegen_multiply(L_5, L_7)))), ((float)il2cpp_codegen_multiply(L_9, L_11))))));
		V_0 = ((float)L_12);
		goto IL_0034;
	}

IL_0034:
	{
		float L_13 = V_0;
		return L_13;
	}
}
