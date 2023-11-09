    export let validate(
      form: RawForm,
      field: String = "",
    ): ErrorMessage | Null {
      let all = field.isEmpty;
      match (field) {
        "", "maxValue", "minValue" -> 0;
      }
      null
    }

    export let ErrorMessage = String;

    export class ParsedForm {
      public minValue: Float64;
      public maxValue: Float64;
    }

    export class RawForm {
      public minValue: String;
      public maxValue: String;

      public parse(): ParsedForm {
        new ParsedForm(
          // minValue = 
        )
      }
    }
