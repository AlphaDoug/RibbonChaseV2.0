//
//  ACCustomNavigationBar.h
//  Appcoachs
//
//  Created by JiangAijun on 16/3/17.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

//Custom navigation bar


#import <UIKit/UIKit.h>

@interface ACCustomNavigationBar : UIView

/**
 *  Parent controller    required
 *
 *  example : In the controller that uses the navigation bar set "navBar.parentVc = self"
 */
@property (nonatomic, weak) UIViewController *parentVc;


/**
 *  Create custom navigation bar
 *
 *  @return navBar
 */
+ (instancetype)navigationBar;

/**
 *  Whether to hide the custom navigation bar
 *
 *  @param hidden Bool
 */
- (void)isNavBarHidden:(BOOL)hidden;


/**
 *  Set the background color of the navigation bar
 *
 *  @param navBgColor  UIColor
 */
- (void)setNavBarBgColor:(UIColor *)navBgColor;


/**
 *  Set navigation bar title
 *
 *  @param title NSString
 */
- (void)setNavTitle:(NSString *)title;


/**
 *  Set navigation bar title font
 *
 *  @param titleFont UIFont
 */
- (void)setNavTitleFont:(UIFont *)titleFont;


/**
 *  Set navigation bar title color
 *
 *  @param titleColor UIColor
 */
- (void)setNavTitleColor:(UIColor *)titleColor;


@end
