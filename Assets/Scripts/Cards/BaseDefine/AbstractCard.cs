using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card
{
    public abstract class AbstractCard
    {
        public string name;
        public string[] paras;
        public AbstractCard(string name, params string[] paras)
        {
            this.name = name;
            this.paras = paras;
        }
        public virtual string GetDesc()
        {
            return "";
        }
    }
}