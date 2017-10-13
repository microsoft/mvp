print "Variables:"
printenv

print "Arguments for updating:"
print " - MCID: $MC_UWP"
print " - Live ID: $LIVE_ID"
print " - Live Secret: $LIVE_SECRET"

# Updating manifest
(Get-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs).replace('MC_UWP', $MC_UWP) | Set-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs
(Get-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs).replace('LIVE_ID', $LIVE_ID) | Set-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs
(Get-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs).replace('LIVE_SECRET', $LIVE_SECRET) | Set-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs


Get-Content $BUILD_REPOSITORY_LOCALPATH\MVP\MVP\MVP\Helpers\CommonConstants.cs | Out-Printer

print "Manifest updated!"