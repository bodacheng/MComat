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
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
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

// System.Collections.ObjectModel.Collection`1<System.Object>
struct Collection_1_t3899E6252BC3D003B1AB1D6F5D7AD93EB1DCEEC3;
// System.Collections.ObjectModel.Collection`1<YamlDotNet.Core.Tokens.TagDirective>
struct Collection_1_tEA96EDF53D170E847CDEC96682AF4B0C1384BAE6;
// System.Collections.Generic.Dictionary`2<System.String,YamlDotNet.Core.Tokens.TagDirective>
struct Dictionary_2_tFC99DEC3B3CB8E7701580DA4A0CAE92132D6B26C;
// System.Collections.Generic.Dictionary`2<System.Text.RegularExpressions.Regex/CachedCodeEntryKey,System.Text.RegularExpressions.Regex/CachedCodeEntry>
struct Dictionary_2_t5B5B38BB06341F50E1C75FB53208A2A66CAE57F7;
// System.Collections.Generic.IEnumerable`1<YamlDotNet.Core.Tokens.TagDirective>
struct IEnumerable_1_t35CD76DEF3AC17416CC3AB951593A18EF9F0254C;
// System.Collections.Generic.IEqualityComparer`1<System.String>
struct IEqualityComparer_1_tAE94C8F24AD5B94D4EE85CA9FC59E3409D41CAF7;
// System.Collections.Generic.IList`1<YamlDotNet.Core.Tokens.TagDirective>
struct IList_1_t14EB3C44FE224C6BC4CCA5BC797C5380512AFC13;
// System.Collections.ObjectModel.KeyedCollection`2<System.Object,System.Object>
struct KeyedCollection_2_tBF854BD0291D71A8D8E9EA5FAE1F0D461C7CBB5F;
// System.Collections.ObjectModel.KeyedCollection`2<System.String,YamlDotNet.Core.Tokens.TagDirective>
struct KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD;
// System.WeakReference`1<System.Text.RegularExpressions.RegexReplacement>
struct WeakReference_1_tDC6E83496181D1BAFA3B89CBC00BCD0B64450257;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// YamlDotNet.Core.Tokens.Anchor
struct Anchor_tEC494D927D531B92F865C0E61947DF32759016B1;
// YamlDotNet.Core.Events.AnchorAlias
struct AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151;
// YamlDotNet.Core.Tokens.AnchorAlias
struct AnchorAlias_tB98567A0A31C86F0CA15323602658ED2C40B029F;
// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
// System.ArgumentNullException
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129;
// System.ArgumentOutOfRangeException
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F;
// YamlDotNet.Core.Tokens.BlockEnd
struct BlockEnd_t12480C7065A2444C9F4D360DC04CA1854065EA31;
// YamlDotNet.Core.Tokens.BlockEntry
struct BlockEntry_t40AC3EA51287B6D5F5DC519033859532ACD94ABD;
// YamlDotNet.Core.Tokens.BlockMappingStart
struct BlockMappingStart_t9C5AB2806D66998C719C3162C8F65BFC8DBFE3BA;
// YamlDotNet.Core.Tokens.BlockSequenceStart
struct BlockSequenceStart_t987AE0CAA2CA963E8FCD79FB59BD11EF90785D56;
// YamlDotNet.Core.Events.Comment
struct Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820;
// YamlDotNet.Core.Tokens.Comment
struct Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B;
// YamlDotNet.Core.Cursor
struct Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A;
// YamlDotNet.Core.Events.DocumentEnd
struct DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8;
// YamlDotNet.Core.Tokens.DocumentEnd
struct DocumentEnd_tFDA49E2D745EE5FC6A3EB52F935E7637AB89F8D4;
// YamlDotNet.Core.Events.DocumentStart
struct DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F;
// YamlDotNet.Core.Tokens.DocumentStart
struct DocumentStart_tB03FCCC6E83EF1B3FE5227ADA4A4CC1044CE6C9C;
// YamlDotNet.Core.Tokens.Error
struct Error_tE299C2E444261688F2B95051EB78045D4014F1C1;
// System.Exception
struct Exception_t;
// System.Text.RegularExpressions.ExclusiveReference
struct ExclusiveReference_t411F04D4CC440EB7399290027E1BBABEF4C28837;
// YamlDotNet.Core.Tokens.FlowEntry
struct FlowEntry_tCF85EE204C191605C0072EF0DE2E8A6C57B29538;
// YamlDotNet.Core.Tokens.FlowMappingEnd
struct FlowMappingEnd_tCE5B3FBC6DC603634536A703D6580DF7765B5CBF;
// YamlDotNet.Core.Tokens.FlowMappingStart
struct FlowMappingStart_t22085B50FB25219B0C3D02A9C4F11D68C0CF2E3D;
// YamlDotNet.Core.Tokens.FlowSequenceEnd
struct FlowSequenceEnd_t2863337BC1A979BC4B93B2F77D820A4233E76BB5;
// YamlDotNet.Core.Tokens.FlowSequenceStart
struct FlowSequenceStart_tAAE66644A7DF27B34E8E481531E0FDEA76F09E11;
// System.Collections.Hashtable
struct Hashtable_tEFC3B6496E6747787D8BB761B51F2AE3A8CFFE2D;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// YamlDotNet.Core.Events.IParsingEventVisitor
struct IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA;
// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
// YamlDotNet.Core.Tokens.Key
struct Key_t614783445825A1A71432AD21DEED478DFA144B4B;
// YamlDotNet.Core.Events.MappingEnd
struct MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7;
// YamlDotNet.Core.Events.MappingStart
struct MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472;
// YamlDotNet.Core.Mark
struct Mark_t950DC067D3EC830050595AD3F189554215D04694;
// YamlDotNet.Core.Events.NodeEvent
struct NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53;
// YamlDotNet.Core.Events.ParsingEvent
struct ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E;
// System.Text.RegularExpressions.Regex
struct Regex_tE773142C2BE45C5D362B0F815AFF831707A51772;
// System.Text.RegularExpressions.RegexCode
struct RegexCode_tA23175D9DA02AD6A79B073E10EC5D225372ED6C7;
// System.Text.RegularExpressions.RegexRunnerFactory
struct RegexRunnerFactory_t72373B672C7D8785F63516DDD88834F286AF41E7;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// YamlDotNet.Core.Events.Scalar
struct Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A;
// YamlDotNet.Core.Tokens.Scalar
struct Scalar_t063F0ED0AE489C799F2F25647718E812CF768796;
// YamlDotNet.Core.SemanticErrorException
struct SemanticErrorException_t0EAAF1E1A5FE24FA81A8761102451E6883F3BA1E;
// YamlDotNet.Core.Events.SequenceEnd
struct SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6;
// YamlDotNet.Core.Events.SequenceStart
struct SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C;
// YamlDotNet.Core.SimpleKey
struct SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5;
// YamlDotNet.Core.Events.StreamEnd
struct StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F;
// YamlDotNet.Core.Tokens.StreamEnd
struct StreamEnd_tAAE42ABA3EB10720E89FE6E3D6A634EBE60485EC;
// YamlDotNet.Core.Events.StreamStart
struct StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18;
// YamlDotNet.Core.Tokens.StreamStart
struct StreamStart_t83283B91848E5BDB56E93F42B704BC068B6B752B;
// System.String
struct String_t;
// YamlDotNet.Core.StringLookAheadBuffer
struct StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E;
// YamlDotNet.Core.SyntaxErrorException
struct SyntaxErrorException_t85D520F4222E570503982C3ED7E3409C86EAE0AB;
// YamlDotNet.Core.Tokens.Tag
struct Tag_t798685C1FB42713672C76CED2942C88CD2899CD2;
// YamlDotNet.Core.Tokens.TagDirective
struct TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061;
// YamlDotNet.Core.TagDirectiveCollection
struct TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3;
// YamlDotNet.Core.Tokens.Token
struct Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38;
// YamlDotNet.Core.Tokens.Value
struct Value_tE038E4AE49F94FD0AC0D180B22AFDA4FCFCA9200;
// YamlDotNet.Core.Version
struct Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3;
// YamlDotNet.Core.Tokens.VersionDirective
struct VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// YamlDotNet.Core.YamlException
struct YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2;
// System.Text.RegularExpressions.Regex/CachedCodeEntry
struct CachedCodeEntry_tE201C3AD65C234AD9ED7A78C95025824A7A9FF39;

IL2CPP_EXTERN_C RuntimeClass* AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_1_t35CD76DEF3AC17416CC3AB951593A18EF9F0254C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_1_t427CCB5B7502F14587A4AD2D527C9A61C5340E27_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* MappingStyle_t00D3BBFC7547E02AA45A0AB9A9109AF5C32D2440_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ScalarStyle_t8B9E83D82F8FD9DB5079F76D03EBB143BFC4D0A2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SequenceStyle_t9924C8E70E226F6A69C95F03D6CAD13804BB9D02_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral09F3589E3F129822338E12B67FB0990E4EF2F3DE;
IL2CPP_EXTERN_C String_t* _stringLiteral15196F05B117690F3E12E56AA0C43803EA0D2A46;
IL2CPP_EXTERN_C String_t* _stringLiteral15799C212077F0C3382CDBD2AA0BBEF54406463B;
IL2CPP_EXTERN_C String_t* _stringLiteral171FB2C5A9D880AA85056C99CA54469A36B3AE62;
IL2CPP_EXTERN_C String_t* _stringLiteral182468BD465AC6BC414B4B961F50F2B7CB9ECC26;
IL2CPP_EXTERN_C String_t* _stringLiteral1883E177578C34C4BBA579EA98A67CBF5D34BE3D;
IL2CPP_EXTERN_C String_t* _stringLiteral1A086D5809CCBE16E0DAC991195BB302E8DDC85D;
IL2CPP_EXTERN_C String_t* _stringLiteral1B090FBEA32D5B639DF18F6ECD1D23F4944A19AB;
IL2CPP_EXTERN_C String_t* _stringLiteral21D4DB462D29C926731F20A0EF0666EF382D13A9;
IL2CPP_EXTERN_C String_t* _stringLiteral2AD47C03F7A83F82E3B2ADFE8A60F1727FD3BEFD;
IL2CPP_EXTERN_C String_t* _stringLiteral3B9A4DA33EB1F3E2359896E044A79CF7F316645E;
IL2CPP_EXTERN_C String_t* _stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8;
IL2CPP_EXTERN_C String_t* _stringLiteral5378C16FB75C6D58FCF9AD334CF92DE0F2E4F752;
IL2CPP_EXTERN_C String_t* _stringLiteral55C7066FE389C4DB122F633D727159777AFB4BBB;
IL2CPP_EXTERN_C String_t* _stringLiteral6D8BCD93E9F5A9C7C071EB22AC111507D9F90887;
IL2CPP_EXTERN_C String_t* _stringLiteral738F291E53E97C08DAE378C71EF70A60E31AE900;
IL2CPP_EXTERN_C String_t* _stringLiteral779CF5DC3CA44DC34A860898B077959B730D6D07;
IL2CPP_EXTERN_C String_t* _stringLiteral82C791C1966A9B7EFCEB102734ECB5B1DB8AF742;
IL2CPP_EXTERN_C String_t* _stringLiteral82D95C9038FADE61EAA402493C3AB02991DF2B25;
IL2CPP_EXTERN_C String_t* _stringLiteral880C93990C8339019D7475FB24E361E6DEA9385F;
IL2CPP_EXTERN_C String_t* _stringLiteral8C026E54DBB79FB881A0A7EE631932C15A9E0A1C;
IL2CPP_EXTERN_C String_t* _stringLiteral8C8056CFB8CBFF1B0947F62BBBC5824D24D194BE;
IL2CPP_EXTERN_C String_t* _stringLiteral91911A5D93C38999B3F0C946DB48AFEFF926C0C2;
IL2CPP_EXTERN_C String_t* _stringLiteral93951CD1D927C264C666D33C8BE2CBD303C32D25;
IL2CPP_EXTERN_C String_t* _stringLiteral948B944155B13DC838C958C29968902C1ADC6391;
IL2CPP_EXTERN_C String_t* _stringLiteralA2F4AC9DD8E1FAC5257E5F7BA5EE1C7C7E5F7AB1;
IL2CPP_EXTERN_C String_t* _stringLiteralB6B15A393A4B575B1D16E5ACDC604FC147869A2D;
IL2CPP_EXTERN_C String_t* _stringLiteralBD3547FA9A379720A33DAE68538E3DA25C3F6B67;
IL2CPP_EXTERN_C String_t* _stringLiteralBFDE951663C61703B7702ACABC6C1A2860B82FF2;
IL2CPP_EXTERN_C String_t* _stringLiteralCB5CDE966F99FDC7AE4101331D907BCEF208D664;
IL2CPP_EXTERN_C String_t* _stringLiteralCB7CBB1BC8BA2BCF7942450A0E34E51300205098;
IL2CPP_EXTERN_C String_t* _stringLiteralD3F9023582F96AC5F3DEB69BCAC72DB7F59028A8;
IL2CPP_EXTERN_C String_t* _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC;
IL2CPP_EXTERN_C String_t* _stringLiteralE8456D0D9B0A8ADFBEAC72C47ED28A9778E515B1;
IL2CPP_EXTERN_C String_t* _stringLiteralE8744A8B8BD390EB66CA0CAE2376C973E6904FFB;
IL2CPP_EXTERN_C String_t* _stringLiteralEE77384131B17CE853EE959871A8222FC81E9CF5;
IL2CPP_EXTERN_C String_t* _stringLiteralFFE3A1B73CD7FC81540FBBE737435B0A887629D5;
IL2CPP_EXTERN_C const RuntimeMethod* AnchorAlias__ctor_m22688D334340CE55DD14B19EABFB8F6FA717027E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AnchorAlias__ctor_mE1D76BED31BB957B4AD8905D4691B3DA928A9175_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Anchor__ctor_m3C2CB16EE5709C5EAB6733DCE3D3C99FA5BDAFA1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Collection_1_Add_m900AC073217A777000AC48D1E9603F5738DE09C4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Comment__ctor_m6603352C505B98077744A37A02BABA36BA40E616_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* KeyedCollection_2_Contains_m819F09DCC75B6B8457150A232BD08272EF970248_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Scalar__ctor_m700DCA04B423E17942E3A4EDAC1DFEF944E6AC21_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* StringLookAheadBuffer_Skip_m0218AE9B3F0A7BDA1C14AD90B3B5CF3E65789FCE_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Tag__ctor_m435D46628F64E044B1B4B3E10CF648FF76B7432B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Version__ctor_m195E6390EC1CC4796B0BC3007F493E094ABC68EF_RuntimeMethod_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Collections.ObjectModel.Collection`1<YamlDotNet.Core.Tokens.TagDirective>
struct Collection_1_tEA96EDF53D170E847CDEC96682AF4B0C1384BAE6  : public RuntimeObject
{
	// System.Collections.Generic.IList`1<T> System.Collections.ObjectModel.Collection`1::items
	RuntimeObject* ___items_0;
};
struct Il2CppArrayBounds;

// YamlDotNet.Core.Cursor
struct Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A  : public RuntimeObject
{
	// System.Int32 YamlDotNet.Core.Cursor::<Index>k__BackingField
	int32_t ___U3CIndexU3Ek__BackingField_0;
	// System.Int32 YamlDotNet.Core.Cursor::<Line>k__BackingField
	int32_t ___U3CLineU3Ek__BackingField_1;
	// System.Int32 YamlDotNet.Core.Cursor::<LineOffset>k__BackingField
	int32_t ___U3CLineOffsetU3Ek__BackingField_2;
};

// YamlDotNet.Core.Mark
struct Mark_t950DC067D3EC830050595AD3F189554215D04694  : public RuntimeObject
{
	// System.Int32 YamlDotNet.Core.Mark::<Index>k__BackingField
	int32_t ___U3CIndexU3Ek__BackingField_1;
	// System.Int32 YamlDotNet.Core.Mark::<Line>k__BackingField
	int32_t ___U3CLineU3Ek__BackingField_2;
	// System.Int32 YamlDotNet.Core.Mark::<Column>k__BackingField
	int32_t ___U3CColumnU3Ek__BackingField_3;
};

struct Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields
{
	// YamlDotNet.Core.Mark YamlDotNet.Core.Mark::Empty
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___Empty_0;
};

// YamlDotNet.Core.Events.ParsingEvent
struct ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E  : public RuntimeObject
{
	// YamlDotNet.Core.Mark YamlDotNet.Core.Events.ParsingEvent::<Start>k__BackingField
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___U3CStartU3Ek__BackingField_0;
	// YamlDotNet.Core.Mark YamlDotNet.Core.Events.ParsingEvent::<End>k__BackingField
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___U3CEndU3Ek__BackingField_1;
};

// YamlDotNet.Core.SimpleKey
struct SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5  : public RuntimeObject
{
	// YamlDotNet.Core.Cursor YamlDotNet.Core.SimpleKey::cursor
	Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* ___cursor_0;
	// System.Boolean YamlDotNet.Core.SimpleKey::<IsPossible>k__BackingField
	bool ___U3CIsPossibleU3Ek__BackingField_1;
	// System.Boolean YamlDotNet.Core.SimpleKey::<IsRequired>k__BackingField
	bool ___U3CIsRequiredU3Ek__BackingField_2;
	// System.Int32 YamlDotNet.Core.SimpleKey::<TokenNumber>k__BackingField
	int32_t ___U3CTokenNumberU3Ek__BackingField_3;
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

// YamlDotNet.Core.StringLookAheadBuffer
struct StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E  : public RuntimeObject
{
	// System.String YamlDotNet.Core.StringLookAheadBuffer::value
	String_t* ___value_0;
	// System.Int32 YamlDotNet.Core.StringLookAheadBuffer::<Position>k__BackingField
	int32_t ___U3CPositionU3Ek__BackingField_1;
};

// YamlDotNet.Core.Tokens.Token
struct Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38  : public RuntimeObject
{
	// YamlDotNet.Core.Mark YamlDotNet.Core.Tokens.Token::<Start>k__BackingField
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___U3CStartU3Ek__BackingField_0;
	// YamlDotNet.Core.Mark YamlDotNet.Core.Tokens.Token::<End>k__BackingField
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___U3CEndU3Ek__BackingField_1;
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

// YamlDotNet.Core.Version
struct Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3  : public RuntimeObject
{
	// System.Int32 YamlDotNet.Core.Version::<Major>k__BackingField
	int32_t ___U3CMajorU3Ek__BackingField_0;
	// System.Int32 YamlDotNet.Core.Version::<Minor>k__BackingField
	int32_t ___U3CMinorU3Ek__BackingField_1;
};

// System.Collections.ObjectModel.KeyedCollection`2<System.String,YamlDotNet.Core.Tokens.TagDirective>
struct KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD  : public Collection_1_tEA96EDF53D170E847CDEC96682AF4B0C1384BAE6
{
	// System.Collections.Generic.IEqualityComparer`1<TKey> System.Collections.ObjectModel.KeyedCollection`2::comparer
	RuntimeObject* ___comparer_1;
	// System.Collections.Generic.Dictionary`2<TKey,TItem> System.Collections.ObjectModel.KeyedCollection`2::dict
	Dictionary_2_tFC99DEC3B3CB8E7701580DA4A0CAE92132D6B26C* ___dict_2;
	// System.Int32 System.Collections.ObjectModel.KeyedCollection`2::keyCount
	int32_t ___keyCount_3;
	// System.Int32 System.Collections.ObjectModel.KeyedCollection`2::threshold
	int32_t ___threshold_4;
};

// YamlDotNet.Core.AnchorName
struct AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E 
{
	// System.String YamlDotNet.Core.AnchorName::value
	String_t* ___value_2;
};

struct AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_StaticFields
{
	// YamlDotNet.Core.AnchorName YamlDotNet.Core.AnchorName::Empty
	AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___Empty_0;
	// System.Text.RegularExpressions.Regex YamlDotNet.Core.AnchorName::AnchorPattern
	Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* ___AnchorPattern_1;
};
// Native definition for P/Invoke marshalling of YamlDotNet.Core.AnchorName
struct AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_marshaled_pinvoke
{
	char* ___value_2;
};
// Native definition for COM marshalling of YamlDotNet.Core.AnchorName
struct AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_marshaled_com
{
	Il2CppChar* ___value_2;
};

// YamlDotNet.Core.Tokens.BlockEnd
struct BlockEnd_t12480C7065A2444C9F4D360DC04CA1854065EA31  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.BlockEntry
struct BlockEntry_t40AC3EA51287B6D5F5DC519033859532ACD94ABD  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.BlockMappingStart
struct BlockMappingStart_t9C5AB2806D66998C719C3162C8F65BFC8DBFE3BA  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.BlockSequenceStart
struct BlockSequenceStart_t987AE0CAA2CA963E8FCD79FB59BD11EF90785D56  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
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

// YamlDotNet.Core.Events.Comment
struct Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
	// System.String YamlDotNet.Core.Events.Comment::<Value>k__BackingField
	String_t* ___U3CValueU3Ek__BackingField_2;
	// System.Boolean YamlDotNet.Core.Events.Comment::<IsInline>k__BackingField
	bool ___U3CIsInlineU3Ek__BackingField_3;
};

// YamlDotNet.Core.Tokens.Comment
struct Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// System.String YamlDotNet.Core.Tokens.Comment::<Value>k__BackingField
	String_t* ___U3CValueU3Ek__BackingField_2;
	// System.Boolean YamlDotNet.Core.Tokens.Comment::<IsInline>k__BackingField
	bool ___U3CIsInlineU3Ek__BackingField_3;
};

// YamlDotNet.Core.Events.DocumentEnd
struct DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
	// System.Boolean YamlDotNet.Core.Events.DocumentEnd::<IsImplicit>k__BackingField
	bool ___U3CIsImplicitU3Ek__BackingField_2;
};

// YamlDotNet.Core.Tokens.DocumentEnd
struct DocumentEnd_tFDA49E2D745EE5FC6A3EB52F935E7637AB89F8D4  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Events.DocumentStart
struct DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
	// YamlDotNet.Core.TagDirectiveCollection YamlDotNet.Core.Events.DocumentStart::<Tags>k__BackingField
	TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* ___U3CTagsU3Ek__BackingField_2;
	// YamlDotNet.Core.Tokens.VersionDirective YamlDotNet.Core.Events.DocumentStart::<Version>k__BackingField
	VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* ___U3CVersionU3Ek__BackingField_3;
	// System.Boolean YamlDotNet.Core.Events.DocumentStart::<IsImplicit>k__BackingField
	bool ___U3CIsImplicitU3Ek__BackingField_4;
};

// YamlDotNet.Core.Tokens.DocumentStart
struct DocumentStart_tB03FCCC6E83EF1B3FE5227ADA4A4CC1044CE6C9C  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.Error
struct Error_tE299C2E444261688F2B95051EB78045D4014F1C1  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// System.String YamlDotNet.Core.Tokens.Error::<Value>k__BackingField
	String_t* ___U3CValueU3Ek__BackingField_2;
};

