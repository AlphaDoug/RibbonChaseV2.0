//
//  ImageAdModel.h
//  AdSDK
//
//  Created by mac on 16/3/13.
//  Copyright © 2016年 appcoach. All rights reserved.
//


#import <UIKit/UIKit.h>

#import "ACAdModel.h"

@interface ACImageAdModel : ACAdModel

/**
 *  广告tId .transaction ID of the ad response
 */
@property (nonatomic, copy) NSString *adTId;

/**
 * App store, Google Play, APK
 */
@property (nonatomic, assign) long long actionType;

/**
 *  素材宽度
 */
@property (nonatomic, assign) CGFloat width;

/**
 *  素材高度
 */
@property (nonatomic, assign) CGFloat height;

/**
 *  Appcoach与客户共同约定的广告位编号
 *
 *  1:   168x28
 *  2:   320x50
 *  3:   728x90    tablet only
 *  4:   250x250
 *  5:   72x72
 *  6:   960x400
 *  7:   480x800
 *  8:   320x130
 */
@property (nonatomic, assign) int slotid;

/**
 *  包名
 */
@property (nonatomic, copy) NSString *pkgName;

/**
 *  价格
 */
@property (nonatomic, assign) CGFloat price;

/**
 *  App name
 */
@property (nonatomic, copy) NSString *name;

/**
 *  平台
 */
@property (nonatomic, copy) NSString *platform;

/**
 *  App description
 */
@property (nonatomic, copy) NSString *desc;

/**
 *  点击URL
 */
@property (nonatomic, copy) NSString *click_url;

/**
 *  素材URL
 */
@property (nonatomic, copy) NSString *image_url;

/**
 *  APP图标URL
 */
@property (nonatomic, copy) NSString *icon_url;

/**
 *  URL of tracking,图片显示impression
 */
@property (nonatomic, copy) NSString *tracking_url;

/**
 *  URL of tracking,视频中EndCard图片的点击track
 */
@property (nonatomic, copy) NSString *video_tracking_url;

- (instancetype)initWithNSDictionary:(NSDictionary*)data;


//用于在cell中控制图片显示后上报情况。
@property (nonatomic, assign, getter=isTracking) BOOL isTracking;



@end
