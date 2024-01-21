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

// System.Action`1<AppleAuth.Enums.CredentialState>
struct Action_1_t7CF092A129C468D25C4E6E5AC20FE3453FC7EC66;
// System.Action`1<AppleAuth.Interfaces.IAppleError>
struct Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA;
// System.Action`1<AppleAuth.Interfaces.ICredential>
struct Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80;
// System.Action`1<System.String>
struct Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<System.String>
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// AppleAuth.AppleAuthManager
struct AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4;
// AppleAuth.Native.AppleError
struct AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA;
// AppleAuth.Native.AppleIDCredential
struct AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// AppleAuth.Native.CredentialStateResponse
struct CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Exception
struct Exception_t;
// AppleAuth.Native.FullPersonName
struct FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8;
// AppleAuth.Interfaces.IAppleError
struct IAppleError_tFE643037BCCCF4362368CEDC73AC94B90E1B5AC0;
// AppleAuth.Interfaces.IAppleIDCredential
struct IAppleIDCredential_t29B66F5F775F5233192B1FF591BE6471AE7A5E9E;
// AppleAuth.Interfaces.ICredential
struct ICredential_t3F553A85F7FED597AA128C5E4A223ABA6735DD52;
// AppleAuth.Interfaces.ICredentialStateResponse
struct ICredentialStateResponse_t1898A0EE06898B4687A7B63A71B0982905D5E151;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// AppleAuth.Interfaces.ILoginWithAppleIdResponse
struct ILoginWithAppleIdResponse_t5D26EEEDB8BED224C65516DD2A29DD041EFFA31E;
// AppleAuth.Interfaces.IPasswordCredential
struct IPasswordCredential_t85F86A7DEBB8A7CE36CF75B443684DCF152E5012;
// AppleAuth.Interfaces.IPayloadDeserializer
struct IPayloadDeserializer_t0C8DAC2DA8B0E14F11F345A24D9390CD6CFD83FF;
// AppleAuth.Interfaces.IPersonName
struct IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591;
// AppleAuth.Native.LoginWithAppleIdResponse
struct LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// AppleAuth.Native.PasswordCredential
struct PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1;
// AppleAuth.Native.PayloadDeserializer
struct PayloadDeserializer_tB856F635041B5A76FCF832A0A329EABD65C3BD07;
// AppleAuth.Native.PersonName
struct PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.String
struct String_t;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IAppleError_tFE643037BCCCF4362368CEDC73AC94B90E1B5AC0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral2386E77CF610F786B06A91AF2C1B3FD2282D2745;
IL2CPP_EXTERN_C String_t* _stringLiteral4B31B12774EADD6A7DE8A6382F262DAD80E785C9;
IL2CPP_EXTERN_C String_t* _stringLiteral66A865E28B2B5657FCF66691A6F7AC2B94FE50BA;
IL2CPP_EXTERN_C String_t* _stringLiteral88ABE2A389F33F220D85F9A81EE6463C0A1A4D33;
IL2CPP_EXTERN_C String_t* _stringLiteralA95898025CC11DF26437FBBC4B43CA5F697F5DB1;
IL2CPP_EXTERN_C String_t* _stringLiteralCA0DA7AFA13D06380B286383F61CFD3BFBFDEB4B;
IL2CPP_EXTERN_C String_t* _stringLiteralCDD4C83DD410201E5E465766DBA93DED78585395;
IL2CPP_EXTERN_C String_t* _stringLiteralDA3990537E355E81C23A6B8320705118BC1046ED;
IL2CPP_EXTERN_C const RuntimeMethod* AppleAuthManager_GetCredentialState_m7646C8E8F7DFA9A8C77E576C39DFE4059BE5AD02_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AppleAuthManager_LoginWithAppleId_m41AA28DCBBCED940184CCD2D353C8B8D7E3EA355_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AppleAuthManager_QuickLogin_m6729A39051EC9885AD1668BA28B94B9489D39F08_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* JsonUtility_FromJson_TisCredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27_mB6AB6E418CE795BA56D5F91DD65AD5E94A942C86_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* JsonUtility_FromJson_TisLoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F_m968AA1B827E76342EC45CF035AAC2D0B2FCF92B8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_ToArray_m2C402D882AA60FC1D5C7C09A129BE7779F833B4A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForObject_TisAppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75_m9E1C59F3B0E71A90A97B4C5FFFEC8E2B5BDE24E8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForObject_TisFullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8_m59A130EAEC5E3F8CC409BC8FC7E9F71454448D6B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForObject_TisPasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1_m7DB7C09BECB1A15FE9EC980663BBBB4BB135CF15_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SerializationTools_FixSerializationForObject_TisPersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78_m42CA8587586194E3CBA0F51F519B99077D3133D9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* AuthorizationErrorCode_t7DF2ED12266D6A7885C609086E1BFA7E359B4087_0_0_0_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t797990FE5768C8E6C4A6DD8010A5EE4D33B41ECD 
{
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

// System.Collections.Generic.List`1<System.String>
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___s_emptyArray_5;
};

// AppleAuth.AppleAuthManager
struct AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4  : public RuntimeObject
{
};

// AppleAuth.Native.AppleError
struct AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA  : public RuntimeObject
{
	// System.Int32 AppleAuth.Native.AppleError::_code
	int32_t ____code_0;
	// System.String AppleAuth.Native.AppleError::_domain
	String_t* ____domain_1;
	// System.String AppleAuth.Native.AppleError::_localizedDescription
	String_t* ____localizedDescription_2;
	// System.String[] AppleAuth.Native.AppleError::_localizedRecoveryOptions
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ____localizedRecoveryOptions_3;
	// System.String AppleAuth.Native.AppleError::_localizedRecoverySuggestion
	String_t* ____localizedRecoverySuggestion_4;
	// System.String AppleAuth.Native.AppleError::_localizedFailureReason
	String_t* ____localizedFailureReason_5;
};

// AppleAuth.Extensions.AppleErrorExtensions
struct AppleErrorExtensions_t28DD633BFF4B001172FC3B560C6233FF96D2F5B4  : public RuntimeObject
{
};

// AppleAuth.Native.AppleIDCredential
struct AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75  : public RuntimeObject
{
	// System.String AppleAuth.Native.AppleIDCredential::_base64IdentityToken
	String_t* ____base64IdentityToken_0;
	// System.String AppleAuth.Native.AppleIDCredential::_base64AuthorizationCode
	String_t* ____base64AuthorizationCode_1;
	// System.String AppleAuth.Native.AppleIDCredential::_state
	String_t* ____state_2;
	// System.String AppleAuth.Native.AppleIDCredential::_user
	String_t* ____user_3;
	// System.String[] AppleAuth.Native.AppleIDCredential::_authorizedScopes
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ____authorizedScopes_4;
	// System.Boolean AppleAuth.Native.AppleIDCredential::_hasFullName
	bool ____hasFullName_5;
	// AppleAuth.Native.FullPersonName AppleAuth.Native.AppleIDCredential::_fullName
	FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8* ____fullName_6;
	// System.String AppleAuth.Native.AppleIDCredential::_email
	String_t* ____email_7;
	// System.Int32 AppleAuth.Native.AppleIDCredential::_realUserStatus
	int32_t ____realUserStatus_8;
	// System.Byte[] AppleAuth.Native.AppleIDCredential::_identityToken
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ____identityToken_9;
	// System.Byte[] AppleAuth.Native.AppleIDCredential::_authorizationCode
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ____authorizationCode_10;
};
struct Il2CppArrayBounds;

// AppleAuth.Native.CredentialStateResponse
struct CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27  : public RuntimeObject
{
	// System.Boolean AppleAuth.Native.CredentialStateResponse::_success
	bool ____success_0;
	// System.Boolean AppleAuth.Native.CredentialStateResponse::_hasCredentialState
	bool ____hasCredentialState_1;
	// System.Boolean AppleAuth.Native.CredentialStateResponse::_hasError
	bool ____hasError_2;
	// System.Int32 AppleAuth.Native.CredentialStateResponse::_credentialState
	int32_t ____credentialState_3;
	// AppleAuth.Native.AppleError AppleAuth.Native.CredentialStateResponse::_error
	AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* ____error_4;
};

// AppleAuth.Native.LoginWithAppleIdResponse
struct LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F  : public RuntimeObject
{
	// System.Boolean AppleAuth.Native.LoginWithAppleIdResponse::_success
	bool ____success_0;
	// System.Boolean AppleAuth.Native.LoginWithAppleIdResponse::_hasAppleIdCredential
	bool ____hasAppleIdCredential_1;
	// System.Boolean AppleAuth.Native.LoginWithAppleIdResponse::_hasPasswordCredential
	bool ____hasPasswordCredential_2;
	// System.Boolean AppleAuth.Native.LoginWithAppleIdResponse::_hasError
	bool ____hasError_3;
	// AppleAuth.Native.AppleIDCredential AppleAuth.Native.LoginWithAppleIdResponse::_appleIdCredential
	AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* ____appleIdCredential_4;
	// AppleAuth.Native.PasswordCredential AppleAuth.Native.LoginWithAppleIdResponse::_passwordCredential
	PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* ____passwordCredential_5;
	// AppleAuth.Native.AppleError AppleAuth.Native.LoginWithAppleIdResponse::_error
	AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* ____error_6;
};

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// AppleAuth.Native.PasswordCredential
struct PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1  : public RuntimeObject
{
	// System.String AppleAuth.Native.PasswordCredential::_user
	String_t* ____user_0;
	// System.String AppleAuth.Native.PasswordCredential::_password
	String_t* ____password_1;
};

// AppleAuth.Native.PayloadDeserializer
struct PayloadDeserializer_tB856F635041B5A76FCF832A0A329EABD65C3BD07  : public RuntimeObject
{
};