// YamlDotNet.Core.Tokens.FlowEntry
struct FlowEntry_tCF85EE204C191605C0072EF0DE2E8A6C57B29538  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.FlowMappingEnd
struct FlowMappingEnd_tCE5B3FBC6DC603634536A703D6580DF7765B5CBF  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.FlowMappingStart
struct FlowMappingStart_t22085B50FB25219B0C3D02A9C4F11D68C0CF2E3D  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.FlowSequenceEnd
struct FlowSequenceEnd_t2863337BC1A979BC4B93B2F77D820A4233E76BB5  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.FlowSequenceStart
struct FlowSequenceStart_tAAE66644A7DF27B34E8E481531E0FDEA76F09E11  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
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

// YamlDotNet.Core.Tokens.Key
struct Key_t614783445825A1A71432AD21DEED478DFA144B4B  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Events.MappingEnd
struct MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
};

// YamlDotNet.Core.Tokens.Scalar
struct Scalar_t063F0ED0AE489C799F2F25647718E812CF768796  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// System.String YamlDotNet.Core.Tokens.Scalar::<Value>k__BackingField
	String_t* ___U3CValueU3Ek__BackingField_2;
	// YamlDotNet.Core.ScalarStyle YamlDotNet.Core.Tokens.Scalar::<Style>k__BackingField
	int32_t ___U3CStyleU3Ek__BackingField_3;
};

// YamlDotNet.Core.Events.SequenceEnd
struct SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
};

// YamlDotNet.Core.Events.StreamEnd
struct StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
};

// YamlDotNet.Core.Tokens.StreamEnd
struct StreamEnd_tAAE42ABA3EB10720E89FE6E3D6A634EBE60485EC  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Events.StreamStart
struct StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
};

// YamlDotNet.Core.Tokens.StreamStart
struct StreamStart_t83283B91848E5BDB56E93F42B704BC068B6B752B  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.Tag
struct Tag_t798685C1FB42713672C76CED2942C88CD2899CD2  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// System.String YamlDotNet.Core.Tokens.Tag::<Handle>k__BackingField
	String_t* ___U3CHandleU3Ek__BackingField_2;
	// System.String YamlDotNet.Core.Tokens.Tag::<Suffix>k__BackingField
	String_t* ___U3CSuffixU3Ek__BackingField_3;
};

// YamlDotNet.Core.Tokens.TagDirective
struct TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// System.String YamlDotNet.Core.Tokens.TagDirective::<Handle>k__BackingField
	String_t* ___U3CHandleU3Ek__BackingField_2;
	// System.String YamlDotNet.Core.Tokens.TagDirective::<Prefix>k__BackingField
	String_t* ___U3CPrefixU3Ek__BackingField_3;
};

struct TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_StaticFields
{
	// System.Text.RegularExpressions.Regex YamlDotNet.Core.Tokens.TagDirective::TagHandlePattern
	Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* ___TagHandlePattern_4;
};

// YamlDotNet.Core.TagName
struct TagName_t15CB29949E97FF28193B6F635B58928554CB5854 
{
	// System.String YamlDotNet.Core.TagName::value
	String_t* ___value_1;
};

struct TagName_t15CB29949E97FF28193B6F635B58928554CB5854_StaticFields
{
	// YamlDotNet.Core.TagName YamlDotNet.Core.TagName::Empty
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___Empty_0;
};
// Native definition for P/Invoke marshalling of YamlDotNet.Core.TagName
struct TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_pinvoke
{
	char* ___value_1;
};
// Native definition for COM marshalling of YamlDotNet.Core.TagName
struct TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_com
{
	Il2CppChar* ___value_1;
};

// System.TimeSpan
struct TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A 
{
	// System.Int64 System.TimeSpan::_ticks
	int64_t ____ticks_22;
};

struct TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A_StaticFields
{
	// System.TimeSpan System.TimeSpan::Zero
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___Zero_19;
	// System.TimeSpan System.TimeSpan::MaxValue
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___MaxValue_20;
	// System.TimeSpan System.TimeSpan::MinValue
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___MinValue_21;
};

// YamlDotNet.Core.Tokens.Value
struct Value_tE038E4AE49F94FD0AC0D180B22AFDA4FCFCA9200  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
};

// YamlDotNet.Core.Tokens.VersionDirective
struct VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// YamlDotNet.Core.Version YamlDotNet.Core.Tokens.VersionDirective::<Version>k__BackingField
	Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* ___U3CVersionU3Ek__BackingField_2;
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

// YamlDotNet.Core.Tokens.Anchor
struct Anchor_tEC494D927D531B92F865C0E61947DF32759016B1  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// YamlDotNet.Core.AnchorName YamlDotNet.Core.Tokens.Anchor::<Value>k__BackingField
	AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___U3CValueU3Ek__BackingField_2;
};

// YamlDotNet.Core.Events.AnchorAlias
struct AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
	// YamlDotNet.Core.AnchorName YamlDotNet.Core.Events.AnchorAlias::<Value>k__BackingField
	AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___U3CValueU3Ek__BackingField_2;
};

// YamlDotNet.Core.Tokens.AnchorAlias
struct AnchorAlias_tB98567A0A31C86F0CA15323602658ED2C40B029F  : public Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38
{
	// YamlDotNet.Core.AnchorName YamlDotNet.Core.Tokens.AnchorAlias::<Value>k__BackingField
	AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___U3CValueU3Ek__BackingField_2;
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

// YamlDotNet.Core.Events.NodeEvent
struct NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53  : public ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E
{
	// YamlDotNet.Core.AnchorName YamlDotNet.Core.Events.NodeEvent::<Anchor>k__BackingField
	AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___U3CAnchorU3Ek__BackingField_2;
	// YamlDotNet.Core.TagName YamlDotNet.Core.Events.NodeEvent::<Tag>k__BackingField
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___U3CTagU3Ek__BackingField_3;
};

// System.Text.RegularExpressions.Regex
struct Regex_tE773142C2BE45C5D362B0F815AFF831707A51772  : public RuntimeObject
{
	// System.TimeSpan System.Text.RegularExpressions.Regex::internalMatchTimeout
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___internalMatchTimeout_10;
	// System.String System.Text.RegularExpressions.Regex::pattern
	String_t* ___pattern_12;
	// System.Text.RegularExpressions.RegexOptions System.Text.RegularExpressions.Regex::roptions
	int32_t ___roptions_13;
	// System.Text.RegularExpressions.RegexRunnerFactory System.Text.RegularExpressions.Regex::factory
	RegexRunnerFactory_t72373B672C7D8785F63516DDD88834F286AF41E7* ___factory_14;
	// System.Collections.Hashtable System.Text.RegularExpressions.Regex::caps
	Hashtable_tEFC3B6496E6747787D8BB761B51F2AE3A8CFFE2D* ___caps_15;
	// System.Collections.Hashtable System.Text.RegularExpressions.Regex::capnames
	Hashtable_tEFC3B6496E6747787D8BB761B51F2AE3A8CFFE2D* ___capnames_16;
	// System.String[] System.Text.RegularExpressions.Regex::capslist
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___capslist_17;
	// System.Int32 System.Text.RegularExpressions.Regex::capsize
	int32_t ___capsize_18;
	// System.Text.RegularExpressions.ExclusiveReference System.Text.RegularExpressions.Regex::_runnerref
	ExclusiveReference_t411F04D4CC440EB7399290027E1BBABEF4C28837* ____runnerref_19;
	// System.WeakReference`1<System.Text.RegularExpressions.RegexReplacement> System.Text.RegularExpressions.Regex::_replref
	WeakReference_1_tDC6E83496181D1BAFA3B89CBC00BCD0B64450257* ____replref_20;
	// System.Text.RegularExpressions.RegexCode System.Text.RegularExpressions.Regex::_code
	RegexCode_tA23175D9DA02AD6A79B073E10EC5D225372ED6C7* ____code_21;
	// System.Boolean System.Text.RegularExpressions.Regex::_refsInitialized
	bool ____refsInitialized_22;
};

struct Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_StaticFields
{
	// System.Int32 System.Text.RegularExpressions.Regex::s_cacheSize
	int32_t ___s_cacheSize_1;
	// System.Collections.Generic.Dictionary`2<System.Text.RegularExpressions.Regex/CachedCodeEntryKey,System.Text.RegularExpressions.Regex/CachedCodeEntry> System.Text.RegularExpressions.Regex::s_cache
	Dictionary_2_t5B5B38BB06341F50E1C75FB53208A2A66CAE57F7* ___s_cache_2;
	// System.Int32 System.Text.RegularExpressions.Regex::s_cacheCount
	int32_t ___s_cacheCount_3;
	// System.Text.RegularExpressions.Regex/CachedCodeEntry System.Text.RegularExpressions.Regex::s_cacheFirst
	CachedCodeEntry_tE201C3AD65C234AD9ED7A78C95025824A7A9FF39* ___s_cacheFirst_4;
	// System.Text.RegularExpressions.Regex/CachedCodeEntry System.Text.RegularExpressions.Regex::s_cacheLast
	CachedCodeEntry_tE201C3AD65C234AD9ED7A78C95025824A7A9FF39* ___s_cacheLast_5;
	// System.TimeSpan System.Text.RegularExpressions.Regex::s_maximumMatchTimeout
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___s_maximumMatchTimeout_6;
	// System.TimeSpan System.Text.RegularExpressions.Regex::s_defaultMatchTimeout
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___s_defaultMatchTimeout_8;
	// System.TimeSpan System.Text.RegularExpressions.Regex::InfiniteMatchTimeout
	TimeSpan_t8195C5B013A2C532FEBDF0B64B6911982E750F5A ___InfiniteMatchTimeout_9;
};

// YamlDotNet.Core.TagDirectiveCollection
struct TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3  : public KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD
{
};

// YamlDotNet.Core.Events.MappingStart
struct MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472  : public NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53
{
	// System.Boolean YamlDotNet.Core.Events.MappingStart::<IsImplicit>k__BackingField
	bool ___U3CIsImplicitU3Ek__BackingField_4;
	// YamlDotNet.Core.Events.MappingStyle YamlDotNet.Core.Events.MappingStart::<Style>k__BackingField
	int32_t ___U3CStyleU3Ek__BackingField_5;
};

// YamlDotNet.Core.Events.Scalar
struct Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A  : public NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53
{
	// System.String YamlDotNet.Core.Events.Scalar::<Value>k__BackingField
	String_t* ___U3CValueU3Ek__BackingField_4;
	// YamlDotNet.Core.ScalarStyle YamlDotNet.Core.Events.Scalar::<Style>k__BackingField
	int32_t ___U3CStyleU3Ek__BackingField_5;
	// System.Boolean YamlDotNet.Core.Events.Scalar::<IsPlainImplicit>k__BackingField
	bool ___U3CIsPlainImplicitU3Ek__BackingField_6;
	// System.Boolean YamlDotNet.Core.Events.Scalar::<IsQuotedImplicit>k__BackingField
	bool ___U3CIsQuotedImplicitU3Ek__BackingField_7;
};

// YamlDotNet.Core.Events.SequenceStart
struct SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C  : public NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53
{
	// System.Boolean YamlDotNet.Core.Events.SequenceStart::<IsImplicit>k__BackingField
	bool ___U3CIsImplicitU3Ek__BackingField_4;
	// YamlDotNet.Core.Events.SequenceStyle YamlDotNet.Core.Events.SequenceStart::<Style>k__BackingField
	int32_t ___U3CStyleU3Ek__BackingField_5;
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// YamlDotNet.Core.YamlException
struct YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2  : public Exception_t
{
	// YamlDotNet.Core.Mark YamlDotNet.Core.YamlException::<Start>k__BackingField
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___U3CStartU3Ek__BackingField_18;
	// YamlDotNet.Core.Mark YamlDotNet.Core.YamlException::<End>k__BackingField
	Mark_t950DC067D3EC830050595AD3F189554215D04694* ___U3CEndU3Ek__BackingField_19;
};

// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	// System.String System.ArgumentException::_paramName
	String_t* ____paramName_18;
};

// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// YamlDotNet.Core.SemanticErrorException
struct SemanticErrorException_t0EAAF1E1A5FE24FA81A8761102451E6883F3BA1E  : public YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2
{
};

// YamlDotNet.Core.SyntaxErrorException
struct SyntaxErrorException_t85D520F4222E570503982C3ED7E3409C86EAE0AB  : public YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2
{
};

// System.ArgumentNullException
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
};

// System.ArgumentOutOfRangeException
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
	// System.Object System.ArgumentOutOfRangeException::_actualValue
	RuntimeObject* ____actualValue_19;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
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


// System.Void System.Collections.ObjectModel.KeyedCollection`2<System.Object,System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void KeyedCollection_2__ctor_mD5D803A09A8DF6BBF09C4D362C3DA681470B4890_gshared (KeyedCollection_2_tBF854BD0291D71A8D8E9EA5FAE1F0D461C7CBB5F* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.ObjectModel.Collection`1<System.Object>::Add(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Collection_1_Add_m4B1AD8CC1C40112C06A7C38FA96C4E125FF5D7D7_gshared (Collection_1_t3899E6252BC3D003B1AB1D6F5D7AD93EB1DCEEC3* __this, RuntimeObject* ___item0, const RuntimeMethod* method) ;
// System.Boolean System.Collections.ObjectModel.KeyedCollection`2<System.Object,System.Object>::Contains(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool KeyedCollection_2_Contains_mD595E54D9A35BB6BD6D39B8DA7424924DFBFCB4D_gshared (KeyedCollection_2_tBF854BD0291D71A8D8E9EA5FAE1F0D461C7CBB5F* __this, RuntimeObject* ___key0, const RuntimeMethod* method) ;

