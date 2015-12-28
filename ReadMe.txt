1.SQL目录有数据库建库脚本，按顺序执行即可

2.JS目录下为uploadify上传插件，本功能的上传使用该插件实现，如果需要单独使用上传插件，直接拷贝该目录即可
  uploadify上传插件（建议直接参考官网，百度出来的很多都是老版本的）
  官网：http://www.uploadify.com
  doc：http://www.uploadify.com/documentation/
  demos：http://www.uploadify.com/demos/
  如果需要修改上传样式，可以对该目录进行修改，定制即可

3.web.config中配置，文件上传保存目录；如果将该模块加入到项目中，请参考当前web.config中的文件大小以及请求大小进行配置

4.建议添加Global文件，并参照本工程中Global文件进行设置。（防止iis自动重启问题）

5.BLL为业务逻辑层 DAL为数据层 Entity为实体 PS：所有工程下的base文件为运行基础包，根据项目的具体情况，可以无需采用，只拷贝其他文件，对应替换即可