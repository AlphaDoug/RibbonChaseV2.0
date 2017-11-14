//
//  ACAdViewEventDelegate.h
//  Appcoachs
//
//  Created by mac on 16/3/24.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <Foundation/Foundation.h>
@class ACAdView;

@protocol ACAdViewEventDelegate <NSObject>

@optional
/**
 * Load advertisement data success.
 */
- (void) adDataDidLoadSuccess:(ACAdView*)adView;


/**
 * Load advertisement data fail.
 */
- (void) adDataDidLoadFail:(ACAdView*)adView Error:(NSError *)error;

/**
 * Load advertisement resource fail. Example load image resource fail.
 */
- (void) adResourceDidLoadFail:(ACAdView*)adView Error:(NSError *)error;

/**
 * Appcoach adversisement view will appear.
 */
- (void) adViewDidAppear:(ACAdView*)adView;


/**
 * Appcoach advertisement view will disappear.
 */
- (void) adViewDidDisappear:(ACAdView*)adView;

/**
 * The advertisement view had clicked;
 */
- (void) adViewDidClicked:(ACAdView*)adView AdInfo:(NSDictionary*) adInfo;

/**
 * The video advertisement play completed or others;
 */
- (void) adViewDidCompleted:(ACAdView*)adView AdInfo:(NSDictionary*) adInfo;

/**
 * The adView had closed.
 */
- (void)adViewDidClose:(ACAdView*)adView;

- (void)adViewDidFullScreen:(ACAdView*)adView isFull:(BOOL)isfull;

/**
 * The adView click Detail Ad
 */
- (void)adViewClickDetail:(ACAdView*)adView;


/**
 * The adView start play video
 */
- (void)adViewStartPlayVideo:(ACAdView*)adView;

/**
 * The adView Play Video Error
 */
- (void)adViewPlayVideoError:(ACAdView*)adView;

@end
