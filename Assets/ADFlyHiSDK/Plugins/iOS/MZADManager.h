//
//  MZADManager.h
//  MZYW_SDK
//
//  Created by mzyw on 16/9/12.
//  Copyright © 2016年 mzyw. All rights reserved.
//

#import <UIKit/UIKit.h>

/**
 拇指广告种类
 */
typedef enum {
    MZADVideoAD = 1,           /** 原生视频 */
    MZADH5VideoAD,             /** h5视频 */
    
}MZADStyleId;



/**
 拇指视频广告完成状态
 */
typedef enum {
    MZVideoADPlayStateStart = 0,     /** 开始播放 */
    MZVideoADPlayStateSuspended,     /** 播放暂停 */
    MZVideoADPlayStateCompleted,     /** 播放完成 */
    MZVideoADPlayStateFailed         /** 播放失败 */
}MZVideoADPlayState;


/**
 拇指插屏广告完成状态
 */
typedef enum {
    MZADViewShowStateFinish = 0,     /** 展示成功 */
    
    MZADViewShowStateFailed,     /** 展示失败 */
    
    MZADViewIsClosed, /** 点击关闭 */
    
}MZADViewShowState;


/**
 游戏屏幕朝向
 */
typedef enum {
    
    portraitOrLandscape = 0, //横竖都可以
    
    portraitOnly,    //只能竖屏
    
    landscapeOnly,   //只能横屏
    
    
}MZADOrientation;


//视频完结果回调
typedef void(^MZVideoADPlayStateBlock)(MZVideoADPlayState state);


//sdk版本
#define sdk_ver @"1.0.6"

//是否为测试模式
#define MZAD_DebugMode 1

@interface MZADManager : NSObject


//推荐广告播放结果回调
typedef void(^MZADViewShowStateBlock)(MZADViewShowState state);


//全屏 banner广告结果回调
@property (nonatomic, copy) MZADViewShowStateBlock mzADViewShowStateBlock;

//视频完结果回调
@property (nonatomic, copy) MZVideoADPlayStateBlock mzVideoADPlayStateBlock;


//广告是否加载好
@property (nonatomic,readonly,assign) BOOL mzADLoadFinished;




/********************* 方法调用 *********************/
/**
 实例化广告管理对象
 */
+ (instancetype)shareMZADManager;


/**
 初始化广告SDK
 */
- (void)mz_initMZADManagerWithProjId:(NSString *)projId andChannelId:(NSString *)channelId;


/**
 *预加载视频广告内容
 */
- (void)mz_loadADDataWithADStyleId:(MZADStyleId)mzADStyleId deviceOrientation:(MZADOrientation)orientation;
/**
 *展示横幅广告
 *bannerFrame  banner的坐标位置，高度固定为45
 *currentVC    放置banner的ViewController
 */
- (void)mz_showBannerADWithBannerFrame:(CGRect)bannerFrame andCurrentViewController:(UIViewController *)currentVC andShowState:(MZADViewShowStateBlock)mzBannerADshowBlock;


/**
 *展示拇指推荐广告
 *recomendViewFrame  recomendView的坐标位置和大小
 *currentVC          放置recomendView的ViewController
 */
-(void)mz_showRecomendADWithViewFrame:(CGRect)recomendViewFrame andCurrentViewController:(UIViewController *)currentVC andShowState:(MZADViewShowStateBlock)mzRecomendADshowBlock;

/**
 * 隐藏拇指推荐广告
 */
-(void)mz_hiddenRecomendADView;


/**
 * 显示拇指推荐广告
 */
-(void)mz_showRecomendADView;

/**
 *展示全屏广告
 *currentVC 放置全屏的ViewController
 */
- (void)mz_showFullScreenADWithCurrentViewController:(UIViewController *)currentVC andShowState:(MZADViewShowStateBlock)mzFullViewADshowBlock;


/**
 *展示开屏广告
 *currentVC 放置全屏的ViewController
 */
- (void)mz_showStartFullScreenAD:(NSInteger)requestTimer andshowVC:(UIViewController *)currentVC andShowState:(MZADViewShowStateBlock)mzFullViewADshowBlock;


/**
 *展示视频广告
 *currentVC  放置视频的ViewController
 * MZVideoADPlayStateBlock 播放状态的block
 * MZVideoADPlayStateStart = 0, 开始播放
 * MZVideoADPlayStateSuspended, 播放暂停
 * MZVideoADPlayStateCompleted, 播放完成
 * MZVideoADPlayStateFailed     播放失败
 */
- (void)mz_showVideoADWithCurrentViewController:(UIViewController *)currentVC andPlayState:(MZVideoADPlayStateBlock)mzVideoADPlayStateBlock;



@end
