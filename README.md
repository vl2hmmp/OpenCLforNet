# OpenCL for Net

OpenCLの.NET Framework向けのラッパーライブラリです。
このライブラリでは以下のことを目標にしています。
・C#から利用しやすいこと
・OpenCL 2.x系の機能を利用できること

# 注意事項

利用には各ベンダーから提供されるOpenCL.dllが必要です。

x64プラットフォーム上で動作します。x86プラットフォーム向けにはポインタをint型に置き換える必要があります。

SVMなど高度な機能の利用にはunsafe宣言が必要になります。

# TODO

ポインタをIntPtr型で置き換える。

# LICENSE

MIT Licenseを採用しています。詳しくはLICENSE.txtを参照してください。