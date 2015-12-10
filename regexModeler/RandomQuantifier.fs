namespace RegexModeler

open ListHelpers
open System

type RandomQuantifier(generator) =

    member _x.generator = generator :> INumGenerator

    interface IQuantifier with
    
        member x.getNFromQuantifier (chrs) = 
            let str = chrsToString chrs   
        
            match chrs with 
            | '}'::_ ->
                let quantStr = new String(Array.rev(str.Substring(1, str.IndexOf('{') - 1).ToCharArray()))
                let rest = stringToChrs <| str.Substring(str.IndexOf('{') + 1)

                if quantStr.Contains ","
                    then let range = quantStr.Split(',') |> Array.map(fun s -> 
                            match Int32.TryParse s with
                            | (true, int) -> Some int
                            | _           -> None)
                         let min, max = range.[0], range.[1]
                         (x.generator.GetNumberInRange min max, rest)          
                else
                    match Int32.TryParse quantStr with
                    | (true, int) -> int, rest
                    | _ -> raise <| InvalidQuantityException "Could not parse quantifier."            
            | '*'::xs ->
                (x.generator.GetNumberInRange (Some(0)) (Some(10)), xs)
            | '+'::xs ->
                (x.generator.GetNumberInRange (Some(1)) (Some(10)), xs)
            | '?'::xs ->
                (x.generator.GetNumberInRange (Some(0)) (Some(1)), xs)
            | _::xs -> raise <| InvalidQuantityException "Could not parse quantifier."
                       (0, xs)
            | [] -> raise <| ArgumentNullException "This list is empty, so."

