//
//  AdPasterVideoView.h
//  Appcoachs
//
//  Created by Aike on 16/6/7.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import "ACVideoView.h"

@interface ACPasterVideoView : ACVideoView

/** 隐藏时间 默认NO */
@property (nonatomic, assign) BOOL isHiddenTime;

/** 隐藏去广告按钮 默认NO*/
@property (nonatomic, assign) BOOL isHiddenCloseBtn;

/** 隐藏详情按钮 默认NO */
@property (nonatomic, assign) BOOL isHiddenDetailBtn;

/** 隐藏全屏按钮 默认NO */
@property (nonatomic, assign) BOOL isHiddenFullBtn;

/** 是否自动开始播放 默认NO */
@property (nonatomic, assign) BOOL isAutoStartPlay;

@end