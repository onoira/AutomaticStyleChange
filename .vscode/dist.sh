#!/usr/bin/bash
set -euo pipefail
IFS=$'\n\t'

MODNAME="AutomaticStyleChange"
EXCLUDE=(
    dist.sh
    .gitignore
    '.git/*'
    '.vscode/obj/*'
    '.vscode/dist/*'
    1.4/Assemblies/.gitkeep
)
version=$(git describe --tags --abbrev=0)

mkdir -p .vscode/dist
rm -f .vscode/dist/*.zip

zip -r .vscode/dist/$MODNAME-$version-with_source.zip . -x "${EXCLUDE[@]}"
zip -r .vscode/dist/$MODNAME-$version.zip . -x "${EXCLUDE[@]}" -x "Source/*" -x ".vscode/*"
