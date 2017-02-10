module SampleForms.Core.NameOf
open Microsoft.FSharp.Quotations.Patterns

    let nameOf quotation =
        match quotation with
        | PropertyGet (_,propertyInfo,_) -> propertyInfo.Name
        | _ -> ""