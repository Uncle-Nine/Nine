﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Framework.UI.Core.Bind
{
    public class BindFactory
    {
        protected List<BaseBind> Binds = new List<BaseBind>();

        protected object Container;

        public BindFactory(object container)
        {
            Container = container;
        }

        //单向绑定
        public void Bind<TComponent, TData>
        (TComponent component, ObservableProperty<TData> property, Action<TData> fieldChangeCb = null,
            Func<TData, TData> prop2CpntWrap = null) where TComponent : class
        {
            var bind = new BindField<TComponent, TData>(Container, component, property, fieldChangeCb, null, BindType.OnWay,
                prop2CpntWrap, null);
            Binds.Add(bind);
        }

        //反向绑定
        public void RevertBind<TComponent, TData>
        (TComponent component, ObservableProperty<TData> property,
            UnityEvent<TData> componentEvent = null,
            Func<TData, TData> cpnt2PropWrap = null) where TComponent : class
        {
            var bind = new BindField<TComponent, TData>(Container, component, property, null, componentEvent,
                BindType.Revert,
                null, cpnt2PropWrap);
            Binds.Add(bind);
        }

        //同类型双向绑定
        public void TwoWayBind<TComponent, TData>
        (TComponent component, ObservableProperty<TData> property,
            UnityEvent<TData> componentEvent = null,
            Action<TData> fieldChangeCb = null,
            Func<TData, TData> cpnt2PropWrap = null,
            Func<TData, TData> prop2CpntWrap = null) where TComponent : class
        {
            Bind(component, property, fieldChangeCb, prop2CpntWrap);
            RevertBind(component, property, componentEvent, cpnt2PropWrap);
        }

        //wrap不同类型单向绑定
        public void Bind<TComponent, TData, TResult>(TComponent component,
            ObservableProperty<TData> property, Func<TData, TResult> field2CpntConvert,
            Action<TResult> fieldChangeCb = null) where TComponent : class
        {
            var bind = new ConvertBindField<TComponent, TData, TResult>(Container, component, property, fieldChangeCb,
                field2CpntConvert, null, null);
            Binds.Add(bind);
        }

        //wrap不同类型反向绑定
        public void RevertBind<TComponent, TData, TResult>(TComponent component,
            ObservableProperty<TData> property,
            Func<TResult, TData> cpnt2FieldConvert,
            UnityEvent<TResult> componentEvent = null) where TComponent : class
        {
            var bind = new ConvertBindField<TComponent, TData, TResult>(Container, component, property, null, null,
                cpnt2FieldConvert, componentEvent);
            Binds.Add(bind);
        }

        //不同类型双向绑定
        public void TwoWayBind<TComponent, TData, TViewEvent>
        (TComponent component, ObservableProperty<TData> property,
            Func<TViewEvent, TData> cpnt2FieldConvert, Func<TData, TViewEvent> field2CpntConvert,
            UnityEvent<TViewEvent> componentEvent = null, Action<TViewEvent> fieldChangeCb = null)
            where TComponent : class
        {
            Bind(component, property, field2CpntConvert, fieldChangeCb);
            RevertBind(component, property, cpnt2FieldConvert, componentEvent);
        }

        //绑定两个field
        public void Bind<TComponent, TData1, TData2, TResult>
        (TComponent component, ObservableProperty<TData1> property1, ObservableProperty<TData2> property2,
            Func<TData1, TData2, TResult> wrapFunc, Action<TResult> filedChangeCb = null)
            where TComponent : class
        {
            var bind = new BindField<TComponent, TData1, TData2, TResult>(Container, component, property1, property2,
                wrapFunc, filedChangeCb);
            Binds.Add(bind);
        }

        public void BindData<TData>(ObservableProperty<TData> property, Action<TData> cb)
        {
            cb?.Invoke(property);
            property.AddListener(cb);
        }

        //绑定command
        public void BindCommand<TComponent>
        (TComponent component, Action command, UnityEvent componentEvent = null,
            Func<Action, Action> wrapFunc = null) where TComponent : class
        {
            var bind = new BindCommand<TComponent>(Container, component, command, componentEvent, wrapFunc);
            Binds.Add(bind);
        }

        //绑定带参数的command
        public void BindCommand<TComponent, TData>
        (TComponent component, Action<TData> command, UnityEvent<TData> componentEvent = null,
            Func<Action<TData>, Action<TData>> wrapFunc = null) where TComponent : class
        {
            var bind = new BindCommandWithPara<TComponent, TData>(Container, component, command, componentEvent, wrapFunc);
            Binds.Add(bind);
        }

        public void BindList<TComponent, TData>(TComponent component, ObservableList<TData> property,
            Action<TComponent, TData> onShow = null, Action<TComponent, TData> onHide = null) where TComponent : Object
        {
            var bind = new BindList<TComponent, TData>(Container, component, property, onShow, onHide);
            Binds.Add(bind);
        }

        public void Reset()
        {
            foreach (var bind in Binds)
            {
                bind.ClearBind();
            }
            Binds.Clear();
        }
    }

    public enum BindType
    {
        OnWay,
        Revert
    }
}