name: 🐞 Bug Report
description: "Report something that doesn't look alright."
labels: ["bug"]
body:
  - type: markdown
    attributes:
      value: |
        _Hi there :wave: and thanks for taking the time to report a bug!_

  - type: input
    id: usage-information
    attributes:
      label: Usage Information
      description: Material.Avalonia Version / Avalonia Version / Operating System
    validations:
      required: true

  - type: textarea
    id: description
    attributes:
      label: Description
      description: Please share a clear and concise description of the problem.
      placeholder: Description
    validations:
      required: true

  - type: textarea
    id: reproduction-steps
    attributes:
      label: Reproduction Steps
      description: |
        Please include minimal steps to reproduce the problem if possible. E.g.: the smallest possible code snippet; or a small project, with steps to run it. Make sure to include logs and exceptions as text rather than screenshots.
        If you share a fully finished minimal project it will be easier for us to figure out what's wrong and solve your problem faster.
      placeholder: Minimal Reproduction
    validations:
      required: true

  - type: textarea
    id: expected-behavior
    attributes:
      label: Expected Behavior
      description: |
        Provide a description of the expected behavior.
      placeholder: |
        1. Add Material.Avalonia...
        1. Use control...
        1. Run '...'
        1. See error...
    validations:
      required: true

  - type: textarea
    id: actual-behavior
    attributes:
      label: Actual Behavior
      description: |
        Provide a description of the actual behavior observed. If applicable please include any error messages or exception stacktraces.
      placeholder: Actual Behavior
    validations:
      required: true

  - type: textarea
    id: regression
    attributes:
      label: Regression?
      description: |
        Did this work in a previous build or release of Material.Avalonia? If you can try a previous release, which would help us narrow down the problem. If you don't know, that's OK.
      placeholder: Regression?
    validations:
      required: false

  - type: textarea
    id: known-workarounds
    attributes:
      label: Known Workarounds
      description: |
        Please provide a description of any known workarounds.
      placeholder: Known Workarounds
    validations:
      required: false

  - type: textarea
    attributes:
      label: Anything else?
      description: |
        Links? References? Anything that will give us more context about the issue you are encountering!
  
        Tip: You can attach images or log files by clicking this area to highlight it and then dragging files in.
    validations:
      required: false

  - type: dropdown
    id: pull-request
    attributes:
      label: Could you help with a pull-request?
      options:
        - "No"
        - "Yes"
    validations:
      required: true

  - type: checkboxes
    id: duplicates
    attributes:
      label: Is there an existing issue for this?
      description: Please search to see if an issue already exists for the bug you encountered.
      options:
      - label: I have searched the existing issues
      required: true
