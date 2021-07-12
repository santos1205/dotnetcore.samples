# FEATURES

1 - react frontend and .net core backend

2 - Custom validation by annotations methods

3 - Exception details:

catch (Exception ex)
            {
                string outputError;
                if (ex.Message.Contains("inner exception"))
                {
                    outputError = ex.InnerException.Message != null ? ex.InnerException.Message : ex.InnerException.InnerException.Message != null ? ex.InnerException.InnerException.Message : ex.Message;
                }
                else
                {
                    outputError = ex.Message;
                }

                return Ok(new { error = outputError });
            }
