#import <Foundation/Foundation.h>
#import "NativeCallProxy.h"


@implementation FrameworkLibAPI

id<NativeCallsProtocol> api = NULL;
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi
{
    api = aApi;

}
@end


extern "C" {
    void showHostMainWindow(const char* color) { return [api showHostMainWindow:[NSString stringWithUTF8String:color]]; 
    void showTime(const char* timex) { return [api showHostMainWindow:[NSString stringWithUTF8String:timex]]; 
	void CallUnityToGameFunction() {
        // Find the MainEntry GameObject and call ToHome function
        UnitySendMessage("GeneralObject", "ToGame", "");
    }
	}
}