// System.Void YamlDotNet.Core.YamlException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_m56DCFD258063E331740F0BB3E81E3550963D56FB (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.YamlException::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_m2E113B1BD7303D541C799174365730373DDE6924 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, String_t* ___message2, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.YamlException::.ctor(System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_mAE5CF47B30D7A830E5D18FACFEBB588185ED6324 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, String_t* ___message0, Exception_t* ___inner1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.SimpleKey::set_IsPossible(System.Boolean)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SimpleKey_set_IsPossible_m9D3BF8BE359A926B73C230A571AB700024E6B161_inline (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, bool ___value0, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.Cursor::get_Index()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Cursor_get_Index_m80BCD59F059558A7AE2D9F6E818E5063DD2A3DC8_inline (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.Cursor::get_Line()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Cursor_get_Line_m4C41A923C959EAEF29D2D0A8C12509FD7FCCEE88_inline (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.Cursor::get_LineOffset()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Cursor_get_LineOffset_m8683346CC221F6CE809AABFE6E5677F035AC5AF6_inline (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) ;
// YamlDotNet.Core.Mark YamlDotNet.Core.Cursor::Mark()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* Cursor_Mark_m28DB7A43DE45EF17422A6285D168444AEB02483C (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Cursor::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Cursor__ctor_m1C5747F1DF5B988AEEDE5734DA730F60D58CE6CB (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Cursor::.ctor(YamlDotNet.Core.Cursor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Cursor__ctor_mCB90283F77973E1DD2CCCB9943F7F7A3EFCBFA0D (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* ___cursor0, const RuntimeMethod* method) ;
// System.Int32 System.String::get_Length()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.StringLookAheadBuffer::get_Position()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t StringLookAheadBuffer_get_Position_m712487E8FF6199BF4E64713EADF7BD41001A2749_inline (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.StringLookAheadBuffer::IsOutside(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool StringLookAheadBuffer_IsOutside_m8745E8A683A08F996667BDEAAEFCFE7DF7560232 (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Char System.String::get_Chars(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3 (String_t* __this, int32_t ___index0, const RuntimeMethod* method) ;
// System.Void System.ArgumentOutOfRangeException::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentOutOfRangeException__ctor_mE5B2755F0BEA043CACF915D5CE140859EE58FA66 (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* __this, String_t* ___paramName0, String_t* ___message1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.StringLookAheadBuffer::set_Position(System.Int32)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void StringLookAheadBuffer_set_Position_mC8275781BC014A2FBB31A0001D6EB3FE0CBC6D5D_inline (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___value0, const RuntimeMethod* method) ;
// System.Void System.Collections.ObjectModel.KeyedCollection`2<System.String,YamlDotNet.Core.Tokens.TagDirective>::.ctor()
inline void KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804 (KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD* __this, const RuntimeMethod* method)
{
	((  void (*) (KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD*, const RuntimeMethod*))KeyedCollection_2__ctor_mD5D803A09A8DF6BBF09C4D362C3DA681470B4890_gshared)(__this, method);
}
// System.Void System.Collections.ObjectModel.Collection`1<YamlDotNet.Core.Tokens.TagDirective>::Add(T)
inline void Collection_1_Add_m900AC073217A777000AC48D1E9603F5738DE09C4 (Collection_1_tEA96EDF53D170E847CDEC96682AF4B0C1384BAE6* __this, TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* ___item0, const RuntimeMethod* method)
{
	((  void (*) (Collection_1_tEA96EDF53D170E847CDEC96682AF4B0C1384BAE6*, TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061*, const RuntimeMethod*))Collection_1_Add_m4B1AD8CC1C40112C06A7C38FA96C4E125FF5D7D7_gshared)(__this, ___item0, method);
}
// System.String YamlDotNet.Core.Tokens.TagDirective::get_Handle()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.ObjectModel.KeyedCollection`2<System.String,YamlDotNet.Core.Tokens.TagDirective>::Contains(TKey)
inline bool KeyedCollection_2_Contains_m819F09DCC75B6B8457150A232BD08272EF970248 (KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD* __this, String_t* ___key0, const RuntimeMethod* method)
{
	return ((  bool (*) (KeyedCollection_2_t3B074B8A5CE6AA505DD31B1D91C0ADD65B701CBD*, String_t*, const RuntimeMethod*))KeyedCollection_2_Contains_mD595E54D9A35BB6BD6D39B8DA7424924DFBFCB4D_gshared)(__this, ___key0, method);
}
// System.Void System.InvalidOperationException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.String YamlDotNet.Core.TagName::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::get_IsEmpty()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0 (String_t* ___a0, String_t* ___b1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::get_IsNonSpecific()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsNonSpecific_m8C52DA91116CEFABC753B6D67E0FFA7E2AA58C68 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::get_IsLocal()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsLocal_mEAB47DD7878C075946A6930D18C91396CE190406 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::get_IsGlobal()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsGlobal_m6BDA6DB1FF1060492B2DF12D0F7F1CF14E8AE2F8 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Void System.ArgumentNullException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___paramName0, const RuntimeMethod* method) ;
// System.Void System.ArgumentException::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___message0, String_t* ___paramName1, const RuntimeMethod* method) ;
// System.Boolean System.Uri::IsWellFormedUriString(System.String,System.UriKind)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Uri_IsWellFormedUriString_m5AA722E1CEB8646560346A31BA0AF7D2696120D4 (String_t* ___uriString0, int32_t ___uriKind1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.TagName::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, String_t* ___value0, const RuntimeMethod* method) ;
// System.String YamlDotNet.Core.TagName::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagName_ToString_m9730BC43A2C96FC8DF1C1BABEE3B5497C7C50889 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Boolean System.Object::Equals(System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_Equals_mF52C7AEB4AA9F136C3EA31AE3C1FD200B831B3D1 (RuntimeObject* ___objA0, RuntimeObject* ___objB1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::Equals(YamlDotNet.Core.TagName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_Equals_m8A2D4CC662A8A3C7908ED2FF59DFE64B9D4C14AF (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___other0, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::Equals(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_Equals_m8852A1B9FD821D6E2FC01789BA1C5142BC4F1B7A (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.TagName::GetHashCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TagName_GetHashCode_m729137351880CECA49FD1339A46C5DBDD0531FEE (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::op_Equality(YamlDotNet.Core.TagName,YamlDotNet.Core.TagName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_op_Equality_mCCC3DB2CB09691B0F32A82F65E4A368C09ABE0EC (TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___left0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___right1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.TagName::op_Equality(YamlDotNet.Core.TagName,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_op_Equality_m5E255EBB6F412B07A3150A79B388856E504AC650 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___left0, String_t* ___right1, const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30 (String_t* ___format0, RuntimeObject* ___arg01, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.Version::get_Major()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Version_get_Major_mB872E778C2275DFD3D1036087E06600DD5DECA68_inline (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.Version::get_Minor()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Version_get_Minor_m7C1B9806936F9D9662B04D58E3821E0583C7F39D_inline (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) ;
// System.Int32 System.Int32::GetHashCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295 (int32_t* __this, const RuntimeMethod* method) ;
// System.Int32 YamlDotNet.Core.HashCode::CombineHashCodes(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t HashCode_CombineHashCodes_mF572D9FE6FDDCABD5A4EA767926E6573CC3FB8B7 (int32_t ___h10, int32_t ___h21, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.YamlException::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark,System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_m227F3710DBF857D1AF0D0BB1B777900494E653A0 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, String_t* ___message2, Exception_t* ___innerException3, const RuntimeMethod* method) ;
// System.Void System.Exception::.ctor(System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m9BC141AAB08F47C34B7ED40C1A6C0C1ADDEC5CB3 (Exception_t* __this, String_t* ___message0, Exception_t* ___innerException1, const RuntimeMethod* method) ;
// YamlDotNet.Core.Mark YamlDotNet.Core.YamlException::get_Start()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* YamlException_get_Start_mB634C9460DF018B29F7CC07A809EFA2783CEC968_inline (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) ;
// YamlDotNet.Core.Mark YamlDotNet.Core.YamlException::get_End()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* YamlException_get_End_mB22BEA3B1C0AFA79DD944184421B4EAC202CA9A2_inline (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object,System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m76BF8F3A6AD789E38B708848A2688D400AAC250A (String_t* ___format0, RuntimeObject* ___arg01, RuntimeObject* ___arg12, RuntimeObject* ___arg23, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Anchor::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Anchor__ctor_m3C2CB16EE5709C5EAB6733DCE3D3C99FA5BDAFA1 (Anchor_tEC494D927D531B92F865C0E61947DF32759016B1* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Token::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895 (Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.AnchorName::get_IsEmpty()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AnchorName_get_IsEmpty_m3A5B371407BD56597EB6D78089E7DCC79BDD7A1B (AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.AnchorAlias::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias__ctor_mE1D76BED31BB957B4AD8905D4691B3DA928A9175 (AnchorAlias_tB98567A0A31C86F0CA15323602658ED2C40B029F* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.BlockEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockEnd__ctor_mAA7D62517217449784158E05FD4AFBCD052B7E46 (BlockEnd_t12480C7065A2444C9F4D360DC04CA1854065EA31* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.BlockEntry::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockEntry__ctor_m36E8DA9FCB315996368CF2B02C36A77F46116D93 (BlockEntry_t40AC3EA51287B6D5F5DC519033859532ACD94ABD* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.BlockMappingStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockMappingStart__ctor_m49F94F1959671529F14D90858C57FC99EFEA0151 (BlockMappingStart_t9C5AB2806D66998C719C3162C8F65BFC8DBFE3BA* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.BlockSequenceStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockSequenceStart__ctor_m523233DE969AB041A416776279F3665732BC64A3 (BlockSequenceStart_t987AE0CAA2CA963E8FCD79FB59BD11EF90785D56* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Comment::.ctor(System.String,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment__ctor_m6603352C505B98077744A37A02BABA36BA40E616 (Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* __this, String_t* ___value0, bool ___isInline1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.DocumentEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd__ctor_m25B7760C25AFBC967ABE7C66FBE9FC3E4D8AC877 (DocumentEnd_tFDA49E2D745EE5FC6A3EB52F935E7637AB89F8D4* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.DocumentStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_mF874B4A5909FB91DD98EDD1AFBF623350FC1B01B (DocumentStart_tB03FCCC6E83EF1B3FE5227ADA4A4CC1044CE6C9C* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.FlowEntry::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowEntry__ctor_mD3152644BE666CA8078123274773884572A44A14 (FlowEntry_tCF85EE204C191605C0072EF0DE2E8A6C57B29538* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.FlowMappingEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowMappingEnd__ctor_m8D28A81563ED747C2BC733820AEEC57BA111211C (FlowMappingEnd_tCE5B3FBC6DC603634536A703D6580DF7765B5CBF* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.FlowMappingStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowMappingStart__ctor_mCA79F88E8110AE7ADB1B74AC7A9A7F6529CC1A4C (FlowMappingStart_t22085B50FB25219B0C3D02A9C4F11D68C0CF2E3D* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.FlowSequenceEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowSequenceEnd__ctor_m0C0EEC7D8988060E4C3EFACBCB082CB214B45790 (FlowSequenceEnd_t2863337BC1A979BC4B93B2F77D820A4233E76BB5* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.FlowSequenceStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowSequenceStart__ctor_m6B31549E92BC82D04AD1BE859F169C1613E562B9 (FlowSequenceStart_tAAE66644A7DF27B34E8E481531E0FDEA76F09E11* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Key::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Key__ctor_m19D84D7A9A3D7BFC89D45FD27A69E3964518B0CD (Key_t614783445825A1A71432AD21DEED478DFA144B4B* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Scalar::.ctor(System.String,YamlDotNet.Core.ScalarStyle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m854F9662DED21464F5F3F7C17ECEFFAFE273B044 (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, String_t* ___value0, int32_t ___style1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Scalar::.ctor(System.String,YamlDotNet.Core.ScalarStyle,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m700DCA04B423E17942E3A4EDAC1DFEF944E6AC21 (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, String_t* ___value0, int32_t ___style1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.StreamEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd__ctor_mFE143CBDE30A429F2B32E3314507D32895E9F192 (StreamEnd_tAAE42ABA3EB10720E89FE6E3D6A634EBE60485EC* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.StreamStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart__ctor_m2B7EBAE7FF60F0726666911A932BFF92D668F2EB (StreamStart_t83283B91848E5BDB56E93F42B704BC068B6B752B* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Tag::.ctor(System.String,System.String,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Tag__ctor_m435D46628F64E044B1B4B3E10CF648FF76B7432B (Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* __this, String_t* ___handle0, String_t* ___suffix1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.TagDirective::.ctor(System.String,System.String,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, String_t* ___handle0, String_t* ___prefix1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) ;
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A (String_t* ___value0, const RuntimeMethod* method) ;
// System.Void System.ArgumentNullException::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m6D9C7B47EA708382838B264BA02EBB7576DFA155 (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___paramName0, String_t* ___message1, const RuntimeMethod* method) ;
// System.Boolean System.Text.RegularExpressions.Regex::IsMatch(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Regex_IsMatch_m7E96E666FBE7259D7638A3A6A21BE824D2406F49 (Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* __this, String_t* ___input0, const RuntimeMethod* method) ;
// System.Boolean System.String::Equals(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_Equals_mCD5F35DEDCAFE51ACD4E033726FC2EF8DF7E9B4D (String_t* __this, String_t* ___value0, const RuntimeMethod* method) ;
// System.String YamlDotNet.Core.Tokens.TagDirective::get_Prefix()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344_inline (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0 (String_t* ___str00, String_t* ___str11, String_t* ___str22, const RuntimeMethod* method) ;
// System.Void System.Text.RegularExpressions.Regex::.ctor(System.String,System.Text.RegularExpressions.RegexOptions)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Regex__ctor_mE3996C71B04A4A6845745D01C93B1D27423D0621 (Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* __this, String_t* ___pattern0, int32_t ___options1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.Value::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Value__ctor_m238823F9A84B298674A56E479763274CD502CA27 (Value_tE038E4AE49F94FD0AC0D180B22AFDA4FCFCA9200* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Tokens.VersionDirective::.ctor(YamlDotNet.Core.Version,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VersionDirective__ctor_m0234E07FB972AD557D070F8B411537AFFF8F0E6B (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* ___version0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) ;
// YamlDotNet.Core.Version YamlDotNet.Core.Tokens.VersionDirective::get_Version()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* VersionDirective_get_Version_mA87382DDF754E55F0FC4261A154017C4B8E1F34F_inline (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.ParsingEvent::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4 (ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.AnchorAlias::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias__ctor_m22688D334340CE55DD14B19EABFB8F6FA717027E (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) ;
// YamlDotNet.Core.AnchorName YamlDotNet.Core.Events.AnchorAlias::get_Value()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E AnchorAlias_get_Value_m6EE2E9089D04C5B3263AAB9E75C3770E10E5C8ED_inline (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.Comment::.ctor(System.String,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment__ctor_m6B6C8BE334E2F8ACC00A505F7F016FFCE89B3469 (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, String_t* ___value0, bool ___isInline1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.Comment::get_IsInline()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Comment_get_IsInline_m440EA3B231F4EA478370A2E877FDC7B6CB6CBEDC_inline (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) ;
// System.String YamlDotNet.Core.Events.Comment::get_Value()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* Comment_get_Value_mEBB0458A9AFC00A9EC918B9225EF324F018DEB19_inline (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_mF8B69BE42B5C5ABCAD3C176FBBE3010E0815D65D (String_t* ___str00, String_t* ___str11, String_t* ___str22, String_t* ___str33, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.DocumentEnd::.ctor(System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd__ctor_m0E0429A6D37136BC7126D36963A7D8B136DB536A (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, bool ___isImplicit0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.DocumentEnd::get_IsImplicit()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DocumentEnd_get_IsImplicit_mE6262DD814A1E2DDF83E17BA52C6D3CE03BB3C6B_inline (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.DocumentStart::.ctor(YamlDotNet.Core.Tokens.VersionDirective,YamlDotNet.Core.TagDirectiveCollection,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_m7CB2C3FE638C905FF1BC1600ECD6B4E73BBB7129 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* ___version0, TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* ___tags1, bool ___isImplicit2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start3, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end4, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.DocumentStart::get_IsImplicit()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DocumentStart_get_IsImplicit_mCA5570162010D98F397D1DA3E39CA5B2E7662FAE_inline (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.MappingEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingEnd__ctor_m1A034F25E943D253E6D929905356655143DD7FC6 (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.MappingStart::get_IsImplicit()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool MappingStart_get_IsImplicit_mAF06D6F6F48C2BF8AE6DF163165367C3BC4D50A8_inline (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.NodeEvent::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NodeEvent__ctor_m845F93BEB38E4833ADFF5F9AC4DBA7A10857EFA7 (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.MappingStart::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.Boolean,YamlDotNet.Core.Events.MappingStyle,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingStart__ctor_mA4BCD7F9BF86C8CD4E29BA818D7F20F610D6FB18 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, bool ___isImplicit2, int32_t ___style3, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start4, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end5, const RuntimeMethod* method) ;
// YamlDotNet.Core.AnchorName YamlDotNet.Core.Events.NodeEvent::get_Anchor()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E NodeEvent_get_Anchor_m173523F48C01AC3BBFBEBF80BA9C6E4F06EEADCA_inline (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, const RuntimeMethod* method) ;
// YamlDotNet.Core.TagName YamlDotNet.Core.Events.NodeEvent::get_Tag()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR TagName_t15CB29949E97FF28193B6F635B58928554CB5854 NodeEvent_get_Tag_m1F6D7FD3D70286B18499E8DB95A5CC2152ADA46E_inline (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, const RuntimeMethod* method) ;
// YamlDotNet.Core.Events.MappingStyle YamlDotNet.Core.Events.MappingStart::get_Style()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t MappingStart_get_Style_mFC44BA401D40910D7FFAC1284C388620623D9134_inline (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55 (String_t* ___format0, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___args1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.Scalar::get_IsPlainImplicit()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Scalar_get_IsPlainImplicit_m866B963306A5FE20C34141040E8023B0328C5E34_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.Scalar::get_IsQuotedImplicit()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Scalar_get_IsQuotedImplicit_mB7E1436613709725349C3C5755D9D63F0FEE81F0_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.Scalar::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.String,YamlDotNet.Core.ScalarStyle,System.Boolean,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m8949526F8BC9C06B576AD7ED7EE84B179E4B1377 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, String_t* ___value2, int32_t ___style3, bool ___isPlainImplicit4, bool ___isQuotedImplicit5, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start6, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end7, const RuntimeMethod* method) ;
// System.String YamlDotNet.Core.Events.Scalar::get_Value()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* Scalar_get_Value_mA2941814EF2497D45943217ABA20277C615097A2_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) ;
// YamlDotNet.Core.ScalarStyle YamlDotNet.Core.Events.Scalar::get_Style()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Scalar_get_Style_m8AD3F9689F11B54847605E21257A0832372B3B99_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.SequenceEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceEnd__ctor_m7A72583BF62589EC220C99930653C5799699D7AC (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Boolean YamlDotNet.Core.Events.SequenceStart::get_IsImplicit()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool SequenceStart_get_IsImplicit_m77C7D3FC1CF334C4116C74A9881E660B69119863_inline (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.SequenceStart::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.Boolean,YamlDotNet.Core.Events.SequenceStyle,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceStart__ctor_mA3B2926756626F57678A7C30277DDAD2324E987A (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, bool ___isImplicit2, int32_t ___style3, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start4, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end5, const RuntimeMethod* method) ;
// YamlDotNet.Core.Events.SequenceStyle YamlDotNet.Core.Events.SequenceStart::get_Style()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t SequenceStart_get_Style_mC8F35040576661331BC6B7B4D5EAA3D3BA4CC8BC_inline (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.StreamEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd__ctor_mE4AD31D5A3096F1634B6E3AEFB6DD37555D55113 (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
// System.Void YamlDotNet.Core.Events.StreamStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart__ctor_m56587E78E790DA2E04076054189FA91A70A8C668 (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void YamlDotNet.Core.SemanticErrorException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SemanticErrorException__ctor_mB28B09517FAEDDFDB06420E10DE980AF1D5C2C2A (SemanticErrorException_t0EAAF1E1A5FE24FA81A8761102451E6883F3BA1E* __this, String_t* ___message0, const RuntimeMethod* method) 
{
	{
		// : base(message)
		String_t* L_0 = ___message0;
		YamlException__ctor_m56DCFD258063E331740F0BB3E81E3550963D56FB(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.SemanticErrorException::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SemanticErrorException__ctor_m1115A4F37076E87F0557AB6FB29AF4BCACFB3C97 (SemanticErrorException_t0EAAF1E1A5FE24FA81A8761102451E6883F3BA1E* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, String_t* ___message2, const RuntimeMethod* method) 
{
	{
		// : base(start, end, message)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		String_t* L_2 = ___message2;
		YamlException__ctor_m2E113B1BD7303D541C799174365730373DDE6924(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.SemanticErrorException::.ctor(System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SemanticErrorException__ctor_mCF13FCD71B1C73B23DB62CC833944295C2CC343E (SemanticErrorException_t0EAAF1E1A5FE24FA81A8761102451E6883F3BA1E* __this, String_t* ___message0, Exception_t* ___inner1, const RuntimeMethod* method) 
{
	{
		// : base(message, inner)
		String_t* L_0 = ___message0;
		Exception_t* L_1 = ___inner1;
		YamlException__ctor_mAE5CF47B30D7A830E5D18FACFEBB588185ED6324(__this, L_0, L_1, NULL);
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
// System.Boolean YamlDotNet.Core.SimpleKey::get_IsPossible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SimpleKey_get_IsPossible_m5980B0A3B3D6801F232AF59800326733A5F8E148 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsPossible { get; private set; }
		bool L_0 = __this->___U3CIsPossibleU3Ek__BackingField_1;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.SimpleKey::set_IsPossible(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SimpleKey_set_IsPossible_m9D3BF8BE359A926B73C230A571AB700024E6B161 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, bool ___value0, const RuntimeMethod* method) 
{
	{
		// public bool IsPossible { get; private set; }
		bool L_0 = ___value0;
		__this->___U3CIsPossibleU3Ek__BackingField_1 = L_0;
		return;
	}
}
// System.Void YamlDotNet.Core.SimpleKey::MarkAsImpossible()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SimpleKey_MarkAsImpossible_mC649B73497D8B6CAFB3B09E43F0953FD8DF15ACA (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// IsPossible = false;
		SimpleKey_set_IsPossible_m9D3BF8BE359A926B73C230A571AB700024E6B161_inline(__this, (bool)0, NULL);
		// }
		return;
	}
}
// System.Boolean YamlDotNet.Core.SimpleKey::get_IsRequired()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SimpleKey_get_IsRequired_m7F38279861C5EE922DB3B7FF0177A962BE147054 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsRequired { get; }
		bool L_0 = __this->___U3CIsRequiredU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Int32 YamlDotNet.Core.SimpleKey::get_TokenNumber()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SimpleKey_get_TokenNumber_m2EBE9D9F0C8859BA969F031D9E030A8AD7EE8859 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public int TokenNumber { get; }
		int32_t L_0 = __this->___U3CTokenNumberU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Int32 YamlDotNet.Core.SimpleKey::get_Index()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SimpleKey_get_Index_m207B1B033705D422D62F4DE4B99B3C9BEF972D0B (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public int Index => cursor.Index;
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_0 = __this->___cursor_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = Cursor_get_Index_m80BCD59F059558A7AE2D9F6E818E5063DD2A3DC8_inline(L_0, NULL);
		return L_1;
	}
}
// System.Int32 YamlDotNet.Core.SimpleKey::get_Line()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SimpleKey_get_Line_m04897C37E7A6D27CA1E95608CDAB6BFF9227D606 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public int Line => cursor.Line;
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_0 = __this->___cursor_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = Cursor_get_Line_m4C41A923C959EAEF29D2D0A8C12509FD7FCCEE88_inline(L_0, NULL);
		return L_1;
	}
}
// System.Int32 YamlDotNet.Core.SimpleKey::get_LineOffset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SimpleKey_get_LineOffset_m3D866F21DB773BE19F6967F3E112D8D9C03CFFF5 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public int LineOffset => cursor.LineOffset;
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_0 = __this->___cursor_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = Cursor_get_LineOffset_m8683346CC221F6CE809AABFE6E5677F035AC5AF6_inline(L_0, NULL);
		return L_1;
	}
}
// YamlDotNet.Core.Mark YamlDotNet.Core.SimpleKey::get_Mark()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* SimpleKey_get_Mark_m0B358B5E3BC9DE938DB46DB0792A392E1217F63E (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	{
		// public Mark Mark => cursor.Mark();
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_0 = __this->___cursor_0;
		NullCheck(L_0);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1;
		L_1 = Cursor_Mark_m28DB7A43DE45EF17422A6285D168444AEB02483C(L_0, NULL);
		return L_1;
	}
}
// System.Void YamlDotNet.Core.SimpleKey::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SimpleKey__ctor_m7A71F53F48A1069405B9492D18211CFEECE78164 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public SimpleKey()
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// cursor = new Cursor();
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_0 = (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A*)il2cpp_codegen_object_new(Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Cursor__ctor_m1C5747F1DF5B988AEEDE5734DA730F60D58CE6CB(L_0, NULL);
		__this->___cursor_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___cursor_0), (void*)L_0);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.SimpleKey::.ctor(System.Boolean,System.Int32,YamlDotNet.Core.Cursor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SimpleKey__ctor_mB7735911354EC5A03AEFC6386CB6FDBD0DDBE285 (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, bool ___isRequired0, int32_t ___tokenNumber1, Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* ___cursor2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public SimpleKey(bool isRequired, int tokenNumber, Cursor cursor)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// IsPossible = true;
		SimpleKey_set_IsPossible_m9D3BF8BE359A926B73C230A571AB700024E6B161_inline(__this, (bool)1, NULL);
		// IsRequired = isRequired;
		bool L_0 = ___isRequired0;
		__this->___U3CIsRequiredU3Ek__BackingField_2 = L_0;
		// TokenNumber = tokenNumber;
		int32_t L_1 = ___tokenNumber1;
		__this->___U3CTokenNumberU3Ek__BackingField_3 = L_1;
		// this.cursor = new Cursor(cursor);
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_2 = ___cursor2;
		Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* L_3 = (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A*)il2cpp_codegen_object_new(Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		Cursor__ctor_mCB90283F77973E1DD2CCCB9943F7F7A3EFCBFA0D(L_3, L_2, NULL);
		__this->___cursor_0 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___cursor_0), (void*)L_3);
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
// System.Int32 YamlDotNet.Core.StringLookAheadBuffer::get_Position()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StringLookAheadBuffer_get_Position_m712487E8FF6199BF4E64713EADF7BD41001A2749 (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, const RuntimeMethod* method) 
{
	{
		// public int Position { get; private set; }
		int32_t L_0 = __this->___U3CPositionU3Ek__BackingField_1;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.StringLookAheadBuffer::set_Position(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringLookAheadBuffer_set_Position_mC8275781BC014A2FBB31A0001D6EB3FE0CBC6D5D (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		// public int Position { get; private set; }
		int32_t L_0 = ___value0;
		__this->___U3CPositionU3Ek__BackingField_1 = L_0;
		return;
	}
}
// System.Void YamlDotNet.Core.StringLookAheadBuffer::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringLookAheadBuffer__ctor_mCEA29814049A50166F39CF788505F276719F82F0 (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	{
		// public StringLookAheadBuffer(string value)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// this.value = value;
		String_t* L_0 = ___value0;
		__this->___value_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___value_0), (void*)L_0);
		// }
		return;
	}
}
// System.Int32 YamlDotNet.Core.StringLookAheadBuffer::get_Length()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StringLookAheadBuffer_get_Length_mDCAC98FB6F06253C72D404585F88C661098A812A (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, const RuntimeMethod* method) 
{
	{
		// public int Length => value.Length;
		String_t* L_0 = __this->___value_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_0, NULL);
		return L_1;
	}
}
// System.Boolean YamlDotNet.Core.StringLookAheadBuffer::get_EndOfInput()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool StringLookAheadBuffer_get_EndOfInput_m14EB642A9EF12608486C1720E1B89F3474739936 (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, const RuntimeMethod* method) 
{
	{
		// public bool EndOfInput => IsOutside(Position);
		int32_t L_0;
		L_0 = StringLookAheadBuffer_get_Position_m712487E8FF6199BF4E64713EADF7BD41001A2749_inline(__this, NULL);
		bool L_1;
		L_1 = StringLookAheadBuffer_IsOutside_m8745E8A683A08F996667BDEAAEFCFE7DF7560232(__this, L_0, NULL);
		return L_1;
	}
}
// System.Char YamlDotNet.Core.StringLookAheadBuffer::Peek(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar StringLookAheadBuffer_Peek_m04F247C3B4B32DBB91AC5894F5A2C906B7B72337 (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___offset0, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		// var index = Position + offset;
		int32_t L_0;
		L_0 = StringLookAheadBuffer_get_Position_m712487E8FF6199BF4E64713EADF7BD41001A2749_inline(__this, NULL);
		int32_t L_1 = ___offset0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_0, L_1));
		// return IsOutside(index) ? '\0' : value[index];
		int32_t L_2 = V_0;
		bool L_3;
		L_3 = StringLookAheadBuffer_IsOutside_m8745E8A683A08F996667BDEAAEFCFE7DF7560232(__this, L_2, NULL);
		if (L_3)
		{
			goto IL_001f;
		}
	}
	{
		String_t* L_4 = __this->___value_0;
		int32_t L_5 = V_0;
		NullCheck(L_4);
		Il2CppChar L_6;
		L_6 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_4, L_5, NULL);
		return L_6;
	}

