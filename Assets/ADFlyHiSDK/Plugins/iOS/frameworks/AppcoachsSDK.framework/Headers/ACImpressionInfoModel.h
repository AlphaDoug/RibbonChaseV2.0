//
//  ImpressionInfoModel.h
//  AdSDK
//
//  Created by mac on 16/3/13.
//  Copyright © 2016年 appcoach. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ACImpressionInfoModel : NSObject

/**
 *  impressionId
 */
@property (nonatomic, strong) NSString *impId;
/**
 *  impressionUrl 视频显示时需要发送消息
 */
@property (nonatomic, strong) NSString *impUrl;

@end