// AppleAuth.Native.PersonName
struct PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78  : public RuntimeObject
{
	// System.String AppleAuth.Native.PersonName::_namePrefix
	String_t* ____namePrefix_0;
	// System.String AppleAuth.Native.PersonName::_givenName
	String_t* ____givenName_1;
	// System.String AppleAuth.Native.PersonName::_middleName
	String_t* ____middleName_2;
	// System.String AppleAuth.Native.PersonName::_familyName
	String_t* ____familyName_3;
	// System.String AppleAuth.Native.PersonName::_nameSuffix
	String_t* ____nameSuffix_4;
	// System.String AppleAuth.Native.PersonName::_nickname
	String_t* ____nickname_5;
};

// AppleAuth.Extensions.PersonNameExtensions
struct PersonNameExtensions_t3AD482DB712313DD237BEC2E86F11D81C7AAE283  : public RuntimeObject
{
};

// AppleAuth.Native.SerializationTools
struct SerializationTools_tB41428FC4691220F44942E228E64E81CE7FE075D  : public RuntimeObject
{
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

// AppleAuth.AppleAuthLoginArgs
struct AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4 
{
	// AppleAuth.Enums.LoginOptions AppleAuth.AppleAuthLoginArgs::Options
	int32_t ___Options_0;
	// System.String AppleAuth.AppleAuthLoginArgs::Nonce
	String_t* ___Nonce_1;
	// System.String AppleAuth.AppleAuthLoginArgs::State
	String_t* ___State_2;
};
// Native definition for P/Invoke marshalling of AppleAuth.AppleAuthLoginArgs
struct AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_pinvoke
{
	int32_t ___Options_0;
	char* ___Nonce_1;
	char* ___State_2;
};
// Native definition for COM marshalling of AppleAuth.AppleAuthLoginArgs
struct AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_com
{
	int32_t ___Options_0;
	Il2CppChar* ___Nonce_1;
	Il2CppChar* ___State_2;
};

// AppleAuth.AppleAuthQuickLoginArgs
struct AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C 
{
	// System.String AppleAuth.AppleAuthQuickLoginArgs::Nonce
	String_t* ___Nonce_0;
	// System.String AppleAuth.AppleAuthQuickLoginArgs::State
	String_t* ___State_1;
};
// Native definition for P/Invoke marshalling of AppleAuth.AppleAuthQuickLoginArgs
struct AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_pinvoke
{
	char* ___Nonce_0;
	char* ___State_1;
};
// Native definition for COM marshalling of AppleAuth.AppleAuthQuickLoginArgs
struct AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_com
{
	Il2CppChar* ___Nonce_0;
	Il2CppChar* ___State_1;
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

// System.Byte
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	// System.Byte System.Byte::m_value
	uint8_t ___m_value_0;
};

// AppleAuth.Native.FullPersonName
struct FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8  : public PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78
{
	// System.Boolean AppleAuth.Native.FullPersonName::_hasPhoneticRepresentation
	bool ____hasPhoneticRepresentation_6;
	// AppleAuth.Native.PersonName AppleAuth.Native.FullPersonName::_phoneticRepresentation
	PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* ____phoneticRepresentation_7;
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

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
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

// System.Action`1<AppleAuth.Enums.CredentialState>
struct Action_1_t7CF092A129C468D25C4E6E5AC20FE3453FC7EC66  : public MulticastDelegate_t
{
};

// System.Action`1<AppleAuth.Interfaces.IAppleError>
struct Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA  : public MulticastDelegate_t
{
};

// System.Action`1<AppleAuth.Interfaces.ICredential>
struct Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80  : public MulticastDelegate_t
{
};

// System.Action`1<System.String>
struct Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A  : public MulticastDelegate_t
{
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
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
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


// System.Void AppleAuth.Native.SerializationTools::FixSerializationForArray<System.Object>(T[]&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationTools_FixSerializationForArray_TisRuntimeObject_mBC1A8C31FC580BFDE234CD39A367DD4382F5ABB5_gshared (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918** ___originalArray0, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<System.Object>(T&,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationTools_FixSerializationForObject_TisRuntimeObject_m48BB56AAD0FC34A024D1B919658D9861349E9D28_gshared (RuntimeObject** ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<System.Int32>(T&,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406_gshared (int32_t* ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method) ;
// T UnityEngine.JsonUtility::FromJson<System.Object>(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* JsonUtility_FromJson_TisRuntimeObject_m3A645CB2B6525E4A5835EA8A8CEBD39C7E2C444A_gshared (String_t* ___json0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::Add(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___item0, const RuntimeMethod* method) ;
// T[] System.Collections.Generic.List`1<System.Object>::ToArray()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* List_1_ToArray_mD7E4F8E7C11C3C67CB5739FCC0A6E86106A6291F_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;

// System.Void AppleAuth.AppleAuthLoginArgs::.ctor(AppleAuth.Enums.LoginOptions,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthLoginArgs__ctor_m979F04437958F29BD5E8FBB8B91085932E47DEC2 (AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4* __this, int32_t ___options0, String_t* ___nonce1, String_t* ___state2, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::Log(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_Log_m86567BCF22BBE7809747817453CACA0E41E68219 (RuntimeObject* ___message0, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void AppleAuth.AppleAuthManager::QuickLogin(AppleAuth.AppleAuthQuickLoginArgs,System.Action`1<AppleAuth.Interfaces.ICredential>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_QuickLogin_m6729A39051EC9885AD1668BA28B94B9489D39F08 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C ___quickLoginArgs0, Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* ___successCallback1, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback2, const RuntimeMethod* method) ;
// System.Void System.Exception::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F (Exception_t* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Void AppleAuth.AppleAuthManager::LoginWithAppleId(AppleAuth.AppleAuthLoginArgs,System.Action`1<AppleAuth.Interfaces.ICredential>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_LoginWithAppleId_m41AA28DCBBCED940184CCD2D353C8B8D7E3EA355 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4 ___loginArgs0, Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* ___successCallback1, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback2, const RuntimeMethod* method) ;
// System.Void AppleAuth.AppleAuthQuickLoginArgs::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthQuickLoginArgs__ctor_m29E70947A3CB463F0138B6F58BC35EA58A851AB4 (AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C* __this, String_t* ___nonce0, String_t* ___state1, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForString(System.String&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4 (String_t** ___originalString0, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForArray<System.String>(T[]&)
inline void SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248** ___originalArray0, const RuntimeMethod* method)
{
	((  void (*) (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248**, const RuntimeMethod*))SerializationTools_FixSerializationForArray_TisRuntimeObject_mBC1A8C31FC580BFDE234CD39A367DD4382F5ABB5_gshared)(___originalArray0, method);
}
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<AppleAuth.Native.FullPersonName>(T&,System.Boolean)
inline void SerializationTools_FixSerializationForObject_TisFullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8_m59A130EAEC5E3F8CC409BC8FC7E9F71454448D6B (FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8** ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method)
{
	((  void (*) (FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8**, bool, const RuntimeMethod*))SerializationTools_FixSerializationForObject_TisRuntimeObject_m48BB56AAD0FC34A024D1B919658D9861349E9D28_gshared)(___originalObject0, ___hasOriginalObject1, method);
}
// System.Byte[] AppleAuth.Native.SerializationTools::GetBytesFromBase64String(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* SerializationTools_GetBytesFromBase64String_mF2110593D1868F8FC190B29D1ADD9112019F08CD (String_t* ___base64String0, String_t* ___fieldName1, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<System.Int32>(T&,System.Boolean)
inline void SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406 (int32_t* ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method)
{
	((  void (*) (int32_t*, bool, const RuntimeMethod*))SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406_gshared)(___originalObject0, ___hasOriginalObject1, method);
}
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<AppleAuth.Native.AppleError>(T&,System.Boolean)
inline void SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA** ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method)
{
	((  void (*) (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA**, bool, const RuntimeMethod*))SerializationTools_FixSerializationForObject_TisRuntimeObject_m48BB56AAD0FC34A024D1B919658D9861349E9D28_gshared)(___originalObject0, ___hasOriginalObject1, method);
}
// System.Void AppleAuth.Native.PersonName::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PersonName_OnAfterDeserialize_m62A1515986426365B43F5A7BF5B7FB130EDA0D6E (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<AppleAuth.Native.PersonName>(T&,System.Boolean)
inline void SerializationTools_FixSerializationForObject_TisPersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78_m42CA8587586194E3CBA0F51F519B99077D3133D9 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78** ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method)
{
	((  void (*) (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78**, bool, const RuntimeMethod*))SerializationTools_FixSerializationForObject_TisRuntimeObject_m48BB56AAD0FC34A024D1B919658D9861349E9D28_gshared)(___originalObject0, ___hasOriginalObject1, method);
}
// System.Void AppleAuth.Native.PersonName::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PersonName__ctor_m0F0E4933C32F156BAA3F4DB66D4BE72313F76C55 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) ;
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<AppleAuth.Native.AppleIDCredential>(T&,System.Boolean)
inline void SerializationTools_FixSerializationForObject_TisAppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75_m9E1C59F3B0E71A90A97B4C5FFFEC8E2B5BDE24E8 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75** ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method)
{
	((  void (*) (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75**, bool, const RuntimeMethod*))SerializationTools_FixSerializationForObject_TisRuntimeObject_m48BB56AAD0FC34A024D1B919658D9861349E9D28_gshared)(___originalObject0, ___hasOriginalObject1, method);
}
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForObject<AppleAuth.Native.PasswordCredential>(T&,System.Boolean)
inline void SerializationTools_FixSerializationForObject_TisPasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1_m7DB7C09BECB1A15FE9EC980663BBBB4BB135CF15 (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1** ___originalObject0, bool ___hasOriginalObject1, const RuntimeMethod* method)
{
	((  void (*) (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1**, bool, const RuntimeMethod*))SerializationTools_FixSerializationForObject_TisRuntimeObject_m48BB56AAD0FC34A024D1B919658D9861349E9D28_gshared)(___originalObject0, ___hasOriginalObject1, method);
}
// T UnityEngine.JsonUtility::FromJson<AppleAuth.Native.CredentialStateResponse>(System.String)
inline CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* JsonUtility_FromJson_TisCredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27_mB6AB6E418CE795BA56D5F91DD65AD5E94A942C86 (String_t* ___json0, const RuntimeMethod* method)
{
	return ((  CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* (*) (String_t*, const RuntimeMethod*))JsonUtility_FromJson_TisRuntimeObject_m3A645CB2B6525E4A5835EA8A8CEBD39C7E2C444A_gshared)(___json0, method);
}
// T UnityEngine.JsonUtility::FromJson<AppleAuth.Native.LoginWithAppleIdResponse>(System.String)
inline LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* JsonUtility_FromJson_TisLoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F_m968AA1B827E76342EC45CF035AAC2D0B2FCF92B8 (String_t* ___json0, const RuntimeMethod* method)
{
	return ((  LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* (*) (String_t*, const RuntimeMethod*))JsonUtility_FromJson_TisRuntimeObject_m3A645CB2B6525E4A5835EA8A8CEBD39C7E2C444A_gshared)(___json0, method);
}
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A (String_t* ___value0, const RuntimeMethod* method) ;
// System.Byte[] System.Convert::FromBase64String(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* Convert_FromBase64String_m421F8600CA5124E047E3D7C2BC1B653F67BC48A1 (String_t* ___s0, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_mAF2CE02CC0CB7460753D0A1A91CCF2B1E9804C5D (String_t* ___str00, String_t* ___str11, const RuntimeMethod* method) ;
// System.Void System.Console::WriteLine(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Console_WriteLine_mB3B6E89C2D3CCB7C284B46F30233782BFF942709 (String_t* ___value0, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m2570A2A5B32A5E9D9F0F38B37459DA18736C823E (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___handle0, const RuntimeMethod* method) ;
// System.Boolean System.Enum::IsDefined(System.Type,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enum_IsDefined_m715E9AAD26B4AAA4B08E4D6AED73237174E82BB4 (Type_t* ___enumType0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.String>::.ctor()
inline void List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1<System.String>::Add(T)
inline void List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_inline (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* __this, String_t* ___item0, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*, String_t*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___item0, method);
}
// T[] System.Collections.Generic.List`1<System.String>::ToArray()
inline StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* List_1_ToArray_m2C402D882AA60FC1D5C7C09A129BE7779F833B4A (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* __this, const RuntimeMethod* method)
{
	return ((  StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* (*) (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*, const RuntimeMethod*))List_1_ToArray_mD7E4F8E7C11C3C67CB5739FCC0A6E86106A6291F_gshared)(__this, method);
}
// System.String System.String::Join(System.String,System.String[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Join_mE405D676C6881553258F8BAD40A20B462D611068 (String_t* ___separator0, StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___value1, const RuntimeMethod* method) ;
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
// Conversion methods for marshalling of: AppleAuth.AppleAuthLoginArgs
IL2CPP_EXTERN_C void AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshal_pinvoke(const AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4& unmarshaled, AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_pinvoke& marshaled)
{
	marshaled.___Options_0 = unmarshaled.___Options_0;
	marshaled.___Nonce_1 = il2cpp_codegen_marshal_string(unmarshaled.___Nonce_1);
	marshaled.___State_2 = il2cpp_codegen_marshal_string(unmarshaled.___State_2);
}
IL2CPP_EXTERN_C void AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshal_pinvoke_back(const AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_pinvoke& marshaled, AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4& unmarshaled)
{
	int32_t unmarshaledOptions_temp_0 = 0;
	unmarshaledOptions_temp_0 = marshaled.___Options_0;
	unmarshaled.___Options_0 = unmarshaledOptions_temp_0;
	unmarshaled.___Nonce_1 = il2cpp_codegen_marshal_string_result(marshaled.___Nonce_1);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___Nonce_1), (void*)il2cpp_codegen_marshal_string_result(marshaled.___Nonce_1));
	unmarshaled.___State_2 = il2cpp_codegen_marshal_string_result(marshaled.___State_2);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___State_2), (void*)il2cpp_codegen_marshal_string_result(marshaled.___State_2));
}
// Conversion method for clean up from marshalling of: AppleAuth.AppleAuthLoginArgs
IL2CPP_EXTERN_C void AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshal_pinvoke_cleanup(AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_marshal_free(marshaled.___Nonce_1);
	marshaled.___Nonce_1 = NULL;
	il2cpp_codegen_marshal_free(marshaled.___State_2);
	marshaled.___State_2 = NULL;
}
// Conversion methods for marshalling of: AppleAuth.AppleAuthLoginArgs
IL2CPP_EXTERN_C void AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshal_com(const AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4& unmarshaled, AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_com& marshaled)
{
	marshaled.___Options_0 = unmarshaled.___Options_0;
	marshaled.___Nonce_1 = il2cpp_codegen_marshal_bstring(unmarshaled.___Nonce_1);
	marshaled.___State_2 = il2cpp_codegen_marshal_bstring(unmarshaled.___State_2);
}
IL2CPP_EXTERN_C void AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshal_com_back(const AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_com& marshaled, AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4& unmarshaled)
{
	int32_t unmarshaledOptions_temp_0 = 0;
	unmarshaledOptions_temp_0 = marshaled.___Options_0;
	unmarshaled.___Options_0 = unmarshaledOptions_temp_0;
	unmarshaled.___Nonce_1 = il2cpp_codegen_marshal_bstring_result(marshaled.___Nonce_1);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___Nonce_1), (void*)il2cpp_codegen_marshal_bstring_result(marshaled.___Nonce_1));
	unmarshaled.___State_2 = il2cpp_codegen_marshal_bstring_result(marshaled.___State_2);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___State_2), (void*)il2cpp_codegen_marshal_bstring_result(marshaled.___State_2));
}
// Conversion method for clean up from marshalling of: AppleAuth.AppleAuthLoginArgs
IL2CPP_EXTERN_C void AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshal_com_cleanup(AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4_marshaled_com& marshaled)
{
	il2cpp_codegen_marshal_free_bstring(marshaled.___Nonce_1);
	marshaled.___Nonce_1 = NULL;
	il2cpp_codegen_marshal_free_bstring(marshaled.___State_2);
	marshaled.___State_2 = NULL;
}
// System.Void AppleAuth.AppleAuthLoginArgs::.ctor(AppleAuth.Enums.LoginOptions,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthLoginArgs__ctor_m979F04437958F29BD5E8FBB8B91085932E47DEC2 (AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4* __this, int32_t ___options0, String_t* ___nonce1, String_t* ___state2, const RuntimeMethod* method) 
{
	{
		// this.Options = options;
		int32_t L_0 = ___options0;
		__this->___Options_0 = L_0;
		// this.Nonce = nonce;
		String_t* L_1 = ___nonce1;
		__this->___Nonce_1 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___Nonce_1), (void*)L_1);
		// this.State = state;
		String_t* L_2 = ___state2;
		__this->___State_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___State_2), (void*)L_2);
		// }
		return;
	}
}
IL2CPP_EXTERN_C  void AppleAuthLoginArgs__ctor_m979F04437958F29BD5E8FBB8B91085932E47DEC2_AdjustorThunk (RuntimeObject* __this, int32_t ___options0, String_t* ___nonce1, String_t* ___state2, const RuntimeMethod* method)
{
	AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4*>(__this + _offset);
	AppleAuthLoginArgs__ctor_m979F04437958F29BD5E8FBB8B91085932E47DEC2(_thisAdjusted, ___options0, ___nonce1, ___state2, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void AppleAuth.AppleAuthManager::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager__cctor_m107F24BE6ABB00C0EBFB52D1E327D9317684D850 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral88ABE2A389F33F220D85F9A81EE6463C0A1A4D33);
		s_Il2CppMethodInitialized = true;
	}
	{
		// UnityEngine.Debug.Log(versionMessage);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m86567BCF22BBE7809747817453CACA0E41E68219(_stringLiteral88ABE2A389F33F220D85F9A81EE6463C0A1A4D33, NULL);
		// }
		return;
	}
}
// System.Boolean AppleAuth.AppleAuthManager::get_IsCurrentPlatformSupported()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AppleAuthManager_get_IsCurrentPlatformSupported_m7A4D6910C7B0B1E2F00006EF43CDBF37FD0E26D1 (const RuntimeMethod* method) 
{
	{
		// return false;
		return (bool)0;
	}
}
// System.Void AppleAuth.AppleAuthManager::.ctor(AppleAuth.Interfaces.IPayloadDeserializer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager__ctor_mFCE1994D8BD7BFCC9EA201E091A1C09BF63945B3 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, RuntimeObject* ___payloadDeserializer0, const RuntimeMethod* method) 
{
	{
		// public AppleAuthManager(IPayloadDeserializer payloadDeserializer)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// }
		return;
	}
}
// System.Void AppleAuth.AppleAuthManager::QuickLogin(System.Action`1<AppleAuth.Interfaces.ICredential>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_QuickLogin_m50DC36CEED4EFA9A98B758F7BE696F01D3957506 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* ___successCallback0, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback1, const RuntimeMethod* method) 
{
	AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// this.QuickLogin(new AppleAuthQuickLoginArgs(), successCallback, errorCallback);
		il2cpp_codegen_initobj((&V_0), sizeof(AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C));
		AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C L_0 = V_0;
		Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* L_1 = ___successCallback0;
		Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* L_2 = ___errorCallback1;
		AppleAuthManager_QuickLogin_m6729A39051EC9885AD1668BA28B94B9489D39F08(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void AppleAuth.AppleAuthManager::QuickLogin(AppleAuth.AppleAuthQuickLoginArgs,System.Action`1<AppleAuth.Interfaces.ICredential>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_QuickLogin_m6729A39051EC9885AD1668BA28B94B9489D39F08 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C ___quickLoginArgs0, Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* ___successCallback1, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback2, const RuntimeMethod* method) 
{
	{
		// throw new Exception("AppleAuthManager is not supported in this platform");
		Exception_t* L_0 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
		NullCheck(L_0);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCDD4C83DD410201E5E465766DBA93DED78585395)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AppleAuthManager_QuickLogin_m6729A39051EC9885AD1668BA28B94B9489D39F08_RuntimeMethod_var)));
	}
}
// System.Void AppleAuth.AppleAuthManager::LoginWithAppleId(AppleAuth.Enums.LoginOptions,System.Action`1<AppleAuth.Interfaces.ICredential>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_LoginWithAppleId_m7F9D42479BB718C417A879E00F86EA3844B0405F (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, int32_t ___options0, Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* ___successCallback1, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback2, const RuntimeMethod* method) 
{
	{
		// this.LoginWithAppleId(new AppleAuthLoginArgs(options), successCallback, errorCallback);
		int32_t L_0 = ___options0;
		AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4 L_1;
		memset((&L_1), 0, sizeof(L_1));
		AppleAuthLoginArgs__ctor_m979F04437958F29BD5E8FBB8B91085932E47DEC2((&L_1), L_0, (String_t*)NULL, (String_t*)NULL, /*hidden argument*/NULL);
		Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* L_2 = ___successCallback1;
		Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* L_3 = ___errorCallback2;
		AppleAuthManager_LoginWithAppleId_m41AA28DCBBCED940184CCD2D353C8B8D7E3EA355(__this, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void AppleAuth.AppleAuthManager::LoginWithAppleId(AppleAuth.AppleAuthLoginArgs,System.Action`1<AppleAuth.Interfaces.ICredential>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_LoginWithAppleId_m41AA28DCBBCED940184CCD2D353C8B8D7E3EA355 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, AppleAuthLoginArgs_tEC38AFD6B2C1CCBD455014707F0E505CDF8972B4 ___loginArgs0, Action_1_tB908D6FD75107728FE1DCDA76696B95672204A80* ___successCallback1, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback2, const RuntimeMethod* method) 
{
	{
		// throw new Exception("AppleAuthManager is not supported in this platform");
		Exception_t* L_0 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
		NullCheck(L_0);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCDD4C83DD410201E5E465766DBA93DED78585395)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AppleAuthManager_LoginWithAppleId_m41AA28DCBBCED940184CCD2D353C8B8D7E3EA355_RuntimeMethod_var)));
	}
}
// System.Void AppleAuth.AppleAuthManager::GetCredentialState(System.String,System.Action`1<AppleAuth.Enums.CredentialState>,System.Action`1<AppleAuth.Interfaces.IAppleError>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_GetCredentialState_m7646C8E8F7DFA9A8C77E576C39DFE4059BE5AD02 (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, String_t* ___userId0, Action_1_t7CF092A129C468D25C4E6E5AC20FE3453FC7EC66* ___successCallback1, Action_1_tC3903E0A1B057943FABF1B75BE7AAD0683CA51CA* ___errorCallback2, const RuntimeMethod* method) 
{
	{
		// throw new Exception("AppleAuthManager is not supported in this platform");
		Exception_t* L_0 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
		NullCheck(L_0);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCDD4C83DD410201E5E465766DBA93DED78585395)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AppleAuthManager_GetCredentialState_m7646C8E8F7DFA9A8C77E576C39DFE4059BE5AD02_RuntimeMethod_var)));
	}
}
// System.Void AppleAuth.AppleAuthManager::SetCredentialsRevokedCallback(System.Action`1<System.String>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_SetCredentialsRevokedCallback_m0C1A9F85CAE89C2AAAB94863C48F535D315E0CBA (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, Action_1_t3CB5D1A819C3ED3F99E9E39F890F18633253949A* ___credentialsRevokedCallback0, const RuntimeMethod* method) 
{
	{
		// }
		return;
	}
}
// System.Void AppleAuth.AppleAuthManager::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthManager_Update_mE6CCC051DC13A8B6A838B4E52CEFDA6683B0956A (AppleAuthManager_t9A6ECFB3E5DA602DB80B9AA03575872FF3B853E4* __this, const RuntimeMethod* method) 
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
// Conversion methods for marshalling of: AppleAuth.AppleAuthQuickLoginArgs
IL2CPP_EXTERN_C void AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshal_pinvoke(const AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C& unmarshaled, AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_pinvoke& marshaled)
{
	marshaled.___Nonce_0 = il2cpp_codegen_marshal_string(unmarshaled.___Nonce_0);
	marshaled.___State_1 = il2cpp_codegen_marshal_string(unmarshaled.___State_1);
}
IL2CPP_EXTERN_C void AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshal_pinvoke_back(const AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_pinvoke& marshaled, AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C& unmarshaled)
{
	unmarshaled.___Nonce_0 = il2cpp_codegen_marshal_string_result(marshaled.___Nonce_0);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___Nonce_0), (void*)il2cpp_codegen_marshal_string_result(marshaled.___Nonce_0));
	unmarshaled.___State_1 = il2cpp_codegen_marshal_string_result(marshaled.___State_1);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___State_1), (void*)il2cpp_codegen_marshal_string_result(marshaled.___State_1));
}
// Conversion method for clean up from marshalling of: AppleAuth.AppleAuthQuickLoginArgs
IL2CPP_EXTERN_C void AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshal_pinvoke_cleanup(AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_marshal_free(marshaled.___Nonce_0);
	marshaled.___Nonce_0 = NULL;
	il2cpp_codegen_marshal_free(marshaled.___State_1);
	marshaled.___State_1 = NULL;
}
// Conversion methods for marshalling of: AppleAuth.AppleAuthQuickLoginArgs
IL2CPP_EXTERN_C void AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshal_com(const AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C& unmarshaled, AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_com& marshaled)
{
	marshaled.___Nonce_0 = il2cpp_codegen_marshal_bstring(unmarshaled.___Nonce_0);
	marshaled.___State_1 = il2cpp_codegen_marshal_bstring(unmarshaled.___State_1);
}
IL2CPP_EXTERN_C void AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshal_com_back(const AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_com& marshaled, AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C& unmarshaled)
{
	unmarshaled.___Nonce_0 = il2cpp_codegen_marshal_bstring_result(marshaled.___Nonce_0);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___Nonce_0), (void*)il2cpp_codegen_marshal_bstring_result(marshaled.___Nonce_0));
	unmarshaled.___State_1 = il2cpp_codegen_marshal_bstring_result(marshaled.___State_1);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___State_1), (void*)il2cpp_codegen_marshal_bstring_result(marshaled.___State_1));
}
// Conversion method for clean up from marshalling of: AppleAuth.AppleAuthQuickLoginArgs
IL2CPP_EXTERN_C void AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshal_com_cleanup(AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C_marshaled_com& marshaled)
{
	il2cpp_codegen_marshal_free_bstring(marshaled.___Nonce_0);
	marshaled.___Nonce_0 = NULL;
	il2cpp_codegen_marshal_free_bstring(marshaled.___State_1);
	marshaled.___State_1 = NULL;
}
// System.Void AppleAuth.AppleAuthQuickLoginArgs::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleAuthQuickLoginArgs__ctor_m29E70947A3CB463F0138B6F58BC35EA58A851AB4 (AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C* __this, String_t* ___nonce0, String_t* ___state1, const RuntimeMethod* method) 
{
	{
		// this.Nonce = nonce;
		String_t* L_0 = ___nonce0;
		__this->___Nonce_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___Nonce_0), (void*)L_0);
		// this.State = state;
		String_t* L_1 = ___state1;
		__this->___State_1 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___State_1), (void*)L_1);
		// }
		return;
	}
}
IL2CPP_EXTERN_C  void AppleAuthQuickLoginArgs__ctor_m29E70947A3CB463F0138B6F58BC35EA58A851AB4_AdjustorThunk (RuntimeObject* __this, String_t* ___nonce0, String_t* ___state1, const RuntimeMethod* method)
{
	AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AppleAuthQuickLoginArgs_tBB2B39ED7803A5FA6916A70CB519F33099AAE38C*>(__this + _offset);
	AppleAuthQuickLoginArgs__ctor_m29E70947A3CB463F0138B6F58BC35EA58A851AB4(_thisAdjusted, ___nonce0, ___state1, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Int32 AppleAuth.Native.AppleError::get_Code()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AppleError_get_Code_m961287BECF360B2EDAD83FA060A8C7FDB6418FD5 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public int Code { get { return this._code; } }
		int32_t L_0 = __this->____code_0;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleError::get_Domain()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleError_get_Domain_m63E5CABE6AF7F50CC0FB9B9C55D074AC01D22DDF (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public string Domain { get { return this._domain; } }
		String_t* L_0 = __this->____domain_1;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleError::get_LocalizedDescription()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleError_get_LocalizedDescription_m430AE126F8F82BCD7D41545FC20102B666C700E5 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public string LocalizedDescription { get { return this._localizedDescription; } }
		String_t* L_0 = __this->____localizedDescription_2;
		return L_0;
	}
}
// System.String[] AppleAuth.Native.AppleError::get_LocalizedRecoveryOptions()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* AppleError_get_LocalizedRecoveryOptions_m70B334E472068D36B4797AF8BE86C641E347F564 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public string[] LocalizedRecoveryOptions { get { return this._localizedRecoveryOptions; } }
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_0 = __this->____localizedRecoveryOptions_3;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleError::get_LocalizedRecoverySuggestion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleError_get_LocalizedRecoverySuggestion_m2CA60B369F391895F8307B37BB767122522B29A9 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public string LocalizedRecoverySuggestion { get { return this._localizedRecoverySuggestion; } }
		String_t* L_0 = __this->____localizedRecoverySuggestion_4;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleError::get_LocalizedFailureReason()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleError_get_LocalizedFailureReason_m51C96BF4FE7F7E50B5739793C800343D05489DB9 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public string LocalizedFailureReason { get { return this._localizedFailureReason; } }
		String_t* L_0 = __this->____localizedFailureReason_5;
		return L_0;
	}
}
// System.Void AppleAuth.Native.AppleError::OnBeforeSerialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleError_OnBeforeSerialize_mAEAD2F39FA81174491484481AD72D7B005DB67F7 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	{
		// public void OnBeforeSerialize() { }
		return;
	}
}
// System.Void AppleAuth.Native.AppleError::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleError_OnAfterDeserialize_m0BCD7127B5E7D5505CE32993DA8F2A4D2AA592E2 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SerializationTools.FixSerializationForString(ref this._domain);
		String_t** L_0 = (&__this->____domain_1);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_0, NULL);
		// SerializationTools.FixSerializationForString(ref this._localizedDescription);
		String_t** L_1 = (&__this->____localizedDescription_2);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_1, NULL);
		// SerializationTools.FixSerializationForString(ref this._localizedRecoverySuggestion);
		String_t** L_2 = (&__this->____localizedRecoverySuggestion_4);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_2, NULL);
		// SerializationTools.FixSerializationForString(ref this._localizedFailureReason);
		String_t** L_3 = (&__this->____localizedFailureReason_5);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_3, NULL);
		// SerializationTools.FixSerializationForArray(ref this._localizedRecoveryOptions);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248** L_4 = (&__this->____localizedRecoveryOptions_3);
		SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E(L_4, SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.AppleError::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleError__ctor_m66C93CADE0B99B97DD2891FDA0B886E2B02AADD1 (AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* __this, const RuntimeMethod* method) 
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
// System.Byte[] AppleAuth.Native.AppleIDCredential::get_IdentityToken()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* AppleIDCredential_get_IdentityToken_m34B56460123F4D5FA0F3F0E9F11889E776583355 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public byte[] IdentityToken { get { return this._identityToken; } }
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = __this->____identityToken_9;
		return L_0;
	}
}
// System.Byte[] AppleAuth.Native.AppleIDCredential::get_AuthorizationCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* AppleIDCredential_get_AuthorizationCode_mF451C6107719D30009CC68965869BA7C0BD50CE8 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public byte[] AuthorizationCode { get { return this._authorizationCode; } }
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = __this->____authorizationCode_10;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleIDCredential::get_State()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleIDCredential_get_State_m84B3905825D39FD46B45AF489FA8163247C74278 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public string State { get { return this._state; } }
		String_t* L_0 = __this->____state_2;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleIDCredential::get_User()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleIDCredential_get_User_m981CFCEE8723C5A25E0C1F4DA3289D9831EA57F5 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public string User { get { return this._user; } }
		String_t* L_0 = __this->____user_3;
		return L_0;
	}
}
// System.String[] AppleAuth.Native.AppleIDCredential::get_AuthorizedScopes()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* AppleIDCredential_get_AuthorizedScopes_m1FC899AEECADDD9871C8622C6F08E73E812C9D92 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public string[] AuthorizedScopes { get { return this._authorizedScopes; } }
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_0 = __this->____authorizedScopes_4;
		return L_0;
	}
}
// AppleAuth.Interfaces.IPersonName AppleAuth.Native.AppleIDCredential::get_FullName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AppleIDCredential_get_FullName_mFD2563DD49DE8936E5F71447BA75E5D1836402D8 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public IPersonName FullName { get { return this._fullName; } }
		FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8* L_0 = __this->____fullName_6;
		return L_0;
	}
}
// System.String AppleAuth.Native.AppleIDCredential::get_Email()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleIDCredential_get_Email_m714C9A646F5DFE85592A0E4A9FD3E68689685ABE (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public string Email { get { return this._email; } }
		String_t* L_0 = __this->____email_7;
		return L_0;
	}
}
// AppleAuth.Enums.RealUserStatus AppleAuth.Native.AppleIDCredential::get_RealUserStatus()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AppleIDCredential_get_RealUserStatus_mE69FF5EC598FB13CF28741DC57528CC2FFB9CF9E (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public RealUserStatus RealUserStatus { get { return (RealUserStatus) this._realUserStatus; } }
		int32_t L_0 = __this->____realUserStatus_8;
		return (int32_t)(L_0);
	}
}
// System.Void AppleAuth.Native.AppleIDCredential::OnBeforeSerialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleIDCredential_OnBeforeSerialize_m131D67351E1C50C5B86C9DF5524BDDDE44AA0E61 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	{
		// public void OnBeforeSerialize() { }
		return;
	}
}
// System.Void AppleAuth.Native.AppleIDCredential::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleIDCredential_OnAfterDeserialize_mB5366B49FF04AF0671E94C27ED9A3715EEB295C9 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisFullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8_m59A130EAEC5E3F8CC409BC8FC7E9F71454448D6B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4B31B12774EADD6A7DE8A6382F262DAD80E785C9);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDA3990537E355E81C23A6B8320705118BC1046ED);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SerializationTools.FixSerializationForString(ref this._base64IdentityToken);
		String_t** L_0 = (&__this->____base64IdentityToken_0);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_0, NULL);
		// SerializationTools.FixSerializationForString(ref this._base64AuthorizationCode);
		String_t** L_1 = (&__this->____base64AuthorizationCode_1);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_1, NULL);
		// SerializationTools.FixSerializationForString(ref this._state);
		String_t** L_2 = (&__this->____state_2);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_2, NULL);
		// SerializationTools.FixSerializationForString(ref this._user);
		String_t** L_3 = (&__this->____user_3);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_3, NULL);
		// SerializationTools.FixSerializationForString(ref this._email);
		String_t** L_4 = (&__this->____email_7);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_4, NULL);
		// SerializationTools.FixSerializationForArray(ref this._authorizedScopes);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248** L_5 = (&__this->____authorizedScopes_4);
		SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E(L_5, SerializationTools_FixSerializationForArray_TisString_t_m63AE28DD4F64C75967D78D09D9D0EBA19CD4996E_RuntimeMethod_var);
		// SerializationTools.FixSerializationForObject(ref this._fullName, this._hasFullName);
		FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8** L_6 = (&__this->____fullName_6);
		bool L_7 = __this->____hasFullName_5;
		SerializationTools_FixSerializationForObject_TisFullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8_m59A130EAEC5E3F8CC409BC8FC7E9F71454448D6B(L_6, L_7, SerializationTools_FixSerializationForObject_TisFullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8_m59A130EAEC5E3F8CC409BC8FC7E9F71454448D6B_RuntimeMethod_var);
		// this._identityToken = SerializationTools.GetBytesFromBase64String(this._base64IdentityToken, "_identityToken");
		String_t* L_8 = __this->____base64IdentityToken_0;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_9;
		L_9 = SerializationTools_GetBytesFromBase64String_mF2110593D1868F8FC190B29D1ADD9112019F08CD(L_8, _stringLiteralDA3990537E355E81C23A6B8320705118BC1046ED, NULL);
		__this->____identityToken_9 = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____identityToken_9), (void*)L_9);
		// this._authorizationCode = SerializationTools.GetBytesFromBase64String(this._base64AuthorizationCode, "_authorizationCode");
		String_t* L_10 = __this->____base64AuthorizationCode_1;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_11;
		L_11 = SerializationTools_GetBytesFromBase64String_mF2110593D1868F8FC190B29D1ADD9112019F08CD(L_10, _stringLiteral4B31B12774EADD6A7DE8A6382F262DAD80E785C9, NULL);
		__this->____authorizationCode_10 = L_11;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____authorizationCode_10), (void*)L_11);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.AppleIDCredential::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleIDCredential__ctor_m51A44FA1D980EC9AF22D63D6072DB75A6FA01C63 (AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* __this, const RuntimeMethod* method) 
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
// System.Boolean AppleAuth.Native.CredentialStateResponse::get_Success()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool CredentialStateResponse_get_Success_m2AE6E6E1D34B0114F4C52C039174DDF9AFA6BC4D (CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* __this, const RuntimeMethod* method) 
{
	{
		// public bool Success { get { return this._success; } }
		bool L_0 = __this->____success_0;
		return L_0;
	}
}
// AppleAuth.Enums.CredentialState AppleAuth.Native.CredentialStateResponse::get_CredentialState()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t CredentialStateResponse_get_CredentialState_m65D7E15BA3EC5C3A7F367597A050193DD4209797 (CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* __this, const RuntimeMethod* method) 
{
	{
		// public CredentialState CredentialState { get { return (CredentialState) this._credentialState; } }
		int32_t L_0 = __this->____credentialState_3;
		return (int32_t)(L_0);
	}
}
// AppleAuth.Interfaces.IAppleError AppleAuth.Native.CredentialStateResponse::get_Error()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* CredentialStateResponse_get_Error_m57C00B45E8F57B0BE47A95942C096E33EE75E13C (CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* __this, const RuntimeMethod* method) 
{
	{
		// public IAppleError Error { get { return this._error; } }
		AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* L_0 = __this->____error_4;
		return L_0;
	}
}
// System.Void AppleAuth.Native.CredentialStateResponse::OnBeforeSerialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CredentialStateResponse_OnBeforeSerialize_mAC5BB42DE4654E67E38843519A7DC253F50850B5 (CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* __this, const RuntimeMethod* method) 
{
	{
		// public void OnBeforeSerialize() { }
		return;
	}
}
// System.Void AppleAuth.Native.CredentialStateResponse::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CredentialStateResponse_OnAfterDeserialize_mFB21B4973EF797A938B387C7055C5929051AEE60 (CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SerializationTools.FixSerializationForObject(ref this._credentialState, this._hasCredentialState);
		int32_t* L_0 = (&__this->____credentialState_3);
		bool L_1 = __this->____hasCredentialState_1;
		SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406(L_0, L_1, SerializationTools_FixSerializationForObject_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC2EDC922BF03B8F84C1649FF430ED1494A744406_RuntimeMethod_var);
		// SerializationTools.FixSerializationForObject(ref this._error, this._hasError);
		AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA** L_2 = (&__this->____error_4);
		bool L_3 = __this->____hasError_2;
		SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301(L_2, L_3, SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.CredentialStateResponse::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CredentialStateResponse__ctor_mD9EDB0D8782201A4B143772B6FE1140C1A20A6B3 (CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* __this, const RuntimeMethod* method) 
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
// AppleAuth.Interfaces.IPersonName AppleAuth.Native.FullPersonName::get_PhoneticRepresentation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* FullPersonName_get_PhoneticRepresentation_m4C047353D1B6B779000A36AD5A9E95112FB4B78D (FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8* __this, const RuntimeMethod* method) 
{
	{
		// public new IPersonName PhoneticRepresentation { get { return _phoneticRepresentation; } }
		PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* L_0 = __this->____phoneticRepresentation_7;
		return L_0;
	}
}
// System.Void AppleAuth.Native.FullPersonName::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FullPersonName_OnAfterDeserialize_mD8D3FA785A18B097DA24A5346927BCE5891E2726 (FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisPersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78_m42CA8587586194E3CBA0F51F519B99077D3133D9_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// base.OnAfterDeserialize();
		PersonName_OnAfterDeserialize_m62A1515986426365B43F5A7BF5B7FB130EDA0D6E(__this, NULL);
		// SerializationTools.FixSerializationForObject(ref this._phoneticRepresentation, this._hasPhoneticRepresentation);
		PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78** L_0 = (&__this->____phoneticRepresentation_7);
		bool L_1 = __this->____hasPhoneticRepresentation_6;
		SerializationTools_FixSerializationForObject_TisPersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78_m42CA8587586194E3CBA0F51F519B99077D3133D9(L_0, L_1, SerializationTools_FixSerializationForObject_TisPersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78_m42CA8587586194E3CBA0F51F519B99077D3133D9_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.FullPersonName::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FullPersonName__ctor_m26740204D45973B681D9A56AAEA22BA07054781B (FullPersonName_t0454AAAB2D27D3182F2AB37B3F6B49BD94B6E9E8* __this, const RuntimeMethod* method) 
{
	{
		PersonName__ctor_m0F0E4933C32F156BAA3F4DB66D4BE72313F76C55(__this, NULL);
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
// System.Boolean AppleAuth.Native.LoginWithAppleIdResponse::get_Success()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool LoginWithAppleIdResponse_get_Success_mE6C377A510A993911F409C936A65584C9049779C (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
{
	{
		// public bool Success { get { return this._success; } }
		bool L_0 = __this->____success_0;
		return L_0;
	}
}
// AppleAuth.Interfaces.IAppleError AppleAuth.Native.LoginWithAppleIdResponse::get_Error()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* LoginWithAppleIdResponse_get_Error_mCCC27C3D0F46A1D52F2CE2D969005696E8E5AE76 (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
{
	{
		// public IAppleError Error { get { return this._error; } }
		AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA* L_0 = __this->____error_6;
		return L_0;
	}
}
// AppleAuth.Interfaces.IAppleIDCredential AppleAuth.Native.LoginWithAppleIdResponse::get_AppleIDCredential()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* LoginWithAppleIdResponse_get_AppleIDCredential_m8621F583B1DC2542599CD2C3A666A478660B2366 (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
{
	{
		// public IAppleIDCredential AppleIDCredential { get { return this._appleIdCredential; } }
		AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75* L_0 = __this->____appleIdCredential_4;
		return L_0;
	}
}
// AppleAuth.Interfaces.IPasswordCredential AppleAuth.Native.LoginWithAppleIdResponse::get_PasswordCredential()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* LoginWithAppleIdResponse_get_PasswordCredential_mF712CF23994A66D33B5AE81DE4DB37BFE0900DD0 (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
{
	{
		// public IPasswordCredential PasswordCredential { get { return this._passwordCredential; } }
		PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* L_0 = __this->____passwordCredential_5;
		return L_0;
	}
}
// System.Void AppleAuth.Native.LoginWithAppleIdResponse::OnBeforeSerialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LoginWithAppleIdResponse_OnBeforeSerialize_m7921D7B73269C8D01C61CD21A99F2D25E2A768D3 (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
{
	{
		// public void OnBeforeSerialize() { }
		return;
	}
}
// System.Void AppleAuth.Native.LoginWithAppleIdResponse::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LoginWithAppleIdResponse_OnAfterDeserialize_m5F4392761E520B362F204ADAA71470944CCB7226 (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisAppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75_m9E1C59F3B0E71A90A97B4C5FFFEC8E2B5BDE24E8_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SerializationTools_FixSerializationForObject_TisPasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1_m7DB7C09BECB1A15FE9EC980663BBBB4BB135CF15_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SerializationTools.FixSerializationForObject(ref this._error, this._hasError);
		AppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA** L_0 = (&__this->____error_6);
		bool L_1 = __this->____hasError_3;
		SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301(L_0, L_1, SerializationTools_FixSerializationForObject_TisAppleError_t1DB8D1CB5586B26914C6ADC9E708F907A4192CEA_m01339CDD14052364E32868B795C7C6CC7FBC9301_RuntimeMethod_var);
		// SerializationTools.FixSerializationForObject(ref this._appleIdCredential, this._hasAppleIdCredential);
		AppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75** L_2 = (&__this->____appleIdCredential_4);
		bool L_3 = __this->____hasAppleIdCredential_1;
		SerializationTools_FixSerializationForObject_TisAppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75_m9E1C59F3B0E71A90A97B4C5FFFEC8E2B5BDE24E8(L_2, L_3, SerializationTools_FixSerializationForObject_TisAppleIDCredential_tD23942C97513881D942DFF29A29F55D0B4EA8A75_m9E1C59F3B0E71A90A97B4C5FFFEC8E2B5BDE24E8_RuntimeMethod_var);
		// SerializationTools.FixSerializationForObject(ref this._passwordCredential, this._hasPasswordCredential);
		PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1** L_4 = (&__this->____passwordCredential_5);
		bool L_5 = __this->____hasPasswordCredential_2;
		SerializationTools_FixSerializationForObject_TisPasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1_m7DB7C09BECB1A15FE9EC980663BBBB4BB135CF15(L_4, L_5, SerializationTools_FixSerializationForObject_TisPasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1_m7DB7C09BECB1A15FE9EC980663BBBB4BB135CF15_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.LoginWithAppleIdResponse::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LoginWithAppleIdResponse__ctor_m559AF38D99EA5F01245B730C66C0C575A7D1DFA5 (LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* __this, const RuntimeMethod* method) 
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
// System.String AppleAuth.Native.PasswordCredential::get_User()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PasswordCredential_get_User_m14F3321F1778473BAD88C092D9DB7E4E6EBA2BFE (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* __this, const RuntimeMethod* method) 
{
	{
		// public string User { get { return this._user; } }
		String_t* L_0 = __this->____user_0;
		return L_0;
	}
}
// System.String AppleAuth.Native.PasswordCredential::get_Password()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PasswordCredential_get_Password_m550FE9C9FFC658487D37E4CCBC81646AAA0B6971 (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* __this, const RuntimeMethod* method) 
{
	{
		// public string Password { get { return this._password; } }
		String_t* L_0 = __this->____password_1;
		return L_0;
	}
}
// System.Void AppleAuth.Native.PasswordCredential::OnBeforeSerialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PasswordCredential_OnBeforeSerialize_m4BFB71F9E84B9BF858BD2B79B725972821DFE288 (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* __this, const RuntimeMethod* method) 
{
	{
		// public void OnBeforeSerialize() { }
		return;
	}
}
// System.Void AppleAuth.Native.PasswordCredential::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PasswordCredential_OnAfterDeserialize_m16D32A34A6F6046F6333F7BB5F69193B37D0F2F5 (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* __this, const RuntimeMethod* method) 
{
	{
		// SerializationTools.FixSerializationForString(ref this._user);
		String_t** L_0 = (&__this->____user_0);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_0, NULL);
		// SerializationTools.FixSerializationForString(ref this._password);
		String_t** L_1 = (&__this->____password_1);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_1, NULL);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.PasswordCredential::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PasswordCredential__ctor_m752B85E2013BE8BDA5F98F2E2BFF18BDFBF7676D (PasswordCredential_t63B67EAA715B6511D38EBF1B1440016648B450C1* __this, const RuntimeMethod* method) 
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
// AppleAuth.Interfaces.ICredentialStateResponse AppleAuth.Native.PayloadDeserializer::DeserializeCredentialStateResponse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PayloadDeserializer_DeserializeCredentialStateResponse_mD711A60594EEB3B4A2B82769E58C745CC50FD516 (PayloadDeserializer_tB856F635041B5A76FCF832A0A329EABD65C3BD07* __this, String_t* ___payload0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JsonUtility_FromJson_TisCredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27_mB6AB6E418CE795BA56D5F91DD65AD5E94A942C86_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return JsonUtility.FromJson<CredentialStateResponse>(payload);
		String_t* L_0 = ___payload0;
		CredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27* L_1;
		L_1 = JsonUtility_FromJson_TisCredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27_mB6AB6E418CE795BA56D5F91DD65AD5E94A942C86(L_0, JsonUtility_FromJson_TisCredentialStateResponse_t168DFACCC4FBA9919D9EBF9314CFC5652199EB27_mB6AB6E418CE795BA56D5F91DD65AD5E94A942C86_RuntimeMethod_var);
		return L_1;
	}
}
// AppleAuth.Interfaces.ILoginWithAppleIdResponse AppleAuth.Native.PayloadDeserializer::DeserializeLoginWithAppleIdResponse(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PayloadDeserializer_DeserializeLoginWithAppleIdResponse_mDD6FE763342C1ADB26C95A9F07ECE906FD33D22A (PayloadDeserializer_tB856F635041B5A76FCF832A0A329EABD65C3BD07* __this, String_t* ___payload0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&JsonUtility_FromJson_TisLoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F_m968AA1B827E76342EC45CF035AAC2D0B2FCF92B8_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return JsonUtility.FromJson<LoginWithAppleIdResponse>(payload);
		String_t* L_0 = ___payload0;
		LoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F* L_1;
		L_1 = JsonUtility_FromJson_TisLoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F_m968AA1B827E76342EC45CF035AAC2D0B2FCF92B8(L_0, JsonUtility_FromJson_TisLoginWithAppleIdResponse_t6A4F5AC518D2615E97D6972C7A90F40A5A10447F_m968AA1B827E76342EC45CF035AAC2D0B2FCF92B8_RuntimeMethod_var);
		return L_1;
	}
}
// System.Void AppleAuth.Native.PayloadDeserializer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PayloadDeserializer__ctor_mFBD229EF68EBE24EC32CA9609AA005AB90C65C42 (PayloadDeserializer_tB856F635041B5A76FCF832A0A329EABD65C3BD07* __this, const RuntimeMethod* method) 
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
// System.String AppleAuth.Native.PersonName::get_NamePrefix()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonName_get_NamePrefix_mD8E065B1341FE87F408B1A43AC98AB752EC37792 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public string NamePrefix { get { return _namePrefix; } }
		String_t* L_0 = __this->____namePrefix_0;
		return L_0;
	}
}
// System.String AppleAuth.Native.PersonName::get_GivenName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonName_get_GivenName_m0284F3BF19006384CE40D9F2584E9B898E3167BB (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public string GivenName { get { return _givenName; } }
		String_t* L_0 = __this->____givenName_1;
		return L_0;
	}
}
// System.String AppleAuth.Native.PersonName::get_MiddleName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonName_get_MiddleName_mCFAC728FA738FF02EA1673A41528F15E771B0127 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public string MiddleName { get { return _middleName; } }
		String_t* L_0 = __this->____middleName_2;
		return L_0;
	}
}
// System.String AppleAuth.Native.PersonName::get_FamilyName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonName_get_FamilyName_mFF3940EE55E62FC22A6CFA6975BCCFF796D6BB26 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public string FamilyName { get { return _familyName; } }
		String_t* L_0 = __this->____familyName_3;
		return L_0;
	}
}
// System.String AppleAuth.Native.PersonName::get_NameSuffix()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonName_get_NameSuffix_m28CC64A01463A58CF35EFC6A4CE6CFADCB2518E8 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public string NameSuffix { get { return _nameSuffix; } }
		String_t* L_0 = __this->____nameSuffix_4;
		return L_0;
	}
}
// System.String AppleAuth.Native.PersonName::get_Nickname()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonName_get_Nickname_m678081BDCCFFD3DED731F9AB66F7FB350166B4C1 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public string Nickname { get { return _nickname; } }
		String_t* L_0 = __this->____nickname_5;
		return L_0;
	}
}
// AppleAuth.Interfaces.IPersonName AppleAuth.Native.PersonName::get_PhoneticRepresentation()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PersonName_get_PhoneticRepresentation_m89AA341DA25ACEBFAA1E062842E646B1165A99DB (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public IPersonName PhoneticRepresentation { get { return null; } }
		return (RuntimeObject*)NULL;
	}
}
// System.Void AppleAuth.Native.PersonName::OnBeforeSerialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PersonName_OnBeforeSerialize_m4D57A809AC62FE6817EB498247E4AEE5B8B0B510 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// public void OnBeforeSerialize() { }
		return;
	}
}
// System.Void AppleAuth.Native.PersonName::OnAfterDeserialize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PersonName_OnAfterDeserialize_m62A1515986426365B43F5A7BF5B7FB130EDA0D6E (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
{
	{
		// SerializationTools.FixSerializationForString(ref this._namePrefix);
		String_t** L_0 = (&__this->____namePrefix_0);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_0, NULL);
		// SerializationTools.FixSerializationForString(ref this._givenName);
		String_t** L_1 = (&__this->____givenName_1);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_1, NULL);
		// SerializationTools.FixSerializationForString(ref this._middleName);
		String_t** L_2 = (&__this->____middleName_2);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_2, NULL);
		// SerializationTools.FixSerializationForString(ref this._familyName);
		String_t** L_3 = (&__this->____familyName_3);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_3, NULL);
		// SerializationTools.FixSerializationForString(ref this._nameSuffix);
		String_t** L_4 = (&__this->____nameSuffix_4);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_4, NULL);
		// SerializationTools.FixSerializationForString(ref this._nickname);
		String_t** L_5 = (&__this->____nickname_5);
		SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4(L_5, NULL);
		// }
		return;
	}
}
// System.Void AppleAuth.Native.PersonName::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PersonName__ctor_m0F0E4933C32F156BAA3F4DB66D4BE72313F76C55 (PersonName_tC85CD1987B6BB8340FF6C66426BE3A97E7753D78* __this, const RuntimeMethod* method) 
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
// System.Void AppleAuth.Native.SerializationTools::FixSerializationForString(System.String&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationTools_FixSerializationForString_mFD522DF2C74A196DF9AA6131A44E6F67ED3BF7C4 (String_t** ___originalString0, const RuntimeMethod* method) 
{
	{
		// if (string.IsNullOrEmpty(originalString))
		String_t** L_0 = ___originalString0;
		String_t* L_1 = *((String_t**)L_0);
		bool L_2;
		L_2 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_1, NULL);
		if (!L_2)
		{
			goto IL_000c;
		}
	}
	{
		// originalString = null;
		String_t** L_3 = ___originalString0;
		*((RuntimeObject**)L_3) = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(RuntimeObject**)L_3, (void*)(RuntimeObject*)NULL);
	}

