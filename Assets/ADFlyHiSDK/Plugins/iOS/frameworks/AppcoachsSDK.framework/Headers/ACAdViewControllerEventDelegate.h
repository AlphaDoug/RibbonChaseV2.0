//
//  ACAdViewControllerEventDelegate.h
//  Appcoachs
//
//  Created by mac on 16/3/24.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <Foundation/Foundation.h>
@class ACAdViewController;

@protocol ACAdViewControllerEventDelegate <NSObject>

@optional

/**
 * Load advertisement data success.
 */
- (void) adVControllerDataDidLoadSuccess:(ACAdViewController*)adController;

/**
 * Load advertisement data fail.
 */
- (void) adVControllerDataDidLoadFail:(ACAdViewController*)adController Error:(NSError *)error;

/**
 * The video advertisement play completed or others;
 */
- (void) adVControllerDidCompleted:(ACAdViewController*)adController AdInfo:(NSDictionary*)info;

/**
 * The advertisement view had clicked;
 */
- (void) adVControllerDidClicked:(ACAdViewController*)adController AdInfo:(NSDictionary*)info;

/**
 * The advertisement had closed.
 */
- (void) adVControllerDidClosed:(ACAdViewController*)adController;

/**
 * The advertisement has start play.
 */
- (void) adVControllerStartPlayVideo:(ACAdViewController*)adController;

@end
