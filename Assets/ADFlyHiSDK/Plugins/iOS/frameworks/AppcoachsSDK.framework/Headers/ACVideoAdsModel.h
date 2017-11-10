//
//  VideoAdsModel.h
//  AdSDK
//
//  Created by mac on 16/3/13.
//  Copyright © 2016年 appcoach. All rights reserved.
//

#import "ACAdModel.h"


@interface ACVideoAdsModel : ACAdModel

/**
 *  视频数据 数组中为ACVideoAdModel对象
 */
@property (nonatomic, strong) NSMutableArray *videos;

/**
 *  错误信息
 */
@property (nonatomic, strong) NSString *adErrorMessage;

/**
 *  是否错误
 */
@property (nonatomic, assign) BOOL adError;

@end