IL_000c:
	{
		// }
		return;
	}
}
// System.Byte[] AppleAuth.Native.SerializationTools::GetBytesFromBase64String(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* SerializationTools_GetBytesFromBase64String_mF2110593D1868F8FC190B29D1ADD9112019F08CD (String_t* ___base64String0, String_t* ___fieldName1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* V_0 = NULL;
	Exception_t* V_1 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	Exception_t* G_B6_0 = NULL;
	String_t* G_B6_1 = NULL;
	Exception_t* G_B5_0 = NULL;
	String_t* G_B5_1 = NULL;
	String_t* G_B7_0 = NULL;
	String_t* G_B7_1 = NULL;
	{
		// if (base64String == null)
		String_t* L_0 = ___base64String0;
		if (L_0)
		{
			goto IL_0005;
		}
	}
	{
		// return null;
		return (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)NULL;
	}

IL_0005:
	{
		// var returnedBytes = default(byte[]);
		V_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)NULL;
	}
	try
	{// begin try (depth: 1)
		// returnedBytes = Convert.FromBase64String(base64String);
		String_t* L_1 = ___base64String0;
		il2cpp_codegen_runtime_class_init_inline(Convert_t7097FF336D592F7C06D88A98349A44646F91EFFC_il2cpp_TypeInfo_var);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_2;
		L_2 = Convert_FromBase64String_m421F8600CA5124E047E3D7C2BC1B653F67BC48A1(L_1, NULL);
		V_0 = L_2;
		// }
		goto IL_0041;
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0010;
		}
		throw e;
	}