IL_001f:
	{
		return 0;
	}
}
// System.Boolean YamlDotNet.Core.StringLookAheadBuffer::IsOutside(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool StringLookAheadBuffer_IsOutside_m8745E8A683A08F996667BDEAAEFCFE7DF7560232 (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___index0, const RuntimeMethod* method) 
{
	{
		// return index >= value.Length;
		int32_t L_0 = ___index0;
		String_t* L_1 = __this->___value_0;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_1, NULL);
		return (bool)((((int32_t)((((int32_t)L_0) < ((int32_t)L_2))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// System.Void YamlDotNet.Core.StringLookAheadBuffer::Skip(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringLookAheadBuffer_Skip_m0218AE9B3F0A7BDA1C14AD90B3B5CF3E65789FCE (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___length0, const RuntimeMethod* method) 
{
	{
		// if (length < 0)
		int32_t L_0 = ___length0;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0014;
		}
	}
	{
		// throw new ArgumentOutOfRangeException(nameof(length), "The length must be positive.");
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_1 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		NullCheck(L_1);
		ArgumentOutOfRangeException__ctor_mE5B2755F0BEA043CACF915D5CE140859EE58FA66(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE8744A8B8BD390EB66CA0CAE2376C973E6904FFB)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral15799C212077F0C3382CDBD2AA0BBEF54406463B)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&StringLookAheadBuffer_Skip_m0218AE9B3F0A7BDA1C14AD90B3B5CF3E65789FCE_RuntimeMethod_var)));
	}

IL_0014:
	{
		// Position += length;
		int32_t L_2;
		L_2 = StringLookAheadBuffer_get_Position_m712487E8FF6199BF4E64713EADF7BD41001A2749_inline(__this, NULL);
		int32_t L_3 = ___length0;
		StringLookAheadBuffer_set_Position_mC8275781BC014A2FBB31A0001D6EB3FE0CBC6D5D_inline(__this, ((int32_t)il2cpp_codegen_add(L_2, L_3)), NULL);
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
// System.Void YamlDotNet.Core.SyntaxErrorException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SyntaxErrorException__ctor_m58DB3C78007B10F9B955EEB3B82EF4C867CCE78D (SyntaxErrorException_t85D520F4222E570503982C3ED7E3409C86EAE0AB* __this, String_t* ___message0, const RuntimeMethod* method) 
{
	{
		// : base(message)
		String_t* L_0 = ___message0;
		YamlException__ctor_m56DCFD258063E331740F0BB3E81E3550963D56FB(__this, L_0, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.SyntaxErrorException::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SyntaxErrorException__ctor_mFC64FA572B541C7C1710BBF15DA9A374E37A6F5B (SyntaxErrorException_t85D520F4222E570503982C3ED7E3409C86EAE0AB* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, String_t* ___message2, const RuntimeMethod* method) 
{
	{
		// : base(start, end, message)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		String_t* L_2 = ___message2;
		YamlException__ctor_m2E113B1BD7303D541C799174365730373DDE6924(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.SyntaxErrorException::.ctor(System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SyntaxErrorException__ctor_mE2D6A61AEF410E11139EB0EFDD454182952744A7 (SyntaxErrorException_t85D520F4222E570503982C3ED7E3409C86EAE0AB* __this, String_t* ___message0, Exception_t* ___inner1, const RuntimeMethod* method) 
{
	{
		// : base(message, inner)
		String_t* L_0 = ___message0;
		Exception_t* L_1 = ___inner1;
		YamlException__ctor_mAE5CF47B30D7A830E5D18FACFEBB588185ED6324(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.TagDirectiveCollection::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagDirectiveCollection__ctor_mC177C00122A91D25DF5F1C23F827F5EEF6CDAD96 (TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public TagDirectiveCollection()
		KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804(__this, KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.TagDirectiveCollection::.ctor(System.Collections.Generic.IEnumerable`1<YamlDotNet.Core.Tokens.TagDirective>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagDirectiveCollection__ctor_m99AFA5F19DAD66304AC24300727BB4BC14B0A7D2 (TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* __this, RuntimeObject* ___tagDirectives0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Collection_1_Add_m900AC073217A777000AC48D1E9603F5738DE09C4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_t35CD76DEF3AC17416CC3AB951593A18EF9F0254C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_t427CCB5B7502F14587A4AD2D527C9A61C5340E27_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* V_1 = NULL;
	{
		// public TagDirectiveCollection(IEnumerable<TagDirective> tagDirectives)
		KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804(__this, KeyedCollection_2__ctor_m0594AE46FFBA85F47CFE49205191AEED3E339804_RuntimeMethod_var);
		// foreach (var tagDirective in tagDirectives)
		RuntimeObject* L_0 = ___tagDirectives0;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<YamlDotNet.Core.Tokens.TagDirective>::GetEnumerator() */, IEnumerable_1_t35CD76DEF3AC17416CC3AB951593A18EF9F0254C_il2cpp_TypeInfo_var, L_0);
		V_0 = L_1;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0027:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_2 = V_0;
					if (!L_2)
					{
						goto IL_0030;
					}
				}
				{
					RuntimeObject* L_3 = V_0;
					NullCheck(L_3);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_3);
				}

IL_0030:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_001d_1;
			}

IL_000f_1:
			{
				// foreach (var tagDirective in tagDirectives)
				RuntimeObject* L_4 = V_0;
				NullCheck(L_4);
				TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_5;
				L_5 = InterfaceFuncInvoker0< TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<YamlDotNet.Core.Tokens.TagDirective>::get_Current() */, IEnumerator_1_t427CCB5B7502F14587A4AD2D527C9A61C5340E27_il2cpp_TypeInfo_var, L_4);
				V_1 = L_5;
				// Add(tagDirective);
				TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_6 = V_1;
				Collection_1_Add_m900AC073217A777000AC48D1E9603F5738DE09C4(__this, L_6, Collection_1_Add_m900AC073217A777000AC48D1E9603F5738DE09C4_RuntimeMethod_var);
			}

IL_001d_1:
			{
				// foreach (var tagDirective in tagDirectives)
				RuntimeObject* L_7 = V_0;
				NullCheck(L_7);
				bool L_8;
				L_8 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_7);
				if (L_8)
				{
					goto IL_000f_1;
				}
			}
			{
				goto IL_0031;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0031:
	{
		// }
		return;
	}
}
// System.String YamlDotNet.Core.TagDirectiveCollection::GetKeyForItem(YamlDotNet.Core.Tokens.TagDirective)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagDirectiveCollection_GetKeyForItem_m4BA1702BBAA7F084185E210DBFF078BC21EDBF95 (TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* __this, TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* ___item0, const RuntimeMethod* method) 
{
	{
		// protected override string GetKeyForItem(TagDirective item) => item.Handle;
		TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_0 = ___item0;
		NullCheck(L_0);
		String_t* L_1;
		L_1 = TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline(L_0, NULL);
		return L_1;
	}
}
// System.Boolean YamlDotNet.Core.TagDirectiveCollection::Contains(YamlDotNet.Core.Tokens.TagDirective)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagDirectiveCollection_Contains_m21DB36CD24F8AE5E8E56F8E2C8130527D008B1F9 (TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* __this, TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* ___directive0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&KeyedCollection_2_Contains_m819F09DCC75B6B8457150A232BD08272EF970248_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return Contains(GetKeyForItem(directive));
		TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_0 = ___directive0;
		String_t* L_1;
		L_1 = VirtualFuncInvoker1< String_t*, TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* >::Invoke(39 /* TKey System.Collections.ObjectModel.KeyedCollection`2<System.String,YamlDotNet.Core.Tokens.TagDirective>::GetKeyForItem(TItem) */, __this, L_0);
		bool L_2;
		L_2 = KeyedCollection_2_Contains_m819F09DCC75B6B8457150A232BD08272EF970248(__this, L_1, KeyedCollection_2_Contains_m819F09DCC75B6B8457150A232BD08272EF970248_RuntimeMethod_var);
		return L_2;
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
// Conversion methods for marshalling of: YamlDotNet.Core.TagName
IL2CPP_EXTERN_C void TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshal_pinvoke(const TagName_t15CB29949E97FF28193B6F635B58928554CB5854& unmarshaled, TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_pinvoke& marshaled)
{
	marshaled.___value_1 = il2cpp_codegen_marshal_string(unmarshaled.___value_1);
}
IL2CPP_EXTERN_C void TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshal_pinvoke_back(const TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_pinvoke& marshaled, TagName_t15CB29949E97FF28193B6F635B58928554CB5854& unmarshaled)
{
	unmarshaled.___value_1 = il2cpp_codegen_marshal_string_result(marshaled.___value_1);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___value_1), (void*)il2cpp_codegen_marshal_string_result(marshaled.___value_1));
}
// Conversion method for clean up from marshalling of: YamlDotNet.Core.TagName
IL2CPP_EXTERN_C void TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshal_pinvoke_cleanup(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_marshal_free(marshaled.___value_1);
	marshaled.___value_1 = NULL;
}
// Conversion methods for marshalling of: YamlDotNet.Core.TagName
IL2CPP_EXTERN_C void TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshal_com(const TagName_t15CB29949E97FF28193B6F635B58928554CB5854& unmarshaled, TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_com& marshaled)
{
	marshaled.___value_1 = il2cpp_codegen_marshal_bstring(unmarshaled.___value_1);
}
IL2CPP_EXTERN_C void TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshal_com_back(const TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_com& marshaled, TagName_t15CB29949E97FF28193B6F635B58928554CB5854& unmarshaled)
{
	unmarshaled.___value_1 = il2cpp_codegen_marshal_bstring_result(marshaled.___value_1);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___value_1), (void*)il2cpp_codegen_marshal_bstring_result(marshaled.___value_1));
}
// Conversion method for clean up from marshalling of: YamlDotNet.Core.TagName
IL2CPP_EXTERN_C void TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshal_com_cleanup(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_marshaled_com& marshaled)
{
	il2cpp_codegen_marshal_free_bstring(marshaled.___value_1);
	marshaled.___value_1 = NULL;
}
// System.String YamlDotNet.Core.TagName::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	String_t* G_B2_0 = NULL;
	String_t* G_B1_0 = NULL;
	{
		// public string Value => value ?? throw new InvalidOperationException("Cannot read the Value of a non-specific tag");
		String_t* L_0 = __this->___value_1;
		String_t* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_0015;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		NullCheck(L_2);
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE8456D0D9B0A8ADFBEAC72C47ED28A9778E515B1)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057_RuntimeMethod_var)));
	}

IL_0015:
	{
		return G_B2_0;
	}
}
IL2CPP_EXTERN_C  String_t* TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	String_t* _returnValue;
	_returnValue = TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057(_thisAdjusted, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::get_IsEmpty()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsEmpty => value is null;
		String_t* L_0 = __this->___value_1;
		return (bool)((((RuntimeObject*)(String_t*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
	}
}
IL2CPP_EXTERN_C  bool TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	bool _returnValue;
	_returnValue = TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361(_thisAdjusted, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::get_IsNonSpecific()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsNonSpecific_m8C52DA91116CEFABC753B6D67E0FFA7E2AA58C68 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral15196F05B117690F3E12E56AA0C43803EA0D2A46);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral738F291E53E97C08DAE378C71EF70A60E31AE900);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public bool IsNonSpecific => !IsEmpty && (value == "!" || value == "?");
		bool L_0;
		L_0 = TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361(__this, NULL);
		if (L_0)
		{
			goto IL_002d;
		}
	}
	{
		String_t* L_1 = __this->___value_1;
		bool L_2;
		L_2 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_1, _stringLiteral15196F05B117690F3E12E56AA0C43803EA0D2A46, NULL);
		if (L_2)
		{
			goto IL_002b;
		}
	}
	{
		String_t* L_3 = __this->___value_1;
		bool L_4;
		L_4 = String_op_Equality_m0D685A924E5CD78078F248ED1726DA5A9D7D6AC0(L_3, _stringLiteral738F291E53E97C08DAE378C71EF70A60E31AE900, NULL);
		return L_4;
	}

IL_002b:
	{
		return (bool)1;
	}

IL_002d:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool TagName_get_IsNonSpecific_m8C52DA91116CEFABC753B6D67E0FFA7E2AA58C68_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	bool _returnValue;
	_returnValue = TagName_get_IsNonSpecific_m8C52DA91116CEFABC753B6D67E0FFA7E2AA58C68(_thisAdjusted, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::get_IsLocal()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsLocal_mEAB47DD7878C075946A6930D18C91396CE190406 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsLocal => !IsEmpty && Value[0] == '!';
		bool L_0;
		L_0 = TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361(__this, NULL);
		if (L_0)
		{
			goto IL_0019;
		}
	}
	{
		String_t* L_1;
		L_1 = TagName_get_Value_mF90B03DA06CAEC546E4F0903152A5D5924A4A057(__this, NULL);
		NullCheck(L_1);
		Il2CppChar L_2;
		L_2 = String_get_Chars_mC49DF0CD2D3BE7BE97B3AD9C995BE3094F8E36D3(L_1, 0, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)((int32_t)33)))? 1 : 0);
	}

IL_0019:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool TagName_get_IsLocal_mEAB47DD7878C075946A6930D18C91396CE190406_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	bool _returnValue;
	_returnValue = TagName_get_IsLocal_mEAB47DD7878C075946A6930D18C91396CE190406(_thisAdjusted, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::get_IsGlobal()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_get_IsGlobal_m6BDA6DB1FF1060492B2DF12D0F7F1CF14E8AE2F8 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsGlobal => !IsEmpty && !IsLocal;
		bool L_0;
		L_0 = TagName_get_IsEmpty_m834D2C3CD6BF067017106E89ED484B57A81AD361(__this, NULL);
		if (L_0)
		{
			goto IL_0012;
		}
	}
	{
		bool L_1;
		L_1 = TagName_get_IsLocal_mEAB47DD7878C075946A6930D18C91396CE190406(__this, NULL);
		return (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
	}

IL_0012:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool TagName_get_IsGlobal_m6BDA6DB1FF1060492B2DF12D0F7F1CF14E8AE2F8_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	bool _returnValue;
	_returnValue = TagName_get_IsGlobal_m6BDA6DB1FF1060492B2DF12D0F7F1CF14E8AE2F8(_thisAdjusted, method);
	return _returnValue;
}
// System.Void YamlDotNet.Core.TagName::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	String_t* G_B2_0 = NULL;
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* G_B2_1 = NULL;
	String_t* G_B1_0 = NULL;
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* G_B1_1 = NULL;
	{
		// this.value = value ?? throw new ArgumentNullException(nameof(value));
		String_t* L_0 = ___value0;
		String_t* L_1 = L_0;
		G_B1_0 = L_1;
		G_B1_1 = __this;
		if (L_1)
		{
			G_B2_0 = L_1;
			G_B2_1 = __this;
			goto IL_0011;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_2 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_2);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E_RuntimeMethod_var)));
	}

IL_0011:
	{
		G_B2_1->___value_1 = G_B2_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B2_1->___value_1), (void*)G_B2_0);
		// if (value.Length == 0)
		String_t* L_3 = ___value0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_3, NULL);
		if (L_4)
		{
			goto IL_002e;
		}
	}
	{
		// throw new ArgumentException("Tag value must not be empty.", nameof(value));
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral21D4DB462D29C926731F20A0EF0666EF382D13A9)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E_RuntimeMethod_var)));
	}

IL_002e:
	{
		// if (IsGlobal && !Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
		bool L_6;
		L_6 = TagName_get_IsGlobal_m6BDA6DB1FF1060492B2DF12D0F7F1CF14E8AE2F8(__this, NULL);
		if (!L_6)
		{
			goto IL_004f;
		}
	}
	{
		String_t* L_7 = ___value0;
		il2cpp_codegen_runtime_class_init_inline(Uri_t1500A52B5F71A04F5D05C0852D0F2A0941842A0E_il2cpp_TypeInfo_var);
		bool L_8;
		L_8 = Uri_IsWellFormedUriString_m5AA722E1CEB8646560346A31BA0AF7D2696120D4(L_7, 0, NULL);
		if (L_8)
		{
			goto IL_004f;
		}
	}
	{
		// throw new ArgumentException("Global tags must be valid URIs.", nameof(value));
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_9 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_9);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_9, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral8C026E54DBB79FB881A0A7EE631932C15A9E0A1C)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_9, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E_RuntimeMethod_var)));
	}

IL_004f:
	{
		// }
		return;
	}
}
IL2CPP_EXTERN_C  void TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E_AdjustorThunk (RuntimeObject* __this, String_t* ___value0, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E(_thisAdjusted, ___value0, method);
}
// System.String YamlDotNet.Core.TagName::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagName_ToString_m9730BC43A2C96FC8DF1C1BABEE3B5497C7C50889 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral738F291E53E97C08DAE378C71EF70A60E31AE900);
		s_Il2CppMethodInitialized = true;
	}
	String_t* G_B2_0 = NULL;
	String_t* G_B1_0 = NULL;
	{
		// public override string ToString() => value ?? "?";
		String_t* L_0 = __this->___value_1;
		String_t* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
	}
	{
		G_B2_0 = _stringLiteral738F291E53E97C08DAE378C71EF70A60E31AE900;
	}

IL_000f:
	{
		return G_B2_0;
	}
}
IL2CPP_EXTERN_C  String_t* TagName_ToString_m9730BC43A2C96FC8DF1C1BABEE3B5497C7C50889_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	String_t* _returnValue;
	_returnValue = TagName_ToString_m9730BC43A2C96FC8DF1C1BABEE3B5497C7C50889(_thisAdjusted, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::Equals(YamlDotNet.Core.TagName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_Equals_m8A2D4CC662A8A3C7908ED2FF59DFE64B9D4C14AF (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___other0, const RuntimeMethod* method) 
{
	{
		// public bool Equals(TagName other) => Equals(value, other.value);
		String_t* L_0 = __this->___value_1;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___other0;
		String_t* L_2 = L_1.___value_1;
		bool L_3;
		L_3 = Object_Equals_mF52C7AEB4AA9F136C3EA31AE3C1FD200B831B3D1(L_0, L_2, NULL);
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool TagName_Equals_m8A2D4CC662A8A3C7908ED2FF59DFE64B9D4C14AF_AdjustorThunk (RuntimeObject* __this, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___other0, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	bool _returnValue;
	_returnValue = TagName_Equals_m8A2D4CC662A8A3C7908ED2FF59DFE64B9D4C14AF(_thisAdjusted, ___other0, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::Equals(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_Equals_m8852A1B9FD821D6E2FC01789BA1C5142BC4F1B7A (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		// return obj is TagName other && Equals(other);
		RuntimeObject* L_0 = ___obj0;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var)))
		{
			goto IL_0017;
		}
	}
	{
		RuntimeObject* L_1 = ___obj0;
		V_0 = ((*(TagName_t15CB29949E97FF28193B6F635B58928554CB5854*)((TagName_t15CB29949E97FF28193B6F635B58928554CB5854*)(TagName_t15CB29949E97FF28193B6F635B58928554CB5854*)UnBox(L_1, TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var))));
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_2 = V_0;
		bool L_3;
		L_3 = TagName_Equals_m8A2D4CC662A8A3C7908ED2FF59DFE64B9D4C14AF(__this, L_2, NULL);
		return L_3;
	}

IL_0017:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool TagName_Equals_m8852A1B9FD821D6E2FC01789BA1C5142BC4F1B7A_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___obj0, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	bool _returnValue;
	_returnValue = TagName_Equals_m8852A1B9FD821D6E2FC01789BA1C5142BC4F1B7A(_thisAdjusted, ___obj0, method);
	return _returnValue;
}
// System.Int32 YamlDotNet.Core.TagName::GetHashCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TagName_GetHashCode_m729137351880CECA49FD1339A46C5DBDD0531FEE (TagName_t15CB29949E97FF28193B6F635B58928554CB5854* __this, const RuntimeMethod* method) 
{
	String_t* G_B2_0 = NULL;
	String_t* G_B1_0 = NULL;
	{
		// return value?.GetHashCode() ?? 0;
		String_t* L_0 = __this->___value_1;
		String_t* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		return 0;
	}

IL_000c:
	{
		NullCheck(G_B2_0);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, G_B2_0);
		return L_2;
	}
}
IL2CPP_EXTERN_C  int32_t TagName_GetHashCode_m729137351880CECA49FD1339A46C5DBDD0531FEE_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	TagName_t15CB29949E97FF28193B6F635B58928554CB5854* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<TagName_t15CB29949E97FF28193B6F635B58928554CB5854*>(__this + _offset);
	int32_t _returnValue;
	_returnValue = TagName_GetHashCode_m729137351880CECA49FD1339A46C5DBDD0531FEE(_thisAdjusted, method);
	return _returnValue;
}
// System.Boolean YamlDotNet.Core.TagName::op_Equality(YamlDotNet.Core.TagName,YamlDotNet.Core.TagName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_op_Equality_mCCC3DB2CB09691B0F32A82F65E4A368C09ABE0EC (TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___left0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___right1, const RuntimeMethod* method) 
{
	{
		// return left.Equals(right);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_0 = ___right1;
		bool L_1;
		L_1 = TagName_Equals_m8A2D4CC662A8A3C7908ED2FF59DFE64B9D4C14AF((&___left0), L_0, NULL);
		return L_1;
	}
}
// System.Boolean YamlDotNet.Core.TagName::op_Inequality(YamlDotNet.Core.TagName,YamlDotNet.Core.TagName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_op_Inequality_mACC3EECF41A295A2139F871BFF402526506C0B7F (TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___left0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___right1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return !(left == right);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_0 = ___left0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___right1;
		il2cpp_codegen_runtime_class_init_inline(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = TagName_op_Equality_mCCC3DB2CB09691B0F32A82F65E4A368C09ABE0EC(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// System.Boolean YamlDotNet.Core.TagName::op_Equality(YamlDotNet.Core.TagName,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_op_Equality_m5E255EBB6F412B07A3150A79B388856E504AC650 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___left0, String_t* ___right1, const RuntimeMethod* method) 
{
	{
		// return Equals(left.value, right);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_0 = ___left0;
		String_t* L_1 = L_0.___value_1;
		String_t* L_2 = ___right1;
		bool L_3;
		L_3 = Object_Equals_mF52C7AEB4AA9F136C3EA31AE3C1FD200B831B3D1(L_1, L_2, NULL);
		return L_3;
	}
}
// System.Boolean YamlDotNet.Core.TagName::op_Inequality(YamlDotNet.Core.TagName,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagName_op_Inequality_m51650D905BFF5E5CD0BF314C6B4F3B76E9CB7704 (TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___left0, String_t* ___right1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return !(left == right);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_0 = ___left0;
		String_t* L_1 = ___right1;
		il2cpp_codegen_runtime_class_init_inline(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = TagName_op_Equality_m5E255EBB6F412B07A3150A79B388856E504AC650(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// YamlDotNet.Core.TagName YamlDotNet.Core.TagName::op_Implicit(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TagName_t15CB29949E97FF28193B6F635B58928554CB5854 TagName_op_Implicit_mAC66740A6339576FB90642A80389324FE6545934 (String_t* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static implicit operator TagName(string? value) => value == null ? Empty : new TagName(value);
		String_t* L_0 = ___value0;
		if (!L_0)
		{
			goto IL_000a;
		}
	}
	{
		String_t* L_1 = ___value0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_2;
		memset((&L_2), 0, sizeof(L_2));
		TagName__ctor_mB2D6252D97E833515ACCB29EE0AB6611CD527A0E((&L_2), L_1, /*hidden argument*/NULL);
		return L_2;
	}

IL_000a:
	{
		il2cpp_codegen_runtime_class_init_inline(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_3 = ((TagName_t15CB29949E97FF28193B6F635B58928554CB5854_StaticFields*)il2cpp_codegen_static_fields_for(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var))->___Empty_0;
		return L_3;
	}
}
// System.Void YamlDotNet.Core.TagName::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagName__cctor_mA0605773C3A8DEA0731812D093960A7118A79B41 (const RuntimeMethod* method) 
{
	{
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
// System.Int32 YamlDotNet.Core.Version::get_Major()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Version_get_Major_mB872E778C2275DFD3D1036087E06600DD5DECA68 (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) 
{
	{
		// public int Major { get; }
		int32_t L_0 = __this->___U3CMajorU3Ek__BackingField_0;
		return L_0;
	}
}
// System.Int32 YamlDotNet.Core.Version::get_Minor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Version_get_Minor_m7C1B9806936F9D9662B04D58E3821E0583C7F39D (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) 
{
	{
		// public int Minor { get; }
		int32_t L_0 = __this->___U3CMinorU3Ek__BackingField_1;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Version::.ctor(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Version__ctor_m195E6390EC1CC4796B0BC3007F493E094ABC68EF (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, int32_t ___major0, int32_t ___minor1, const RuntimeMethod* method) 
{
	Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* G_B2_0 = NULL;
	Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* G_B1_0 = NULL;
	Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* G_B4_0 = NULL;
	Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* G_B3_0 = NULL;
	{
		// public Version(int major, int minor)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// Major = major >= 0
		//     ? major
		//     : throw new ArgumentOutOfRangeException(nameof(major), $"{major} should be >= 0");
		int32_t L_0 = ___major0;
		G_B1_0 = __this;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			G_B2_0 = __this;
			goto IL_0026;
		}
	}
	{
		int32_t L_1 = ___major0;
		int32_t L_2 = L_1;
		RuntimeObject* L_3 = Box(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)), &L_2);
		String_t* L_4;
		L_4 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral779CF5DC3CA44DC34A860898B077959B730D6D07)), L_3, NULL);
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_5 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ArgumentOutOfRangeException__ctor_mE5B2755F0BEA043CACF915D5CE140859EE58FA66(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral82C791C1966A9B7EFCEB102734ECB5B1DB8AF742)), L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Version__ctor_m195E6390EC1CC4796B0BC3007F493E094ABC68EF_RuntimeMethod_var)));
	}

IL_0026:
	{
		int32_t L_6 = ___major0;
		NullCheck(G_B2_0);
		G_B2_0->___U3CMajorU3Ek__BackingField_0 = L_6;
		// Minor = minor >= 0
		//     ? minor
		//     : throw new ArgumentOutOfRangeException(nameof(minor), $"{minor} should be >= 0");
		int32_t L_7 = ___minor1;
		G_B3_0 = __this;
		if ((((int32_t)L_7) >= ((int32_t)0)))
		{
			G_B4_0 = __this;
			goto IL_004c;
		}
	}
	{
		int32_t L_8 = ___minor1;
		int32_t L_9 = L_8;
		RuntimeObject* L_10 = Box(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)), &L_9);
		String_t* L_11;
		L_11 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral779CF5DC3CA44DC34A860898B077959B730D6D07)), L_10, NULL);
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_12 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		NullCheck(L_12);
		ArgumentOutOfRangeException__ctor_mE5B2755F0BEA043CACF915D5CE140859EE58FA66(L_12, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD3F9023582F96AC5F3DEB69BCAC72DB7F59028A8)), L_11, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_12, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Version__ctor_m195E6390EC1CC4796B0BC3007F493E094ABC68EF_RuntimeMethod_var)));
	}

