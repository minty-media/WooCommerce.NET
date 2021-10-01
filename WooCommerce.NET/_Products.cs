using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WooCommerce.NET
{
    public class _Products
    {
        private WooCommerce _wooCommerce { get; set; }

        public _Products(WooCommerce _wooCommerce)
        {
            this._wooCommerce = _wooCommerce;
        }
    }
}