#!/usr/bin/env bash

echo "Variables:"
printenv

echo "Arguments for updating:"
echo " - MCID: $MC_ANDROID"
echo " - Live ID: $LIVE_ID"
echo " - Live Secret: $LIVE_SECRET"

# Updating manifest
sed -i '' "s/MC_ANDROID/$MC_ANDROID/g" $BUILD_REPOSITORY_LOCALPATH/MVP/MVP/MVP/Helpers/CommonConstants.cs
sed -i '' "s/LIVE_ID/$LIVE_ID/g" $BUILD_REPOSITORY_LOCALPATH/MVP/MVP/MVP/Helpers/CommonConstants.cs
sed -i '' "s/LIVE_SECRET/$LIVE_SECRET/g" $BUILD_REPOSITORY_LOCALPATH/MVP/MVP/MVP/Helpers/CommonConstants.cs

cat $BUILD_REPOSITORY_LOCALPATH/MVP/MVP/MVP/Helpers/CommonConstants.cs

echo "Manifest updated!"
