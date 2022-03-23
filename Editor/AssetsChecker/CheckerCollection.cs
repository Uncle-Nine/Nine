﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Framework.Editor.AssetsChecker
{
    using System.Collections.Generic;

    public abstract class CheckerCollection
    {
        public abstract string Name { get; }

        [ShowInInspector]
        [TableList(AlwaysExpanded = true, IsReadOnly = true, HideToolbar = true)]
        protected List<IRule> Rules = new List<IRule>();

        public virtual void AddRule(IRule rule)
        {
            var attrs = rule.GetType().GetCustomAttributes(typeof(BelongToCollectionAttribute), false);
            if (attrs.Length != 1)
            {
                Debug.LogError($"{rule.GetType()} 缺少属性BelongToCollectionAttribute");
                return;
            }

            var belongType = (attrs[0] as BelongToCollectionAttribute).BelongType;
            if (belongType != GetType())
                return;
            Rules.Add(rule);
        }

        [Button("运行", ButtonSizes.Medium)]
        private void Run()
        {
            
        }
    }
}