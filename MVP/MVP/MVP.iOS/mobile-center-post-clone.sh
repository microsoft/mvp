#!/usr/bin/env bash

echo "Variables:"
printenv

echo "Arguments for updating:"
echo " - MCID: $MC_IOS"
echo " - Live ID: $LIVE_ID"
echo " - Live Secret: $LIVE_SECRET"

# Updating manifest
sed -i '' 's/MC_IOS/$MC_IOS/g' ../MVP/Helpers/CommonConstants.cs
sed -i '' 's/LIVE_ID/$LIVE_ID/g' ../MVP/Helpers/CommonConstants.cs
sed -i '' 's/LIVE_SECRET/$LIVE_SECRET/g' ../MVP/Helpers/CommonConstants.cs

echo "Manifest updated!"