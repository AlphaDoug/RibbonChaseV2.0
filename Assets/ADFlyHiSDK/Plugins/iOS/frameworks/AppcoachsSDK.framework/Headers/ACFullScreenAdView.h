//
//  ACFullScreenAdView.h
//  Appcoachs
//
//  Created by JiangAijun on 16/3/17.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

//FullScreenAdView

#import "ACAdView.h"

@interface ACFullScreenAdView : ACAdView

/**
 *  Set the countdown time
 *
 *  If you set the countdown , the isAutoClose need to be set to YES
 */
@property (nonatomic, assign) NSInteger time;


/**
 *  automatically close     Default YES
 *
 */
@property (nonatomic, assign) BOOL isAutoClose;

/**
 *  is exist image data
 */
- (BOOL)isExistImageData;

- (NSArray*)parserImageData:(NSString *)data;

@end