IL_004c:
	{
		int32_t L_13 = ___minor1;
		NullCheck(G_B4_0);
		G_B4_0->___U3CMinorU3Ek__BackingField_1 = L_13;
		// }
		return;
	}
}
// System.Boolean YamlDotNet.Core.Version::Equals(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Version_Equals_m164178BB64F3C3A2D753DB3777E48AF1853CB66E (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* V_0 = NULL;
	{
		// return obj is Version other
		//     && Major == other.Major
		//     && Minor == other.Minor;
		RuntimeObject* L_0 = ___obj0;
		V_0 = ((Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3*)IsInstSealed((RuntimeObject*)L_0, Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3_il2cpp_TypeInfo_var));
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_1 = V_0;
		if (!L_1)
		{
			goto IL_0027;
		}
	}
	{
		int32_t L_2;
		L_2 = Version_get_Major_mB872E778C2275DFD3D1036087E06600DD5DECA68_inline(__this, NULL);
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = Version_get_Major_mB872E778C2275DFD3D1036087E06600DD5DECA68_inline(L_3, NULL);
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0027;
		}
	}
	{
		int32_t L_5;
		L_5 = Version_get_Minor_m7C1B9806936F9D9662B04D58E3821E0583C7F39D_inline(__this, NULL);
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_6 = V_0;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = Version_get_Minor_m7C1B9806936F9D9662B04D58E3821E0583C7F39D_inline(L_6, NULL);
		return (bool)((((int32_t)L_5) == ((int32_t)L_7))? 1 : 0);
	}

IL_0027:
	{
		return (bool)0;
	}
}
// System.Int32 YamlDotNet.Core.Version::GetHashCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Version_GetHashCode_m4C719E4C36186D132F8BBE0A0FAA3E65CA8C69B0 (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		// return HashCode.CombineHashCodes(Major.GetHashCode(), Minor.GetHashCode());
		int32_t L_0;
		L_0 = Version_get_Major_mB872E778C2275DFD3D1036087E06600DD5DECA68_inline(__this, NULL);
		V_0 = L_0;
		int32_t L_1;
		L_1 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295((&V_0), NULL);
		int32_t L_2;
		L_2 = Version_get_Minor_m7C1B9806936F9D9662B04D58E3821E0583C7F39D_inline(__this, NULL);
		V_0 = L_2;
		int32_t L_3;
		L_3 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295((&V_0), NULL);
		int32_t L_4;
		L_4 = HashCode_CombineHashCodes_mF572D9FE6FDDCABD5A4EA767926E6573CC3FB8B7(L_1, L_3, NULL);
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
// YamlDotNet.Core.Mark YamlDotNet.Core.YamlException::get_Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* YamlException_get_Start_mB634C9460DF018B29F7CC07A809EFA2783CEC968 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) 
{
	{
		// public Mark Start { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CStartU3Ek__BackingField_18;
		return L_0;
	}
}
// YamlDotNet.Core.Mark YamlDotNet.Core.YamlException::get_End()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* YamlException_get_End_mB22BEA3B1C0AFA79DD944184421B4EAC202CA9A2 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) 
{
	{
		// public Mark End { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CEndU3Ek__BackingField_19;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.YamlException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_m56DCFD258063E331740F0BB3E81E3550963D56FB (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, String_t* ___message0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty, message)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		String_t* L_2 = ___message0;
		YamlException__ctor_m2E113B1BD7303D541C799174365730373DDE6924(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.YamlException::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_m2E113B1BD7303D541C799174365730373DDE6924 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, String_t* ___message2, const RuntimeMethod* method) 
{
	{
		// : this(start, end, message, null)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		String_t* L_2 = ___message2;
		YamlException__ctor_m227F3710DBF857D1AF0D0BB1B777900494E653A0(__this, L_0, L_1, L_2, (Exception_t*)NULL, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.YamlException::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark,System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_m227F3710DBF857D1AF0D0BB1B777900494E653A0 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, String_t* ___message2, Exception_t* ___innerException3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : base(message, innerException)
		String_t* L_0 = ___message2;
		Exception_t* L_1 = ___innerException3;
		il2cpp_codegen_runtime_class_init_inline(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9BC141AAB08F47C34B7ED40C1A6C0C1ADDEC5CB3(__this, L_0, L_1, NULL);
		// Start = start;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ___start0;
		__this->___U3CStartU3Ek__BackingField_18 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CStartU3Ek__BackingField_18), (void*)L_2);
		// End = end;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___end1;
		__this->___U3CEndU3Ek__BackingField_19 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CEndU3Ek__BackingField_19), (void*)L_3);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.YamlException::.ctor(System.String,System.Exception)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YamlException__ctor_mAE5CF47B30D7A830E5D18FACFEBB588185ED6324 (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, String_t* ___message0, Exception_t* ___inner1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty, message, inner)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		String_t* L_2 = ___message0;
		Exception_t* L_3 = ___inner1;
		YamlException__ctor_m227F3710DBF857D1AF0D0BB1B777900494E653A0(__this, L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.YamlException::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* YamlException_ToString_mC2FCD7A058875CB4DCA27EDE6DEEC11690EEA31F (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1A086D5809CCBE16E0DAC991195BB302E8DDC85D);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"({Start}) - ({End}): {Message}";
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0;
		L_0 = YamlException_get_Start_mB634C9460DF018B29F7CC07A809EFA2783CEC968_inline(__this, NULL);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1;
		L_1 = YamlException_get_End_mB22BEA3B1C0AFA79DD944184421B4EAC202CA9A2_inline(__this, NULL);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(5 /* System.String System.Exception::get_Message() */, __this);
		String_t* L_3;
		L_3 = String_Format_m76BF8F3A6AD789E38B708848A2688D400AAC250A(_stringLiteral1A086D5809CCBE16E0DAC991195BB302E8DDC85D, L_0, L_1, L_2, NULL);
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
// YamlDotNet.Core.AnchorName YamlDotNet.Core.Tokens.Anchor::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E Anchor_get_Value_m53912B3088B36DC65AE755A924F77E35B219288B (Anchor_tEC494D927D531B92F865C0E61947DF32759016B1* __this, const RuntimeMethod* method) 
{
	{
		// public AnchorName Value { get; }
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.Anchor::.ctor(YamlDotNet.Core.AnchorName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Anchor__ctor_mFAE80974CE2AB25425D634618CF9F75A1B3F2BAE (Anchor_tEC494D927D531B92F865C0E61947DF32759016B1* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(value, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___value0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Anchor__ctor_m3C2CB16EE5709C5EAB6733DCE3D3C99FA5BDAFA1(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Anchor::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Anchor__ctor_m3C2CB16EE5709C5EAB6733DCE3D3C99FA5BDAFA1 (Anchor_tEC494D927D531B92F865C0E61947DF32759016B1* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end2;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// if (value.IsEmpty)
		bool L_2;
		L_2 = AnchorName_get_IsEmpty_m3A5B371407BD56597EB6D78089E7DCC79BDD7A1B((&___value0), NULL);
		if (!L_2)
		{
			goto IL_001c;
		}
	}
	{
		// throw new ArgumentNullException(nameof(value));
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_3 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_3);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Anchor__ctor_m3C2CB16EE5709C5EAB6733DCE3D3C99FA5BDAFA1_RuntimeMethod_var)));
	}

IL_001c:
	{
		// this.Value = value;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_4 = ___value0;
		__this->___U3CValueU3Ek__BackingField_2 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CValueU3Ek__BackingField_2))->___value_2), (void*)NULL);
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
// YamlDotNet.Core.AnchorName YamlDotNet.Core.Tokens.AnchorAlias::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E AnchorAlias_get_Value_mF1BAE4BE0CCECFC09EC640847F0710EEAC8709BD (AnchorAlias_tB98567A0A31C86F0CA15323602658ED2C40B029F* __this, const RuntimeMethod* method) 
{
	{
		// public AnchorName Value { get; }
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.AnchorAlias::.ctor(YamlDotNet.Core.AnchorName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias__ctor_m00A98909564E1438CD4B54890E9628CE8A21F16A (AnchorAlias_tB98567A0A31C86F0CA15323602658ED2C40B029F* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(value, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___value0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		AnchorAlias__ctor_mE1D76BED31BB957B4AD8905D4691B3DA928A9175(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.AnchorAlias::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias__ctor_mE1D76BED31BB957B4AD8905D4691B3DA928A9175 (AnchorAlias_tB98567A0A31C86F0CA15323602658ED2C40B029F* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end2;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// if (value.IsEmpty)
		bool L_2;
		L_2 = AnchorName_get_IsEmpty_m3A5B371407BD56597EB6D78089E7DCC79BDD7A1B((&___value0), NULL);
		if (!L_2)
		{
			goto IL_001c;
		}
	}
	{
		// throw new ArgumentNullException(nameof(value));
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_3 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_3);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AnchorAlias__ctor_mE1D76BED31BB957B4AD8905D4691B3DA928A9175_RuntimeMethod_var)));
	}

IL_001c:
	{
		// this.Value = value;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_4 = ___value0;
		__this->___U3CValueU3Ek__BackingField_2 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CValueU3Ek__BackingField_2))->___value_2), (void*)NULL);
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
// System.Void YamlDotNet.Core.Tokens.BlockEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockEnd__ctor_m1F3E3F024715AE0B552F73CD62022DD0D2AD491D (BlockEnd_t12480C7065A2444C9F4D360DC04CA1854065EA31* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		BlockEnd__ctor_mAA7D62517217449784158E05FD4AFBCD052B7E46(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.BlockEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockEnd__ctor_mAA7D62517217449784158E05FD4AFBCD052B7E46 (BlockEnd_t12480C7065A2444C9F4D360DC04CA1854065EA31* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.BlockEntry::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockEntry__ctor_m2D7AEB66B58A467254A73913ED6FE0103CD7EB37 (BlockEntry_t40AC3EA51287B6D5F5DC519033859532ACD94ABD* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		BlockEntry__ctor_m36E8DA9FCB315996368CF2B02C36A77F46116D93(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.BlockEntry::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockEntry__ctor_m36E8DA9FCB315996368CF2B02C36A77F46116D93 (BlockEntry_t40AC3EA51287B6D5F5DC519033859532ACD94ABD* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.BlockMappingStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockMappingStart__ctor_mF584A4257E2E7B29DB74953ADC90E885E8FF1343 (BlockMappingStart_t9C5AB2806D66998C719C3162C8F65BFC8DBFE3BA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		BlockMappingStart__ctor_m49F94F1959671529F14D90858C57FC99EFEA0151(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.BlockMappingStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockMappingStart__ctor_m49F94F1959671529F14D90858C57FC99EFEA0151 (BlockMappingStart_t9C5AB2806D66998C719C3162C8F65BFC8DBFE3BA* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.BlockSequenceStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockSequenceStart__ctor_mEA39FF79DA6F4A20B41B1BC0F806F8FB466007C6 (BlockSequenceStart_t987AE0CAA2CA963E8FCD79FB59BD11EF90785D56* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		BlockSequenceStart__ctor_m523233DE969AB041A416776279F3665732BC64A3(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.BlockSequenceStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BlockSequenceStart__ctor_m523233DE969AB041A416776279F3665732BC64A3 (BlockSequenceStart_t987AE0CAA2CA963E8FCD79FB59BD11EF90785D56* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.String YamlDotNet.Core.Tokens.Comment::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Comment_get_Value_m8D3460688A80AE5C63EC876A68F070A9547C9310 (Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* __this, const RuntimeMethod* method) 
{
	{
		// public string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Tokens.Comment::get_IsInline()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Comment_get_IsInline_m08ACCCC9BB14AC458AF5D794EA27B20FD813F8FB (Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsInline { get; }
		bool L_0 = __this->___U3CIsInlineU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.Comment::.ctor(System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment__ctor_mF087D73FC03706272A5DB8FC6A6C27A45680B4E1 (Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* __this, String_t* ___value0, bool ___isInline1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(value, isInline, Mark.Empty, Mark.Empty)
		String_t* L_0 = ___value0;
		bool L_1 = ___isInline1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Comment__ctor_m6603352C505B98077744A37A02BABA36BA40E616(__this, L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Comment::.ctor(System.String,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment__ctor_m6603352C505B98077744A37A02BABA36BA40E616 (Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* __this, String_t* ___value0, bool ___isInline1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) 
{
	String_t* G_B2_0 = NULL;
	Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* G_B2_1 = NULL;
	String_t* G_B1_0 = NULL;
	Comment_tD10755E00A1A794C69920C2584CA9A3498DEBD0B* G_B1_1 = NULL;
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start2;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end3;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// Value = value ?? throw new ArgumentNullException(nameof(value));
		String_t* L_2 = ___value0;
		String_t* L_3 = L_2;
		G_B1_0 = L_3;
		G_B1_1 = __this;
		if (L_3)
		{
			G_B2_0 = L_3;
			G_B2_1 = __this;
			goto IL_001a;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_4 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Comment__ctor_m6603352C505B98077744A37A02BABA36BA40E616_RuntimeMethod_var)));
	}

IL_001a:
	{
		NullCheck(G_B2_1);
		G_B2_1->___U3CValueU3Ek__BackingField_2 = G_B2_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B2_1->___U3CValueU3Ek__BackingField_2), (void*)G_B2_0);
		// IsInline = isInline;
		bool L_5 = ___isInline1;
		__this->___U3CIsInlineU3Ek__BackingField_3 = L_5;
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
// System.Void YamlDotNet.Core.Tokens.DocumentEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd__ctor_m063EDE2A7EE2BAA94B967259FE0E1FC70A718769 (DocumentEnd_tFDA49E2D745EE5FC6A3EB52F935E7637AB89F8D4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		DocumentEnd__ctor_m25B7760C25AFBC967ABE7C66FBE9FC3E4D8AC877(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.DocumentEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd__ctor_m25B7760C25AFBC967ABE7C66FBE9FC3E4D8AC877 (DocumentEnd_tFDA49E2D745EE5FC6A3EB52F935E7637AB89F8D4* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.DocumentStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_mDF1F66645AC7540B300F30B57B1A59FADFEEEDF7 (DocumentStart_tB03FCCC6E83EF1B3FE5227ADA4A4CC1044CE6C9C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		DocumentStart__ctor_mF874B4A5909FB91DD98EDD1AFBF623350FC1B01B(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.DocumentStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_mF874B4A5909FB91DD98EDD1AFBF623350FC1B01B (DocumentStart_tB03FCCC6E83EF1B3FE5227ADA4A4CC1044CE6C9C* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.String YamlDotNet.Core.Tokens.Error::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Error_get_Value_mE94CE321359E70F39E33A6B16BCA959FE84741F1 (Error_tE299C2E444261688F2B95051EB78045D4014F1C1* __this, const RuntimeMethod* method) 
{
	{
		// internal string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.Error::.ctor(System.String,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Error__ctor_mFD33A412A31C5C514E33582969D560FFE660F1DE (Error_tE299C2E444261688F2B95051EB78045D4014F1C1* __this, String_t* ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end2;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// Value = value;
		String_t* L_2 = ___value0;
		__this->___U3CValueU3Ek__BackingField_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CValueU3Ek__BackingField_2), (void*)L_2);
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
// System.Void YamlDotNet.Core.Tokens.FlowEntry::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowEntry__ctor_m43D119B5D0F9EDC38A62BF98002808F2C1A9A18B (FlowEntry_tCF85EE204C191605C0072EF0DE2E8A6C57B29538* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		FlowEntry__ctor_mD3152644BE666CA8078123274773884572A44A14(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.FlowEntry::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowEntry__ctor_mD3152644BE666CA8078123274773884572A44A14 (FlowEntry_tCF85EE204C191605C0072EF0DE2E8A6C57B29538* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.FlowMappingEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowMappingEnd__ctor_mF6780247AAB15AC7507D2BD4063E658401229F86 (FlowMappingEnd_tCE5B3FBC6DC603634536A703D6580DF7765B5CBF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		FlowMappingEnd__ctor_m8D28A81563ED747C2BC733820AEEC57BA111211C(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.FlowMappingEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowMappingEnd__ctor_m8D28A81563ED747C2BC733820AEEC57BA111211C (FlowMappingEnd_tCE5B3FBC6DC603634536A703D6580DF7765B5CBF* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.FlowMappingStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowMappingStart__ctor_m9AEA402A2ADF3024D59350155C223DDE8C6E52B4 (FlowMappingStart_t22085B50FB25219B0C3D02A9C4F11D68C0CF2E3D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		FlowMappingStart__ctor_mCA79F88E8110AE7ADB1B74AC7A9A7F6529CC1A4C(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.FlowMappingStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowMappingStart__ctor_mCA79F88E8110AE7ADB1B74AC7A9A7F6529CC1A4C (FlowMappingStart_t22085B50FB25219B0C3D02A9C4F11D68C0CF2E3D* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.FlowSequenceEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowSequenceEnd__ctor_mA37C3BB38E3DE3825B415203EB282E7C3D7AB2F6 (FlowSequenceEnd_t2863337BC1A979BC4B93B2F77D820A4233E76BB5* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		FlowSequenceEnd__ctor_m0C0EEC7D8988060E4C3EFACBCB082CB214B45790(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.FlowSequenceEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowSequenceEnd__ctor_m0C0EEC7D8988060E4C3EFACBCB082CB214B45790 (FlowSequenceEnd_t2863337BC1A979BC4B93B2F77D820A4233E76BB5* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.FlowSequenceStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowSequenceStart__ctor_mC51E2AD922E52D25E979B843F3E8B8629E398161 (FlowSequenceStart_tAAE66644A7DF27B34E8E481531E0FDEA76F09E11* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		FlowSequenceStart__ctor_m6B31549E92BC82D04AD1BE859F169C1613E562B9(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.FlowSequenceStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FlowSequenceStart__ctor_m6B31549E92BC82D04AD1BE859F169C1613E562B9 (FlowSequenceStart_tAAE66644A7DF27B34E8E481531E0FDEA76F09E11* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.Key::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Key__ctor_m233198FC1B3671024775A12C2B531ECD09D5A87D (Key_t614783445825A1A71432AD21DEED478DFA144B4B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Key__ctor_m19D84D7A9A3D7BFC89D45FD27A69E3964518B0CD(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Key::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Key__ctor_m19D84D7A9A3D7BFC89D45FD27A69E3964518B0CD (Key_t614783445825A1A71432AD21DEED478DFA144B4B* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.String YamlDotNet.Core.Tokens.Scalar::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Scalar_get_Value_mCE3B3C68C9E6520E9785FF7AB36DF42839076D1B (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, const RuntimeMethod* method) 
{
	{
		// public string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// YamlDotNet.Core.ScalarStyle YamlDotNet.Core.Tokens.Scalar::get_Style()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Scalar_get_Style_m3FDB554FD726B7C5DFB7EA24611EF30BC7E9F69A (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, const RuntimeMethod* method) 
{
	{
		// public ScalarStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.Scalar::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_mC1B6FE0F61C0730218C6932B76EC3D3A97F952FF (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	{
		// : this(value, ScalarStyle.Any)
		String_t* L_0 = ___value0;
		Scalar__ctor_m854F9662DED21464F5F3F7C17ECEFFAFE273B044(__this, L_0, 0, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Scalar::.ctor(System.String,YamlDotNet.Core.ScalarStyle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m854F9662DED21464F5F3F7C17ECEFFAFE273B044 (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, String_t* ___value0, int32_t ___style1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(value, style, Mark.Empty, Mark.Empty)
		String_t* L_0 = ___value0;
		int32_t L_1 = ___style1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Scalar__ctor_m700DCA04B423E17942E3A4EDAC1DFEF944E6AC21(__this, L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Scalar::.ctor(System.String,YamlDotNet.Core.ScalarStyle,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m700DCA04B423E17942E3A4EDAC1DFEF944E6AC21 (Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* __this, String_t* ___value0, int32_t ___style1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) 
{
	String_t* G_B2_0 = NULL;
	Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* G_B2_1 = NULL;
	String_t* G_B1_0 = NULL;
	Scalar_t063F0ED0AE489C799F2F25647718E812CF768796* G_B1_1 = NULL;
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start2;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end3;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// this.Value = value ?? throw new ArgumentNullException(nameof(value));
		String_t* L_2 = ___value0;
		String_t* L_3 = L_2;
		G_B1_0 = L_3;
		G_B1_1 = __this;
		if (L_3)
		{
			G_B2_0 = L_3;
			G_B2_1 = __this;
			goto IL_001a;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_4 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral46F273EF641E07D271D91E0DC24A4392582671F8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Scalar__ctor_m700DCA04B423E17942E3A4EDAC1DFEF944E6AC21_RuntimeMethod_var)));
	}

IL_001a:
	{
		NullCheck(G_B2_1);
		G_B2_1->___U3CValueU3Ek__BackingField_2 = G_B2_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B2_1->___U3CValueU3Ek__BackingField_2), (void*)G_B2_0);
		// this.Style = style;
		int32_t L_5 = ___style1;
		__this->___U3CStyleU3Ek__BackingField_3 = L_5;
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
// System.Void YamlDotNet.Core.Tokens.StreamEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd__ctor_mEF999E99895B9BCD2CCE25E2DE2C8E1CD4876040 (StreamEnd_tAAE42ABA3EB10720E89FE6E3D6A634EBE60485EC* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		StreamEnd__ctor_mFE143CBDE30A429F2B32E3314507D32895E9F192(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.StreamEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd__ctor_mFE143CBDE30A429F2B32E3314507D32895E9F192 (StreamEnd_tAAE42ABA3EB10720E89FE6E3D6A634EBE60485EC* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.Void YamlDotNet.Core.Tokens.StreamStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart__ctor_m6093F120ED15F97AFA5630738494A98F161B8917 (StreamStart_t83283B91848E5BDB56E93F42B704BC068B6B752B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		StreamStart__ctor_m2B7EBAE7FF60F0726666911A932BFF92D668F2EB(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.StreamStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart__ctor_m2B7EBAE7FF60F0726666911A932BFF92D668F2EB (StreamStart_t83283B91848E5BDB56E93F42B704BC068B6B752B* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// System.String YamlDotNet.Core.Tokens.Tag::get_Handle()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Tag_get_Handle_m96409FDD58D2BF63689E9CD5E3402E0933402D65 (Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* __this, const RuntimeMethod* method) 
{
	{
		// public string Handle { get; }
		String_t* L_0 = __this->___U3CHandleU3Ek__BackingField_2;
		return L_0;
	}
}
// System.String YamlDotNet.Core.Tokens.Tag::get_Suffix()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Tag_get_Suffix_mBD2F92731978FE2D2E903197147A55695AB25B52 (Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* __this, const RuntimeMethod* method) 
{
	{
		// public string Suffix { get; }
		String_t* L_0 = __this->___U3CSuffixU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.Tag::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Tag__ctor_mD608CFD14EE973073141D6BC57F33B6C07A1D775 (Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* __this, String_t* ___handle0, String_t* ___suffix1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(handle, suffix, Mark.Empty, Mark.Empty)
		String_t* L_0 = ___handle0;
		String_t* L_1 = ___suffix1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Tag__ctor_m435D46628F64E044B1B4B3E10CF648FF76B7432B(__this, L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Tag::.ctor(System.String,System.String,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Tag__ctor_m435D46628F64E044B1B4B3E10CF648FF76B7432B (Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* __this, String_t* ___handle0, String_t* ___suffix1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) 
{
	String_t* G_B2_0 = NULL;
	Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* G_B2_1 = NULL;
	String_t* G_B1_0 = NULL;
	Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* G_B1_1 = NULL;
	String_t* G_B4_0 = NULL;
	Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* G_B4_1 = NULL;
	String_t* G_B3_0 = NULL;
	Tag_t798685C1FB42713672C76CED2942C88CD2899CD2* G_B3_1 = NULL;
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start2;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end3;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// this.Handle = handle ?? throw new ArgumentNullException(nameof(handle));
		String_t* L_2 = ___handle0;
		String_t* L_3 = L_2;
		G_B1_0 = L_3;
		G_B1_1 = __this;
		if (L_3)
		{
			G_B2_0 = L_3;
			G_B2_1 = __this;
			goto IL_001a;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_4 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralFFE3A1B73CD7FC81540FBBE737435B0A887629D5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Tag__ctor_m435D46628F64E044B1B4B3E10CF648FF76B7432B_RuntimeMethod_var)));
	}

IL_001a:
	{
		NullCheck(G_B2_1);
		G_B2_1->___U3CHandleU3Ek__BackingField_2 = G_B2_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B2_1->___U3CHandleU3Ek__BackingField_2), (void*)G_B2_0);
		// this.Suffix = suffix ?? throw new ArgumentNullException(nameof(suffix));
		String_t* L_5 = ___suffix1;
		String_t* L_6 = L_5;
		G_B3_0 = L_6;
		G_B3_1 = __this;
		if (L_6)
		{
			G_B4_0 = L_6;
			G_B4_1 = __this;
			goto IL_0030;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_7 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_7);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_7, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralEE77384131B17CE853EE959871A8222FC81E9CF5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_7, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Tag__ctor_m435D46628F64E044B1B4B3E10CF648FF76B7432B_RuntimeMethod_var)));
	}

IL_0030:
	{
		NullCheck(G_B4_1);
		G_B4_1->___U3CSuffixU3Ek__BackingField_3 = G_B4_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B4_1->___U3CSuffixU3Ek__BackingField_3), (void*)G_B4_0);
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
// System.String YamlDotNet.Core.Tokens.TagDirective::get_Handle()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) 
{
	{
		// public string Handle { get; }
		String_t* L_0 = __this->___U3CHandleU3Ek__BackingField_2;
		return L_0;
	}
}
// System.String YamlDotNet.Core.Tokens.TagDirective::get_Prefix()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) 
{
	{
		// public string Prefix { get; }
		String_t* L_0 = __this->___U3CPrefixU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.TagDirective::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagDirective__ctor_m500FAD6ECC185F99F93819925D40255A521ACE51 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, String_t* ___handle0, String_t* ___prefix1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(handle, prefix, Mark.Empty, Mark.Empty)
		String_t* L_0 = ___handle0;
		String_t* L_1 = ___prefix1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167(__this, L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.TagDirective::.ctor(System.String,System.String,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, String_t* ___handle0, String_t* ___prefix1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start2;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end3;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// if (string.IsNullOrEmpty(handle))
		String_t* L_2 = ___handle0;
		bool L_3;
		L_3 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_2, NULL);
		if (!L_3)
		{
			goto IL_0021;
		}
	}
	{
		// throw new ArgumentNullException(nameof(handle), "Tag handle must not be empty.");
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_4 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_4);
		ArgumentNullException__ctor_m6D9C7B47EA708382838B264BA02EBB7576DFA155(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralFFE3A1B73CD7FC81540FBBE737435B0A887629D5)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral1B090FBEA32D5B639DF18F6ECD1D23F4944A19AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167_RuntimeMethod_var)));
	}

IL_0021:
	{
		// if (!TagHandlePattern.IsMatch(handle))
		il2cpp_codegen_runtime_class_init_inline(TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var);
		Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* L_5 = ((TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_StaticFields*)il2cpp_codegen_static_fields_for(TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var))->___TagHandlePattern_4;
		String_t* L_6 = ___handle0;
		NullCheck(L_5);
		bool L_7;
		L_7 = Regex_IsMatch_m7E96E666FBE7259D7638A3A6A21BE824D2406F49(L_5, L_6, NULL);
		if (L_7)
		{
			goto IL_003e;
		}
	}
	{
		// throw new ArgumentException("Tag handle must start and end with '!' and contain alphanumerical characters only.", nameof(handle));
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_8 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_8);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_8, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCB7CBB1BC8BA2BCF7942450A0E34E51300205098)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralFFE3A1B73CD7FC81540FBBE737435B0A887629D5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_8, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167_RuntimeMethod_var)));
	}

