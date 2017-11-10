//
//  AdFullScreenController.h
//  AdVideoPlayer
//
//  Created by Aike on 16/6/6.
//  Copyright © 2016年 rain. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ACAdViewController.h"

@class ACVideoAdModel;

@interface ACFullScreenController : ACAdViewController

@property (nonatomic, assign) ACFullScreenShowAdType showAdType;


- (BOOL)isReady;

- (void)loadVideoAdWithSlotid:(NSInteger)slotId;

- (BOOL)isAdAvailable;

- (void)setupAcAdVideoModel:(ACVideoAdModel *)adModel;

- (void)playVideoAd:(UIViewController*)viewController;

@end
