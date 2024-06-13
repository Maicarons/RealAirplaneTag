import os
import re
import subprocess

def find_csproj_path():
    """查找当前目录下的.csproj文件路径"""
    for root, dirs, files in os.walk('.'):
        for file in files:
            if file.endswith('.csproj'):
                return os.path.join(root, file)
    return None

# 获取当前目录
current_dir = os.getcwd()

# 查找.csproj文件
csproj_path = find_csproj_path()
if csproj_path is None:
    print("在当前目录及其子目录下未找到.csproj文件。")
    exit(1)

# 从csproj文件中读取版本号
with open(csproj_path, 'r', encoding='utf-8') as file:
    content = file.read()
    match = re.search(r'<Version>([\d.]+)</Version>', content)
    if match:
        version = match.group(1)
    else:
        print("无法找到Version标签")
        exit(1)

print(f"当前版本: {version}")

# 构建标签名并执行Git命令打标签并推送
tag_name = f"v{version}"
try:
    subprocess.run(['git', 'tag', '-a', tag_name, '-m', f"Release {tag_name}"], check=True)
    subprocess.run(['git', 'push', 'origin', '--tags'], check=True)
    print(f"标签 {tag_name} 已成功创建并推送。")
except subprocess.CalledProcessError as e:
    print(f"执行Git命令时发生错误: {e}")