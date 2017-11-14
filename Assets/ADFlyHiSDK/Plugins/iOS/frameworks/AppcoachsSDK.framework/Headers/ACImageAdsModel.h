//
//  ImageAdsModel.h
//  AdSDK
//
//  Created by mac on 16/3/13.
//  Copyright © 2016年 appcoach. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ACAdModel.h"
@interface ACImageAdsModel : ACAdModel

@property (nonatomic, copy) NSString *adsId;

@property (nonatomic, assign) int count;

@property (nonatomic, strong) NSMutableArray *ads;

- (instancetype)initWithDictionary:(NSDictionary*)dictionary;
@end
