using System;
using Foundation;
using CoreBluetooth;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace Movesense
{
	[Static]
	partial interface Constants
	{
        /// <summary>
        /// Morten 2024.04.16: having theese in here made linker failing with the error below
		/// This seems to be an issue with new .net sdk or workload for MAUI worked fine around 28.3 (in pipeline) and stoped working after updating VS4Mac
		/// that updated workload and sdk at 14.4 but these updates was manualy kept back.
        /// </summary>
        /// /usr/local/share/dotnet/packs/Microsoft.iOS.Sdk/17.2.8053/targets/Xamarin.Shared.Sdk.targets(3,3): Error: clang++ exited with code 1:
		/// Undefined symbols for architecture arm64:
		/// "_MovesenseApiVersionNumber", referenced from:
		/// -exported_symbol[s_list] command line option
		/// "_MovesenseApiVersionString", referenced from:
		/// -exported_symbol[s_list] command line option
		/// ld: symbol(s) not found for architecture arm64
		/// clang: error: linker command failed with exit code 1 (use -v to see invocation) (NothingApp3)

        // extern double MovesenseApiVersionNumber;
        //[Field("MovesenseApiVersionNumber", "__Internal")]
        //double MovesenseApiVersionNumber { get; }

        // extern const unsigned char[] MovesenseApiVersionString;
        //[Field("MovesenseApiVersionString", "__Internal")]
        //IntPtr MovesenseApiVersionString { get; }

        // extern NSString *const _Nonnull MovesenseServiceUUID;
        [Field ("MovesenseServiceUUID", "__Internal")]
		NSString MovesenseServiceUUID { get; }
	}

	// @interface MDSResponse : NSObject
	[BaseType (typeof(NSObject))]
	public interface MDSResponse
	{
		// @property (readonly, nonatomic) NSDictionary * _Nonnull header;
		[Export ("header")]
		NSDictionary Header { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull bodyData;
		[Export ("bodyData")]
		NSData BodyData { get; }

		// @property (readonly, nonatomic) NSDictionary * _Nullable bodyDictionary;
		[NullAllowed, Export ("bodyDictionary")]
		NSDictionary BodyDictionary { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull contentType;
		[Export ("contentType")]
		string ContentType { get; }

		// @property (readonly, nonatomic) NSInteger statusCode;
		[Export ("statusCode")]
		nint StatusCode { get; }

		// @property (readonly, nonatomic) MDSResponseMethod method;
		[Export ("method")]
        MDSResponseMethod Method { get; }

		// -(NSData * _Nonnull)getStreamData;
		[Export ("getStreamData")]
		NSData StreamData { get; }
	}

	// typedef void (^MDSResponseBlock)(MDSResponse * _Nonnull);
    public delegate void MDSResponseBlock (MDSResponse arg0);

	// @interface MDSEvent : NSObject
	[BaseType (typeof(NSObject))]
    public interface MDSEvent
	{
		// @property (readonly, nonatomic) NSDictionary * _Nonnull header;
		[Export ("header")]
		NSDictionary Header { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull bodyData;
		[Export ("bodyData")]
		NSData BodyData { get; }

		// @property (readonly, nonatomic) NSDictionary * _Nullable bodyDictionary;
		[NullAllowed, Export ("bodyDictionary")]
		NSDictionary BodyDictionary { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull contentType;
		[Export ("contentType")]
		string ContentType { get; }
	}

	// typedef void (^MDSEventBlock)(MDSEvent * _Nonnull);
    public delegate void MDSEventBlock (MDSEvent arg0);

	// @interface MDSTunnelResponse : NSObject
	[BaseType (typeof(NSObject))]
    public interface MDSTunnelResponse
	{
		// @property (readonly, nonatomic) NSInteger status;
		[Export ("status")]
		nint Status { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull response;
		[Export ("response")]
		string Response { get; }

		// -(instancetype _Nonnull)initWithStatus:(NSInteger)status response:(NSString * _Nonnull)response;
		[Export ("initWithStatus:response:")]
        NativeHandle Constructor (nint status, string response);
	}

	// @interface MDSTunnelRequest : NSObject
	[BaseType (typeof(NSObject))]
    public interface MDSTunnelRequest
	{
		// @property (readonly, nonatomic) NSDictionary * _Nonnull header;
		[Export ("header")]
		NSDictionary Header { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull bodyData;
		[Export ("bodyData")]
		NSData BodyData { get; }

		// @property (readonly, nonatomic) NSDictionary * _Nullable bodyDictionary;
		[NullAllowed, Export ("bodyDictionary")]
		NSDictionary BodyDictionary { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull contentType;
		[Export ("contentType")]
		string ContentType { get; }
	}

	// typedef MDSTunnelResponse * _Nullable (^MDSTunnelRequestBlock)(MDSTunnelRequest * _Nonnull);
    public delegate MDSTunnelResponse MDSTunnelRequestBlock (MDSTunnelRequest arg0);

	// @interface MDSResourceRequestResponse : NSObject
	[BaseType (typeof(NSObject))]
    public interface MDSResourceRequestResponse
	{
		// @property (readonly, nonatomic) NSInteger status;
		[Export ("status")]
		nint Status { get; }

		// @property (readonly, nonatomic) BOOL isStream;
		[Export ("isStream")]
		bool IsStream { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull response;
		[Export ("response")]
		NSData Response { get; }

		// -(instancetype _Nonnull)initWithStatus:(NSInteger)status message:(NSString * _Nonnull)response;
		[Export ("initWithStatus:message:")]
        NativeHandle Constructor (nint status, string response);

		// -(instancetype _Nonnull)initWithStatus:(NSInteger)status streamData:(NSData * _Nonnull)response;
		[Export ("initWithStatus:streamData:")]
        NativeHandle Constructor (nint status, NSData response);
	}

	// @interface MDSResourceRequest : NSObject
	[BaseType (typeof(NSObject))]
    public interface MDSResourceRequest
	{
		// @property (readonly, nonatomic) MDSRequestMethod method;
		[Export ("method")]
        MDSRequestMethod Method { get; }

        // @property (readonly, nonatomic) NSDictionary * _Nonnull header;
        [Export ("header")]
		NSDictionary Header { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull contentType;
		[Export ("contentType")]
		string ContentType { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull bodyData;
		[Export ("bodyData")]
		NSData BodyData { get; }

		// @property (readonly, nonatomic) NSData * _Nonnull streamData;
		[Export ("streamData")]
		NSData StreamData { get; }

		// @property (readonly, nonatomic) NSDictionary * _Nullable bodyDictionary;
		[NullAllowed, Export ("bodyDictionary")]
		NSDictionary BodyDictionary { get; }
	}

	// typedef MDSResourceRequestResponse * _Nullable (^MDSResourceRequestBlock)(MDSResourceRequest * _Nonnull);
    public delegate MDSResourceRequestResponse MDSResourceRequestBlock (MDSResourceRequest arg0);

	// @protocol MDSConnectivityServiceDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
    public interface MDSConnectivityServiceDelegate
	{
		// @optional -(void)didFailToConnectWithError:(const NSError * _Nonnull)error;
		[Export ("didFailToConnectWithError:")]
		void DidFailToConnectWithError (NSError error);

        // @optional -(void)didFailToConnectWithErrorToUUID:(const NSError * _Nullable)error uuid:(const NSUUID * _Nullable)uuid;
        [Export("didFailToConnectWithErrorToUUID:uuid:")]
        void DidFailToConnectWithErrorToUUID([NullAllowed] NSError error, [NullAllowed] NSUuid uuid);
    }

	// @interface MDSWrapper : NSObject
	[BaseType (typeof(NSObject))]
    public interface MDSWrapper
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDSConnectivityServiceDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDSConnectivityServiceDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nonnull)init:(NSBundle * _Nonnull)configBundle;
		[Export ("init:")]
        NativeHandle Constructor (NSBundle configBundle);

		// -(void)connectPeripheralWithUUID:(NSUUID * _Nonnull)uuid;
		[Export ("connectPeripheralWithUUID:")]
		void ConnectPeripheralWithUUID (NSUuid uuid);

		// -(BOOL)disconnectPeripheralWithUUID:(NSUUID * _Nonnull)uuid;
		[Export ("disconnectPeripheralWithUUID:")]
		bool DisconnectPeripheralWithUUID (NSUuid uuid);

		// -(void)disableAutoReconnectForDeviceWithUUID:(NSUUID * _Nonnull)uuid;
		[Export ("disableAutoReconnectForDeviceWithUUID:")]
		void DisableAutoReconnectForDeviceWithUUID (NSUuid uuid);

		// -(void)disableAutoReconnectForDeviceWithSerial:(NSString * _Nonnull)serial;
		[Export ("disableAutoReconnectForDeviceWithSerial:")]
		void DisableAutoReconnectForDeviceWithSerial (string serial);

		// -(void)doSubscribe:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract response:(MDSResponseBlock _Nonnull)response onEvent:(MDSEventBlock _Nonnull)onEvent;
		[Export ("doSubscribe:contract:response:onEvent:")]
		void DoSubscribe (string resourcePath, NSDictionary contract, MDSResponseBlock response, MDSEventBlock onEvent);

		// -(void)doUpdateSubscription:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract completion:(MDSResponseBlock _Nonnull)completion;
		[Export ("doUpdateSubscription:contract:completion:")]
		void DoUpdateSubscription (string resourcePath, NSDictionary contract, MDSResponseBlock completion);

		// -(void)doUnsubscribe:(NSString * _Nonnull)resourcePath;
		[Export ("doUnsubscribe:")]
		void DoUnsubscribe (string resourcePath);

		// -(void)doRegisterResource:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract response:(MDSResponseBlock _Nonnull)response onRequest:(MDSTunnelRequestBlock _Nonnull)onRequest;
		[Export ("doRegisterResource:contract:response:onRequest:")]
		void DoRegisterResource (string resourcePath, NSDictionary contract, MDSResponseBlock response, MDSTunnelRequestBlock onRequest);

		// -(void)doRegisterResource:(NSURL * _Nonnull)resourceMetadataBinary response:(MDSResponseBlock _Nonnull)response onRequest:(MDSResourceRequestBlock _Nonnull)onRequest;
		[Export ("doRegisterResource:response:onRequest:")]
		void DoRegisterResource (NSUrl resourceMetadataBinary, MDSResponseBlock response, MDSResourceRequestBlock onRequest);

		// -(void)doUnregisterResource:(NSURL * _Nonnull)resourceMetadataBinary response:(MDSResponseBlock _Nonnull)response;
		[Export ("doUnregisterResource:response:")]
		void DoUnregisterResource (NSUrl resourceMetadataBinary, MDSResponseBlock response);

		// -(void)doSendResourceNotification:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract;
		[Export ("doSendResourceNotification:contract:")]
		void DoSendResourceNotification (string resourcePath, NSDictionary contract);

		// -(void)doGet:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract completion:(MDSResponseBlock _Nonnull)completion;
		[Export ("doGet:contract:completion:")]
		void DoGet (string resourcePath, NSDictionary contract, MDSResponseBlock completion);

		// -(void)doPut:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract completion:(MDSResponseBlock _Nonnull)completion;
		[Export ("doPut:contract:completion:")]
		void DoPut (string resourcePath, NSDictionary contract, MDSResponseBlock completion);

		// -(void)doPost:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract completion:(MDSResponseBlock _Nonnull)completion;
		[Export ("doPost:contract:completion:")]
		void DoPost (string resourcePath, NSDictionary contract, MDSResponseBlock completion);

		// -(void)doDelete:(NSString * _Nonnull)resourcePath contract:(NSDictionary * _Nonnull)contract completion:(MDSResponseBlock _Nonnull)completion;
		[Export ("doDelete:contract:completion:")]
		void DoDelete (string resourcePath, NSDictionary contract, MDSResponseBlock completion);

		// -(void)deactivate;
		[Export ("deactivate")]
		void Deactivate ();

        // -(void)setDeviceDiscovery:(BOOL)state;
        [Export("setDeviceDiscovery:")]
        void SetDeviceDiscovery(bool state);

        // -(void)setLogLevel:(MDSLogLevel)level;
        [Export("setLogLevel:")]
        void SetLogLevel(MDSLogLevel level);
    }
}
