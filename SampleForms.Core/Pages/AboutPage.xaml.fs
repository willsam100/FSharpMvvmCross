﻿namespace SampleForms.Core.Pages

open Xamarin.Forms
open Xamarin.Forms.Xaml

type AboutPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<AboutPage>)
