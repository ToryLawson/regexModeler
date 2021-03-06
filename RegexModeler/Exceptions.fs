﻿namespace RegexModeler

    /// Quantifiers can only be applied to things which are repeatable.
    exception InvalidQuantifierTargetException of string

    /// Shorthand classes have not all been implemented in this program, and not all characters are used for shorthands in the wild.
    exception InvalidShorthandClassException of string

    /// Character sets must be well-formed to be used. Verify that this one is not empty and that it does not contain any stray escapes.
    exception InvalidCharacterSetException of string

    /// Quantity must be parseable integer.
    exception InvalidQuantityException of string

    /// You have to follow at least some rules sometimes.
    exception MalformedRegexException of string

    /// Mock objects need implementations of all exercised members.
    exception UnimplementedMemberException of string