IL_003e:
	{
		// this.Handle = handle;
		String_t* L_9 = ___handle0;
		__this->___U3CHandleU3Ek__BackingField_2 = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CHandleU3Ek__BackingField_2), (void*)L_9);
		// if (string.IsNullOrEmpty(prefix))
		String_t* L_10 = ___prefix1;
		bool L_11;
		L_11 = String_IsNullOrEmpty_m54CF0907E7C4F3AFB2E796A13DC751ECBB8DB64A(L_10, NULL);
		if (!L_11)
		{
			goto IL_005d;
		}
	}
	{
		// throw new ArgumentNullException(nameof(prefix), "Tag prefix must not be empty.");
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_12 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_12);
		ArgumentNullException__ctor_m6D9C7B47EA708382838B264BA02EBB7576DFA155(L_12, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCB5CDE966F99FDC7AE4101331D907BCEF208D664)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral948B944155B13DC838C958C29968902C1ADC6391)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_12, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TagDirective__ctor_m77BD10FCD22A19BCD106F56C3AE2B6F38C6A5167_RuntimeMethod_var)));
	}

IL_005d:
	{
		// this.Prefix = prefix;
		String_t* L_13 = ___prefix1;
		__this->___U3CPrefixU3Ek__BackingField_3 = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CPrefixU3Ek__BackingField_3), (void*)L_13);
		// }
		return;
	}
}
// System.Boolean YamlDotNet.Core.Tokens.TagDirective::Equals(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TagDirective_Equals_m670C1EAA3BB87ECD712B6FAD727F35C0C8B1A0B3 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* V_0 = NULL;
	{
		// return obj is TagDirective other
		//     && Handle.Equals(other.Handle)
		//     && Prefix.Equals(other.Prefix);
		RuntimeObject* L_0 = ___obj0;
		V_0 = ((TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061*)IsInstClass((RuntimeObject*)L_0, TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var));
		TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_1 = V_0;
		if (!L_1)
		{
			goto IL_002f;
		}
	}
	{
		String_t* L_2;
		L_2 = TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline(__this, NULL);
		TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_3 = V_0;
		NullCheck(L_3);
		String_t* L_4;
		L_4 = TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline(L_3, NULL);
		NullCheck(L_2);
		bool L_5;
		L_5 = String_Equals_mCD5F35DEDCAFE51ACD4E033726FC2EF8DF7E9B4D(L_2, L_4, NULL);
		if (!L_5)
		{
			goto IL_002f;
		}
	}
	{
		String_t* L_6;
		L_6 = TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344_inline(__this, NULL);
		TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* L_7 = V_0;
		NullCheck(L_7);
		String_t* L_8;
		L_8 = TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344_inline(L_7, NULL);
		NullCheck(L_6);
		bool L_9;
		L_9 = String_Equals_mCD5F35DEDCAFE51ACD4E033726FC2EF8DF7E9B4D(L_6, L_8, NULL);
		return L_9;
	}

IL_002f:
	{
		return (bool)0;
	}
}
// System.Int32 YamlDotNet.Core.Tokens.TagDirective::GetHashCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TagDirective_GetHashCode_m78E5837D5286A7996D1EB94E1DDEAAE59D760793 (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) 
{
	{
		// return Handle.GetHashCode() ^ Prefix.GetHashCode();
		String_t* L_0;
		L_0 = TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline(__this, NULL);
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_0);
		String_t* L_2;
		L_2 = TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344_inline(__this, NULL);
		NullCheck(L_2);
		int32_t L_3;
		L_3 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_2);
		return ((int32_t)(L_1^L_3));
	}
}
// System.String YamlDotNet.Core.Tokens.TagDirective::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* TagDirective_ToString_m0E781AB2ACEB065029490358210F1668E4781F2C (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral82D95C9038FADE61EAA402493C3AB02991DF2B25);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"{Handle} => {Prefix}";
		String_t* L_0;
		L_0 = TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline(__this, NULL);
		String_t* L_1;
		L_1 = TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344_inline(__this, NULL);
		String_t* L_2;
		L_2 = String_Concat_m9B13B47FCB3DF61144D9647DDA05F527377251B0(L_0, _stringLiteral82D95C9038FADE61EAA402493C3AB02991DF2B25, L_1, NULL);
		return L_2;
	}
}
// System.Void YamlDotNet.Core.Tokens.TagDirective::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TagDirective__cctor_m07365D5A5471CFF370A95904AC0B16B6842FD272 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral91911A5D93C38999B3F0C946DB48AFEFF926C0C2);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private static readonly Regex TagHandlePattern = new Regex(@"^!([0-9A-Za-z_\-]*!)?$", StandardRegexOptions.Compiled);
		Regex_tE773142C2BE45C5D362B0F815AFF831707A51772* L_0 = (Regex_tE773142C2BE45C5D362B0F815AFF831707A51772*)il2cpp_codegen_object_new(Regex_tE773142C2BE45C5D362B0F815AFF831707A51772_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Regex__ctor_mE3996C71B04A4A6845745D01C93B1D27423D0621(L_0, _stringLiteral91911A5D93C38999B3F0C946DB48AFEFF926C0C2, 0, NULL);
		((TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_StaticFields*)il2cpp_codegen_static_fields_for(TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var))->___TagHandlePattern_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_StaticFields*)il2cpp_codegen_static_fields_for(TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061_il2cpp_TypeInfo_var))->___TagHandlePattern_4), (void*)L_0);
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
// YamlDotNet.Core.Mark YamlDotNet.Core.Tokens.Token::get_Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* Token_get_Start_m66256FAF45A17DD7B6A033C04CBDBDA037B44F61 (Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* __this, const RuntimeMethod* method) 
{
	{
		// public Mark Start { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CStartU3Ek__BackingField_0;
		return L_0;
	}
}
// YamlDotNet.Core.Mark YamlDotNet.Core.Tokens.Token::get_End()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* Token_get_End_m36D727E16C563820DD2248B4D8C05C28E0F9C6E0 (Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* __this, const RuntimeMethod* method) 
{
	{
		// public Mark End { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CEndU3Ek__BackingField_1;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.Token::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895 (Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B2_0 = NULL;
	Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* G_B2_1 = NULL;
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B1_0 = NULL;
	Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* G_B1_1 = NULL;
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B4_0 = NULL;
	Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* G_B4_1 = NULL;
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B3_0 = NULL;
	Token_tBF9A8215C30363F3FD515BB7813C50A69413BD38* G_B3_1 = NULL;
	{
		// protected Token(Mark start, Mark end)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// this.Start = start ?? throw new ArgumentNullException(nameof(start));
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = L_0;
		G_B1_0 = L_1;
		G_B1_1 = __this;
		if (L_1)
		{
			G_B2_0 = L_1;
			G_B2_1 = __this;
			goto IL_0017;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_2 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_2);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2AD47C03F7A83F82E3B2ADFE8A60F1727FD3BEFD)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895_RuntimeMethod_var)));
	}

IL_0017:
	{
		NullCheck(G_B2_1);
		G_B2_1->___U3CStartU3Ek__BackingField_0 = G_B2_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B2_1->___U3CStartU3Ek__BackingField_0), (void*)G_B2_0);
		// this.End = end ?? throw new ArgumentNullException(nameof(end));
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___end1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = L_3;
		G_B3_0 = L_4;
		G_B3_1 = __this;
		if (L_4)
		{
			G_B4_0 = L_4;
			G_B4_1 = __this;
			goto IL_002d;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_5 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA2F4AC9DD8E1FAC5257E5F7BA5EE1C7C7E5F7AB1)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895_RuntimeMethod_var)));
	}

