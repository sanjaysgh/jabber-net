/* --------------------------------------------------------------------------
 * Copyrights
 * 
 * Portions created by or assigned to Cursive Systems, Inc. are 
 * Copyright (c) 2002-2004 Cursive Systems, Inc.  All Rights Reserved.  Contact
 * information for Cursive Systems, Inc. is available at
 * http://www.cursive.net/.
 *
 * License
 * 
 * Jabber-Net can be used under either JOSL or the GPL.  
 * See LICENSE.txt for details.
 * --------------------------------------------------------------------------*/
#if !NO_STRINGPREP


using System;
using NUnit.Framework;
using stringprep;
using stringprep.steps;

namespace test.stringprep
{
    [TestFixture]
    public class TestDraft
    {
        private Profile nameprep = new Nameprep();

        // 4.1 Map to nothing
        public void Test_4_01()
        {
            string input = "\x0066\x006f\x006f\x00ad\x034f\x1806\x180b\x0062\x0061\x0072\x200b\x2060\x0062\x0061\x007a\xfe00\xfe08\xfe0f\xfeff";
            string expected = "\x0066\x006f\x006f\x0062\x0061\x0072\x0062\x0061\x007a";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.2 Case folding ASCII U+0043 U+0041 U+0046 U+0045
        public void Test_4_02()
        {
            string input = "\x0043\x0041\x0046\x0045";
            string expected = "\x0063\x0061\x0066\x0065";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.3 Case folding 8bit U+00DF (german sharp s)
        public void Test_4_03()
        {
            string input = "\x00df";
            string expected = "\x0073\x0073";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.4 Case folding U+0130 (turkish capital I with dot)
        public void Test_4_04()
        {
            string input = "\x0130";
            string expected = "\x0069\x0307";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.5 Case folding multibyte U+0143 U+037A
        public void Test_4_05()
        {
            string input = "\x0143\x037a";
            string expected = "\x0144\x0020\x03b9";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        [Ignore("fails, due to lack of UTF-16 in .Net")]
        // 4.6 Case folding U+2121 U+33C6 U+1D7BB
        public void Test_4_06()
        {
            string input = "\x2121\x33c6\xd835\xdfbb";
            string expected = "\x0074\x0065\x006c\x0063\x2215\x006b\x0067\x03c3";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.7 Normalization of U+006a U+030c U+00A0 U+00AA
        public void Test_4_07()
        {
            string input = "\x006a\x030c\x00a0\x00aa";
            string expected = "\x01f0\x0020\x0061";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.8 Case folding U+1FB7 and normalization
        public void Test_4_08()
        {
            string input = "\x1fb7";
            string expected = "\x1fb6\x03b9";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.9 Self-reverting case folding U+01F0 and normalization
        public void Test_4_09()
        {
            string input = "\x01f0";
            string expected = "\x01f0";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.10 Self-reverting case folding U+0390 and normalization
        public void Test_4_10()
        {
            string input = "\x0390";
            string expected = "\x0390";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.11 Self-reverting case folding U+03B0 and normalization
        public void Test_4_11()
        {
            string input = "\x03b0";
            string expected = "\x03b0";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.12 Self-reverting case folding U+1E96 and normalization
        public void Test_4_12()
        {
            string input = "\x1e96";
            string expected = "\x1e96";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.13 Self-reverting case folding U+1F56 and normalization
        public void Test_4_13()
        {
            string input = "\x1f56";
            string expected = "\x1f56";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.14 ASCII space character U+0020
        public void Test_4_14()
        {
            string input = "\x0020";
            string expected = "\x0020";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.15 Non-ASCII 8bit space character U+00A0
        public void Test_4_15()
        {
            string input = "\x00a0";
            string expected = "\x0020";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.16 Non-ASCII multibyte space character U+1680
        public void Test_4_16()
        {
            string input = "\x1680";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.17 Non-ASCII multibyte space character U+2000
        public void Test_4_17()
        {
            string input = "\x2000";
            string expected = "\x0020";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.18 Zero Width Space U+200b
        public void Test_4_18()
        {
            string input = "\x200b";
            string expected = "";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.19 Non-ASCII multibyte space character U+3000
        public void Test_4_19()
        {
            string input = "\x3000";
            string expected = "\x0020";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.20 ASCII control characters U+0010 U+007F
        public void Test_4_20()
        {
            string input = "\x0010\x007f";
            string expected = "\x0010\x007f";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.21 Non-ASCII 8bit control character U+0085
        public void Test_4_21()
        {
            string input = "\x0085";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.22 Non-ASCII multibyte control character U+180E
        public void Test_4_22()
        {
            string input = "\x180e";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.23 Zero Width No-Break Space U+FEFF
        public void Test_4_23()
        {
            string input = "\xfeff";
            string expected = "";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.24 Non-ASCII control character U+1D175
        public void Test_4_24()
        {
            string input = "\xd834\xdd75";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.25 Plane 0 private use character U+F123
        public void Test_4_25()
        {
            string input = "\xf123";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.26 Plane 15 private use character U+F1234
        public void Test_4_26()
        {
            string input = "\xdb84\xde34";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.27 Plane 16 private use character U+10F234
        public void Test_4_27()
        {
            string input = "\xdbfc\xde34";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.28 Non-character code point U+8FFFE
        public void Test_4_28()
        {
            string input = "\xd9ff\xdffe";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.29 Non-character code point U+10FFFF
        public void Test_4_29()
        {
            string input = "\xdbff\xdfff";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.30 Surrogate code U+DF42
        public void Test_4_30()
        {
            string input = "\xdf42";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.31 Non-plain text character U+FFFD
        public void Test_4_31()
        {
            string input = "\xfffd";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.32 Ideographic description character U+2FF5
        public void Test_4_32()
        {
            string input = "\x2ff5";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.33 Display property character U+0341
        public void Test_4_33()
        {
            string input = "\x0341";
            string expected = "\x0301";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.34 Left-to-right mark U+200E
        public void Test_4_34()
        {
            string input = "\x200e";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.35 Deprecated U+202A
        public void Test_4_35()
        {
            string input = "\x202a";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.36 Language tagging character U+E0001
        public void Test_4_36()
        {
            string input = "\xdb40\xdc01";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.37 Language tagging character U+E0042
        public void Test_4_37()
        {
            string input = "\xdb40\xdc42";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.38 Bidi: RandALCat character U+05BE and LCat characters
        public void Test_4_38()
        {
            string input = "\x0066\x006f\x006f\x05be\x0062\x0061\x0072";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected BidiException", false);
            }
            catch (BidiException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected BidiException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.39 Bidi: RandALCat character U+FD50 and LCat characters
        public void Test_4_39()
        {
            string input = "\x0066\x006f\x006f\xfd50\x0062\x0061\x0072";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected BidiException", false);
            }
            catch (BidiException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected BidiException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.40 Bidi: RandALCat character U+FB38 and LCat characters
        public void Test_4_40()
        {
            string input = "\x0066\x006f\x006f\xfe76\x0062\x0061\x0072";
            string expected = "\x0066\x006f\x006f\x0020\x064e\x0062\x0061\x0072";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.41 Bidi: RandALCat without trailing RandALCat U+0627 U+0031
        public void Test_4_41()
        {
            string input = "\x0627\x0031";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected BidiException", false);
            }
            catch (BidiException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected BidiException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.42 Bidi: RandALCat character U+0627 U+0031 U+0628
        public void Test_4_42()
        {
            string input = "\x0627\x0031\x0628";
            string expected = "\x0627\x0031\x0628";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.43 Unassigned code point U+E0002
        public void Test_4_43()
        {
            string input = "\xdb40\xdc02";
            string expected = "";
            try
            {
                expected = nameprep.Prepare(input);
                Assertion.Assert("Expected ProhibitedCharacterException", false);
            }
            catch (ProhibitedCharacterException)
            {
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception e)
            {
               Assertion.Assert("Expected ProhibitedCharacterException, got " + e.GetType().ToString(), false);
            }
        }

        // 4.44 Larger test (shrinking)
        public void Test_4_44()
        {
            string input = "\x0058\x00ad\x00df\x0130\x2121\x006a\x030c\x00a0\x00aa\x03b0\x2000";
            string expected = "\x0078\x0073\x0073\x0069\x0307\x0074\x0065\x006c\x01f0\x0020\x0061\x03b0\x0020";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
        // 4.45 Larger test (expanding)
        public void Test_4_45()
        {
            string input = "\x0058\x00df\x3316\x0130\x2121\x249f\x3300";
            string expected = "\x0078\x0073\x0073\x30ad\x30ed\x30e1\x30fc\x30c8\x30eb\x0069\x0307\x0074\x0065\x006c\x0028\x0064\x0029\x30a2\x30d1\x30fc\x30c8";
            Assertion.AssertEquals(expected, nameprep.Prepare(input));
        }
    }
}
#endif
