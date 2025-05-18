rem %1 must be the path of the AngelLoader updates repo and must be surrounded by quotes, eg., "C:\Users\Brian\source\repos\AngelLoaderUpdates"
rem %2 must be the version (not surrounded by quotes), eg., 1.10.2
cd %1
git add --all
git commit -m "Update info for version %2"
git pull
git push