IL_002d:
	{
		NullCheck(G_B4_1);
		G_B4_1->___U3CEndU3Ek__BackingField_1 = G_B4_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B4_1->___U3CEndU3Ek__BackingField_1), (void*)G_B4_0);
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
// System.Void YamlDotNet.Core.Tokens.Value::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Value__ctor_m28F9485EB3878C386A901C8444ECAD30AB408DC3 (Value_tE038E4AE49F94FD0AC0D180B22AFDA4FCFCA9200* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Value__ctor_m238823F9A84B298674A56E479763274CD502CA27(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.Value::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Value__ctor_m238823F9A84B298674A56E479763274CD502CA27 (Value_tE038E4AE49F94FD0AC0D180B22AFDA4FCFCA9200* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
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
// YamlDotNet.Core.Version YamlDotNet.Core.Tokens.VersionDirective::get_Version()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* VersionDirective_get_Version_mA87382DDF754E55F0FC4261A154017C4B8E1F34F (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, const RuntimeMethod* method) 
{
	{
		// public Version Version { get; }
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_0 = __this->___U3CVersionU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Tokens.VersionDirective::.ctor(YamlDotNet.Core.Version)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VersionDirective__ctor_m8167F05DE922B2BF72BB9BB58E5D759026D23EC3 (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* ___version0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(version, Mark.Empty, Mark.Empty)
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_0 = ___version0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		VersionDirective__ctor_m0234E07FB972AD557D070F8B411537AFFF8F0E6B(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Tokens.VersionDirective::.ctor(YamlDotNet.Core.Version,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void VersionDirective__ctor_m0234E07FB972AD557D070F8B411537AFFF8F0E6B (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* ___version0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end2;
		Token__ctor_m2D40245AE8D316CC1801FBEBD9A0C906E84B4895(__this, L_0, L_1, NULL);
		// this.Version = version;
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_2 = ___version0;
		__this->___U3CVersionU3Ek__BackingField_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVersionU3Ek__BackingField_2), (void*)L_2);
		// }
		return;
	}
}
// System.Boolean YamlDotNet.Core.Tokens.VersionDirective::Equals(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool VersionDirective_Equals_m400905895B32BFDB20AC0A3E109E4AD07E9FB376 (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, RuntimeObject* ___obj0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* V_0 = NULL;
	{
		// return obj is VersionDirective other
		//     && Version.Equals(other.Version);
		RuntimeObject* L_0 = ___obj0;
		V_0 = ((VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5*)IsInstSealed((RuntimeObject*)L_0, VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5_il2cpp_TypeInfo_var));
		VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_2;
		L_2 = VersionDirective_get_Version_mA87382DDF754E55F0FC4261A154017C4B8E1F34F_inline(__this, NULL);
		VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* L_3 = V_0;
		NullCheck(L_3);
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_4;
		L_4 = VersionDirective_get_Version_mA87382DDF754E55F0FC4261A154017C4B8E1F34F_inline(L_3, NULL);
		NullCheck(L_2);
		bool L_5;
		L_5 = VirtualFuncInvoker1< bool, RuntimeObject* >::Invoke(0 /* System.Boolean System.Object::Equals(System.Object) */, L_2, L_4);
		return L_5;
	}

IL_001c:
	{
		return (bool)0;
	}
}
// System.Int32 YamlDotNet.Core.Tokens.VersionDirective::GetHashCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t VersionDirective_GetHashCode_m500C791F10A2536D13CC7364934C54ABCAEF7C18 (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, const RuntimeMethod* method) 
{
	{
		// return Version.GetHashCode();
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_0;
		L_0 = VersionDirective_get_Version_mA87382DDF754E55F0FC4261A154017C4B8E1F34F_inline(__this, NULL);
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_0);
		return L_1;
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
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.AnchorAlias::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AnchorAlias_get_Type_m7F6530CE02DF60DDB1481BF78D7F6D41A2DB13A7 (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.Alias;
		return (int32_t)(5);
	}
}
// YamlDotNet.Core.AnchorName YamlDotNet.Core.Events.AnchorAlias::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E AnchorAlias_get_Value_m6EE2E9089D04C5B3263AAB9E75C3770E10E5C8ED (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, const RuntimeMethod* method) 
{
	{
		// public AnchorName Value { get; }
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.AnchorAlias::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias__ctor_m22688D334340CE55DD14B19EABFB8F6FA717027E (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end2;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// if (value.IsEmpty)
		bool L_2;
		L_2 = AnchorName_get_IsEmpty_m3A5B371407BD56597EB6D78089E7DCC79BDD7A1B((&___value0), NULL);
		if (!L_2)
		{
			goto IL_001e;
		}
	}
	{
		// throw new YamlException(start, end, "Anchor value must not be empty.");
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ___end2;
		YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* L_5 = (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		YamlException__ctor_m2E113B1BD7303D541C799174365730373DDE6924(L_5, L_3, L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral1883E177578C34C4BBA579EA98A67CBF5D34BE3D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AnchorAlias__ctor_m22688D334340CE55DD14B19EABFB8F6FA717027E_RuntimeMethod_var)));
	}

IL_001e:
	{
		// this.Value = value;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_6 = ___value0;
		__this->___U3CValueU3Ek__BackingField_2 = L_6;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CValueU3Ek__BackingField_2))->___value_2), (void*)NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.AnchorAlias::.ctor(YamlDotNet.Core.AnchorName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias__ctor_m07BE642857D63B1A2EB45A657C5FC5BE16CAF386 (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(value, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___value0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		AnchorAlias__ctor_m22688D334340CE55DD14B19EABFB8F6FA717027E(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.AnchorAlias::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AnchorAlias_ToString_mC2B068C209D3BB718A6B4F45E38C48BF6250ABFF (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8C8056CFB8CBFF1B0947F62BBBC5824D24D194BE);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"Alias [value = {Value}]";
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0;
		L_0 = AnchorAlias_get_Value_m6EE2E9089D04C5B3263AAB9E75C3770E10E5C8ED_inline(__this, NULL);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_1 = L_0;
		RuntimeObject* L_2 = Box(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var, &L_1);
		String_t* L_3;
		L_3 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(_stringLiteral8C8056CFB8CBFF1B0947F62BBBC5824D24D194BE, L_2, NULL);
		return L_3;
	}
}
// System.Void YamlDotNet.Core.Events.AnchorAlias::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnchorAlias_Accept_m56A08452A57AA4CC70496C955BACF7CFF36D362B (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* >::Invoke(0 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.AnchorAlias) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.String YamlDotNet.Core.Events.Comment::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Comment_get_Value_mEBB0458A9AFC00A9EC918B9225EF324F018DEB19 (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) 
{
	{
		// public string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.Comment::get_IsInline()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Comment_get_IsInline_m440EA3B231F4EA478370A2E877FDC7B6CB6CBEDC (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsInline { get; }
		bool L_0 = __this->___U3CIsInlineU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.Comment::.ctor(System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment__ctor_mAC6D1DF685B499D42519894E425B7F64EE7B4ED3 (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, String_t* ___value0, bool ___isInline1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(value, isInline, Mark.Empty, Mark.Empty)
		String_t* L_0 = ___value0;
		bool L_1 = ___isInline1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Comment__ctor_m6B6C8BE334E2F8ACC00A505F7F016FFCE89B3469(__this, L_0, L_1, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.Comment::.ctor(System.String,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment__ctor_m6B6C8BE334E2F8ACC00A505F7F016FFCE89B3469 (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, String_t* ___value0, bool ___isInline1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start2;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end3;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// Value = value;
		String_t* L_2 = ___value0;
		__this->___U3CValueU3Ek__BackingField_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CValueU3Ek__BackingField_2), (void*)L_2);
		// IsInline = isInline;
		bool L_3 = ___isInline1;
		__this->___U3CIsInlineU3Ek__BackingField_3 = L_3;
		// }
		return;
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.Comment::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Comment_get_Type_mBB18D879CBEDB6A698717B0983FC07532EB9857D (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.Comment;
		return (int32_t)(((int32_t)11));
	}
}
// System.Void YamlDotNet.Core.Events.Comment::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Comment_Accept_m3FDC21EBC87696538D462897E5144BB47A5F02A9 (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* >::Invoke(10 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.Comment) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.Comment::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Comment_ToString_m68478CAEE1CC4FF85A03697039DA90E928B977E6 (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral09F3589E3F129822338E12B67FB0990E4EF2F3DE);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3B9A4DA33EB1F3E2359896E044A79CF7F316645E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral880C93990C8339019D7475FB24E361E6DEA9385F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC);
		s_Il2CppMethodInitialized = true;
	}
	String_t* G_B3_0 = NULL;
	{
		// return $"{(IsInline ? "Inline" : "Block")} Comment [{Value}]";
		bool L_0;
		L_0 = Comment_get_IsInline_m440EA3B231F4EA478370A2E877FDC7B6CB6CBEDC_inline(__this, NULL);
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		G_B3_0 = _stringLiteral3B9A4DA33EB1F3E2359896E044A79CF7F316645E;
		goto IL_0014;
	}

IL_000f:
	{
		G_B3_0 = _stringLiteral880C93990C8339019D7475FB24E361E6DEA9385F;
	}

IL_0014:
	{
		String_t* L_1;
		L_1 = Comment_get_Value_mEBB0458A9AFC00A9EC918B9225EF324F018DEB19_inline(__this, NULL);
		String_t* L_2;
		L_2 = String_Concat_mF8B69BE42B5C5ABCAD3C176FBBE3010E0815D65D(G_B3_0, _stringLiteral09F3589E3F129822338E12B67FB0990E4EF2F3DE, L_1, _stringLiteralE166C9564FBDE461738077E3B1B506525EB6ACCC, NULL);
		return L_2;
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
// System.Int32 YamlDotNet.Core.Events.DocumentEnd::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t DocumentEnd_get_NestingIncrease_m715AE11F6676C529F7DA41AD423EAE22804476A8 (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, const RuntimeMethod* method) 
{
	{
		// public override int NestingIncrease => -1;
		return (-1);
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.DocumentEnd::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t DocumentEnd_get_Type_mE94EB6AA62AF2525A2BFDC3514282912AB3D9FD0 (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.DocumentEnd;
		return (int32_t)(4);
	}
}
// System.Boolean YamlDotNet.Core.Events.DocumentEnd::get_IsImplicit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool DocumentEnd_get_IsImplicit_mE6262DD814A1E2DDF83E17BA52C6D3CE03BB3C6B (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentEnd::.ctor(System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd__ctor_m0E0429A6D37136BC7126D36963A7D8B136DB536A (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, bool ___isImplicit0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end2, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end2;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// this.IsImplicit = isImplicit;
		bool L_2 = ___isImplicit0;
		__this->___U3CIsImplicitU3Ek__BackingField_2 = L_2;
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentEnd::.ctor(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd__ctor_mAAC5BF85706AAA029D89A6AE59D453C5843B090B (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, bool ___isImplicit0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(isImplicit, Mark.Empty, Mark.Empty)
		bool L_0 = ___isImplicit0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		DocumentEnd__ctor_m0E0429A6D37136BC7126D36963A7D8B136DB536A(__this, L_0, L_1, L_2, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.DocumentEnd::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* DocumentEnd_ToString_m3A40903B9F4E43575E3DB9DBE45930FA081F9E64 (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB6B15A393A4B575B1D16E5ACDC604FC147869A2D);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"Document end [isImplicit = {IsImplicit}]";
		bool L_0;
		L_0 = DocumentEnd_get_IsImplicit_mE6262DD814A1E2DDF83E17BA52C6D3CE03BB3C6B_inline(__this, NULL);
		bool L_1 = L_0;
		RuntimeObject* L_2 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_1);
		String_t* L_3;
		L_3 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(_stringLiteralB6B15A393A4B575B1D16E5ACDC604FC147869A2D, L_2, NULL);
		return L_3;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentEnd::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentEnd_Accept_m61A8A1BF2360A2B55BA64EF0DFDF2FCD4F0B62DE (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* >::Invoke(4 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.DocumentEnd) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.DocumentStart::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t DocumentStart_get_NestingIncrease_mD6315857F411CF237004EF092CCC8BAC57D81B98 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	{
		// public override int NestingIncrease => 1;
		return 1;
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.DocumentStart::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t DocumentStart_get_Type_mEF0DCC99BD4008E570E6CE22F2EB66C5A1577A51 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.DocumentStart;
		return (int32_t)(3);
	}
}
// YamlDotNet.Core.TagDirectiveCollection YamlDotNet.Core.Events.DocumentStart::get_Tags()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* DocumentStart_get_Tags_mE5B2EA0C04BADBB81E095490DD9D6596F0E8E36E (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	{
		// public TagDirectiveCollection? Tags { get; }
		TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* L_0 = __this->___U3CTagsU3Ek__BackingField_2;
		return L_0;
	}
}
// YamlDotNet.Core.Tokens.VersionDirective YamlDotNet.Core.Events.DocumentStart::get_Version()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* DocumentStart_get_Version_m7701A7A0AF7D260F04AC3F29BF4AE1AE5AB86D9C (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	{
		// public VersionDirective? Version { get; }
		VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* L_0 = __this->___U3CVersionU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.DocumentStart::get_IsImplicit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool DocumentStart_get_IsImplicit_mCA5570162010D98F397D1DA3E39CA5B2E7662FAE (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_4;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentStart::.ctor(YamlDotNet.Core.Tokens.VersionDirective,YamlDotNet.Core.TagDirectiveCollection,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_m7CB2C3FE638C905FF1BC1600ECD6B4E73BBB7129 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* ___version0, TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* ___tags1, bool ___isImplicit2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start3, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end4, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start3;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end4;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// this.Version = version;
		VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* L_2 = ___version0;
		__this->___U3CVersionU3Ek__BackingField_3 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVersionU3Ek__BackingField_3), (void*)L_2);
		// this.Tags = tags;
		TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* L_3 = ___tags1;
		__this->___U3CTagsU3Ek__BackingField_2 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CTagsU3Ek__BackingField_2), (void*)L_3);
		// this.IsImplicit = isImplicit;
		bool L_4 = ___isImplicit2;
		__this->___U3CIsImplicitU3Ek__BackingField_4 = L_4;
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentStart::.ctor(YamlDotNet.Core.Tokens.VersionDirective,YamlDotNet.Core.TagDirectiveCollection,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_mDAA724F06C3B24BCC607C8DEB821895CA90E4D74 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* ___version0, TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* ___tags1, bool ___isImplicit2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(version, tags, isImplicit, Mark.Empty, Mark.Empty)
		VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* L_0 = ___version0;
		TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3* L_1 = ___tags1;
		bool L_2 = ___isImplicit2;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		DocumentStart__ctor_m7CB2C3FE638C905FF1BC1600ECD6B4E73BBB7129(__this, L_0, L_1, L_2, L_3, L_4, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_m63AFEEF9237958E8291BB8B194C41C9DA2D236C2 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : this(null, null, true, start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		DocumentStart__ctor_m7CB2C3FE638C905FF1BC1600ECD6B4E73BBB7129(__this, (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5*)NULL, (TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3*)NULL, (bool)1, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart__ctor_mEC87F89F6F47655BFA664784CDD5A46E7C32476E (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(null, null, true, Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		DocumentStart__ctor_m7CB2C3FE638C905FF1BC1600ECD6B4E73BBB7129(__this, (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5*)NULL, (TagDirectiveCollection_t481CA3EBA69A9C486F25C9E36B75CAD521CE91D3*)NULL, (bool)1, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.DocumentStart::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* DocumentStart_ToString_mA314C469ECA55BD1878FC8229FE381FD09C6747E (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral182468BD465AC6BC414B4B961F50F2B7CB9ECC26);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"Document start [isImplicit = {IsImplicit}]";
		bool L_0;
		L_0 = DocumentStart_get_IsImplicit_mCA5570162010D98F397D1DA3E39CA5B2E7662FAE_inline(__this, NULL);
		bool L_1 = L_0;
		RuntimeObject* L_2 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_1);
		String_t* L_3;
		L_3 = String_Format_m8C122B26BC5AA10E2550AECA16E57DAE10F07E30(_stringLiteral182468BD465AC6BC414B4B961F50F2B7CB9ECC26, L_2, NULL);
		return L_3;
	}
}
// System.Void YamlDotNet.Core.Events.DocumentStart::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DocumentStart_Accept_mD541FFF27F39CDA8626CAAC4EF5BEA2681EE2754 (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* >::Invoke(3 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.DocumentStart) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.MappingEnd::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t MappingEnd_get_NestingIncrease_mECF9839AEF8E908839603C96B8043932BC9B8B48 (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, const RuntimeMethod* method) 
{
	{
		// public override int NestingIncrease => -1;
		return (-1);
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.MappingEnd::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t MappingEnd_get_Type_mA0845E9350B241128D5C97A45D10FAD159BEA4DF (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.MappingEnd;
		return (int32_t)(((int32_t)10));
	}
}
// System.Void YamlDotNet.Core.Events.MappingEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingEnd__ctor_m1A034F25E943D253E6D929905356655143DD7FC6 (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.MappingEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingEnd__ctor_m99B689189E59C64059A3EDEE241762B6F5FC709C (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		MappingEnd__ctor_m1A034F25E943D253E6D929905356655143DD7FC6(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.MappingEnd::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* MappingEnd_ToString_mDB8738748AFF433BEF3060B1BA1739F682D41570 (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral93951CD1D927C264C666D33C8BE2CBD303C32D25);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return "Mapping end";
		return _stringLiteral93951CD1D927C264C666D33C8BE2CBD303C32D25;
	}
}
// System.Void YamlDotNet.Core.Events.MappingEnd::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingEnd_Accept_m340DC428004015D954295A6B8A9FBE84FD4F2AF0 (MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< MappingEnd_t938DD6C700C19AA89BF3CC6CD53DC7EDAEB64EA7* >::Invoke(9 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.MappingEnd) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.MappingStart::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t MappingStart_get_NestingIncrease_m58D860953360F74EADD8D22A3D2EA14A5F8619F5 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// public override int NestingIncrease => 1;
		return 1;
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.MappingStart::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t MappingStart_get_Type_mC13B3CE9EC91FBBA454DD13E1D45237F8310489E (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.MappingStart;
		return (int32_t)(((int32_t)9));
	}
}
// System.Boolean YamlDotNet.Core.Events.MappingStart::get_IsImplicit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool MappingStart_get_IsImplicit_mAF06D6F6F48C2BF8AE6DF163165367C3BC4D50A8 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_4;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.MappingStart::get_IsCanonical()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool MappingStart_get_IsCanonical_mC3D1663C467DBDE5CF7B5B7BB337B075F9E15AAF (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// public override bool IsCanonical => !IsImplicit;
		bool L_0;
		L_0 = MappingStart_get_IsImplicit_mAF06D6F6F48C2BF8AE6DF163165367C3BC4D50A8_inline(__this, NULL);
		return (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
	}
}
// YamlDotNet.Core.Events.MappingStyle YamlDotNet.Core.Events.MappingStart::get_Style()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t MappingStart_get_Style_mFC44BA401D40910D7FFAC1284C388620623D9134 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// public MappingStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_5;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.MappingStart::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.Boolean,YamlDotNet.Core.Events.MappingStyle,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingStart__ctor_mA4BCD7F9BF86C8CD4E29BA818D7F20F610D6FB18 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, bool ___isImplicit2, int32_t ___style3, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start4, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end5, const RuntimeMethod* method) 
{
	{
		// : base(anchor, tag, start, end)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ___start4;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___end5;
		NodeEvent__ctor_m845F93BEB38E4833ADFF5F9AC4DBA7A10857EFA7(__this, L_0, L_1, L_2, L_3, NULL);
		// this.IsImplicit = isImplicit;
		bool L_4 = ___isImplicit2;
		__this->___U3CIsImplicitU3Ek__BackingField_4 = L_4;
		// this.Style = style;
		int32_t L_5 = ___style3;
		__this->___U3CStyleU3Ek__BackingField_5 = L_5;
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.MappingStart::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.Boolean,YamlDotNet.Core.Events.MappingStyle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingStart__ctor_m5A83BC1181493C443E483B04E2477D8D645C3162 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, bool ___isImplicit2, int32_t ___style3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(anchor, tag, isImplicit, style, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		bool L_2 = ___isImplicit2;
		int32_t L_3 = ___style3;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_5 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		MappingStart__ctor_mA4BCD7F9BF86C8CD4E29BA818D7F20F610D6FB18(__this, L_0, L_1, L_2, L_3, L_4, L_5, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.MappingStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingStart__ctor_mDEA733FA5655C2579C59B21DD940159F3CD3FF08 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(AnchorName.Empty, TagName.Empty, true, MappingStyle.Any, Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ((AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_StaticFields*)il2cpp_codegen_static_fields_for(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var))->___Empty_0;
		il2cpp_codegen_runtime_class_init_inline(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ((TagName_t15CB29949E97FF28193B6F635B58928554CB5854_StaticFields*)il2cpp_codegen_static_fields_for(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var))->___Empty_0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		MappingStart__ctor_mA4BCD7F9BF86C8CD4E29BA818D7F20F610D6FB18(__this, L_0, L_1, (bool)1, 0, L_2, L_3, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.MappingStart::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* MappingStart_ToString_mB61A293C79B658B8C1F70B7D4C5D980A221DDFD8 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MappingStyle_t00D3BBFC7547E02AA45A0AB9A9109AF5C32D2440_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBD3547FA9A379720A33DAE68538E3DA25C3F6B67);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"Mapping start [anchor = {Anchor}, tag = {Tag}, isImplicit = {IsImplicit}, style = {Style}]";
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = L_0;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_2;
		L_2 = NodeEvent_get_Anchor_m173523F48C01AC3BBFBEBF80BA9C6E4F06EEADCA_inline(__this, NULL);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_3 = L_2;
		RuntimeObject* L_4 = Box(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var, &L_3);
		NullCheck(L_1);
		ArrayElementTypeCheck (L_1, L_4);
		(L_1)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_5 = L_1;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_6;
		L_6 = NodeEvent_get_Tag_m1F6D7FD3D70286B18499E8DB95A5CC2152ADA46E_inline(__this, NULL);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_7 = L_6;
		RuntimeObject* L_8 = Box(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var, &L_7);
		NullCheck(L_5);
		ArrayElementTypeCheck (L_5, L_8);
		(L_5)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_8);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_5;
		bool L_10;
		L_10 = MappingStart_get_IsImplicit_mAF06D6F6F48C2BF8AE6DF163165367C3BC4D50A8_inline(__this, NULL);
		bool L_11 = L_10;
		RuntimeObject* L_12 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_11);
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_12);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_12);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_13 = L_9;
		int32_t L_14;
		L_14 = MappingStart_get_Style_mFC44BA401D40910D7FFAC1284C388620623D9134_inline(__this, NULL);
		int32_t L_15 = L_14;
		RuntimeObject* L_16 = Box(MappingStyle_t00D3BBFC7547E02AA45A0AB9A9109AF5C32D2440_il2cpp_TypeInfo_var, &L_15);
		NullCheck(L_13);
		ArrayElementTypeCheck (L_13, L_16);
		(L_13)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_16);
		String_t* L_17;
		L_17 = String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55(_stringLiteralBD3547FA9A379720A33DAE68538E3DA25C3F6B67, L_13, NULL);
		return L_17;
	}
}
// System.Void YamlDotNet.Core.Events.MappingStart::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MappingStart_Accept_m959F2F6A3902C9762E53FF37A99E0052EF7C9C60 (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* >::Invoke(8 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.MappingStart) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// YamlDotNet.Core.AnchorName YamlDotNet.Core.Events.NodeEvent::get_Anchor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E NodeEvent_get_Anchor_m173523F48C01AC3BBFBEBF80BA9C6E4F06EEADCA (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, const RuntimeMethod* method) 
{
	{
		// public AnchorName Anchor { get; }
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = __this->___U3CAnchorU3Ek__BackingField_2;
		return L_0;
	}
}
// YamlDotNet.Core.TagName YamlDotNet.Core.Events.NodeEvent::get_Tag()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TagName_t15CB29949E97FF28193B6F635B58928554CB5854 NodeEvent_get_Tag_m1F6D7FD3D70286B18499E8DB95A5CC2152ADA46E (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, const RuntimeMethod* method) 
{
	{
		// public TagName Tag { get; }
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_0 = __this->___U3CTagU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.NodeEvent::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NodeEvent__ctor_m845F93BEB38E4833ADFF5F9AC4DBA7A10857EFA7 (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start2, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end3, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start2;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end3;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// this.Anchor = anchor;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_2 = ___anchor0;
		__this->___U3CAnchorU3Ek__BackingField_2 = L_2;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CAnchorU3Ek__BackingField_2))->___value_2), (void*)NULL);
		// this.Tag = tag;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_3 = ___tag1;
		__this->___U3CTagU3Ek__BackingField_3 = L_3;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___U3CTagU3Ek__BackingField_3))->___value_1), (void*)NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.NodeEvent::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NodeEvent__ctor_m27A4345BC50B69F961641FE6C8ECEF1A1454FD9F (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(anchor, tag, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		NodeEvent__ctor_m845F93BEB38E4833ADFF5F9AC4DBA7A10857EFA7(__this, L_0, L_1, L_2, L_3, NULL);
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
// System.Int32 YamlDotNet.Core.Events.ParsingEvent::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ParsingEvent_get_NestingIncrease_mA43867BF947DF4F01A1D369F1FF433598F0D7AC8 (ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* __this, const RuntimeMethod* method) 
{
	{
		// public virtual int NestingIncrease => 0;
		return 0;
	}
}
// YamlDotNet.Core.Mark YamlDotNet.Core.Events.ParsingEvent::get_Start()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* ParsingEvent_get_Start_mEA925A3B226C047EFE2411D879340DAF173DADF6 (ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* __this, const RuntimeMethod* method) 
{
	{
		// public Mark Start { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CStartU3Ek__BackingField_0;
		return L_0;
	}
}
// YamlDotNet.Core.Mark YamlDotNet.Core.Events.ParsingEvent::get_End()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* ParsingEvent_get_End_mAAEA6809412D647ED26B9A2F31F49E81FAB98F79 (ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* __this, const RuntimeMethod* method) 
{
	{
		// public Mark End { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CEndU3Ek__BackingField_1;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.ParsingEvent::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4 (ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B2_0 = NULL;
	ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* G_B2_1 = NULL;
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B1_0 = NULL;
	ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* G_B1_1 = NULL;
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B4_0 = NULL;
	ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* G_B4_1 = NULL;
	Mark_t950DC067D3EC830050595AD3F189554215D04694* G_B3_0 = NULL;
	ParsingEvent_tE58420F975B5631C8D828FAEAF925C00B889570E* G_B3_1 = NULL;
	{
		// internal ParsingEvent(Mark start, Mark end)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// this.Start = start ?? throw new System.ArgumentNullException(nameof(start));
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = L_0;
		G_B1_0 = L_1;
		G_B1_1 = __this;
		if (L_1)
		{
			G_B2_0 = L_1;
			G_B2_1 = __this;
			goto IL_0017;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_2 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_2);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2AD47C03F7A83F82E3B2ADFE8A60F1727FD3BEFD)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4_RuntimeMethod_var)));
	}

IL_0017:
	{
		NullCheck(G_B2_1);
		G_B2_1->___U3CStartU3Ek__BackingField_0 = G_B2_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B2_1->___U3CStartU3Ek__BackingField_0), (void*)G_B2_0);
		// this.End = end ?? throw new System.ArgumentNullException(nameof(end));
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___end1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = L_3;
		G_B3_0 = L_4;
		G_B3_1 = __this;
		if (L_4)
		{
			G_B4_0 = L_4;
			G_B4_1 = __this;
			goto IL_002d;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_5 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA2F4AC9DD8E1FAC5257E5F7BA5EE1C7C7E5F7AB1)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4_RuntimeMethod_var)));
	}