CATCH_0010:
	{// begin catch(System.Exception)
		{
			// catch (Exception exception)
			V_1 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));
			// Console.WriteLine("Received exception while deserializing byte array for " + fieldName);
			String_t* L_3 = ___fieldName1;
			String_t* L_4;
			L_4 = String_Concat_mAF2CE02CC0CB7460753D0A1A91CCF2B1E9804C5D(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral66A865E28B2B5657FCF66691A6F7AC2B94FE50BA)), L_3, NULL);
			il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var)));
			Console_WriteLine_mB3B6E89C2D3CCB7C284B46F30233782BFF942709(L_4, NULL);
			// Console.WriteLine("Exception: " + exception);
			Exception_t* L_5 = V_1;
			Exception_t* L_6 = L_5;
			G_B5_0 = L_6;
			G_B5_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA95898025CC11DF26437FBBC4B43CA5F697F5DB1));
			if (L_6)
			{
				G_B6_0 = L_6;
				G_B6_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA95898025CC11DF26437FBBC4B43CA5F697F5DB1));
				goto IL_002e;
			}
		}
		{
			G_B7_0 = ((String_t*)(NULL));
			G_B7_1 = G_B5_1;
			goto IL_0033;
		}

IL_002e:
		{
			NullCheck(G_B6_0);
			String_t* L_7;
			L_7 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, G_B6_0);
			G_B7_0 = L_7;
			G_B7_1 = G_B6_1;
		}

