//
//  UIView+Extension.h
//

#import <UIKit/UIKit.h>

@interface UIView (Extension) <UIGestureRecognizerDelegate>
@property (nonatomic, assign) CGFloat x;
@property (nonatomic, assign) CGFloat y;
@property (nonatomic, assign) CGFloat centerX;
@property (nonatomic, assign) CGFloat centerY;
@property (nonatomic, assign) CGFloat width;
@property (nonatomic, assign) CGFloat height;
@property (nonatomic, assign) CGSize size;

@property (nonatomic) CGFloat touchPointX;
@property (nonatomic) CGFloat touchPointY;

// 判断View是否显示在屏幕上
- (BOOL)isDisplayedInScreen;

@end
