
/* BEGIN EXTERNAL SOURCE */

        function toggleTextbox(checkboxId, textboxId) {
            var checkbox = document.getElementById(checkboxId);
            var textbox = document.getElementById(textboxId);

            if (checkbox.checked) {
                textbox.disabled = false;
            } else {
                textbox.disabled = true;
            }
        }
    
/* END EXTERNAL SOURCE */