IL_0033:
		{
			String_t* L_8;
			L_8 = String_Concat_mAF2CE02CC0CB7460753D0A1A91CCF2B1E9804C5D(G_B7_1, G_B7_0, NULL);
			il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Console_t5EDF9498D011BD48287171978EDBBA6964829C3E_il2cpp_TypeInfo_var)));
			Console_WriteLine_mB3B6E89C2D3CCB7C284B46F30233782BFF942709(L_8, NULL);
			// returnedBytes = null;
			V_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)NULL;
			// }
			IL2CPP_POP_ACTIVE_EXCEPTION();
			goto IL_0041;
		}
	}// end catch (depth: 1)

IL_0041:
	{
		// return returnedBytes;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_9 = V_0;
		return L_9;
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
// AppleAuth.Enums.AuthorizationErrorCode AppleAuth.Extensions.AppleErrorExtensions::GetAuthorizationErrorCode(AppleAuth.Interfaces.IAppleError)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AppleErrorExtensions_GetAuthorizationErrorCode_m60C5F29160375471CFE688E42620D1727224C333 (RuntimeObject* ___error0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AuthorizationErrorCode_t7DF2ED12266D6A7885C609086E1BFA7E359B4087_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAppleError_tFE643037BCCCF4362368CEDC73AC94B90E1B5AC0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCA0DA7AFA13D06380B286383F61CFD3BFBFDEB4B);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (error.Domain == "com.apple.AuthenticationServices.AuthorizationError" &&
		//     Enum.IsDefined(typeof(AuthorizationErrorCode), error.Code))
		RuntimeObject* L_0 = ___error0;
		NullCheck(L_0);
		String_t* L_1;
		L_1 = InterfaceFuncInvoker0< String_t* >::Invoke(1 /* System.String AppleAuth.Interfaces.IAppleError::get_Domain() */, IAppleError_tFE643037BCCCF4362368CEDC73AC94B90E1B5AC0_il2cpp_TypeInfo_var, L_0);
		bool L_2;
		L_2 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, _stringLiteralCA0DA7AFA13D06380B286383F61CFD3BFBFDEB4B, NULL);
		if (!L_2)
		{
			goto IL_0035;
		}
	}
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_3 = { reinterpret_cast<intptr_t> (AuthorizationErrorCode_t7DF2ED12266D6A7885C609086E1BFA7E359B4087_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_4;
		L_4 = Type_GetTypeFromHandle_m2570A2A5B32A5E9D9F0F38B37459DA18736C823E(L_3, NULL);
		RuntimeObject* L_5 = ___error0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0 /* System.Int32 AppleAuth.Interfaces.IAppleError::get_Code() */, IAppleError_tFE643037BCCCF4362368CEDC73AC94B90E1B5AC0_il2cpp_TypeInfo_var, L_5);
		int32_t L_7 = L_6;
		RuntimeObject* L_8 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_7);
		il2cpp_codegen_runtime_class_init_inline(Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var);
		bool L_9;
		L_9 = Enum_IsDefined_m715E9AAD26B4AAA4B08E4D6AED73237174E82BB4(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_0035;
		}
	}
	{
		// return (AuthorizationErrorCode)error.Code;
		RuntimeObject* L_10 = ___error0;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = InterfaceFuncInvoker0< int32_t >::Invoke(0 /* System.Int32 AppleAuth.Interfaces.IAppleError::get_Code() */, IAppleError_tFE643037BCCCF4362368CEDC73AC94B90E1B5AC0_il2cpp_TypeInfo_var, L_10);
		return (int32_t)(L_11);
	}

