//
//  ACAdModel.h
//  Appcoachs
//
//  Created by JiangAijun on 16/3/4.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ACAdModel : NSObject

/**
 * Appcoach与客户共同约定的广告位编号
 */
@property (nonatomic, copy) NSString *tid;



//Model 转 字典
- (NSDictionary*)convertToDictionary;

@end
