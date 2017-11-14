//
//  AdVideoWallController.h
//  Appcoachs
//
//  Created by Aike on 16/6/8.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import "ACAdViewController.h"

@interface ACVideoWallController : ACAdViewController

@property (nonatomic, assign) ACFullScreenShowAdType showAdType;


- (void)loadVideoAdWithSlotid:(NSInteger)slotId;

- (instancetype)initWithPriorityPlay:(BOOL)priority;

@end
