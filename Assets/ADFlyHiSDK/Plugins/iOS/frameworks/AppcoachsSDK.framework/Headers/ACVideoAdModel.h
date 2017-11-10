//
//  ACVideoAdModel.h
//  AdSDK
//
//  Created by mac on 16/3/13.
//  Copyright © 2016年 appcoach. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "ACImpressionInfoModel.h"
#import "ACLineIconInfoModel.h"
#import "ACLineTrackingInfo.h"

@interface ACVideoAdModel : NSObject
/**
 *  存放ACImpressionInfoModel对象
 */
@property (nonatomic, strong) NSMutableArray<ACImpressionInfoModel *> *impression;

/**
 *  存放ACLineIconInfoModel对象
 */
@property (nonatomic, strong) NSMutableArray<ACLineIconInfoModel *> *lineIcon;

/**
 *  存放ACLineTrackingInfo对象
 */
@property (nonatomic, strong) NSMutableArray<ACLineTrackingInfo *> *lineTracking;

/**
 *  广告id
 */
@property (nonatomic, strong) NSString *adId;

/**
 *  广告tId .transaction ID of the ad response
 */
@property (nonatomic, strong) NSString *adTId;

/**
 *  多个广告时的排序号
 */
@property (nonatomic, assign) int adSequence;

/**
 *  the version of the ad server that returned the ad
 */
@property (nonatomic, strong) NSString *adSystemVersion;

/**
 *  the name of the ad server that returned the ad
 */
@property (nonatomic, strong) NSString *adSystemName;

/**
 *  ad's title
 */
@property (nonatomic, strong) NSString *adTitle;

/**
 *  广告描述
 */
@property (nonatomic, strong) NSString *adDescription;

/**
 *  creativeId
 */
@property (nonatomic, strong) NSString *creativeId;

/**
 *  creativeApiFramework
 */
@property (nonatomic, strong) NSString *creativeApiFramework;

/**
 *  creativeAdID
 */
@property (nonatomic, strong) NSString *creativeAdID;

/**
 *  creativeSequence
 */
@property (nonatomic, assign) int creativeSequence;

/**
 *  视频进度类型，百分百和时间
 */
@property (nonatomic, assign) BOOL offsetPercentType;
@property (nonatomic, assign) int skipOffset;
@property (nonatomic, assign) int duration;

/**
 *  视频id
 */
@property (nonatomic, strong) NSString *videoId;
/**
 *  视频的ApiFramework
 */
@property (nonatomic, strong) NSString *videoApiFramework;
/**
 *  视频类型
 */
@property (nonatomic, strong) NSString *videoType;
@property (nonatomic, strong) NSString *videoDelivery;
/**
 *  视频高度
 */
@property (nonatomic, assign) int videoHeight;
/**
 *  视频宽度
 */
@property (nonatomic, assign) int videoWidth;
/**
 *  视频资源url
 */
@property (nonatomic, strong) NSString *videoUrl;
@property (nonatomic, strong) NSString *videoClickThroughId;
@property (nonatomic, strong) NSString *videoClickThroughUrl;
@property (nonatomic, strong) NSString *videoClickTrackingId;
@property (nonatomic, strong) NSString *videoClickTrackingUrl;
@property (nonatomic, strong) NSString *videoCustomClickId;

/**
 *
 */
@property (nonatomic, strong) NSString *videoCustomClickUrl;

/**
 *  是否显示endCard
 */
@property (nonatomic, assign) BOOL endCard;

/**
 *  应用评分
 */
@property (nonatomic, assign) float appScore;

/**
 * Appcoach与客户共同约定的广告位编号
 */
@property (nonatomic, copy) NSString *tid;

//Model 转 字典
- (NSDictionary*)convertToDictionary;

@end