@echo off
setlocal enabledelayedexpansion

rem 设置.csproj文件的路径，假设它与批处理脚本在同一目录下
set "csprojPath=RealAirplaneTag.csproj"

rem 使用findstr命令提取Version标签的内容
for /f "tokens=2 delims=<>" %%i in ('type "%csprojPath%" ^| findstr /c:"<Version>"') do (
    set "version=%%i"
)

rem 移除版本号中的前后空格
set "version=!version: =!"

rem 打印版本号以确认
echo Current version: !version!

rem 使用Git命令打标签并推送
git tag -a v!version! -m "Release v!version!"
git push origin --tags

endlocal