IL_002d:
	{
		NullCheck(G_B4_1);
		G_B4_1->___U3CEndU3Ek__BackingField_1 = G_B4_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B4_1->___U3CEndU3Ek__BackingField_1), (void*)G_B4_0);
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
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.Scalar::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Scalar_get_Type_mE4CFABD04061D371D5425FF255C54E4076913685 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.Scalar;
		return (int32_t)(6);
	}
}
// System.String YamlDotNet.Core.Events.Scalar::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Scalar_get_Value_mA2941814EF2497D45943217ABA20277C615097A2 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_4;
		return L_0;
	}
}
// YamlDotNet.Core.ScalarStyle YamlDotNet.Core.Events.Scalar::get_Style()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Scalar_get_Style_m8AD3F9689F11B54847605E21257A0832372B3B99 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public ScalarStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_5;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.Scalar::get_IsPlainImplicit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Scalar_get_IsPlainImplicit_m866B963306A5FE20C34141040E8023B0328C5E34 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsPlainImplicit { get; }
		bool L_0 = __this->___U3CIsPlainImplicitU3Ek__BackingField_6;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.Scalar::get_IsQuotedImplicit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Scalar_get_IsQuotedImplicit_mB7E1436613709725349C3C5755D9D63F0FEE81F0 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsQuotedImplicit { get; }
		bool L_0 = __this->___U3CIsQuotedImplicitU3Ek__BackingField_7;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.Scalar::get_IsCanonical()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Scalar_get_IsCanonical_m2E1AD1EF70D0C349C02CCBFA2F41CE9E92B529A2 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public override bool IsCanonical => !IsPlainImplicit && !IsQuotedImplicit;
		bool L_0;
		L_0 = Scalar_get_IsPlainImplicit_m866B963306A5FE20C34141040E8023B0328C5E34_inline(__this, NULL);
		if (L_0)
		{
			goto IL_0012;
		}
	}
	{
		bool L_1;
		L_1 = Scalar_get_IsQuotedImplicit_mB7E1436613709725349C3C5755D9D63F0FEE81F0_inline(__this, NULL);
		return (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
	}

IL_0012:
	{
		return (bool)0;
	}
}
// System.Void YamlDotNet.Core.Events.Scalar::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.String,YamlDotNet.Core.ScalarStyle,System.Boolean,System.Boolean,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m8949526F8BC9C06B576AD7ED7EE84B179E4B1377 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, String_t* ___value2, int32_t ___style3, bool ___isPlainImplicit4, bool ___isQuotedImplicit5, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start6, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end7, const RuntimeMethod* method) 
{
	{
		// : base(anchor, tag, start, end)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ___start6;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___end7;
		NodeEvent__ctor_m845F93BEB38E4833ADFF5F9AC4DBA7A10857EFA7(__this, L_0, L_1, L_2, L_3, NULL);
		// this.Value = value;
		String_t* L_4 = ___value2;
		__this->___U3CValueU3Ek__BackingField_4 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CValueU3Ek__BackingField_4), (void*)L_4);
		// this.Style = style;
		int32_t L_5 = ___style3;
		__this->___U3CStyleU3Ek__BackingField_5 = L_5;
		// this.IsPlainImplicit = isPlainImplicit;
		bool L_6 = ___isPlainImplicit4;
		__this->___U3CIsPlainImplicitU3Ek__BackingField_6 = L_6;
		// this.IsQuotedImplicit = isQuotedImplicit;
		bool L_7 = ___isQuotedImplicit5;
		__this->___U3CIsQuotedImplicitU3Ek__BackingField_7 = L_7;
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.Scalar::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.String,YamlDotNet.Core.ScalarStyle,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m6D025FEF62A18C1BBDDE080E447319C41866DFB5 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, String_t* ___value2, int32_t ___style3, bool ___isPlainImplicit4, bool ___isQuotedImplicit5, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(anchor, tag, value, style, isPlainImplicit, isQuotedImplicit, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		String_t* L_2 = ___value2;
		int32_t L_3 = ___style3;
		bool L_4 = ___isPlainImplicit4;
		bool L_5 = ___isQuotedImplicit5;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_6 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_7 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Scalar__ctor_m8949526F8BC9C06B576AD7ED7EE84B179E4B1377(__this, L_0, L_1, L_2, L_3, L_4, L_5, L_6, L_7, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.Scalar::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_mF7D2E1C897790F07E3B0A731B18FBD4B118146E0 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, String_t* ___value0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(AnchorName.Empty, TagName.Empty, value, ScalarStyle.Any, true, true, Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ((AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_StaticFields*)il2cpp_codegen_static_fields_for(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var))->___Empty_0;
		il2cpp_codegen_runtime_class_init_inline(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ((TagName_t15CB29949E97FF28193B6F635B58928554CB5854_StaticFields*)il2cpp_codegen_static_fields_for(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var))->___Empty_0;
		String_t* L_2 = ___value0;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Scalar__ctor_m8949526F8BC9C06B576AD7ED7EE84B179E4B1377(__this, L_0, L_1, L_2, 0, (bool)1, (bool)1, L_3, L_4, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.Scalar::.ctor(YamlDotNet.Core.TagName,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m0E159234071998E9E7FE88BF8D2632722C74ACDC (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag0, String_t* ___value1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(AnchorName.Empty, tag, value, ScalarStyle.Any, true, true, Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ((AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_StaticFields*)il2cpp_codegen_static_fields_for(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var))->___Empty_0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag0;
		String_t* L_2 = ___value1;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Scalar__ctor_m8949526F8BC9C06B576AD7ED7EE84B179E4B1377(__this, L_0, L_1, L_2, 0, (bool)1, (bool)1, L_3, L_4, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.Scalar::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar__ctor_m9522A7C016AEBEE44F3348DB35CF7DFA0A557BB2 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, String_t* ___value2, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(anchor, tag, value, ScalarStyle.Any, true, true, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		String_t* L_2 = ___value2;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Scalar__ctor_m8949526F8BC9C06B576AD7ED7EE84B179E4B1377(__this, L_0, L_1, L_2, 0, (bool)1, (bool)1, L_3, L_4, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.Scalar::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Scalar_ToString_m47BF3C689F1C577CDF84AA73ECAED2999E81B808 (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ScalarStyle_t8B9E83D82F8FD9DB5079F76D03EBB143BFC4D0A2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral55C7066FE389C4DB122F633D727159777AFB4BBB);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"Scalar [anchor = {Anchor}, tag = {Tag}, value = {Value}, style = {Style}, isPlainImplicit = {IsPlainImplicit}, isQuotedImplicit = {IsQuotedImplicit}]";
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)6);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = L_0;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_2;
		L_2 = NodeEvent_get_Anchor_m173523F48C01AC3BBFBEBF80BA9C6E4F06EEADCA_inline(__this, NULL);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_3 = L_2;
		RuntimeObject* L_4 = Box(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var, &L_3);
		NullCheck(L_1);
		ArrayElementTypeCheck (L_1, L_4);
		(L_1)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_5 = L_1;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_6;
		L_6 = NodeEvent_get_Tag_m1F6D7FD3D70286B18499E8DB95A5CC2152ADA46E_inline(__this, NULL);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_7 = L_6;
		RuntimeObject* L_8 = Box(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var, &L_7);
		NullCheck(L_5);
		ArrayElementTypeCheck (L_5, L_8);
		(L_5)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_8);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_5;
		String_t* L_10;
		L_10 = Scalar_get_Value_mA2941814EF2497D45943217ABA20277C615097A2_inline(__this, NULL);
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_10);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_10);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_11 = L_9;
		int32_t L_12;
		L_12 = Scalar_get_Style_m8AD3F9689F11B54847605E21257A0832372B3B99_inline(__this, NULL);
		int32_t L_13 = L_12;
		RuntimeObject* L_14 = Box(ScalarStyle_t8B9E83D82F8FD9DB5079F76D03EBB143BFC4D0A2_il2cpp_TypeInfo_var, &L_13);
		NullCheck(L_11);
		ArrayElementTypeCheck (L_11, L_14);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_14);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_15 = L_11;
		bool L_16;
		L_16 = Scalar_get_IsPlainImplicit_m866B963306A5FE20C34141040E8023B0328C5E34_inline(__this, NULL);
		bool L_17 = L_16;
		RuntimeObject* L_18 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_17);
		NullCheck(L_15);
		ArrayElementTypeCheck (L_15, L_18);
		(L_15)->SetAt(static_cast<il2cpp_array_size_t>(4), (RuntimeObject*)L_18);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_19 = L_15;
		bool L_20;
		L_20 = Scalar_get_IsQuotedImplicit_mB7E1436613709725349C3C5755D9D63F0FEE81F0_inline(__this, NULL);
		bool L_21 = L_20;
		RuntimeObject* L_22 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_21);
		NullCheck(L_19);
		ArrayElementTypeCheck (L_19, L_22);
		(L_19)->SetAt(static_cast<il2cpp_array_size_t>(5), (RuntimeObject*)L_22);
		String_t* L_23;
		L_23 = String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55(_stringLiteral55C7066FE389C4DB122F633D727159777AFB4BBB, L_19, NULL);
		return L_23;
	}
}
// System.Void YamlDotNet.Core.Events.Scalar::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Scalar_Accept_m15B31E721E504CDC6E706CEB3D45A086D7B9529D (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* >::Invoke(5 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.Scalar) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.SequenceEnd::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SequenceEnd_get_NestingIncrease_m657A610BF96DF406264B2C42FF1B242A535569A8 (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, const RuntimeMethod* method) 
{
	{
		// public override int NestingIncrease => -1;
		return (-1);
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.SequenceEnd::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SequenceEnd_get_Type_mAC7BC00E70177DB59D7FB87A8756F01AA1C0CD7E (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.SequenceEnd;
		return (int32_t)(8);
	}
}
// System.Void YamlDotNet.Core.Events.SequenceEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceEnd__ctor_m7A72583BF62589EC220C99930653C5799699D7AC (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.SequenceEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceEnd__ctor_m1AB625499FFDDF32DDF9BDBB70758D152FC4C84B (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		SequenceEnd__ctor_m7A72583BF62589EC220C99930653C5799699D7AC(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.SequenceEnd::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* SequenceEnd_ToString_m6586BAA4D265CAE3839595D67ACDFA32595F0C9F (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6D8BCD93E9F5A9C7C071EB22AC111507D9F90887);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return "Sequence end";
		return _stringLiteral6D8BCD93E9F5A9C7C071EB22AC111507D9F90887;
	}
}
// System.Void YamlDotNet.Core.Events.SequenceEnd::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceEnd_Accept_m3D9E367B7B2FC4B6A59BD16C58C1642E947B9215 (SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< SequenceEnd_t99855797E005B57FBC865501E44A9C82F3E6A0E6* >::Invoke(7 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.SequenceEnd) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.SequenceStart::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SequenceStart_get_NestingIncrease_m7EA8112EC0D45470590B4194875219084D487E23 (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// public override int NestingIncrease => 1;
		return 1;
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.SequenceStart::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SequenceStart_get_Type_m88E86040EF80AC20B83EBBB70BBD73CDB5F4A5A3 (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// internal override EventType Type => EventType.SequenceStart;
		return (int32_t)(7);
	}
}
// System.Boolean YamlDotNet.Core.Events.SequenceStart::get_IsImplicit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SequenceStart_get_IsImplicit_m77C7D3FC1CF334C4116C74A9881E660B69119863 (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_4;
		return L_0;
	}
}
// System.Boolean YamlDotNet.Core.Events.SequenceStart::get_IsCanonical()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SequenceStart_get_IsCanonical_m1A44AA6DA00690F2EE6623F4061312060B53281D (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// public override bool IsCanonical => !IsImplicit;
		bool L_0;
		L_0 = SequenceStart_get_IsImplicit_m77C7D3FC1CF334C4116C74A9881E660B69119863_inline(__this, NULL);
		return (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
	}
}
// YamlDotNet.Core.Events.SequenceStyle YamlDotNet.Core.Events.SequenceStart::get_Style()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t SequenceStart_get_Style_mC8F35040576661331BC6B7B4D5EAA3D3BA4CC8BC (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// public SequenceStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_5;
		return L_0;
	}
}
// System.Void YamlDotNet.Core.Events.SequenceStart::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.Boolean,YamlDotNet.Core.Events.SequenceStyle,YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceStart__ctor_mA3B2926756626F57678A7C30277DDAD2324E987A (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, bool ___isImplicit2, int32_t ___style3, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start4, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end5, const RuntimeMethod* method) 
{
	{
		// : base(anchor, tag, start, end)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_2 = ___start4;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_3 = ___end5;
		NodeEvent__ctor_m845F93BEB38E4833ADFF5F9AC4DBA7A10857EFA7(__this, L_0, L_1, L_2, L_3, NULL);
		// this.IsImplicit = isImplicit;
		bool L_4 = ___isImplicit2;
		__this->___U3CIsImplicitU3Ek__BackingField_4 = L_4;
		// this.Style = style;
		int32_t L_5 = ___style3;
		__this->___U3CStyleU3Ek__BackingField_5 = L_5;
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.SequenceStart::.ctor(YamlDotNet.Core.AnchorName,YamlDotNet.Core.TagName,System.Boolean,YamlDotNet.Core.Events.SequenceStyle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceStart__ctor_m98541DF25E3E51488E94B0889D4B7BF973BC6875 (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E ___anchor0, TagName_t15CB29949E97FF28193B6F635B58928554CB5854 ___tag1, bool ___isImplicit2, int32_t ___style3, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(anchor, tag, isImplicit, style, Mark.Empty, Mark.Empty)
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = ___anchor0;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_1 = ___tag1;
		bool L_2 = ___isImplicit2;
		int32_t L_3 = ___style3;
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_4 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_5 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		SequenceStart__ctor_mA3B2926756626F57678A7C30277DDAD2324E987A(__this, L_0, L_1, L_2, L_3, L_4, L_5, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.SequenceStart::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* SequenceStart_ToString_m6727087BD9FFB6E073448169A49E66DECBBB0A03 (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SequenceStyle_t9924C8E70E226F6A69C95F03D6CAD13804BB9D02_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral171FB2C5A9D880AA85056C99CA54469A36B3AE62);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return $"Sequence start [anchor = {Anchor}, tag = {Tag}, isImplicit = {IsImplicit}, style = {Style}]";
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = L_0;
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_2;
		L_2 = NodeEvent_get_Anchor_m173523F48C01AC3BBFBEBF80BA9C6E4F06EEADCA_inline(__this, NULL);
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_3 = L_2;
		RuntimeObject* L_4 = Box(AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E_il2cpp_TypeInfo_var, &L_3);
		NullCheck(L_1);
		ArrayElementTypeCheck (L_1, L_4);
		(L_1)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_5 = L_1;
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_6;
		L_6 = NodeEvent_get_Tag_m1F6D7FD3D70286B18499E8DB95A5CC2152ADA46E_inline(__this, NULL);
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_7 = L_6;
		RuntimeObject* L_8 = Box(TagName_t15CB29949E97FF28193B6F635B58928554CB5854_il2cpp_TypeInfo_var, &L_7);
		NullCheck(L_5);
		ArrayElementTypeCheck (L_5, L_8);
		(L_5)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_8);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = L_5;
		bool L_10;
		L_10 = SequenceStart_get_IsImplicit_m77C7D3FC1CF334C4116C74A9881E660B69119863_inline(__this, NULL);
		bool L_11 = L_10;
		RuntimeObject* L_12 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_11);
		NullCheck(L_9);
		ArrayElementTypeCheck (L_9, L_12);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(2), (RuntimeObject*)L_12);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_13 = L_9;
		int32_t L_14;
		L_14 = SequenceStart_get_Style_mC8F35040576661331BC6B7B4D5EAA3D3BA4CC8BC_inline(__this, NULL);
		int32_t L_15 = L_14;
		RuntimeObject* L_16 = Box(SequenceStyle_t9924C8E70E226F6A69C95F03D6CAD13804BB9D02_il2cpp_TypeInfo_var, &L_15);
		NullCheck(L_13);
		ArrayElementTypeCheck (L_13, L_16);
		(L_13)->SetAt(static_cast<il2cpp_array_size_t>(3), (RuntimeObject*)L_16);
		String_t* L_17;
		L_17 = String_Format_m74FC0A1259DFA02F3DF6538FC7F3ACF3E1AF0C55(_stringLiteral171FB2C5A9D880AA85056C99CA54469A36B3AE62, L_13, NULL);
		return L_17;
	}
}
// System.Void YamlDotNet.Core.Events.SequenceStart::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SequenceStart_Accept_mE9B528EBC2CEFACEA0871792E32A7F3A3E0F1115 (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* >::Invoke(6 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.SequenceStart) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.StreamEnd::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StreamEnd_get_NestingIncrease_mC94EB75001ACDFC85FC454EDE8599E33F569B261 (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, const RuntimeMethod* method) 
{
	{
		// return -1;
		return (-1);
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.StreamEnd::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StreamEnd_get_Type_m767F0992D110F57C34C4CE48354698B7FBE0E5EB (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, const RuntimeMethod* method) 
{
	{
		// return EventType.StreamEnd;
		return (int32_t)(2);
	}
}
// System.Void YamlDotNet.Core.Events.StreamEnd::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd__ctor_mE4AD31D5A3096F1634B6E3AEFB6DD37555D55113 (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.StreamEnd::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd__ctor_m0C655F15BAFCA99FB7003556D9BDB7A61E18E286 (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		StreamEnd__ctor_mE4AD31D5A3096F1634B6E3AEFB6DD37555D55113(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.StreamEnd::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* StreamEnd_ToString_mC469891F36626B2BFCDD1AD2867998400F970C85 (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBFDE951663C61703B7702ACABC6C1A2860B82FF2);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return "Stream end";
		return _stringLiteralBFDE951663C61703B7702ACABC6C1A2860B82FF2;
	}
}
// System.Void YamlDotNet.Core.Events.StreamEnd::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamEnd_Accept_m97C0AD683B879F0E4FBB119BA17D7A73B936FAB6 (StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< StreamEnd_t684BAC1718208BB0A7D33AF85A7CCCBC83611F8F* >::Invoke(2 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.StreamEnd) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
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
// System.Int32 YamlDotNet.Core.Events.StreamStart::get_NestingIncrease()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StreamStart_get_NestingIncrease_m76C52CA09D6A6131BBB15FD8B6E415DF7B9E507E (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, const RuntimeMethod* method) 
{
	{
		// return 1;
		return 1;
	}
}
// YamlDotNet.Core.Events.EventType YamlDotNet.Core.Events.StreamStart::get_Type()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t StreamStart_get_Type_m78AF60245860E9DE97DF73E9D0C45F16F7C18B11 (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, const RuntimeMethod* method) 
{
	{
		// return EventType.StreamStart;
		return (int32_t)(1);
	}
}
// System.Void YamlDotNet.Core.Events.StreamStart::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart__ctor_m002E6583E5FDAEDA56B0374695B49E5086DD2FBE (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// : this(Mark.Empty, Mark.Empty)
		il2cpp_codegen_runtime_class_init_inline(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var);
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ((Mark_t950DC067D3EC830050595AD3F189554215D04694_StaticFields*)il2cpp_codegen_static_fields_for(Mark_t950DC067D3EC830050595AD3F189554215D04694_il2cpp_TypeInfo_var))->___Empty_0;
		StreamStart__ctor_m56587E78E790DA2E04076054189FA91A70A8C668(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.Void YamlDotNet.Core.Events.StreamStart::.ctor(YamlDotNet.Core.Mark,YamlDotNet.Core.Mark)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart__ctor_m56587E78E790DA2E04076054189FA91A70A8C668 (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___start0, Mark_t950DC067D3EC830050595AD3F189554215D04694* ___end1, const RuntimeMethod* method) 
{
	{
		// : base(start, end)
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = ___start0;
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_1 = ___end1;
		ParsingEvent__ctor_m5AFCAA0B753E61FDFE3ED58434A36D603D721EC4(__this, L_0, L_1, NULL);
		// }
		return;
	}
}
// System.String YamlDotNet.Core.Events.StreamStart::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* StreamStart_ToString_m1446E0DB2F2D0053509084897914D33F92A6B176 (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5378C16FB75C6D58FCF9AD334CF92DE0F2E4F752);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return "Stream start";
		return _stringLiteral5378C16FB75C6D58FCF9AD334CF92DE0F2E4F752;
	}
}
// System.Void YamlDotNet.Core.Events.StreamStart::Accept(YamlDotNet.Core.Events.IParsingEventVisitor)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StreamStart_Accept_m11FB506B7CE29C7E6E90FF8B8FEF5B541EC145A3 (StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* __this, RuntimeObject* ___visitor0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// visitor.Visit(this);
		RuntimeObject* L_0 = ___visitor0;
		NullCheck(L_0);
		InterfaceActionInvoker1< StreamStart_t3E7060B1BD6A42973E6540889CA212BBB121AF18* >::Invoke(1 /* System.Void YamlDotNet.Core.Events.IParsingEventVisitor::Visit(YamlDotNet.Core.Events.StreamStart) */, IParsingEventVisitor_t6D0957321F4EC362C5182D4E3A26FD7F28EBC8DA_il2cpp_TypeInfo_var, L_0, __this);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SimpleKey_set_IsPossible_m9D3BF8BE359A926B73C230A571AB700024E6B161_inline (SimpleKey_tAA9873A8DBA17FC74C727FEFF969817FED10B6E5* __this, bool ___value0, const RuntimeMethod* method) 
{
	{
		// public bool IsPossible { get; private set; }
		bool L_0 = ___value0;
		__this->___U3CIsPossibleU3Ek__BackingField_1 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Cursor_get_Index_m80BCD59F059558A7AE2D9F6E818E5063DD2A3DC8_inline (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) 
{
	{
		// public int Index { get; private set; }
		int32_t L_0 = __this->___U3CIndexU3Ek__BackingField_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Cursor_get_Line_m4C41A923C959EAEF29D2D0A8C12509FD7FCCEE88_inline (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) 
{
	{
		// public int Line { get; private set; }
		int32_t L_0 = __this->___U3CLineU3Ek__BackingField_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Cursor_get_LineOffset_m8683346CC221F6CE809AABFE6E5677F035AC5AF6_inline (Cursor_t0FE2C47986399D5B11A6570F7A019649AC01CA9A* __this, const RuntimeMethod* method) 
{
	{
		// public int LineOffset { get; private set; }
		int32_t L_0 = __this->___U3CLineOffsetU3Ek__BackingField_2;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t StringLookAheadBuffer_get_Position_m712487E8FF6199BF4E64713EADF7BD41001A2749_inline (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, const RuntimeMethod* method) 
{
	{
		// public int Position { get; private set; }
		int32_t L_0 = __this->___U3CPositionU3Ek__BackingField_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void StringLookAheadBuffer_set_Position_mC8275781BC014A2FBB31A0001D6EB3FE0CBC6D5D_inline (StringLookAheadBuffer_tD214FF6EC4ABC3A75B8B794FF1972D1D69317A7E* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		// public int Position { get; private set; }
		int32_t L_0 = ___value0;
		__this->___U3CPositionU3Ek__BackingField_1 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* TagDirective_get_Handle_m88B729D6ADAA19042CD9F7455C1CEBFEF4EED047_inline (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) 
{
	{
		// public string Handle { get; }
		String_t* L_0 = __this->___U3CHandleU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Version_get_Major_mB872E778C2275DFD3D1036087E06600DD5DECA68_inline (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) 
{
	{
		// public int Major { get; }
		int32_t L_0 = __this->___U3CMajorU3Ek__BackingField_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Version_get_Minor_m7C1B9806936F9D9662B04D58E3821E0583C7F39D_inline (Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* __this, const RuntimeMethod* method) 
{
	{
		// public int Minor { get; }
		int32_t L_0 = __this->___U3CMinorU3Ek__BackingField_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* YamlException_get_Start_mB634C9460DF018B29F7CC07A809EFA2783CEC968_inline (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) 
{
	{
		// public Mark Start { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CStartU3Ek__BackingField_18;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Mark_t950DC067D3EC830050595AD3F189554215D04694* YamlException_get_End_mB22BEA3B1C0AFA79DD944184421B4EAC202CA9A2_inline (YamlException_t2C01321FA41830189C2B0A275952FEF9FEC9C8B2* __this, const RuntimeMethod* method) 
{
	{
		// public Mark End { get; }
		Mark_t950DC067D3EC830050595AD3F189554215D04694* L_0 = __this->___U3CEndU3Ek__BackingField_19;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* TagDirective_get_Prefix_mA40573A7254C97AC90A67D685BB96FA8AEAF2344_inline (TagDirective_t4409B04F388D4B52826D69ACC99F67D08166F061* __this, const RuntimeMethod* method) 
{
	{
		// public string Prefix { get; }
		String_t* L_0 = __this->___U3CPrefixU3Ek__BackingField_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* VersionDirective_get_Version_mA87382DDF754E55F0FC4261A154017C4B8E1F34F_inline (VersionDirective_tA2D5B7E5BAE8CC67A93A5F981EF228413EE95DC5* __this, const RuntimeMethod* method) 
{
	{
		// public Version Version { get; }
		Version_t5D5264A8ABF0DFBE6386270CE34A6238698E0AE3* L_0 = __this->___U3CVersionU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E AnchorAlias_get_Value_m6EE2E9089D04C5B3263AAB9E75C3770E10E5C8ED_inline (AnchorAlias_tFE7F49EC06BDB62A98D2329ACEBB5B37F9E46151* __this, const RuntimeMethod* method) 
{
	{
		// public AnchorName Value { get; }
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Comment_get_IsInline_m440EA3B231F4EA478370A2E877FDC7B6CB6CBEDC_inline (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsInline { get; }
		bool L_0 = __this->___U3CIsInlineU3Ek__BackingField_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* Comment_get_Value_mEBB0458A9AFC00A9EC918B9225EF324F018DEB19_inline (Comment_tFBCC1272F9B8A83CDD676D9A88E280CBD8B65820* __this, const RuntimeMethod* method) 
{
	{
		// public string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DocumentEnd_get_IsImplicit_mE6262DD814A1E2DDF83E17BA52C6D3CE03BB3C6B_inline (DocumentEnd_t8BD191B0DEE29A6D96BFEEA95EFEE5A6BCAA30D8* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DocumentStart_get_IsImplicit_mCA5570162010D98F397D1DA3E39CA5B2E7662FAE_inline (DocumentStart_t6BD38591480EB8184ACF7EEC1A8F965B117FF73F* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool MappingStart_get_IsImplicit_mAF06D6F6F48C2BF8AE6DF163165367C3BC4D50A8_inline (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E NodeEvent_get_Anchor_m173523F48C01AC3BBFBEBF80BA9C6E4F06EEADCA_inline (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, const RuntimeMethod* method) 
{
	{
		// public AnchorName Anchor { get; }
		AnchorName_t94EA697EB10B53ECF53C1E86750105E5BA43A67E L_0 = __this->___U3CAnchorU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR TagName_t15CB29949E97FF28193B6F635B58928554CB5854 NodeEvent_get_Tag_m1F6D7FD3D70286B18499E8DB95A5CC2152ADA46E_inline (NodeEvent_t034E58CD2B198ADC5AA764B5A94844F799DEDF53* __this, const RuntimeMethod* method) 
{
	{
		// public TagName Tag { get; }
		TagName_t15CB29949E97FF28193B6F635B58928554CB5854 L_0 = __this->___U3CTagU3Ek__BackingField_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t MappingStart_get_Style_mFC44BA401D40910D7FFAC1284C388620623D9134_inline (MappingStart_t1D9283C03950FF661E73AF19DC5C47C3234E7472* __this, const RuntimeMethod* method) 
{
	{
		// public MappingStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_5;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Scalar_get_IsPlainImplicit_m866B963306A5FE20C34141040E8023B0328C5E34_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsPlainImplicit { get; }
		bool L_0 = __this->___U3CIsPlainImplicitU3Ek__BackingField_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Scalar_get_IsQuotedImplicit_mB7E1436613709725349C3C5755D9D63F0FEE81F0_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsQuotedImplicit { get; }
		bool L_0 = __this->___U3CIsQuotedImplicitU3Ek__BackingField_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* Scalar_get_Value_mA2941814EF2497D45943217ABA20277C615097A2_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public string Value { get; }
		String_t* L_0 = __this->___U3CValueU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Scalar_get_Style_m8AD3F9689F11B54847605E21257A0832372B3B99_inline (Scalar_tF3D7C43C8E9AAC96A22B5091CE7DCCDCE4F9945A* __this, const RuntimeMethod* method) 
{
	{
		// public ScalarStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_5;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool SequenceStart_get_IsImplicit_m77C7D3FC1CF334C4116C74A9881E660B69119863_inline (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsImplicit { get; }
		bool L_0 = __this->___U3CIsImplicitU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t SequenceStart_get_Style_mC8F35040576661331BC6B7B4D5EAA3D3BA4CC8BC_inline (SequenceStart_t0A3A47CCEC6FEFB715BEDA4117948F9F7127AF5C* __this, const RuntimeMethod* method) 
{
	{
		// public SequenceStyle Style { get; }
		int32_t L_0 = __this->___U3CStyleU3Ek__BackingField_5;
		return L_0;
	}
}
