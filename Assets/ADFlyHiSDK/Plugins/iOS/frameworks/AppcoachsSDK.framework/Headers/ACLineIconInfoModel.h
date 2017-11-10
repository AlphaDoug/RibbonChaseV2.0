//
//  LineIconInfoModel.h
//  AdSDK
//
//  Created by mac on 16/3/13.
//  Copyright © 2016年 appcoach. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ACLineIconInfoModel : NSObject
/**
 *  图片的位置，大小等相关信息
 */
@property (nonatomic, assign) int width;
@property (nonatomic, assign) int height;
@property (nonatomic, assign) int xPosition;
@property (nonatomic, assign) int yPosition;
@property (nonatomic, strong) NSString *program;
@property (nonatomic, strong) NSString *offset;
/**
 *  资源类型
 */
@property (nonatomic, strong) NSString *resourceType;
@property (nonatomic, strong) NSString *resourceCreativeType;
/**
 *  图片URL
 */
@property (nonatomic, strong) NSString *resourceUrl;

/**
 *  点击图片需要跳转的url
 */
@property (nonatomic, strong) NSString *clickThroughUrl;
/**
 *  trackingId
 */
@property (nonatomic, strong) NSString *clickTrackingId;
/**
 *  点击图片需要发送的跟踪信息
 */
@property (nonatomic, strong) NSString *clickTrackingUrl;

/**
 * 图片显示 trackingURL
 */
@property (nonatomic, strong) NSString *iconViewTracking;
@end
