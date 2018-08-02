namespace WpfApp10
{
    public class Student 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImgPath { get; set; }
        public string Age { get; set; }

        #region MyRegion


        //static FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
        //    new PropertyChangedCallback(ChangedCallbackMethod), new CoerceValueCallback(CoerceValueCallbackMethod));


        //public string Age
        //{
        //    get { return (string)GetValue(AgeProperty); }
        //    set { SetValue(AgeProperty, value); }
        //}


        //public static readonly DependencyProperty AgeProperty =
        //    DependencyProperty.Register(
        //        "Age",
        //        typeof(string),
        //        typeof(Student),
        //        metadata,
        //        new ValidateValueCallback(ValidateValueCallbackMethod));

        //private static object CoerceValueCallbackMethod(DependencyObject d, object baseValue)
        //{
        //    if ((int)baseValue >= 100)
        //    {
        //        return 100;
        //    }
        //    return baseValue;
        //}
        //static bool ValidateValueCallbackMethod(object value)
        //{
        //    if((int) value > 100)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //static void ChangedCallbackMethod(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{

        //}
        #endregion
    }

}
