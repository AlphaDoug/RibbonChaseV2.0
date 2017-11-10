//
//  AdGroupVideoView.h
//  AdVideoPlayer
//
//  Created by Aike on 16/6/6.
//  Copyright © 2016年 rain. All rights reserved.
//

#import "ACVideoView.h"

typedef enum {
    AdGroupVideoTypeUp,
    AdGroupVideoTypeDown
} AdGroupVideoType;

@interface ACGroupVideoView : ACVideoView

@property (nonatomic, assign) AdGroupVideoType groupType;



@end
