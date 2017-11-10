//
//  ACInsertControllerViewController.h
//  Appcoachs
//
//  Created by JiangAijun on 16/5/7.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import "ACAdViewController.h"

@interface ACInsertScreenViewController : ACAdViewController

/**
 *  Sets the frame of image ads
 */
@property (nonatomic, assign) CGRect frame;

/**
 *  Set the background   （default is 0.8）
 */
@property (nonatomic, assign) float alpha;

/**
 *  Set the countdown time    Default 3 Second
 *
 *  If you set the countdown , the isAutoClose need to be set to YES
 */
@property (nonatomic, assign) NSInteger time;

/**
 *  automatically close    Default YES
 */
@property (nonatomic, assign) BOOL isAutoClose;

/**
 *  time is Hidden  Default NO
 */
@property (nonatomic, assign) BOOL isHiddenTime;




/**
 *  is exist image data
 */
- (BOOL)isExistImageData;


@end
