using System;

namespace LivestreamingE_commercePlatform.Helper
{
    public class ResultT<T>
    {
        //状态码
        public int State { get; set; }
        //具体错误提示
        public string Message { get; set; }
        //返回数据
        public T Data { get; set; }
    }
}
