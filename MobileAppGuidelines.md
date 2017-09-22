# Mobile App Guidelines
A quick set of guidelines to follow when contributing to the mobile apps.

### Icons/Images
All image resources are placed in the platform specific folders for iOS/Android/UWP.

1. Toolbar images should start with toolbar_NAME.png
2. Tab images should start with tab_NAME.png
3. All others: image_NAME.png
4. UWP images should be places in the Resources folder.

For icon generation use the platform specific versions:
* Android: https://romannurik.github.io/AndroidAssetStudio/ and https://materialdesignicons.com/
* iOS: https://icons8.com/icon/set/search/ios7
* UWP: http://modernuiicons.com/

### Main Navigation
This application uses Tabs for the main navigation.
* Android/UWP has a root Navigation page and the tabs are wrapped inside of it. Navigation NEVER occurs inside of a tab, always to a new page.
* iOS has the TabbedPage as its root and sub navigation pages

### Modal Pages
Modal pages should ALWAYS be wrapped in a NavigationPage.

* Android/UWP -> On the top right place the close toolbar icon and when pressed pop the modal page
* iOS -> Use the word *Close* and then perform the same action 


