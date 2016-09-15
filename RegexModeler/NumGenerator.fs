namespace RegexModeler

open RegexModeler.Interfaces

    type NumGenerator() = 
        
        let random = System.Random()

        interface INumGenerator with 

            member _x.GetNumber max = 
                random.Next(max + 1)

            member _x.GetNumberInRange min max = 
                let min' = match min with
                           | Some(a) -> a
                           | None    -> 0                                
                let max' = match max with
                           | Some(a) -> a
                           | None    -> min' + 10
                random.Next(min', max' + 1)
