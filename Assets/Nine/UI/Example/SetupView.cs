using Nine.UI.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using State = Nine.Enums.State;

namespace Nine.UI.Example
{
    public class SetupView : View<SetupViewModel>
    {

        public Text nameMessageText;
        public Text mulBindText;
        public InputField atkInputField;
        public Toggle joinToggle;
        public Button joinInButton;
        public Image img;
        public Transform item;

        void Start()
        {
            ////nameMessageText show or hide by vm.visible
            //Bind(nameMessageText, (vm) => vm.Visible).OneWay();
            ////nameMessageText.text show text by vm.Name
            //Bind(nameMessageText, (vm) => vm.Name).OneWay();
            ////mulBindText.text show text by (vm.ATK , vm.Name) , and wrap by third para
            //Bind(mulBindText, (vm) => vm.Name, (vm) => vm.ATK, (name, atk) => $"name = {name} atk = {atk.ToString()}").OneWay();
            ////button bind vm.OnButtonClick
            //BindCommand(joinInButton, (vm) => vm.OnButtonClick).Wrap(callback =>
            //{
            //    return () =>
            //    {
            //        callback();
            //        print("Wrap 按钮");
            //    };
            //}).OneWay();
            ////image bind path, when path changed, img.sprite change to res.load(path)
            //Bind(img, (vm) => vm.Path).OneWay();
            //Bind(joinToggle, (vm) => vm.Visible).Wrap((value) =>
            //{
            //    Log.I($"改为{value}");
            //    return value;
            //}).Revert();
            //Bind(atkInputField, (vm) => vm.Name).Wrap((value) =>
            //{
            //    Log.I($"改为{value}");
            //    return value;
            //}).TwoWay();
            //BindCommand<InputField, string>(atkInputField, (vm) => vm.OnInputChanged).Wrap((valueChangedFunc) =>
            //{
            //    return (value) =>
            //    {
            //        valueChangedFunc(value);
            //        print("Wrap InputField");
            //    };
            //}).OneWay();
            BindList(item.gameObject.AddComponent<ItemView>(), Data.Items).Init();
        }
    }

    public class ItemView : View<ItemViewModel>
    {
        public Image img;

        void Start()
        {
            Bind(img,(vm)=>vm.Path).OneWay();
        }
    }
}