IL_0035:
	{
		// return AuthorizationErrorCode.Unknown;
		return (int32_t)(((int32_t)1000));
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
// System.String AppleAuth.Extensions.PersonNameExtensions::ToLocalizedString(AppleAuth.Interfaces.IPersonName,AppleAuth.Enums.PersonNameFormatterStyle,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* PersonNameExtensions_ToLocalizedString_m74A85EC66DABA587AC809A8032E75223CA1695A5 (RuntimeObject* ___personName0, int32_t ___style1, bool ___usePhoneticRepresentation2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_ToArray_m2C402D882AA60FC1D5C7C09A129BE7779F833B4A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2386E77CF610F786B06A91AF2C1B3FD2282D2745);
		s_Il2CppMethodInitialized = true;
	}
	List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* V_0 = NULL;
	{
		// var orderedParts = new System.Collections.Generic.List<string>();
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_0 = (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*)il2cpp_codegen_object_new(List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E(L_0, List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		V_0 = L_0;
		// if (string.IsNullOrEmpty(personName.NamePrefix))
		RuntimeObject* L_1 = ___personName0;
		NullCheck(L_1);
		String_t* L_2;
		L_2 = InterfaceFuncInvoker0< String_t* >::Invoke(0 /* System.String AppleAuth.Interfaces.IPersonName::get_NamePrefix() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_1);
		bool L_3;
		L_3 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_2, NULL);
		if (!L_3)
		{
			goto IL_001f;
		}
	}
	{
		// orderedParts.Add(personName.NamePrefix);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_4 = V_0;
		RuntimeObject* L_5 = ___personName0;
		NullCheck(L_5);
		String_t* L_6;
		L_6 = InterfaceFuncInvoker0< String_t* >::Invoke(0 /* System.String AppleAuth.Interfaces.IPersonName::get_NamePrefix() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_5);
		NullCheck(L_4);
		List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_inline(L_4, L_6, List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var);
	}

IL_001f:
	{
		// if (string.IsNullOrEmpty(personName.GivenName))
		RuntimeObject* L_7 = ___personName0;
		NullCheck(L_7);
		String_t* L_8;
		L_8 = InterfaceFuncInvoker0< String_t* >::Invoke(1 /* System.String AppleAuth.Interfaces.IPersonName::get_GivenName() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_7);
		bool L_9;
		L_9 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_8, NULL);
		if (!L_9)
		{
			goto IL_0038;
		}
	}
	{
		// orderedParts.Add(personName.GivenName);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_10 = V_0;
		RuntimeObject* L_11 = ___personName0;
		NullCheck(L_11);
		String_t* L_12;
		L_12 = InterfaceFuncInvoker0< String_t* >::Invoke(1 /* System.String AppleAuth.Interfaces.IPersonName::get_GivenName() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_11);
		NullCheck(L_10);
		List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_inline(L_10, L_12, List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var);
	}

IL_0038:
	{
		// if (string.IsNullOrEmpty(personName.MiddleName))
		RuntimeObject* L_13 = ___personName0;
		NullCheck(L_13);
		String_t* L_14;
		L_14 = InterfaceFuncInvoker0< String_t* >::Invoke(2 /* System.String AppleAuth.Interfaces.IPersonName::get_MiddleName() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_13);
		bool L_15;
		L_15 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_14, NULL);
		if (!L_15)
		{
			goto IL_0051;
		}
	}
	{
		// orderedParts.Add(personName.MiddleName);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_16 = V_0;
		RuntimeObject* L_17 = ___personName0;
		NullCheck(L_17);
		String_t* L_18;
		L_18 = InterfaceFuncInvoker0< String_t* >::Invoke(2 /* System.String AppleAuth.Interfaces.IPersonName::get_MiddleName() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_17);
		NullCheck(L_16);
		List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_inline(L_16, L_18, List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var);
	}

IL_0051:
	{
		// if (string.IsNullOrEmpty(personName.FamilyName))
		RuntimeObject* L_19 = ___personName0;
		NullCheck(L_19);
		String_t* L_20;
		L_20 = InterfaceFuncInvoker0< String_t* >::Invoke(3 /* System.String AppleAuth.Interfaces.IPersonName::get_FamilyName() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_19);
		bool L_21;
		L_21 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_20, NULL);
		if (!L_21)
		{
			goto IL_006a;
		}
	}
	{
		// orderedParts.Add(personName.FamilyName);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_22 = V_0;
		RuntimeObject* L_23 = ___personName0;
		NullCheck(L_23);
		String_t* L_24;
		L_24 = InterfaceFuncInvoker0< String_t* >::Invoke(3 /* System.String AppleAuth.Interfaces.IPersonName::get_FamilyName() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_23);
		NullCheck(L_22);
		List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_inline(L_22, L_24, List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var);
	}

IL_006a:
	{
		// if (string.IsNullOrEmpty(personName.NameSuffix))
		RuntimeObject* L_25 = ___personName0;
		NullCheck(L_25);
		String_t* L_26;
		L_26 = InterfaceFuncInvoker0< String_t* >::Invoke(4 /* System.String AppleAuth.Interfaces.IPersonName::get_NameSuffix() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_25);
		bool L_27;
		L_27 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_26, NULL);
		if (!L_27)
		{
			goto IL_0083;
		}
	}
	{
		// orderedParts.Add(personName.NameSuffix);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_28 = V_0;
		RuntimeObject* L_29 = ___personName0;
		NullCheck(L_29);
		String_t* L_30;
		L_30 = InterfaceFuncInvoker0< String_t* >::Invoke(4 /* System.String AppleAuth.Interfaces.IPersonName::get_NameSuffix() */, IPersonName_tD95EE7AB5A8B25C9DC7D160FDAECDDA149AB5591_il2cpp_TypeInfo_var, L_29);
		NullCheck(L_28);
		List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_inline(L_28, L_30, List_1_Add_mF10DB1D3CBB0B14215F0E4F8AB4934A1955E5351_RuntimeMethod_var);
	}

IL_0083:
	{
		// return string.Join(" ", orderedParts.ToArray());
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_31 = V_0;
		NullCheck(L_31);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_32;
		L_32 = List_1_ToArray_m2C402D882AA60FC1D5C7C09A129BE7779F833B4A(L_31, List_1_ToArray_m2C402D882AA60FC1D5C7C09A129BE7779F833B4A_RuntimeMethod_var);
		String_t* L_33;
		L_33 = String_Join_mE405D676C6881553258F8BAD40A20B462D611068(_stringLiteral2386E77CF610F786B06A91AF2C1B3FD2282D2745, L_32, NULL);
		return L_33;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
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
