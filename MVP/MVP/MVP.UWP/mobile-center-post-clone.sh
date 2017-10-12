#!/usr/bin/env bash

echo "Variables:"
printenv

echo "Arguments for updating:"
echo " - MCID: $MC_UWP"
echo " - Live ID: $LIVE_ID"
echo " - Live Secret: $LIVE_SECRET"

# Updating manifest
sed -i '' 's/MC_UWP/$MC_UWP/g' ../MVP/Helpers/CommonConstants.cs
sed -i '' 's/LIVE_ID/$LIVE_ID/g' ../MVP/Helpers/CommonConstants.cs
sed -i '' 's/LIVE_SECRET/$LIVE_SECRET/g' ../MVP/Helpers/CommonConstants.cs

echo "Manifest updated!"