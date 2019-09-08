﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Options
{
    public class JwtSettings
    {
        public String Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}
